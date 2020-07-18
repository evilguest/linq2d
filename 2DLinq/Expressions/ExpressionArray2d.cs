using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Processing2d.Expressions
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using static Expression;
    using static ExpressionHelper;
    using static Math;

    unsafe delegate Action StripeHandlerFactory<T, R>(int xmin, int xmax, int ymin, int ymax, UnsafeArray2d<T> sourcePtr, UnsafeArray2d<R> targetPtr)
    where T : unmanaged
    where R : unmanaged;


    public static class ExpressionArray2d
    {
        public static Expression<Filter<T, R>> GenerateExprFilter<T, R>(Expression<Kernel<T, R>> kernelExpr)
            where R : unmanaged
        {
            var km = KernelMeasure.Measure(kernelExpr.Compile());
            var data_arg = Parameter(typeof(T[,]), "data");
            var result_var = Variable(typeof(R[,]), "result");
            var i_var = Variable(typeof(int), "i");
            var j_var = Variable(typeof(int), "j");
            var h_var = Variable(typeof(int), "h");
            var w_var = Variable(typeof(int), "w");
            var inlinedKernel = new CellAccessInliner<T>(kernelExpr.Parameters[0], data_arg, i_var, j_var).Visit(kernelExpr.Body);

            var fe = Block(typeof(R[,]), new[] { data_arg, h_var, w_var, result_var },
                new[] {
                Assign(h_var, Call(data_arg, typeof(T[,]).GetMethod("GetLength", new [] { typeof(int) }), Constant(0))),
                Assign(w_var, Call(data_arg, typeof(T[,]).GetMethod("GetLength", new [] { typeof(int) }), Constant(1))),
                Assign(result_var, New(typeof(R[,]).GetConstructor(new[] { typeof(int), typeof(int) }), h_var, w_var)),
                ExpressionHelper.For(i_var,
                    Constant(-km.xmin),
                    LessThan(i_var, Subtract(h_var, Constant(km.xmax))),
                    AddAssign(i_var, Constant(1)),
                        ExpressionHelper.For(j_var,
                        Constant(-km.ymin),
                        LessThan(j_var, Subtract(h_var, Constant(km.xmax))),
                        AddAssign(j_var, Constant(1)),
                        Assign(MakeIndex(result_var, typeof(R[,]).GetProperty("Item", new Type[] { typeof(int), typeof(int) }), new[] { i_var, j_var }),
                            inlinedKernel))
                    ),
                    result_var
                }
               );
            var nne = Lambda<Filter<T, R>>(fe, data_arg);
            return nne;
        }

        unsafe internal delegate Action StripeMultiHandlerFactory(int xmin, int xmax, int ymin, int ymax, UnsafeArray2d[] sources, object targetPtr);

        internal static StripeMultiHandlerFactory GenerateStripeHandlerFactory<R>(LambdaExpression kernel, bool recurrent)
        {
            var ab = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("ExprStripeHandler"), AssemblyBuilderAccess.RunAndSave);

            var tb = ab.DefineDynamicModule("ExprStripeHandler", "ExprStripeHandler.dll", true).DefineType("StripeHandler", TypeAttributes.Class | TypeAttributes.Public);

            var xmin = tb.DefineField("_xmin", typeof(int), FieldAttributes.Private);
            var xmax = tb.DefineField("_xmax", typeof(int), FieldAttributes.Private);
            var ymin = tb.DefineField("_ymin", typeof(int), FieldAttributes.Private);
            var ymax = tb.DefineField("_ymax", typeof(int), FieldAttributes.Private);
            var sources = new List<FieldBuilder>();
            foreach(var p in kernel.Parameters)
            {
                var pType = p.Type;
                if (pType.IsConstructedGenericType && pType.GetGenericTypeDefinition() == typeof(ICell<>))
                    pType = pType.GetGenericArguments()[0];

                sources.Add(tb.DefineField("_source." + p.Name, UnsafeArray2d.GetTypeInstance(pType), FieldAttributes.Private));
            }
            var target = tb.DefineField("_target", UnsafeArray2d.GetTypeInstance(typeof(R)), FieldAttributes.Private);

            if (recurrent)
                sources.RemoveAt(sources.Count - 1);

            ConstructorBuilder cb = BuildConstructor<R>(tb, xmin, xmax, ymin, ymax, sources, target);

            MethodBuilder rm = BuildRunMethod<R>(kernel, tb, xmin, xmax, ymin, ymax, sources, target, recurrent);

            var t = tb.CreateType();
            var constructor = t.GetConstructor((from p in cb.GetParameters() select p.ParameterType).ToArray());
            var runMethod = t.GetMethod(rm.Name); // there is only one method with this name

            ab.Save("ExprStripeHandler.dll");
            return PrepareFactory<R>(constructor, runMethod);
        }

        private static StripeHandlerFactory<T, R> GenerateStripeHandlerFactory<T, R>(Expression<Kernel<T, R>> kernel)
           where T : unmanaged
           where R : unmanaged
        {
            var ab = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("ExprStripeHandler"), AssemblyBuilderAccess.RunAndSave);

            var tb = ab.DefineDynamicModule("ExprStripeHandler", "ExprStripeHandler.dll", true).DefineType("StripeHandler", TypeAttributes.Class | TypeAttributes.Public);

            var xmin = tb.DefineField("_xmin", typeof(int), FieldAttributes.Private);
            var xmax = tb.DefineField("_xmax", typeof(int), FieldAttributes.Private);
            var ymin = tb.DefineField("_ymin", typeof(int), FieldAttributes.Private);
            var ymax = tb.DefineField("_ymax", typeof(int), FieldAttributes.Private);
            //var delta = tb.DefineField("_delta", typeof(long), @private);
            //var stride = tb.DefineField("_stride", typeof(int), FieldAttributes.Private);
            var source = tb.DefineField("_source", typeof(UnsafeArray2d<T>), FieldAttributes.Private);
            var target = tb.DefineField("_target", UnsafeArray2d.GetTypeInstance<R>(), FieldAttributes.Private);

            ConstructorBuilder cb = BuildConstructor<T, R>(tb, xmin, xmax, ymin, ymax, source, target);

            MethodBuilder rm = BuildRunMethod(kernel, tb, xmin, xmax, ymin, ymax, source, target);

            var t = tb.CreateType();
            var constructor = t.GetConstructor((from p in cb.GetParameters() select p.ParameterType).ToArray());
            var runMethod = t.GetMethod(rm.Name); // there is only one method with this name

            ab.Save("ExprStripeHandler.dll");
            return PrepareFactory<T, R>(constructor, runMethod);
        }

        private static ConstructorBuilder BuildConstructor<R>(TypeBuilder tb, FieldInfo xmin, FieldInfo xmax, FieldInfo ymin, FieldInfo ymax, IEnumerable<FieldInfo> sources, FieldInfo target)
        {
            var paramTypes = Enumerable.Repeat(typeof(int), 4).Concat(from s in sources select s.FieldType).Append(UnsafeArray2d.GetTypeInstance<R>()).ToArray();

            var c = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.HasThis, paramTypes);
            c.DefineParameter(1, ParameterAttributes.None, "xmin");
            c.DefineParameter(2, ParameterAttributes.None, "xmax");
            c.DefineParameter(3, ParameterAttributes.None, "ymin");
            c.DefineParameter(4, ParameterAttributes.None, "ymax");

            var cilg = c.GetILGenerator();
            cilg.Emit(OpCodes.Ldarg, 0);
            cilg.Emit(OpCodes.Ldarg, 1);
            cilg.Emit(OpCodes.Stfld, xmin);

            cilg.Emit(OpCodes.Ldarg, 0);
            cilg.Emit(OpCodes.Ldarg, 2);
            cilg.Emit(OpCodes.Stfld, xmax);

            cilg.Emit(OpCodes.Ldarg, 0);
            cilg.Emit(OpCodes.Ldarg, 3);
            cilg.Emit(OpCodes.Stfld, ymin);

            cilg.Emit(OpCodes.Ldarg, 0);
            cilg.Emit(OpCodes.Ldarg, 4);
            cilg.Emit(OpCodes.Stfld, ymax);

            var i = 5;
            foreach (var sourceField in sources)
            {
                c.DefineParameter(i, ParameterAttributes.None, sourceField.Name.Substring(1));
                cilg.Emit(OpCodes.Ldarg, 0);
                cilg.Emit(OpCodes.Ldarg, i);
                cilg.Emit(OpCodes.Stfld, sourceField);
                i++;
            }
            c.DefineParameter(i, ParameterAttributes.None, "target");

            cilg.Emit(OpCodes.Ldarg, 0);
            cilg.Emit(OpCodes.Ldarg, i);
            cilg.Emit(OpCodes.Stfld, target);
            cilg.Emit(OpCodes.Ret);

            return c;
        }

        private static ConstructorBuilder BuildConstructor<T, R>(TypeBuilder tb, FieldInfo xmin, FieldInfo xmax, FieldInfo ymin, FieldInfo ymax, FieldInfo source, FieldInfo target)
            where T : unmanaged
            where R : unmanaged
        {
            var c = tb.DefineConstructor<int, int, int, int, UnsafeArray2d<T>, UnsafeArray2d<R>>(MethodAttributes.Public, CallingConventions.HasThis);
            //c.DefineParameter(0, ParameterAttributes.None, "this");
            c.DefineParameter(1, ParameterAttributes.None, "xmin");
            c.DefineParameter(2, ParameterAttributes.None, "xmax");
            c.DefineParameter(3, ParameterAttributes.None, "ymin");
            c.DefineParameter(4, ParameterAttributes.None, "ymax");
            c.DefineParameter(5, ParameterAttributes.None, "source");
            c.DefineParameter(6, ParameterAttributes.None, "target");
            var cilg = c.GetILGenerator();
            cilg.Emit(OpCodes.Ldarg, 0);
            cilg.Emit(OpCodes.Ldarg, 1);
            cilg.Emit(OpCodes.Stfld, xmin);

            cilg.Emit(OpCodes.Ldarg, 0);
            cilg.Emit(OpCodes.Ldarg, 2);
            cilg.Emit(OpCodes.Stfld, xmax);

            cilg.Emit(OpCodes.Ldarg, 0);
            cilg.Emit(OpCodes.Ldarg, 3);
            cilg.Emit(OpCodes.Stfld, ymin);

            cilg.Emit(OpCodes.Ldarg, 0);
            cilg.Emit(OpCodes.Ldarg, 4);
            cilg.Emit(OpCodes.Stfld, ymax);

            cilg.Emit(OpCodes.Ldarg, 0);
            cilg.Emit(OpCodes.Ldarg, 5);
            cilg.Emit(OpCodes.Stfld, source);

            cilg.Emit(OpCodes.Ldarg, 0);
            cilg.Emit(OpCodes.Ldarg, 6);
            cilg.Emit(OpCodes.Stfld, target);
            cilg.Emit(OpCodes.Ret);
            return c;
        }

        private static MethodBuilder BuildRunMethod<R>(LambdaExpression kernel, TypeBuilder tb, FieldInfo xmin, FieldInfo xmax, FieldInfo ymin, FieldInfo ymax, IEnumerable<FieldInfo> sources, FieldInfo target, bool recurrent)
        {
            var km = KernelMeasure.Measure(kernel); //TODO: perform an expression-only measure
            var sourceTypes = (from s in sources let elemType = s.FieldType.GetGenericArguments()[0] select typeof(UnsafeArray2dPtr<>).MakeGenericType(elemType)).ToArray();
            var paramTypes = Enumerable.Repeat(typeof(int), 5)
                .Concat(sourceTypes)
                .Append(UnsafeArray2d.GetRefType<R>()).ToArray();

            var staticRun = tb.DefineMethod("RunStatic", MethodAttributes.HideBySig | MethodAttributes.Private | MethodAttributes.Static, null, paramTypes);

            var xmin_arg = Parameter(typeof(int), "xmin");
            var xmax_arg = Parameter(typeof(int), "xmax");
            var ymin_arg = Parameter(typeof(int), "ymin");
            var ymax_arg = Parameter(typeof(int), "ymax");
            var height_arg = Parameter(typeof(int), "height");

            var sourceArgs = new List<ParameterExpression>();
            var i = 0;
            foreach(var s in sources)
                sourceArgs.Add(Parameter(sourceTypes[i++], s.Name.Substring(1) + "Ptr"));

            var targetPtr_arg = Parameter(UnsafeArray2d.GetRefType<R>(), "targetPtr");

            var paramMap = new Dictionary<ParameterExpression, ParameterExpression>();
            for (i = 0; i < sourceArgs.Count; i++)
                //if(kernel.Parameters[i].Type.IsConstructedGenericType && kernel.Parameters[i].Type.GetGenericTypeDefinition() == typeof(ICell<>))
                    paramMap.Add(kernel.Parameters[i], sourceArgs[i]);

            if (recurrent)
                paramMap.Add(kernel.Parameters[i], targetPtr_arg);

            var i_var = Variable(typeof(int), "i");
            var j_var = Variable(typeof(int), "j");
            var w_var = Variable(typeof(int), "w");
            var inlinedKernelBody = new CellAccessUnsafeInliner(paramMap, i_var, j_var, height_arg, w_var).Visit(kernel.Body);

            BlockExpression fe = null;
            {
                fe = Block(
                    new[] { w_var },
                    Assign(w_var, Property(targetPtr_arg, UnsafeArray2d.GetRefType<R>().GetProperty("Stride"))),
                    For(i_var,
                        xmin_arg,
                        LessThan(i_var, xmax_arg),
                        Constant(1),
                        Block(
                            For(
                                j_var,
                                ymin_arg,
                                LessThan(j_var, ymax_arg),
                                Constant(1),
                                Block(
                                    Assign(
                                        MakeIndex(
                                            targetPtr_arg,
                                            UnsafeArray2d.GetRefType<R>().GetProperty("Item", new[] { typeof(int) }),
                                            new[] { Constant(0) }
                                        ),
                                        inlinedKernelBody
                                    ),
                                    Block(from s in sourceArgs select AddAssign(s, Constant(1))), // skip to the next element
                                    AddAssign(targetPtr_arg, Constant(1))
                                )
                            ),
                            Block(from s in sourceArgs select AddAssign(s, Constant(km.ymax - km.ymin))), // skip to the next row
                            AddAssign(targetPtr_arg, Constant(km.ymax - km.ymin))
                        )
                    )
                );
            }
            LambdaExpression staticRunLambda = Lambda(fe, (new[] { xmin_arg, xmax_arg, ymin_arg, ymax_arg, height_arg }).Concat(sourceArgs).Append(targetPtr_arg));
            staticRunLambda.CompileToMethod(staticRun);


            return BuildInstanceRun<R>(tb, xmin, xmax, ymin, ymax, sources, target, staticRun);
        }

        private static MethodBuilder BuildRunMethod<T, R>(Expression<Kernel<T, R>> kernel, TypeBuilder tb, FieldInfo xmin, FieldInfo xmax, FieldInfo ymin, FieldInfo ymax, FieldInfo source, FieldInfo target)
            where T : unmanaged
            where R : unmanaged
        {
            var km = KernelMeasure.Measure(kernel.Compile()); //TODO: perform an expression-only measure

            var staticRun = tb.DefineMethod("RunStatic", MethodAttributes.HideBySig | MethodAttributes.Private | MethodAttributes.Static, null, new[]
                { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(UnsafeArray2dPtr<T>), UnsafeArray2d.GetRefType<R>()});

            var xmin_arg = Parameter(typeof(int), "xmin");
            var xmax_arg = Parameter(typeof(int), "xmax");
            var ymin_arg = Parameter(typeof(int), "ymin");
            var ymax_arg = Parameter(typeof(int), "ymax");
            var height_arg = Parameter(typeof(int), "height");

            var sourcePtr_arg = Parameter(typeof(UnsafeArray2dPtr<T>), "sourcePtr");
            var targetPtr_arg = Parameter(UnsafeArray2d.GetRefType<R>(), "targetPtr");


            var i_var = Variable(typeof(int), "i");
            var j_var = Variable(typeof(int), "j");
            var w_var = Variable(typeof(int), "w");
            var inlinedKernel = new CellAccessUnsafeInliner(kernel.Parameters[0], sourcePtr_arg, i_var, j_var, height_arg, w_var).Visit(kernel.Body);

            BlockExpression fe = null;
            if (typeof(T) == typeof(R)) // slight optimization
            {
                var l = Label();
                var d_var = Variable(typeof(int), "d");
                var lineEnd_var = Variable(typeof(UnsafeArray2dPtr<T>), "lineEnd");
                fe = Block(
                    new[] { w_var, d_var, lineEnd_var },
                    Assign(w_var, Property(sourcePtr_arg, typeof(UnsafeArray2dPtr<T>).GetProperty(nameof(UnsafeArray2dPtr<T>.Stride)))),
                    Assign(d_var, Subtract(targetPtr_arg, sourcePtr_arg)),
                    For(i_var,
                        xmin_arg,
                        LessThan(i_var, xmax_arg),
                        Constant(1),
                        Block(
                            Assign(lineEnd_var, Subtract(Add(sourcePtr_arg, ymax_arg), ymin_arg)),
                            Loop(
                                IfThenElse(
                                    LessThan(sourcePtr_arg, lineEnd_var),
                                    Block(
                                        Assign(
                                            MakeIndex(
                                                sourcePtr_arg,
                                                typeof(UnsafeArray2dPtr<T>).GetProperty("Item", new[] { typeof(int) }),
                                                new[] { d_var }
                                            ),
                                            inlinedKernel
                                        ),
                                        AddAssign(sourcePtr_arg, Constant(1)) // skip to the next element
                                    ),
                                    Break(l)
                                ),
                                l
                            ),
                            AddAssign(sourcePtr_arg, Constant(km.ymax - km.ymin)) // skip to the next row
                        )
                    )
                );
            }
            else
            {
                fe = Block(
                    new[] { w_var },
                    Assign(w_var, Property(sourcePtr_arg, typeof(UnsafeArray2dPtr<T>).GetProperty(nameof(UnsafeArray2dPtr<T>.Stride)))),
                    For(i_var,
                        xmin_arg,
                        LessThan(i_var, xmax_arg),
                        Constant(1),
                        Block(
                            For(
                                j_var,
                                ymin_arg,
                                LessThan(j_var, ymax_arg),
                                Constant(1),
                                Block(
                                    Assign(
                                        MakeIndex(
                                            targetPtr_arg,
                                            UnsafeArray2d.GetRefType<R>().GetProperty("Item", new[] { typeof(int) }),
                                            new[] { Constant(0) }
                                        ),
                                        inlinedKernel
                                    ),
                                    AddAssign(sourcePtr_arg, Constant(1)), // skip to the next element
                                    AddAssign(targetPtr_arg, Constant(1))
                                )
                            ),
                            AddAssign(sourcePtr_arg, Constant(km.ymax - km.ymin)), // skip to the next row
                            AddAssign(targetPtr_arg, Constant(km.ymax - km.ymin))
                        )
                    )
                );
            }
            LambdaExpression staticRunLambda = Lambda(fe, xmin_arg, xmax_arg, ymin_arg, ymax_arg, height_arg, sourcePtr_arg, targetPtr_arg);
            staticRunLambda.CompileToMethod(staticRun);


            return BuildInstanceRun<T, R>(tb, xmin, xmax, ymin, ymax, source, target, staticRun);
        }

        private static MethodBuilder BuildInstanceRun<R>(TypeBuilder tb, FieldInfo xmin, FieldInfo xmax, FieldInfo ymin, FieldInfo ymax, IEnumerable<FieldInfo> sources, FieldInfo target, MethodBuilder staticRun)
        {
            var hss = tb.DefineMethod<Action>("Run", MethodAttributes.HideBySig | MethodAttributes.Public);
            var ilg = hss.GetILGenerator();

            ilg.Emit(OpCodes.Ldarg, 0);
            ilg.Emit(OpCodes.Ldfld, xmin);

            ilg.Emit(OpCodes.Ldarg, 0);
            ilg.Emit(OpCodes.Ldfld, xmax);

            ilg.Emit(OpCodes.Ldarg, 0);
            ilg.Emit(OpCodes.Ldfld, ymin);

            ilg.Emit(OpCodes.Ldarg, 0);
            ilg.Emit(OpCodes.Ldfld, ymax);

            ilg.Emit(OpCodes.Ldarg, 0);
            ilg.Emit(OpCodes.Ldfld, target);
            ilg.Emit(OpCodes.Call, UnsafeArray2d.GetTypeInstance<R>().GetProperty(nameof(UnsafeArray2d.Height)).GetGetMethod());

            foreach (var source in sources)
            {
                ilg.Emit(OpCodes.Ldarg, 0);
                ilg.Emit(OpCodes.Ldfld, source);

                ilg.Emit(OpCodes.Ldarg, 0);
                ilg.Emit(OpCodes.Ldfld, xmin);

                ilg.Emit(OpCodes.Ldarg, 0);
                ilg.Emit(OpCodes.Ldfld, ymin);

                ilg.Emit(OpCodes.Call, typeof(UnsafeArray2d<>).MakeGenericType(source.FieldType.GetGenericArguments()[0]).GetProperty("Item").GetGetMethod());
            }

            ilg.Emit(OpCodes.Ldarg, 0);
            ilg.Emit(OpCodes.Ldfld, target);

            ilg.Emit(OpCodes.Ldarg, 0);
            ilg.Emit(OpCodes.Ldfld, xmin);

            ilg.Emit(OpCodes.Ldarg, 0);
            ilg.Emit(OpCodes.Ldfld, ymin);

            ilg.Emit(OpCodes.Call, UnsafeArray2d.GetTypeInstance<R>().GetProperty("Item").GetGetMethod());

            ilg.Emit(OpCodes.Call, staticRun);

            ilg.Emit(OpCodes.Ret);
            return hss;
        }

        private static MethodBuilder BuildInstanceRun<T, R>(TypeBuilder tb, FieldInfo xmin, FieldInfo xmax, FieldInfo ymin, FieldInfo ymax, FieldInfo source, FieldInfo target, MethodBuilder staticRun)
            where T : unmanaged
            where R : unmanaged
        {
            var hss = tb.DefineMethod<Action>("Run", MethodAttributes.HideBySig | MethodAttributes.Public);
            var ilg = hss.GetILGenerator();

            ilg.Emit(OpCodes.Ldarg, 0);
            ilg.Emit(OpCodes.Ldfld, xmin);

            ilg.Emit(OpCodes.Ldarg, 0);
            ilg.Emit(OpCodes.Ldfld, xmax);

            ilg.Emit(OpCodes.Ldarg, 0);
            ilg.Emit(OpCodes.Ldfld, ymin);

            ilg.Emit(OpCodes.Ldarg, 0);
            ilg.Emit(OpCodes.Ldfld, ymax);


            ilg.Emit(OpCodes.Ldarg, 0);
            ilg.Emit(OpCodes.Ldfld, source);
            ilg.Emit(OpCodes.Call, typeof(UnsafeArray2d<T>).GetProperty(nameof(UnsafeArray2d<T>.Height)).GetGetMethod());

            ilg.Emit(OpCodes.Ldarg, 0);
            ilg.Emit(OpCodes.Ldfld, source);

            ilg.Emit(OpCodes.Ldarg, 0);
            ilg.Emit(OpCodes.Ldfld, xmin);

            ilg.Emit(OpCodes.Ldarg, 0);
            ilg.Emit(OpCodes.Ldfld, ymin);

            ilg.Emit(OpCodes.Call, typeof(UnsafeArray2d<T>).GetProperty("Item").GetGetMethod());


            ilg.Emit(OpCodes.Ldarg, 0);
            ilg.Emit(OpCodes.Ldfld, target);

            ilg.Emit(OpCodes.Ldarg, 0);
            ilg.Emit(OpCodes.Ldfld, xmin);

            ilg.Emit(OpCodes.Ldarg, 0);
            ilg.Emit(OpCodes.Ldfld, ymin);

            ilg.Emit(OpCodes.Call, UnsafeArray2d.GetTypeInstance<R>().GetProperty("Item").GetGetMethod());

            ilg.Emit(OpCodes.Call, staticRun);

            ilg.Emit(OpCodes.Ret);
            return hss;
        }


        private static StripeMultiHandlerFactory PrepareFactory<R>(ConstructorInfo constructor, MethodInfo runMethod)
        {
            var dm = new DynamicMethod<StripeMultiHandlerFactory>("Factory", typeof(ExpressionArray2d).Module);
            var filg = dm.GetILGenerator();
            filg.Emit(OpCodes.Ldarg, 0); // xmin
            filg.Emit(OpCodes.Ldarg, 1); // xmax
            filg.Emit(OpCodes.Ldarg, 2); // ymin
            filg.Emit(OpCodes.Ldarg, 3); // ymax
            for(var i = 0; i<constructor.GetParameters().Length-5;i++)
            {
                filg.Emit(OpCodes.Ldarg, 4); // sources
                filg.Emit(OpCodes.Ldc_I4, i); // #i
                filg.Emit(OpCodes.Ldelem);
            }

            filg.Emit(OpCodes.Ldarg, 5); // targetPtr

            filg.Emit(OpCodes.Newobj, constructor); // create new Handler object
            filg.Emit(OpCodes.Ldftn, runMethod); // &Run 
            filg.Emit(OpCodes.Newobj, typeof(Action).GetConstructor(new[] { typeof(object), typeof(IntPtr) }));
            filg.Emit(OpCodes.Ret);

            return dm.CreateDelegate();
        }

        private static StripeHandlerFactory<T, R> PrepareFactory<T, R>(ConstructorInfo constructor, MethodInfo runMethod)
            where T : unmanaged
            where R : unmanaged
        {
            var dm = new DynamicMethod<StripeHandlerFactory<T, R>>("Factory", typeof(ExpressionArray2d).Module);
            var filg = dm.GetILGenerator();
            filg.Emit(OpCodes.Ldarg, 0); // xmin
            filg.Emit(OpCodes.Ldarg, 1); // xmax
            filg.Emit(OpCodes.Ldarg, 2); // ymin
            filg.Emit(OpCodes.Ldarg, 3); // ymax
            filg.Emit(OpCodes.Ldarg, 4); // sourcePtr
            filg.Emit(OpCodes.Ldarg, 5); // targetPtr

            filg.Emit(OpCodes.Newobj, constructor); // create new Handler object
            filg.Emit(OpCodes.Ldftn, runMethod); // &Run 
            filg.Emit(OpCodes.Newobj, typeof(Action).GetConstructor(new[] { typeof(object), typeof(IntPtr) }));
            filg.Emit(OpCodes.Ret);

            return dm.CreateDelegate();

        }

        public unsafe static R[,] FilterParallel<T, R>(Expression<Kernel<T, R>> kernel, T[,] source)
            where T : unmanaged
            where R : unmanaged
        {
            var h = source.GetLength(0);
            var w = source.GetLength(1);
            var result = new R[h, w];
            var km = KernelMeasure.Measure(kernel.Compile());

            //h -= km.xmax; // decrease to use as the cycle limit

            var factory = GenerateStripeHandlerFactory(kernel);


            var linesCount = h + km.xmin - km.xmax;
            var taskCount = Min(linesCount, Environment.ProcessorCount);

            using (var sourceRef = new UnsafeArray2d<T>(source))
            using (var resultRef = new UnsafeArray2d<R>(result))
            {
                var tasks = new Task[taskCount - 1];
                for (var taskNum = 0; taskNum < taskCount; taskNum++)
                {
                    int xmin = -km.xmin + taskNum * linesCount / taskCount;
                    int xmax = -km.xmin + (taskNum + 1) * linesCount / taskCount;
                    var stripeHandler = factory(xmin, xmax, -km.ymin, w - km.ymax, sourceRef, resultRef);
                    if (taskNum + 1 == taskCount)                   // the last task
                        stripeHandler();                            // is executed in the main thread                      
                    else                                            // the others 
                        tasks[taskNum] = Task.Run(stripeHandler);   // are executed in the worker threads
                }
                Task.WaitAll(tasks);
            }
            return result;
        }

        //public static R[,] Select<T, R>(this T[,] source, Expression<Func<T, R>> kernel)
        //    where T : unmanaged
        //    where R : unmanaged
        //    => Select(source.Wrap(Bounds.Skip), (cell) => kernel(cell[0, 0]));

        public static R[,] Select<T, R>(this IRelQueryableArray2d<T> source, Expression<Kernel<T, R>> kernel)
            where T : unmanaged
            where R : unmanaged
        {
            if (source is ArrayWrapper<T> wrapper)
                return FilterParallel(kernel, wrapper._array);
            else
                throw new ArgumentOutOfRangeException(nameof(source), $"Sources of type {source.GetType()} aren't yet supported");
        }
        public static IQueryableArray2d<R> Select<T, R>(this IQueryableArray2d<T> source, Expression<Func<T, R>> kernel) 
            => new MultiSelectWrapper<R>(source, kernel, false);

        public static IQueryableArray2d<R> SelectMany<T, A, R>(this T[,] source, Func<T, IRelQueryableArray2d<A>> secondSelector, Expression<Func<T, ICell<A>, R>> resultSelector)
            => MultiSelectWrapper<R>.Create(source, secondSelector(default), resultSelector, false);
        public static IQueryableArray2d<R> SelectMany<T, A, R>(this T[,] source, Func<T, A[,]> secondSelector, Expression<Func<T, A, R>> resultSelector)
            => MultiSelectWrapper<R>.Create(new ArrayWrapper<T>(source, Bounds.Skip), new ArrayWrapper<A>(secondSelector(default), Bounds.Skip), resultSelector, false);

        public static IQueryableArray2d<R> SelectMany<T, A, R>(this IQueryableArray2d<T> source, Func<T, IQueryableArray2d<A>> secondSelector, Expression<Func<T, A, R>> resultSelector) 
            => MultiSelectWrapper<R>.Create(source, secondSelector(default), resultSelector, false);
        public static IQueryableArray2d<R> SelectMany<T, A, R>(this IQueryableArray2d<T> source, Func<T, IRelQueryableArray2d<A>> secondSelector, Expression<Func<T, ICell<A>, R>> resultSelector)
            => MultiSelectWrapper<R>.Create(source, secondSelector(default), resultSelector, false);


        public static IQueryableArray2d<R> SelectMany<T, A, R>(this IRelQueryableArray2d<T> source, Func<IRelQueryableArray2d<T>, IRelQueryableArray2d<A>> secondSelector, Expression<Func<ICell<T>, ICell<A>, R>> resultSelector)
            => MultiSelectWrapper<R>.Create(source, secondSelector(default), resultSelector, false);

        public static IQueryableArray2d<R> SelectMany<T, R>(this T[,] source, Func<T, ArrayRecurrence<R>> secondSelector, Expression<Func<T, ICell<R>, R>> resultSelector)
            => MultiSelectWrapper<R>.Create(source, resultSelector, true);


        public static IArray2d<R> SelectMany<T, R>(this IRelQueryableArray2d<T> source, Func<ICell<T>, ArrayRecurrence<R>> secondSelector, Expression<Func<ICell<T>, ICell<R>, R>> resultSelector) 
            => new MultiSelectWrapper<R>(source, resultSelector, true);


    }

    class ParameterReplacer: ExpressionVisitor
    {
        private readonly Expression _old;
        private readonly Expression _new;

        public ParameterReplacer(Expression old, Expression @new)
        {
            _old = old ?? throw new ArgumentNullException(nameof(old));
            _new = @new ?? throw new ArgumentNullException(nameof(@new));
        }
        public override Expression Visit(Expression node)
        {
            if (_old.Equals(node))
                return _new;
            else
                return base.Visit(node);
        }
    }

    class CellAccessUnsafeInliner : ExpressionVisitor
    {
        private readonly ParameterExpression _i;
        private readonly ParameterExpression _j;
        private readonly ParameterExpression _h;
        private readonly ParameterExpression _w;

        private readonly IReadOnlyDictionary<ParameterExpression, ParameterExpression> _cellToPointerMap;

        public CellAccessUnsafeInliner(IReadOnlyDictionary<ParameterExpression, ParameterExpression> cellToPointerMap, ParameterExpression i, ParameterExpression j, ParameterExpression h, ParameterExpression w)
        {
            _cellToPointerMap = cellToPointerMap ?? throw new ArgumentNullException(nameof(cellToPointerMap));
            _i = i ?? throw new ArgumentNullException(nameof(i));
            _j = j ?? throw new ArgumentNullException(nameof(j));
            _h = h ?? throw new ArgumentNullException(nameof(h));
            _w = w ?? throw new ArgumentNullException(nameof(w));
        }

        public CellAccessUnsafeInliner(ParameterExpression cell, ParameterExpression source, ParameterExpression i, ParameterExpression j, ParameterExpression h, ParameterExpression w)
            : this(new Dictionary<ParameterExpression, ParameterExpression>() { { cell, source } }, i, j, h, w) { }


 
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_cellToPointerMap.ContainsKey(node))
            {
                if (node.Type.IsConstructedGenericType && node.Type.GetGenericTypeDefinition() == typeof(ICell<>))
                    throw new InvalidOperationException("Cannot pass the ICell to an external method - inlining is impossible!");
                var sourcePtr = _cellToPointerMap[node];
                return MakeIndex
                (
                    sourcePtr,
                    sourcePtr.Type.GetProperties().Where(p => p.GetIndexParameters().Length == 1).Single(),
                    new[] {Expression.Constant(0)}
                ).Reduce();
            }
            // TODO: spawn a new Cell<T> instance instead.
            else
                return base.VisitParameter(node);
        }

        //private static readonly MethodInfo _iCellItemGet = typeof(ICell<>).GetProperty("Item").GetGetMethod();
        //private static readonly MethodInfo _iCellXGet = typeof(ICell<>).GetProperty("X").GetGetMethod();
        //private static readonly MethodInfo _iCellYGet = typeof(ICell<>).GetProperty("Y").GetGetMethod();
        //private static readonly MethodInfo _iCellHGet = typeof(ICell<>).GetProperty("H").GetGetMethod();
        //private static readonly MethodInfo _iCellWGet = typeof(ICell<>).GetProperty("W").GetGetMethod();
        //private static readonly PropertyInfo _ua2dpItem = typeof(UnsafeArray2dPtr<>).GetProperties().Where(p => p.GetIndexParameters().Length == 2).Single();

        protected override Expression VisitMember(MemberExpression node)
        {
            if(node.Expression is ParameterExpression cellRef && _cellToPointerMap.ContainsKey(cellRef))
            {
                switch (node.Member.Name)
                {
                    case "X": return _i;
                    case "Y": return _j;
                    case "H": return _h;
                    case "W": return _w;
                }
            }
            if(node.Expression is ConstantExpression ce)
            {
                switch(node.Member)
                {
                    case FieldInfo fe: return Expression.Constant(fe.GetValue(ce.Value));
                    case PropertyInfo pe: return Expression.Constant(pe.GetValue(ce.Value));
                    default: throw new NotSupportedException($"The member is of unknown type ${node.Member.GetType().Name}");
                }
            }
            return base.VisitMember(node);
        }
        //protected override Expression VisitBinary(BinaryExpression node)
        //{
        //    var n = base.VisitBinary(node);
        //    if (n != node)
        //        Console.WriteLine("Bingo!");
        //    return n;
        //}
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if(node.Object is ParameterExpression cellRef && _cellToPointerMap.ContainsKey(cellRef))
            {
                var sourcePtr = _cellToPointerMap[cellRef];
                if (node.Method.Name == "get_Item")
                    return MakeIndex
                    (
                        sourcePtr,
                        sourcePtr.Type.GetProperties().Where(p => p.GetIndexParameters().Length == 2).Single(),
                        new[] { Visit(node.Arguments[0]), Visit(node.Arguments[1]) }
                    ).Reduce();

                throw new InvalidOperationException($"Unsupported method ICell<T>.{node.Method}");
            }

            return base.VisitMethodCall(node);
        }
    }

    internal class DualSelectWrapper<T, A, R> : SlowDeferredArrayWrapperBase<R>, IQueryableArray2d<R>
    {
        private readonly IArray2d<T> _left;
        private readonly IArray2d<A> _right;
        private readonly Expression<Func<ICell<T>, ICell<A>, R>> _kernel;
        private readonly Func<ICell<T>, ICell<A>, R> _compiledKernel;
        private readonly KernelMeasure _km;
        private readonly int _h;
        private readonly int _w;

        public R this[int x, int y]
        {
            get
            {
                var cell1 = new Cell<T>(_left, x, y);
                var cell2 = new Cell<A>(_right, x, y);
                return _compiledKernel(cell1, cell2);
            }
        }

        public int GetLength(int dimension)
        {
            switch (dimension)
            {
                case 0: return _h;
                case 1: return _w;
                default: throw new ArgumentOutOfRangeException(nameof(dimension), dimension, "only 0 or 1 can be specified");
            }
        }

        public override R[,] ToArray()
        {
            var result = new R[_h, _w];

            var cell1 = new Cell<T>(_left);
            var cell2 = new Cell<A>(_right);

            for (cell1._x = -_km.xmin; cell1._x < _h - _km.xmax; cell1._x++)
            {
                cell2._x = cell1._x;
                for (cell1._y = -_km.ymin; cell1._y < _w - _km.ymax; cell1._y++)
                {
                    cell2._y = cell1._y;
                    result[cell1._x, cell1._y] = _compiledKernel(cell1, cell2);
                }
            }

            return result;
        }

        public DualSelectWrapper(IArray2d<T> left, IArray2d<A> right, Expression<Func<ICell<T>, ICell<A>, R>> kernel)
        {
            _left = left ?? throw new ArgumentNullException(nameof(left));
            _right = right ?? throw new ArgumentNullException(nameof(right));
            _kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
            _compiledKernel = _kernel.Compile();

            _h = Min(left.GetLength(0), right.GetLength(0));
            _w = Min(left.GetLength(1), right.GetLength(1));

            _km = new KernelMeasure();
            var km1 = new KernelMeasure<T>(_km);
            var km2 = new KernelMeasure<A>(_km);
            _compiledKernel(km1, km2);
        }

    }

    internal abstract class SlowDeferredArrayWrapperBase<T> : IStructuralEquatable
    {
    
        public bool Equals(object other, IEqualityComparer comparer)
        {
            return ((IStructuralEquatable)ToArray()).Equals(other, comparer);
        }

        public int GetHashCode(IEqualityComparer comparer)
        {
            return ((IStructuralEquatable)ToArray()).GetHashCode(comparer);
        }

        public abstract T[,] ToArray();

    }



}

using Linq2d.CodeGen;
using Linq2d.Expressions;
using Mono.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using static System.Linq.Expressions.Expression;

namespace Linq2d
{
    internal class ArrayQueryBase: IVectorizable
    {
        public LambdaExpression Kernel { get; }
        public List<ArraySource> Sources { get; } = new List<ArraySource>();
        public List<Type> Results { get; } = new List<Type>();
        public List<object> ResultReplacements { get;} = new List<object>();
        public string MethodName { get; protected set; }
        protected ArrayQueryBase(ArraySource source, LambdaExpression kernel)
        {
            Sources.Add(source);
            Kernel = kernel;
            Results.AddRange(GetTypes(kernel.ReturnType));
        }

        protected ArrayQueryBase(ArraySource source, LambdaExpression kernel, object resultInit) : this(source, kernel) => ResultReplacements.Add(resultInit);
        private static IEnumerable<Type> GetTypes(Type t) 
            => typeof(ITuple).IsAssignableFrom(t) ? t.GetGenericArguments() : (new[] { t });
        protected ArrayQueryBase(IArrayQuery source, LambdaExpression kernel)
        {
            switch (source)
            {
                case ArrayQueryBase aqb:
                    Sources.AddRange(aqb.Sources);
                    ResultReplacements.AddRange(aqb.ResultReplacements);
                    ParameterExpression outerArg = kernel.Parameters[0];
                    if (outerArg.Type != aqb.Kernel.ReturnType)
                        throw new InvalidCastException($"Outer lambda expects to consume type {outerArg.Type}, inner produces {aqb.Kernel.ReturnType}");

                    var newBody = ExpressionReplacer.Replace(kernel.Body, outerArg, aqb.Kernel.Body); // consider introducing a local instead of repetitive expression calculation
                    newBody = new IntermediateAnonymousRemovalVisitor().Visit(newBody);

                    var newLambda = Lambda(newBody, aqb.Kernel.Parameters.Union(kernel.Parameters.Skip(1)));
                    Kernel = newLambda;
                    break;

                default: throw new ArgumentException($"Unknown source type {source.GetType()}", nameof(source));
            }
            Results.AddRange(GetTypes(Kernel.ReturnType));
            MethodName = new System.Diagnostics.StackTrace().GetFrame(5).GetMethod().Name;
            //(_h, _w) = EnsureSameSize(Sources);
        }
        protected ArrayQueryBase(IArrayQuery source, LambdaExpression kernel, object resultInit) : this(source, kernel) => ResultReplacements.Add(resultInit);

        protected ArrayQueryBase(ArraySource left, ArraySource right, LambdaExpression kernel) : this(left, kernel) => Sources.Add(right);
        protected ArrayQueryBase(ArraySource left, ArraySource right, LambdaExpression kernel, object resultInit) : this(left, kernel, resultInit) => Sources.Add(right);

        protected ArrayQueryBase(IArrayQuery left, ArraySource right, LambdaExpression kernel) : this(left, kernel) => Sources.Add(right);
        protected ArrayQueryBase(IArrayQuery left, ArraySource right, LambdaExpression kernel, object resultInit) : this(left, kernel)
        {
            Sources.Add(right);
            ResultReplacements.Add(resultInit);
        }
        public bool Vectorized { get; private set; } = false;
        public VectorizationResult VectorizationResult { get; private set; } = null;

        protected D BuildTransform<D>()
            where D : Delegate
        {
            MethodInfo invoke = typeof(D).GetMethod("Invoke");
            var paramTypes = from p in invoke.GetParameters() select p.ParameterType;
            paramTypes = paramTypes.Union(GetTypes(invoke.ReturnType));

            if ((from t in paramTypes select t.GetElementType()).All(TypeHelper.IsUnmanaged))
                return BuildUnsafeTransform<D>();
            return BuildSafeTransform<D>();
        }


        private D BuildUnsafeTransform<D>()
            where D : Delegate
        {
            var resultVars = new ParameterExpression[Results.Count];
            for (int i = 0; i < Results.Count; i++)
                resultVars[i] = Variable(Results[i].MakeArrayType(2), "result" + (i + 1));

            var sourceArgs = new ParameterExpression[Sources.Count];
            for (int i = 0; i < Sources.Count; i++)
                sourceArgs[i] = Parameter(Sources[i].Type.MakeArrayType(2), "source" + (i + 1));

            var iVar = Variable(typeof(int), "i");
            var jVar = Variable(typeof(int), "j");
            var hVar = Variable(typeof(int), "h");
            var wVar = Variable(typeof(int), "w");

            var simplifiedKernel = Arithmetic.Simplify(Kernel.Body, GetBaseRanges(hVar, wVar));
            var km = new KernelMeasurer();
            km.Visit(simplifiedKernel);

            // now split kernel into the individual kernels
            var kernels = new Expression[Results.Count];
            if (Results.Count == 1)
                kernels[0] = Kernel.Body;
            else
            {
                if (Kernel.Body is IArgumentProvider ip && ip.ArgumentCount == Results.Count)
                {
                    for (int i = 0; i < Results.Count; i++)
                        kernels[i] = ip.GetArgument(i);
                }
                else throw new InvalidOperationException($"Cannot recognize the selector function. Expect a ValueTuple construction call with the number of components equal to {Results.Count}");
                  
            }


            (int minX, int maxX, int minY, int maxY) = km.MergedAccesses;

            var baseRanges = GetBaseRanges(hVar, wVar, maxX - minX, maxY - minY);

            var dm = new DynamicMethod<D>(MethodName, saveAssembly: true);

            dm.GenerateIL(ilg =>
            {
                #region prolog
                var kcs = new KernelCompilerScalar(ilg, wVar);

                var h = ilg.DeclareLocal<int>(); kcs.VariableMap[hVar] = h;
                var w = ilg.DeclareLocal<int>(); kcs.VariableMap[wVar] = w;
                var i = ilg.DeclareLocal<int>(); kcs.VariableMap[iVar] = i;
                var j = ilg.DeclareLocal<int>(); kcs.VariableMap[jVar] = j;


                var pSources = new LocalBuilder[Sources.Count];
                var pSrcs = new LocalBuilder[Sources.Count];
                var sources = ilg.DeclareLocal<Array[]>();

                ilg.Ldc(Sources.Count);
                ilg.Newobj(() => new Array[0]);
                ilg.Stloc(sources);

                for (var c = 0; c < Sources.Count; c++)
                {
                    pSources[c] = ilg.DeclareLocal(Sources[c].Type.MakeByRefType(), true);
                    pSrcs[c] = ilg.DeclareLocal(Sources[c].Type.MakePointerType());
                    kcs.VariableMap[sourceArgs[c]] = pSrcs[c];

                    ilg.Ldarg(c);
                    ilg.Ldc0();
                    ilg.Ldc0();

                    ilg.Call(ArrayAddressMethod(Sources[c].Type));
                    ilg.Stloc(pSources[c]);

                    ilg.Ldloc(pSources[c]);
                    ilg.Emit(OpCodes.Conv_U);
                    ilg.Stloc(pSrcs[c]);

                    ilg.Ldloc(sources);
                    ilg.Ldc(c);
                    ilg.Ldarg(c);
                    ilg.Emit(OpCodes.Stelem_Ref);
                }
                #endregion
                var size = ilg.DeclareLocal<(int, int)>();
                #region initVars
                ilg.Ldc(maxX - minX);
                ilg.Ldc(maxY - minY);
                ilg.Ldloc(sources);
                ilg.Call((int h, int w, Array[] s) => ArrayHelper.EnsureSize(h, w, s));
                ilg.Stloc(size);
                ilg.Ldloc(size);
                ilg.Ldfld(((int, int) s) => s.Item1);
                ilg.Stloc(h);
                ilg.Ldloc(size);
                ilg.Ldfld(((int, int) s) => s.Item2);
                ilg.Stloc(w);


                var targets = new LocalBuilder[Results.Count];
                var pTargets = new LocalBuilder[Results.Count];
                var pTrgs = new LocalBuilder[Results.Count];

                for (var c = 0; c < Results.Count; c++)
                {
                    targets[c] = ilg.DeclareLocal(Results[c].MakeArrayType(2));
                    pTargets[c] = ilg.DeclareLocal(Results[c].MakeByRefType(), true);
                    pTrgs[c] = ilg.DeclareLocal(Results[c].MakePointerType());
                    kcs.VariableMap[resultVars[c]] = pTrgs[c];

                    ilg.Ldloc(h);
                    ilg.Ldloc(w);
                    ilg.Emit(OpCodes.Newobj, targets[c].LocalType.GetConstructor(new[] { typeof(int), typeof(int) }));
                    ilg.Stloc(targets[c]);

                    ilg.Ldloc(targets[c]);
                    ilg.Ldc0();
                    ilg.Ldc0();
                    ilg.Call(ArrayAddressMethod(Results[c]));
                    ilg.Stloc(pTargets[c]);

                    ilg.Ldloc(pTargets[c]);
                    ilg.Emit(OpCodes.Conv_U);
                    ilg.Stloc(pTrgs[c]);
                }
                #endregion

                var lineCounter = 0;
                while (lineCounter < -minX)
                {
                    int colCounter;
                    for(colCounter = 0; colCounter < -minY; colCounter++)
                        for (int c = 0; c < Results.Count; c++)
                            HandleSingleResultElement(ilg, kernels[c], Results[c], resultVars, sourceArgs, hVar, wVar, baseRanges, kcs, pTrgs[c], Constant(lineCounter), Constant(colCounter));

                    ilg.Ldc(colCounter);
                    ilg.Stloc(j);

                    var loopJStart = ilg.DefineLabel();
                    ilg.Br(loopJStart);
                    {
                        var loopJBody = ilg.DefineAndMarkLabel();

                        for (int c = 0; c < Results.Count; c++)
                            HandleSingleResultElement(ilg, kernels[c], Results[c], resultVars, sourceArgs, hVar, wVar, baseRanges.Add(jVar, Constant(colCounter), Subtract(wVar, Constant(maxY + 1))), kcs, pTrgs[c], Constant(lineCounter), jVar);

                        ilg.Increment(j);

                        ilg.MarkLabel(loopJStart);
                        ilg.Ldloc(j);
                        ilg.Ldloc(w);
                        if (maxY > 0)
                        {
                            ilg.Ldc(maxY);
                            ilg.Sub();
                        }
                        ilg.Emit(OpCodes.Blt, loopJBody);
                    }
                    for (colCounter = -maxY; colCounter < 0; colCounter++)
                        for (int c = 0; c < Results.Count; c++)
                            HandleSingleResultElement(ilg, kernels[c], Results[c], resultVars, sourceArgs, hVar, wVar, baseRanges, kcs, pTrgs[c], Constant(lineCounter), Subtract(wVar, Constant(-colCounter)));

                    lineCounter++;
                }

                ilg.Ldc(-minX);
                ilg.Stloc(i);

                var loopIStart = ilg.DefineLabel();
                ilg.Br(loopIStart);
                {
                    var loopIBody = ilg.DefineAndMarkLabel();

                    int colCounter;
                    var coreRanges = baseRanges.Add(iVar, Constant(-minX), Subtract(hVar, Constant(maxX + 1)));
                    for (colCounter=0; colCounter < -minY; colCounter++)
                        for (int c = 0; c < Results.Count; c++)
                            HandleSingleResultElement(ilg, kernels[c], Results[c], resultVars, sourceArgs, hVar, wVar, coreRanges, kcs, pTrgs[c], iVar, Constant(colCounter));

                    ilg.Ldc(-minY);
                    ilg.Stloc(j);

                    // Vector part
                    {

                        //var vectorNodes = new HashSet<Expression>();
                        var vectorizable = true;
                        var coreKernels = new Expression[Results.Count];
                        coreRanges = baseRanges.Add(iVar, Constant(-minX), Subtract(hVar, Constant(maxX + 1))).Add(jVar, Constant(-minY), Subtract(wVar, Constant(maxY + 1)));

                        var argTypes = (from s in Sources select s.Type).Union(Results);
                        var stepSizes = new int[Results.Count];
                        var vectorKernels = new Expression[Results.Count];
                        //var step = 32 / (from t in argTypes select (int)typeof(Unsafe).GetMethod("SizeOf").MakeGenericMethod(t).Invoke(null, null)).Max();
                        for (int c = 0; (c < Results.Count) && vectorizable; c++)
                        {
                            //if (c<ResultReplacements.Count)
                            //    recurrentOverlap |= (km.Accesses[Kernel.Parameters[Sources.Count + c]].minY < 0);
                            coreKernels[c] = InlineKernel(kernels[c], iVar, jVar, hVar, wVar, coreRanges, resultVars, sourceArgs);
                            //vectorizable &= VectorVerify.CanBeVectorized(coreKernels[c], vectorNodes);
                            var v = new VectorizationResult(false, null, null, null);
                            var step = 32;
                            while (!v.Success && step > 1)
                            {
                                v = Vectorizer.Vectorize(step, coreKernels[c], resultVars, sourceArgs); 
                                stepSizes[c] = step;
                                step >>= 1; // try smaller step sizes until it works
                            }
                            if (v.Success)
                            {
                                vectorKernels[c] = v.Expression;
                            }
                            else
                            {
                                vectorizable = false;
                                VectorizationResult = v;
                            }
                        }

                        if (vectorizable)
                        {
                            Vectorized = true;
                            var maxStep = stepSizes.Max();
                            var minStep = stepSizes.Min();
                            //var kcv = new KernelCompilerVector(ilg, wVar, vectorNodes);
                            //foreach (var vm in kcs.VariableMap)
                            //    kcv.VariableMap[vm.Key] = vm.Value;


                            var loopJVectorStart = ilg.DefineLabel();
                            ilg.Br(loopJVectorStart);
                            {
                                var loopJVectorBody = ilg.DefineAndMarkLabel();

                                for (var unroll = 0; unroll < maxStep / minStep; unroll++)
                                {
                                    for (int c = 0; c < Results.Count; c++)
                                    {
                                        if (minStep * unroll % stepSizes[c] == 0)
                                        {
                                            ilg.Ldloc(pTrgs[c]);
                                            kcs.Load2dPointerOffset(Results[c], iVar, jVar);
                                            kcs.Visit(vectorKernels[c]);
                                            ilg.Call(VectorData.VectorInfo[stepSizes[c]].StoreOperations[Results[c]]);
                                        }
                                    }
                                    ilg.Increment(j, minStep);
                                }

                                ilg.MarkLabel(loopJVectorStart);
                                ilg.Ldloc(j);
                                ilg.Ldc(maxStep + maxY);
                                ilg.Add();
                                ilg.Ldloc(w);
                                ilg.Emit(OpCodes.Ble, loopJVectorBody);
                            }
                        }

                        var loopJStart = ilg.DefineLabel(); // now we get to the scalar part
                        ilg.Br(loopJStart);
                        {
                            var loopJBody = ilg.DefineAndMarkLabel();

                            for (int c = 0; c < Results.Count; c++)
                                HandleSingleResultElement(ilg, kernels[c], Results[c], resultVars, sourceArgs, hVar, wVar, coreRanges, kcs, pTrgs[c], iVar, jVar);

                            ilg.Increment(j);

                            ilg.MarkLabel(loopJStart);
                            ilg.Ldloc(j);
                            ilg.Ldloc(w);
                            if (maxY > 0)
                            {
                                ilg.Ldc(maxY);
                                ilg.Sub();
                            }
                            ilg.Emit(OpCodes.Blt, loopJBody);
                        }
                    }

                    for (colCounter = -maxY; colCounter < 0; colCounter++)
                        for (int c = 0; c < Results.Count; c++)
                            HandleSingleResultElement(ilg, kernels[c], Results[c], resultVars, sourceArgs, hVar, wVar, baseRanges.Add(iVar, Constant(-minX), Subtract(hVar, Constant(maxX + 1))), kcs, pTrgs[c], iVar, Add(wVar, Constant(colCounter)));

                    ilg.Increment(i);

                    ilg.MarkLabel(loopIStart);
                    ilg.Ldloc(i);
                    ilg.Ldloc(h);
                    if (maxX > 0)
                    {
                        ilg.Ldc(maxX);
                        ilg.Sub();
                    }
                    ilg.Emit(OpCodes.Blt, loopIBody);
                }

                for (lineCounter = -maxX; lineCounter < 0; lineCounter++)
                {
                    int colCounter;

                    for (colCounter=0; colCounter < -minY;colCounter++)
                        for (int c = 0; c < Results.Count; c++)
                            HandleSingleResultElement(ilg, kernels[c], Results[c], resultVars, sourceArgs, hVar, wVar, baseRanges.Add(iVar, Constant(-minX), Subtract(hVar, Constant(maxX + 1))), kcs, pTrgs[c], Add(hVar, Constant(lineCounter)), Constant(colCounter));


                    ilg.Ldc(colCounter);
                    ilg.Stloc(j);

                    var loopJStart = ilg.DefineLabel();
                    ilg.Br(loopJStart);
                    {
                        var loopJBody = ilg.DefineAndMarkLabel();

                        for (int c = 0; c < Results.Count; c++)
                            HandleSingleResultElement(ilg, kernels[c], Results[c], resultVars, sourceArgs, hVar, wVar, baseRanges.Add(jVar, Constant(colCounter), Subtract(wVar, Constant(maxY + 1))), kcs, pTrgs[c], Subtract(hVar, Constant(-lineCounter)), jVar);

                        ilg.Increment(j);

                        ilg.MarkLabel(loopJStart);
                        ilg.Ldloc(j);
                        ilg.Ldloc(w);
                        if (maxY > 0)
                        {
                            ilg.Ldc(maxY);
                            ilg.Sub();
                        }
                        ilg.Emit(OpCodes.Blt, loopJBody);
                    }
                    for (colCounter = -maxY; colCounter < 0; colCounter++)
                        for (int c = 0; c < Results.Count; c++)
                            HandleSingleResultElement(ilg, kernels[c], Results[c], resultVars, sourceArgs, hVar, wVar, baseRanges, kcs, pTrgs[c], Subtract(hVar, Constant(-lineCounter)), Subtract(wVar, Constant(-colCounter)));

                    //ilg.Increment(i);
                }

                //ilg.Call(() => Console.ReadLine());
                //ilg.Emit(OpCodes.Pop);
                //ilg.Emit(OpCodes.Break);
                for (int c = 0; c < Results.Count; c++)
                    ilg.Ldloc(targets[c]);

                if (Results.Count > 1)
                {
                    var r = typeof(D).GetMethod("Invoke").ReturnType;
                    var cc = r.GetConstructors();
                    ilg.Emit(OpCodes.Newobj, cc[0]);
                }
                ilg.Ret();
            });
            return dm.CreateDelegate();
        }

        private void HandleSingleResultElement(ILGenerator ilg, Expression kernel, Type resultType, ParameterExpression[] resultVars, ParameterExpression[] sourceArgs, ParameterExpression hVar, ParameterExpression wVar, IReadOnlyDictionary<Expression, (Expression minVal, Expression maxVal)> ranges, KernelCompilerScalar kcs, LocalBuilder pTarget, Expression i, Expression j)
        {
            ilg.Ldloc(pTarget);
            kcs.Load2dPointerOffset(resultType, i, j);
            var inlinedKernel = InlineKernel(kernel, i, j, hVar, wVar, ranges, resultVars, sourceArgs);
            kcs.Visit(inlinedKernel);
            ilg.Store(resultType);
        }

        private static IReadOnlyDictionary<Expression, (Expression minVal, Expression maxVal)> GetBaseRanges(ParameterExpression hVar, ParameterExpression wVar, int minH=0, int minW=0)
        {
            return Ranges.No.Add(hVar, Constant(minH)).Add(wVar, Constant(minW));
        }
        private static MethodInfo ArrayAddressMethod(Type type) => type.MakeArrayType(2).GetMethod("Address", new[] { typeof(int), typeof(int) });

        private D BuildSafeTransform<D>()
            where D: Delegate
        {
            var resultVars = new ParameterExpression[Results.Count];
            for (int i = 0; i < Results.Count; i++)
                resultVars[i] = Variable(Results[i].MakeArrayType(2), "result" + (i + 1));

            var sourceArgs = new ParameterExpression[Sources.Count];
            for (int i = 0; i < Sources.Count; i++)
                sourceArgs[i] = Parameter(Sources[i].Type.MakeArrayType(2), "source" + (i + 1));
            var i_var = Variable(typeof(int), "i");
            var j_var = Variable(typeof(int), "j");
            var h_var = Variable(typeof(int), "h");
            var w_var = Variable(typeof(int), "w");
   
            var inlinedKernel = InlineKernel(Kernel.Body, i_var, j_var, h_var, w_var, GetBaseRanges(h_var, w_var), resultVars, sourceArgs);
            var ne = inlinedKernel as NewExpression;
            var block = new List<Expression>();
            block.Add(Assign(h_var, Call(sourceArgs[0], sourceArgs[0].Type.GetMethod("GetLength", new[] { typeof(int) }), Constant(0))));
            block.Add(Assign(w_var, Call(sourceArgs[0], sourceArgs[0].Type.GetMethod("GetLength", new[] { typeof(int) }), Constant(1))));
            for (int c = 0; c < Results.Count; c++)
                block.Add(Assign(resultVars[c], NewArrayBounds(Results[c], h_var, w_var)));

            var loopBody = new List<Expression>();
            for (int c = 0; c < Results.Count; c++)
                loopBody.Add(Assign(MakeIndex(resultVars[c], Results[c].MakeArrayType(2).GetProperty("Item", new Type[] { typeof(int), typeof(int) }), new[] { i_var, j_var }), Results.Count>1?ne.Arguments[c]:inlinedKernel));

            block.Add(ExpressionHelper.For(i_var,
                    Constant(0),
                    LessThan(i_var, h_var),
                    Constant(1),
                        ExpressionHelper.For(j_var,
                        Constant(0),
                        LessThan(j_var, w_var),
                        Constant(1),
                        Block(loopBody)
                )));
            if(Results.Count==1)
            {
                block.Add(resultVars[0]);
            }
            else
            {
                var r = typeof(D).GetMethod("Invoke").ReturnType;
                block.Add(New(r.GetConstructors()[0], resultVars)); 
            }

            var fe = Block(resultVars.Append(h_var).Append(w_var), block);
            var nne = Lambda<D>(fe, sourceArgs);
            //var s = nne.ToCSharpCode();
            //Console.WriteLine(s);
            return nne.Compile();
        }

        private Expression InlineKernel(Expression kernel, Expression i, Expression j, Expression h, Expression w, IReadOnlyDictionary<Expression, (Expression minVal, Expression maxVal)> variableRanges, ParameterExpression[] resultVars, ParameterExpression[] sourceArgs)
        {
            var inlinedKernel = GetKernelInliner(i, j, h, w, resultVars, sourceArgs).Visit(kernel);

            inlinedKernel = Arithmetic.Simplify(inlinedKernel, variableRanges);
            inlinedKernel = CommonSubexpressions.Eliminate(inlinedKernel);
            return inlinedKernel;
        }

        protected CellAccessInliner GetKernelInliner(Expression i, Expression j, Expression h, Expression w, ParameterExpression[] resultVars, ParameterExpression[] sourceArgs)
        {
            var replacements = new (Expression from, Expression to, OutOfBoundsStrategy strategy)[sourceArgs.Length+ResultReplacements.Count];

            int sourceCount = sourceArgs.Length;
            for (int c = 0; c < sourceArgs.Length; c++)
                replacements[c] = (Kernel.Parameters[c], sourceArgs[c], Sources[c].OutOfBounds);
            for (int c = 0; c < ResultReplacements.Count; c++)
                replacements[sourceCount + c] = (Kernel.Parameters[sourceCount + c], resultVars[c], OutOfBoundsStrategy.Substitute(ResultReplacements[c]));

            return new CellAccessInliner(i, j, h, w, replacements);
        }
    }
}

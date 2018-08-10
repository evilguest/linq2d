using ClrTest.Reflection;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using static System.Math;


namespace System.Linq.Processing2d.FastUnsafe
{
    public static class Array2D
    {

        private static class Cache<K, V>
        {
            private static Dictionary<K, V>
                _cache = new Dictionary<K, V>();

            public static V Get(K key, Func<K, V> generator)
            {
                if (!_cache.TryGetValue(key, out V value))
                {
                    value = generator(key);
                    _cache[key] = value;
                };
                return value;
            }

        }
        private static Filter<T, R> GetFilter<T, R>(Kernel<T, R> kernel, Func<Kernel<T, R>, Filter<T, R>> filterGen) 
            => Cache<Kernel<T, R>, Filter<T, R>>.Get(kernel, filterGen);

        private static Filter<T, R> GetUnsafeFilter<T, R>(Kernel<T, R> kernel)
            where T : unmanaged
            where R : unmanaged => Cache<Kernel<T, R>, Filter<T, R>>.Get(kernel, GenerateUnsafeFilter);

        private unsafe static Filter<T,R> GenerateUnsafeFilter<T,R>(Kernel<T, R> kernel)
            where T: unmanaged
            where R: unmanaged
        {
            var km = KernelMeasure.Measure(kernel);

            var dm = new DynamicMethod<Func<object, T[,], R[,]>>("Filter." + kernel.Method.Name, typeof(Array2D).Module);


            var ilg = dm.GetILGenerator();

            var h = ilg.DeclareLocal(typeof(int)); 
            var w = ilg.DeclareLocal(typeof(int)); 
            var psource = ilg.DeclareLocal(typeof(T*)); 
            var sourcePin = ilg.DeclareLocal(typeof(T[,]), true); // a replica of the arg1, to force the GC pinning
            var targetPin = ilg.DeclareLocal(typeof(R[,]), true); // a target structure
            var i = ilg.DeclareLocal(typeof(int)); 
            var j = ilg.DeclareLocal(typeof(int)); 
            var W = ilg.DeclareLocal(typeof(int)); 
            var delta = ilg.DeclareLocal(typeof(int)); 


            ilg.Emit(OpCodes.Ldarg_1); // source
            ilg.Emit(OpCodes.Ldc_I4_0);// 0
            var getLength = typeof(T[,]).GetMethod(nameof(Array.GetLength), new [] { typeof(int) });
            ilg.Emit(OpCodes.Call, getLength);
            ilg.Emit(OpCodes.Stloc, h);


            ilg.Emit(OpCodes.Ldarg_1); // source
            ilg.Emit(OpCodes.Ldc_I4_1);// 1
            ilg.Emit(OpCodes.Call, getLength);
            ilg.Emit(OpCodes.Stloc, w);

            ilg.Emit(OpCodes.Ldloc, h);
            ilg.Emit(OpCodes.Ldloc, w);
            ilg.Emit(OpCodes.Newobj, typeof(R[,]).GetConstructor(new[] { typeof(int), typeof(int) }));
            ilg.Emit(OpCodes.Stloc, targetPin); // pinning the target

            ilg.Emit(OpCodes.Ldarg_1); // source
            ilg.Emit(OpCodes.Stloc, sourcePin); // pinning the source

            ilg.Emit(OpCodes.Ldloc, sourcePin);
            ilg.Emit(OpCodes.Ldc_I4, -km.xmin); // -km.xmin, source
            ilg.Emit(OpCodes.Ldc_I4, -km.ymin); // -km.ymin, -km.xmin, source
            MethodInfo arrayofTAddressGetter = typeof(T[,]).GetMethod("Address", new[] { typeof(int), typeof(int) });
            ilg.Emit(OpCodes.Call, arrayofTAddressGetter);// &source[1,1];
            ilg.Emit(OpCodes.Conv_U);
            ilg.Emit(OpCodes.Stloc, psource);

            ilg.Emit(OpCodes.Ldloc, targetPin); // load result
            ilg.Emit(OpCodes.Ldc_I4, -km.xmin); // -km.xmin, target
            ilg.Emit(OpCodes.Ldc_I4, -km.ymin); // -km.ymin, -km.xmin, target
            ilg.Emit(OpCodes.Call, arrayofTAddressGetter);// &target[1,1];
            ilg.Emit(OpCodes.Conv_U);
            ilg.Emit(OpCodes.Ldloc, psource);
            ilg.Emit(OpCodes.Sub);
            ilg.Emit(OpCodes.Stloc, delta);

            ilg.Emit(OpCodes.Ldloc, w);
            ilg.Emit(OpCodes.Stloc, W);

            ilg.EmitIncrease(h, -km.ymax);
            ilg.EmitIncrease(w, -km.xmax);


            ilg.EmitFor(i, 0 - km.xmin, h, () =>
            {
                ilg.EmitFor(j, 0 - km.ymin, w, () =>
                {
                    ilg.Emit(OpCodes.Ldloc, psource); 
                    ilg.Emit(OpCodes.Ldloc, delta);
                    ilg.Emit(OpCodes.Add); // source+delta = target;

                    var usilv = new UnsafeInliningILInstructionVisitor<T>(ilg, i, j, W, psource);
                    new ILReader(kernel.Method).Accept(usilv);

                    ilg.Emit(OpCodes.Stind_I4);

                    ilg.EmitIncrease(psource, sizeof(T)); // psource ++
                    ilg.EmitIncrease(delta, sizeof(R)-sizeof(T)); // delta += sizediff; zero in case of same-type;
                });
                ilg.EmitIncrease(psource, sizeof(T) * (km.ymax - km.ymin));
            });
            
            ilg.Emit(OpCodes.Ldloc, targetPin);
            ilg.Emit(OpCodes.Ret);

            var inlined = dm.CreateDelegate();

            R[,] filter(T[,] data) => inlined(kernel.Target, data);

            return filter;
        }

        unsafe delegate Action StripeHandlerFactory<T>(T* sourcePtr, int length, long delta, int width, object target)
            where T: unmanaged;

        private static StripeHandlerFactory<T> GetStripeHandlerFactory<T, R>(Kernel<T, R> kernel)
            where T : unmanaged
            where R : unmanaged => Cache<Kernel<T, R>, StripeHandlerFactory<T>>.Get(kernel, GenerateStripeHandlerFactory);

        unsafe private static StripeHandlerFactory<T> GenerateStripeHandlerFactory<T, R>(Kernel<T, R> kernel)
            where T : unmanaged
            where R : unmanaged
        {
            var ab = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("StripeHandler"), AssemblyBuilderAccess.RunAndCollect);

            var tb = ab.DefineDynamicModule("StripeHandler", "StripeHandler.dll").DefineType("StripeHandler", TypeAttributes.Class | TypeAttributes.Public);

            var length = tb.DefineField("_length", typeof(int), FieldAttributes.Private);
            var sourcePtr = tb.DefineField("_sourcePtr", typeof(T*), FieldAttributes.Private);
            var delta = tb.DefineField("_delta", typeof(long), FieldAttributes.Private);
            var width = tb.DefineField("_width", typeof(int), FieldAttributes.Private);
            var target = tb.DefineField("_target", typeof(object), FieldAttributes.Private);

            ConstructorBuilder cb = BuildConstructor<T>(tb, length, sourcePtr, delta, width, target);

            MethodBuilder rm = BuildRunMethod(kernel, tb, length, sourcePtr, delta, width, target);

            var t = tb.CreateType();
            var constructor = t.GetConstructor((from p in cb.GetParameters() select p.ParameterType).ToArray());
            var runMethod = t.GetMethod(rm.Name); // there is only one method with this name

            return PrepareFactory<T>(constructor, runMethod);
        }

        private static unsafe ConstructorBuilder BuildConstructor<T>(TypeBuilder tb, FieldBuilder length, FieldBuilder sourcePtr, FieldBuilder delta, FieldBuilder width, FieldBuilder target) where T : unmanaged
        {
            var c = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.HasThis, new[] { typeof(T*), typeof(int), typeof(long), typeof(int), typeof(object) });
            var cilg = c.GetILGenerator();
            cilg.Emit(OpCodes.Ldarg, 0);
            cilg.Emit(OpCodes.Ldarg, 1);
            cilg.Emit(OpCodes.Stfld, sourcePtr);

            cilg.Emit(OpCodes.Ldarg, 0);
            cilg.Emit(OpCodes.Ldarg, 2);
            cilg.Emit(OpCodes.Stfld, length);

            cilg.Emit(OpCodes.Ldarg, 0);
            cilg.Emit(OpCodes.Ldarg, 3);
            cilg.Emit(OpCodes.Stfld, delta);

            cilg.Emit(OpCodes.Ldarg, 0);
            cilg.Emit(OpCodes.Ldarg, 4);
            cilg.Emit(OpCodes.Stfld, width);

            cilg.Emit(OpCodes.Ldarg, 0);
            cilg.Emit(OpCodes.Ldarg, 5);
            cilg.Emit(OpCodes.Stfld, target);
            cilg.Emit(OpCodes.Ret);
            return c;
        }

        private static unsafe MethodBuilder BuildRunMethod<T, R>(Kernel<T, R> kernel, TypeBuilder tb, FieldInfo length, FieldInfo sourcePtr, FieldInfo delta, FieldInfo width, FieldInfo target)
            where T : unmanaged
            where R : unmanaged
        {
            var km = KernelMeasure.Measure(kernel);

            var hs = tb.DefineMethod("Run", MethodAttributes.HideBySig | MethodAttributes.Public, null, new Type[0]);

            //var hs = new DynamicMethod("HandleStripe." + kernel.Method.Name, null, new[] { typeof(object), typeof(T*), typeof(int), typeof(int), typeof(long), typeof(int) }, typeof(Array2D).Module);
            var ilg = hs.GetILGenerator();
            var tempw = ilg.DeclareLocal(typeof(int));
            var tempW = ilg.DeclareLocal(typeof(int));
            var tempI = ilg.DeclareLocal(typeof(int));
            var tempJ = ilg.DeclareLocal(typeof(int));
            var lengthVar = ilg.DeclareLocal(typeof(int));
            var deltaVar = ilg.DeclareLocal(typeof(long));
            var srcPtr = ilg.DeclareLocal(typeof(T*));

            ilg.Emit(OpCodes.Ldarg_0); // this
            ilg.Emit(OpCodes.Ldfld, width);
            ilg.Emit(OpCodes.Dup);
            ilg.Emit(OpCodes.Stloc, tempW); // W = width;

            ilg.Emit(OpCodes.Ldc_I4, km.ymax);
            ilg.Emit(OpCodes.Sub);
            ilg.Emit(OpCodes.Stloc, tempw); // w = width-1;

            ilg.Emit(OpCodes.Ldarg_0); // this 
            ilg.Emit(OpCodes.Ldfld, sourcePtr);
            ilg.Emit(OpCodes.Stloc, srcPtr); // srcPtr = sourcePtr;

            ilg.Emit(OpCodes.Ldarg_0); // this 
            ilg.Emit(OpCodes.Ldfld, delta);
            ilg.Emit(OpCodes.Stloc, deltaVar); // delta = delta;

            ilg.Emit(OpCodes.Ldarg_0);// this
            ilg.Emit(OpCodes.Ldfld, length);
            ilg.Emit(OpCodes.Stloc, lengthVar);


            ilg.EmitFor(tempI, 0, lengthVar, () =>
            {

                ilg.EmitFor(tempJ, -km.ymin, tempw, () =>
                {
                    ilg.Emit(OpCodes.Ldloc, srcPtr); 
                    ilg.Emit(OpCodes.Ldloc, deltaVar); 
                    ilg.Emit(OpCodes.Conv_I); // (int) delta;
                    ilg.Emit(OpCodes.Add); // srcPtr+delta = target;


                    var usilv = new UnsafeParallelInliningILInstructionVisitor<T>(ilg, tempI, tempJ, tempW, srcPtr, target);
                    new ILReader(kernel.Method).Accept(usilv);

                    ilg.Emit(OpCodes.Stind_I4); //todo: work with different types

                    ilg.EmitIncrease(srcPtr, sizeof(T)); // srcPtr++;

                    ilg.EmitIncrease(deltaVar, sizeof(R) - sizeof(T));
                });

                ilg.EmitIncrease(srcPtr, sizeof(T) * (km.ymax - km.ymin));
                ilg.EmitIncrease(deltaVar, ((km.ymax - km.ymin)) * (sizeof(R) - sizeof(T)));

            });
            ilg.Emit(OpCodes.Ret);
            return hs;
        }

        private static unsafe StripeHandlerFactory<T> PrepareFactory<T>(ConstructorInfo constructor, MethodInfo runMethod) where T : unmanaged
        {
            var dm = new DynamicMethod("Factory", typeof(Action), new[] { typeof(T*), typeof(int), typeof(long), typeof(int), typeof(object) }, typeof(Array2D).Module);
            var filg = dm.GetILGenerator();
            filg.Emit(OpCodes.Ldarg, 0); // srcPtr
            filg.Emit(OpCodes.Ldarg, 1); // length
            filg.Emit(OpCodes.Ldarg, 2); // delta
            filg.Emit(OpCodes.Ldarg, 3); // width
            filg.Emit(OpCodes.Ldarg, 4); // target
            filg.Emit(OpCodes.Newobj, constructor); // create new Handler object
            filg.Emit(OpCodes.Ldftn, runMethod); // &Run 
            filg.Emit(OpCodes.Newobj, typeof(Action).GetConstructor(new[] { typeof(object), typeof(IntPtr) }));
            filg.Emit(OpCodes.Ret);

            return (StripeHandlerFactory<T>)dm.CreateDelegate(typeof(StripeHandlerFactory<T>));

            //return (T* srcPtr, int length, long delta, int width, object target) => Activator.CreateInstance(typeof(T), new object[] { (object)srcPtr, length, delta, width, target });
        }

        public static R[,] Select<T, R>(this T[,] source, Func<T, R> kernel)
            where T : unmanaged
            where R : unmanaged
            => Select(new ArrayWrapper<T>(source), (cell) => kernel(cell[0, 0]));

        public static R[,] Select<T, R>(this IRelQueryableArray2d<T> source, Kernel<T, R> kernel)
            where T : unmanaged
            where R : unmanaged
        {
            if (source is ArrayWrapper<T> wrapper)
                return FilterParallel(kernel, wrapper._array);
            else
                throw new ArgumentOutOfRangeException(nameof(source), $"Sources of type {source.GetType()} aren't yet supported");
        }

        private unsafe static R[,] FilterParallel<T, R>(Kernel<T, R> kernel, T[,] source)
            where T : unmanaged
            where R : unmanaged
        {
            var h = source.GetLength(0);
            var w = source.GetLength(1);
            var result = new R[h, w];
            var km = KernelMeasure.Measure(kernel);

            h -= km.xmax; // decrease to use as the cycle limit

            var factory = GetStripeHandlerFactory(kernel);


            var linesCount = h+km.xmin;
            var taskCount = Min(linesCount, Environment.ProcessorCount);

            fixed (T* psource = &source[-km.xmin, -km.ymin])
            fixed (R* presult = &result[-km.xmin, -km.ymin])
            {
                var tasks = new Task[taskCount - 1];
                long delta = 0;
                for (var taskNum = 0; taskNum < taskCount; taskNum++)
                {
                    int startInclusive = taskNum * linesCount / taskCount;
                    int endExclusive = (taskNum + 1) * linesCount / taskCount;
                    var psourcetmp = psource + startInclusive * w;
                    delta = (long)presult - (long)psource + startInclusive * w * (sizeof(R) - sizeof(T));
                    var stripeHandler = factory(psourcetmp, endExclusive - startInclusive, delta, w, kernel.Target);
                    if (taskNum + 1 == taskCount)                   // the last task
                        stripeHandler();                            // is executed in the main thread                      
                    else                                            // the others 
                        tasks[taskNum] = Task.Run(stripeHandler);   // are executed in the worker threads
                }
                Task.WaitAll(tasks);
            }
            return result;
        }

        public static IQueryableArray2d<R> SelectMany<T, A, R>(this IQueryableArray2d<T> source, Func<T, IQueryableArray2d<A>> secondSelector, Func<T, A, R> resultSelector) 
            => new SlowDeferred.DualSelectWrapper<T, A, R>(source, secondSelector(default), (cellL, cellR) => resultSelector(cellL[0, 0], cellR[0, 0]));
        public static IQueryableArray2d<R> SelectMany<T, A, R>(this IQueryableArray2d<T> source, Func<T, IRelQueryableArray2d<A>> secondSelector, Func<T, ICell<A>, R> resultSelector) 
            => new SlowDeferred.DualSelectWrapper<T, A, R>(source, secondSelector(default), (cellL, cellR) => resultSelector(cellL[0, 0], cellR));
        public static IQueryableArray2d<R> SelectMany<T, A, R>(this IRelQueryableArray2d<T> source, Func<ICell<T>, IQueryableArray2d<A>> secondSelector, Func<ICell<T>, A, R> resultSelector) 
            => new SlowDeferred.DualSelectWrapper<T, A, R>(source, secondSelector(null), (cellL, cellR) => resultSelector(cellL, cellR[0, 0]));
        public static IQueryableArray2d<R> SelectMany<T, A, R>(this IRelQueryableArray2d<T> source, Func<ICell<T>, IRelQueryableArray2d<A>> secondSelector, Func<ICell<T>, ICell<A>, R> resultSelector) 
            => new SlowDeferred.DualSelectWrapper<T, A, R>(source, secondSelector(null), resultSelector);
        public static IArray2d<R> SelectMany<T, R>(this IRelQueryableArray2d<T> source, Func<ICell<T>, ArrayRecurrence<R>> secondSelector, Func<ICell<T>, ICell<R>, R> resultSelector) 
            => new SlowDeferred.RecurrentSelectWrapper<T, R>(source, resultSelector);
        public static IArray2d<R> SelectMany<T, R>(this IQueryableArray2d<T> source, Func<T, ArrayRecurrence<R>> secondSelector, Func<T, ICell<R>, R> resultSelector)
            => new SlowDeferred.RecurrentSelectWrapper<T, R>(source, (cell1, cell2) => resultSelector(cell1[0, 0], cell2));

    }


    public struct SpecialArray<T>
    {
        internal T[,] source;

        public SpecialArray(T[,] source) => this.source = source ?? throw new ArgumentNullException(nameof(source));
    }
}
using ClrTest.Reflection;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Processing2d.Fast
{
    public delegate IArray2d<R> Filter<T, R>(IArray2d<T> data);

    public static class InliningArray2D
    {
        private static class Cache<T, R>
        {
            private static Dictionary<Kernel<T, R>, Filter<T, R>>
                _filters = new Dictionary<Kernel<T, R>, Filter<T, R>>();

            public static Filter<T, R> GetFilter(Kernel<T, R> kernel, Func<Kernel<T, R>, Filter<T, R>> filterGen)
            {
                if (!_filters.TryGetValue(kernel, out Filter<T, R> filter))
                {
                    filter = filterGen(kernel);
                    _filters[kernel] = filter;
                };
                return filter;
            }
        }

        public static Filter<T, R> GenerateFilter<T, R>(Kernel<T, R> kernel)
        {
            var km = KernelMeasure.Measure(kernel);

            DynamicMethod dm = new DynamicMethod("Filter" + kernel.Method.Name, typeof(IArray2d<R>), new[] {typeof(object), typeof(IArray2d<T>) });

            var ilg = dm.GetILGenerator();

            var h_var = ilg.DeclareLocal(typeof(int));
            var w_var = ilg.DeclareLocal(typeof(int));
            var result_var = ilg.DeclareLocal(typeof(R[,]));
            var i_var = ilg.DeclareLocal(typeof(int));
            var j_var = ilg.DeclareLocal(typeof(int));


            ilg.Emit(OpCodes.Ldarg_1); // source
            ilg.Emit(OpCodes.Ldc_I4_0);// 0
            MethodInfo getLength = typeof(IArray2d).GetMethod(nameof(IArray2d.GetLength), new []{ typeof(int) });
            ilg.Emit(OpCodes.Callvirt, getLength);
            ilg.Emit(OpCodes.Stloc, h_var);


            ilg.Emit(OpCodes.Ldarg_1); // source
            ilg.Emit(OpCodes.Ldc_I4_1);// 0
            ilg.Emit(OpCodes.Callvirt, getLength);
            ilg.Emit(OpCodes.Stloc, w_var);

            ilg.Emit(OpCodes.Ldloc, h_var);
            ilg.Emit(OpCodes.Ldloc, w_var);
            ilg.Emit(OpCodes.Newobj, typeof(R[,]).GetConstructor(new[]{ typeof(int), typeof(int) }));
            ilg.Emit(OpCodes.Stloc, result_var);
            
            ilg.EmitIncrease(h_var, -km.ymax);
            ilg.EmitIncrease(w_var, -km.xmax);

            ilg.EmitFor(i_var, 0 - km.xmin, h_var, () =>
              {
                  ilg.EmitFor(j_var, 0 - km.ymin, w_var, () =>
                  {
                      ilg.Emit(OpCodes.Ldloc, result_var);
                      ilg.Emit(OpCodes.Ldloc, i_var);
                      ilg.Emit(OpCodes.Ldloc, j_var);
                      // call the kernel - begin
                      var ki = new SafeInliningILInstructionVisitor<T>(ilg, i_var, j_var);
                      new ILReader(kernel.Method).Accept(ki);
                      // call the kernel - end

                      ilg.Emit(OpCodes.Call, typeof(R[,]).GetMethod("Set", new [] { typeof(int), typeof(int), typeof(R)}));
                  });
              });
              
            ilg.Emit(OpCodes.Ldloc, result_var);
            ilg.Emit(OpCodes.Newobj, typeof(ArrayWrapper<R>).GetConstructor(new[] { typeof(R[,]) }));
            ilg.Emit(OpCodes.Box, typeof(ArrayWrapper<R>));
            ilg.Emit(OpCodes.Ret);

            var inlined = (Func<object, IArray2d<T>, IArray2d<R>>)dm.CreateDelegate(typeof(Func<object, IArray2d<T>, IArray2d<R>>));
            //Print(inlined.Method);
            return data => inlined(kernel.Target, data);
        }

        public static IArray2d<R> Select<T, R>(this T[,] source, Func<T, R> kernel)
            => GenerateFilter<T,R>((cell) => kernel(cell[0, 0]))(new ArrayWrapper<T>(source));
        public static IArray2d<R> Select<T, R>(this IRelQueryableArray2d<T> source, Kernel<T, R> kernel) 
            => GenerateFilter(kernel)(source);



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

}
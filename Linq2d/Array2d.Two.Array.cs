using System;
using System.Linq.Expressions;

namespace Linq2d
{
	public static partial class Array2d
	{

		// final select of 1 result
        public static IArrayQuery<T1, T2, R> SelectMany<T1, T2, R>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, R>> resultSelector) 
            => new ArrayQuery<T1, T2, R>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);

        public static IArrayQuery<T1, T2, R> SelectMany<T1, T2, R>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, R>> resultSelector) 
            => new ArrayQuery<T1, T2, R>(source.Wrap(), secondSelector(default), resultSelector);

		// final select of 2 results
        public static IArrayTransform2<T1, T2, R1, R2> SelectMany<T1, T2, R1, R2>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, R1, R2>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);

        public static IArrayTransform2<T1, T2, R1, R2> SelectMany<T1, T2, R1, R2>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, R1, R2>(source.Wrap(), secondSelector(default), resultSelector);

		// final select of 3 results
        public static IArrayTransform3<T1, T2, R1, R2, R3> SelectMany<T1, T2, R1, R2, R3>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, R1, R2, R3>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);

        public static IArrayTransform3<T1, T2, R1, R2, R3> SelectMany<T1, T2, R1, R2, R3>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, R1, R2, R3>(source.Wrap(), secondSelector(default), resultSelector);

	}

}

using System;
using System.Linq.Expressions;

namespace Linq2d
{
	public static partial class Array2d
	{
        // recurrent select of a single result
        public static IArrayQueryRecurrent<T, A, R> SelectMany<T, A, R>(this T[,] source, Func<object, Result<R>> recurrentSelector, Expression<Func<Cell<T>, RelativeCell<R>, A>> resultSelector)
        where A: class
            => new ArrayQueryRecurrent<T, A, R>(source.Wrap(), recurrentSelector(default).InitValue, resultSelector);

		// final select of 1 result
        public static IArrayQuery<T, R> Select<T, R>(this T[,] source, Expression<Func<Cell<T>, R>> resultSelector) 
            => new ArrayQuery<T, R>(source.Wrap(), resultSelector);


        // final-recurrent select of 1 results
        public static IArrayTransform<T, R> SelectMany<T, R>(this T[,] source, Func<object, Result<R>> recurrentResult1Selector, Expression<Func<Cell<T>, RelativeCell<R>, R>> resultSelector) 
            => new ArrayQuery<T, R>(source.Wrap(), recurrentResult1Selector(default).InitValue, resultSelector);

		// final select of 2 result
        public static IArrayTransform2<T, R1, R2> Select<T, R1, R2>(this T[,] source, Expression<Func<Cell<T>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T, R1, R2>(source.Wrap(), resultSelector);


        // final-recurrent select of 2 results
        public static IArrayTransform2<T, R1, R2> SelectMany<T, R1, R2>(this T[,] source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T, R1, R2>(source.Wrap(), recurrentResult1Selector(default).InitValue, resultSelector);

		// final select of 3 result
        public static IArrayTransform3<T, R1, R2, R3> Select<T, R1, R2, R3>(this T[,] source, Expression<Func<Cell<T>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T, R1, R2, R3>(source.Wrap(), resultSelector);


        // final-recurrent select of 3 results
        public static IArrayTransform3<T, R1, R2, R3> SelectMany<T, R1, R2, R3>(this T[,] source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T, R1, R2, R3>(source.Wrap(), recurrentResult1Selector(default).InitValue, resultSelector);

	}

}

using System;
using System.Linq.Expressions;

namespace Linq2d
{
    public static partial class Array2d
    {
        #region 1 source
        // intermediate select (variable definition) for the 1 source with 1 results from a recurrent source
        public static IArrayQueryRecurrent<T, A, R> Select<T, _, A, R>(this IArrayQueryRecurrent<T, _, R> source, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent<T, A, R>(source, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 1 source that have captured 1 result
        public static IArrayQueryRecurrent2<T, A, R1, R2> SelectMany<T, _, A, R1, R2>(this IArrayQueryRecurrent<T, _, R1> source, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent2<T, A, R1, R2>(source, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 1 result from 1 recurrent source that have captured 1 result
        public static IArrayTransform<T, R> Select<T, _, R>(this IArrayQueryRecurrent<T, _, R> source, Expression<Func<_, R>> resultSelector)
              => new ArrayQuery<T, R>(source, resultSelector);


		// final select of 2 results from 1 recurrent source that have captured 1 result
        public static IArrayTransform2<T, R1, R2> Select<T, _, R1, R2>(this IArrayQueryRecurrent<T, _, R1> source, Expression<Func<_, (R1, R2)>> resultSelector)
              => new ArrayQuery2<T, R1, R2>(source, resultSelector);

        // final-recurrent select of 2 results from 1 recurrent source that have captured 1 result
        public static IArrayTransform2<T, R1, R2> SelectMany<T, _, R1, R2>(this IArrayQueryRecurrent<T, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T, R1, R2>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 3 results from 1 recurrent source that have captured 1 result
        public static IArrayTransform3<T, R1, R2, R3> Select<T, _, R1, R2, R3>(this IArrayQueryRecurrent<T, _, R1> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T, R1, R2, R3>(source, resultSelector);

        // final-recurrent select of 3 results from 1 recurrent source that have captured 1 result
        public static IArrayTransform3<T, R1, R2, R3> SelectMany<T, _, R1, R2, R3>(this IArrayQueryRecurrent<T, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T, R1, R2, R3>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 4 results from 1 recurrent source that have captured 1 result
        public static IArrayTransform4<T, R1, R2, R3, R4> Select<T, _, R1, R2, R3, R4>(this IArrayQueryRecurrent<T, _, R1> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T, R1, R2, R3, R4>(source, resultSelector);

        // final-recurrent select of 4 results from 1 recurrent source that have captured 1 result
        public static IArrayTransform4<T, R1, R2, R3, R4> SelectMany<T, _, R1, R2, R3, R4>(this IArrayQueryRecurrent<T, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T, R1, R2, R3, R4>(sources, recurrentResult2Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 1 source with 2 results from a recurrent source
        public static IArrayQueryRecurrent2<T, A, R1, R2> Select<T, _, A, R1, R2>(this IArrayQueryRecurrent2<T, _, R1, R2> source, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent2<T, A, R1, R2>(source, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 1 source that have captured 2 results
        public static IArrayQueryRecurrent3<T, A, R1, R2, R3> SelectMany<T, _, A, R1, R2, R3>(this IArrayQueryRecurrent2<T, _, R1, R2> source, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent3<T, A, R1, R2, R3>(source, recurrentResult3Selector(default).InitValue, resultSelector);

		// final select of 2 results from 1 recurrent source that have captured 2 results
        public static IArrayTransform2<T, R1, R2> Select<T, _, R1, R2>(this IArrayQueryRecurrent2<T, _, R1, R2> source, Expression<Func<_, (R1, R2)>> resultSelector)
              => new ArrayQuery2<T, R1, R2>(source, resultSelector);


		// final select of 3 results from 1 recurrent source that have captured 2 results
        public static IArrayTransform3<T, R1, R2, R3> Select<T, _, R1, R2, R3>(this IArrayQueryRecurrent2<T, _, R1, R2> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T, R1, R2, R3>(source, resultSelector);

        // final-recurrent select of 3 results from 1 recurrent source that have captured 2 results
        public static IArrayTransform3<T, R1, R2, R3> SelectMany<T, _, R1, R2, R3>(this IArrayQueryRecurrent2<T, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T, R1, R2, R3>(sources, recurrentResult3Selector(default).InitValue, resultSelector);

		// final select of 4 results from 1 recurrent source that have captured 2 results
        public static IArrayTransform4<T, R1, R2, R3, R4> Select<T, _, R1, R2, R3, R4>(this IArrayQueryRecurrent2<T, _, R1, R2> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T, R1, R2, R3, R4>(source, resultSelector);

        // final-recurrent select of 4 results from 1 recurrent source that have captured 2 results
        public static IArrayTransform4<T, R1, R2, R3, R4> SelectMany<T, _, R1, R2, R3, R4>(this IArrayQueryRecurrent2<T, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T, R1, R2, R3, R4>(sources, recurrentResult3Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 1 source with 3 results from a recurrent source
        public static IArrayQueryRecurrent3<T, A, R1, R2, R3> Select<T, _, A, R1, R2, R3>(this IArrayQueryRecurrent3<T, _, R1, R2, R3> source, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent3<T, A, R1, R2, R3>(source, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 1 source that have captured 3 results
        public static IArrayQueryRecurrent4<T, A, R1, R2, R3, R4> SelectMany<T, _, A, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T, _, R1, R2, R3> source, Func<object, Result<R4>> recurrentResult4Selector, Expression<Func<_, RelativeCell<R4>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent4<T, A, R1, R2, R3, R4>(source, recurrentResult4Selector(default).InitValue, resultSelector);

		// final select of 3 results from 1 recurrent source that have captured 3 results
        public static IArrayTransform3<T, R1, R2, R3> Select<T, _, R1, R2, R3>(this IArrayQueryRecurrent3<T, _, R1, R2, R3> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T, R1, R2, R3>(source, resultSelector);


		// final select of 4 results from 1 recurrent source that have captured 3 results
        public static IArrayTransform4<T, R1, R2, R3, R4> Select<T, _, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T, _, R1, R2, R3> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T, R1, R2, R3, R4>(source, resultSelector);

        // final-recurrent select of 4 results from 1 recurrent source that have captured 3 results
        public static IArrayTransform4<T, R1, R2, R3, R4> SelectMany<T, _, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T, _, R1, R2, R3> sources, Func<object, Result<R4>> recurrentResult4Selector, Expression<Func<_, RelativeCell<R4>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T, R1, R2, R3, R4>(sources, recurrentResult4Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 1 source with 4 results from a recurrent source
        public static IArrayQueryRecurrent4<T, A, R1, R2, R3, R4> Select<T, _, A, R1, R2, R3, R4>(this IArrayQueryRecurrent4<T, _, R1, R2, R3, R4> source, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent4<T, A, R1, R2, R3, R4>(source, resultSelector); 


		// final select of 4 results from 1 recurrent source that have captured 4 results
        public static IArrayTransform4<T, R1, R2, R3, R4> Select<T, _, R1, R2, R3, R4>(this IArrayQueryRecurrent4<T, _, R1, R2, R3, R4> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T, R1, R2, R3, R4>(source, resultSelector);

        #endregion

        #region 2 sources
        // intermediate select (variable definition) for the 2 sources with 1 results from a recurrent source
        public static IArrayQueryRecurrent<T1, T2, A, R> Select<T1, T2, _, A, R>(this IArrayQueryRecurrent<T1, T2, _, R> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent<T1, T2, A, R>(sources, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 2 sources that have captured 1 result
        public static IArrayQueryRecurrent2<T1, T2, A, R1, R2> SelectMany<T1, T2, _, A, R1, R2>(this IArrayQueryRecurrent<T1, T2, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent2<T1, T2, A, R1, R2>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 1 result from 2 recurrent sources that have captured 1 result
        public static IArrayTransform<T1, T2, R> Select<T1, T2, _, R>(this IArrayQueryRecurrent<T1, T2, _, R> sources, Expression<Func<_, R>> resultSelector)
              => new ArrayQuery<T1, T2, R>(sources, resultSelector);


		// final select of 2 results from 2 recurrent sources that have captured 1 result
        public static IArrayTransform2<T1, T2, R1, R2> Select<T1, T2, _, R1, R2>(this IArrayQueryRecurrent<T1, T2, _, R1> sources, Expression<Func<_, (R1, R2)>> resultSelector)
              => new ArrayQuery2<T1, T2, R1, R2>(sources, resultSelector);

        // final-recurrent select of 2 results from 2 recurrent sources that have captured 1 result
        public static IArrayTransform2<T1, T2, R1, R2> SelectMany<T1, T2, _, R1, R2>(this IArrayQueryRecurrent<T1, T2, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, R1, R2>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 3 results from 2 recurrent sources that have captured 1 result
        public static IArrayTransform3<T1, T2, R1, R2, R3> Select<T1, T2, _, R1, R2, R3>(this IArrayQueryRecurrent<T1, T2, _, R1> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T1, T2, R1, R2, R3>(sources, resultSelector);

        // final-recurrent select of 3 results from 2 recurrent sources that have captured 1 result
        public static IArrayTransform3<T1, T2, R1, R2, R3> SelectMany<T1, T2, _, R1, R2, R3>(this IArrayQueryRecurrent<T1, T2, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, R1, R2, R3>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 4 results from 2 recurrent sources that have captured 1 result
        public static IArrayTransform4<T1, T2, R1, R2, R3, R4> Select<T1, T2, _, R1, R2, R3, R4>(this IArrayQueryRecurrent<T1, T2, _, R1> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 2 recurrent sources that have captured 1 result
        public static IArrayTransform4<T1, T2, R1, R2, R3, R4> SelectMany<T1, T2, _, R1, R2, R3, R4>(this IArrayQueryRecurrent<T1, T2, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, R1, R2, R3, R4>(sources, recurrentResult2Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 2 sources with 2 results from a recurrent source
        public static IArrayQueryRecurrent2<T1, T2, A, R1, R2> Select<T1, T2, _, A, R1, R2>(this IArrayQueryRecurrent2<T1, T2, _, R1, R2> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent2<T1, T2, A, R1, R2>(sources, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 2 sources that have captured 2 results
        public static IArrayQueryRecurrent3<T1, T2, A, R1, R2, R3> SelectMany<T1, T2, _, A, R1, R2, R3>(this IArrayQueryRecurrent2<T1, T2, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent3<T1, T2, A, R1, R2, R3>(sources, recurrentResult3Selector(default).InitValue, resultSelector);

		// final select of 2 results from 2 recurrent sources that have captured 2 results
        public static IArrayTransform2<T1, T2, R1, R2> Select<T1, T2, _, R1, R2>(this IArrayQueryRecurrent2<T1, T2, _, R1, R2> sources, Expression<Func<_, (R1, R2)>> resultSelector)
              => new ArrayQuery2<T1, T2, R1, R2>(sources, resultSelector);


		// final select of 3 results from 2 recurrent sources that have captured 2 results
        public static IArrayTransform3<T1, T2, R1, R2, R3> Select<T1, T2, _, R1, R2, R3>(this IArrayQueryRecurrent2<T1, T2, _, R1, R2> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T1, T2, R1, R2, R3>(sources, resultSelector);

        // final-recurrent select of 3 results from 2 recurrent sources that have captured 2 results
        public static IArrayTransform3<T1, T2, R1, R2, R3> SelectMany<T1, T2, _, R1, R2, R3>(this IArrayQueryRecurrent2<T1, T2, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, R1, R2, R3>(sources, recurrentResult3Selector(default).InitValue, resultSelector);

		// final select of 4 results from 2 recurrent sources that have captured 2 results
        public static IArrayTransform4<T1, T2, R1, R2, R3, R4> Select<T1, T2, _, R1, R2, R3, R4>(this IArrayQueryRecurrent2<T1, T2, _, R1, R2> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 2 recurrent sources that have captured 2 results
        public static IArrayTransform4<T1, T2, R1, R2, R3, R4> SelectMany<T1, T2, _, R1, R2, R3, R4>(this IArrayQueryRecurrent2<T1, T2, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, R1, R2, R3, R4>(sources, recurrentResult3Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 2 sources with 3 results from a recurrent source
        public static IArrayQueryRecurrent3<T1, T2, A, R1, R2, R3> Select<T1, T2, _, A, R1, R2, R3>(this IArrayQueryRecurrent3<T1, T2, _, R1, R2, R3> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent3<T1, T2, A, R1, R2, R3>(sources, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 2 sources that have captured 3 results
        public static IArrayQueryRecurrent4<T1, T2, A, R1, R2, R3, R4> SelectMany<T1, T2, _, A, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T1, T2, _, R1, R2, R3> sources, Func<object, Result<R4>> recurrentResult4Selector, Expression<Func<_, RelativeCell<R4>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent4<T1, T2, A, R1, R2, R3, R4>(sources, recurrentResult4Selector(default).InitValue, resultSelector);

		// final select of 3 results from 2 recurrent sources that have captured 3 results
        public static IArrayTransform3<T1, T2, R1, R2, R3> Select<T1, T2, _, R1, R2, R3>(this IArrayQueryRecurrent3<T1, T2, _, R1, R2, R3> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T1, T2, R1, R2, R3>(sources, resultSelector);


		// final select of 4 results from 2 recurrent sources that have captured 3 results
        public static IArrayTransform4<T1, T2, R1, R2, R3, R4> Select<T1, T2, _, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T1, T2, _, R1, R2, R3> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 2 recurrent sources that have captured 3 results
        public static IArrayTransform4<T1, T2, R1, R2, R3, R4> SelectMany<T1, T2, _, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T1, T2, _, R1, R2, R3> sources, Func<object, Result<R4>> recurrentResult4Selector, Expression<Func<_, RelativeCell<R4>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, R1, R2, R3, R4>(sources, recurrentResult4Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 2 sources with 4 results from a recurrent source
        public static IArrayQueryRecurrent4<T1, T2, A, R1, R2, R3, R4> Select<T1, T2, _, A, R1, R2, R3, R4>(this IArrayQueryRecurrent4<T1, T2, _, R1, R2, R3, R4> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent4<T1, T2, A, R1, R2, R3, R4>(sources, resultSelector); 


		// final select of 4 results from 2 recurrent sources that have captured 4 results
        public static IArrayTransform4<T1, T2, R1, R2, R3, R4> Select<T1, T2, _, R1, R2, R3, R4>(this IArrayQueryRecurrent4<T1, T2, _, R1, R2, R3, R4> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, R1, R2, R3, R4>(sources, resultSelector);

        #endregion

        #region 3 sources
        // intermediate select (variable definition) for the 3 sources with 1 results from a recurrent source
        public static IArrayQueryRecurrent<T1, T2, T3, A, R> Select<T1, T2, T3, _, A, R>(this IArrayQueryRecurrent<T1, T2, T3, _, R> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent<T1, T2, T3, A, R>(sources, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 3 sources that have captured 1 result
        public static IArrayQueryRecurrent2<T1, T2, T3, A, R1, R2> SelectMany<T1, T2, T3, _, A, R1, R2>(this IArrayQueryRecurrent<T1, T2, T3, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent2<T1, T2, T3, A, R1, R2>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 1 result from 3 recurrent sources that have captured 1 result
        public static IArrayTransform<T1, T2, T3, R> Select<T1, T2, T3, _, R>(this IArrayQueryRecurrent<T1, T2, T3, _, R> sources, Expression<Func<_, R>> resultSelector)
              => new ArrayQuery<T1, T2, T3, R>(sources, resultSelector);


		// final select of 2 results from 3 recurrent sources that have captured 1 result
        public static IArrayTransform2<T1, T2, T3, R1, R2> Select<T1, T2, T3, _, R1, R2>(this IArrayQueryRecurrent<T1, T2, T3, _, R1> sources, Expression<Func<_, (R1, R2)>> resultSelector)
              => new ArrayQuery2<T1, T2, T3, R1, R2>(sources, resultSelector);

        // final-recurrent select of 2 results from 3 recurrent sources that have captured 1 result
        public static IArrayTransform2<T1, T2, T3, R1, R2> SelectMany<T1, T2, T3, _, R1, R2>(this IArrayQueryRecurrent<T1, T2, T3, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, R1, R2>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 3 results from 3 recurrent sources that have captured 1 result
        public static IArrayTransform3<T1, T2, T3, R1, R2, R3> Select<T1, T2, T3, _, R1, R2, R3>(this IArrayQueryRecurrent<T1, T2, T3, _, R1> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(sources, resultSelector);

        // final-recurrent select of 3 results from 3 recurrent sources that have captured 1 result
        public static IArrayTransform3<T1, T2, T3, R1, R2, R3> SelectMany<T1, T2, T3, _, R1, R2, R3>(this IArrayQueryRecurrent<T1, T2, T3, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 4 results from 3 recurrent sources that have captured 1 result
        public static IArrayTransform4<T1, T2, T3, R1, R2, R3, R4> Select<T1, T2, T3, _, R1, R2, R3, R4>(this IArrayQueryRecurrent<T1, T2, T3, _, R1> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 3 recurrent sources that have captured 1 result
        public static IArrayTransform4<T1, T2, T3, R1, R2, R3, R4> SelectMany<T1, T2, T3, _, R1, R2, R3, R4>(this IArrayQueryRecurrent<T1, T2, T3, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(sources, recurrentResult2Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 3 sources with 2 results from a recurrent source
        public static IArrayQueryRecurrent2<T1, T2, T3, A, R1, R2> Select<T1, T2, T3, _, A, R1, R2>(this IArrayQueryRecurrent2<T1, T2, T3, _, R1, R2> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent2<T1, T2, T3, A, R1, R2>(sources, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 3 sources that have captured 2 results
        public static IArrayQueryRecurrent3<T1, T2, T3, A, R1, R2, R3> SelectMany<T1, T2, T3, _, A, R1, R2, R3>(this IArrayQueryRecurrent2<T1, T2, T3, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent3<T1, T2, T3, A, R1, R2, R3>(sources, recurrentResult3Selector(default).InitValue, resultSelector);

		// final select of 2 results from 3 recurrent sources that have captured 2 results
        public static IArrayTransform2<T1, T2, T3, R1, R2> Select<T1, T2, T3, _, R1, R2>(this IArrayQueryRecurrent2<T1, T2, T3, _, R1, R2> sources, Expression<Func<_, (R1, R2)>> resultSelector)
              => new ArrayQuery2<T1, T2, T3, R1, R2>(sources, resultSelector);


		// final select of 3 results from 3 recurrent sources that have captured 2 results
        public static IArrayTransform3<T1, T2, T3, R1, R2, R3> Select<T1, T2, T3, _, R1, R2, R3>(this IArrayQueryRecurrent2<T1, T2, T3, _, R1, R2> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(sources, resultSelector);

        // final-recurrent select of 3 results from 3 recurrent sources that have captured 2 results
        public static IArrayTransform3<T1, T2, T3, R1, R2, R3> SelectMany<T1, T2, T3, _, R1, R2, R3>(this IArrayQueryRecurrent2<T1, T2, T3, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(sources, recurrentResult3Selector(default).InitValue, resultSelector);

		// final select of 4 results from 3 recurrent sources that have captured 2 results
        public static IArrayTransform4<T1, T2, T3, R1, R2, R3, R4> Select<T1, T2, T3, _, R1, R2, R3, R4>(this IArrayQueryRecurrent2<T1, T2, T3, _, R1, R2> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 3 recurrent sources that have captured 2 results
        public static IArrayTransform4<T1, T2, T3, R1, R2, R3, R4> SelectMany<T1, T2, T3, _, R1, R2, R3, R4>(this IArrayQueryRecurrent2<T1, T2, T3, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(sources, recurrentResult3Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 3 sources with 3 results from a recurrent source
        public static IArrayQueryRecurrent3<T1, T2, T3, A, R1, R2, R3> Select<T1, T2, T3, _, A, R1, R2, R3>(this IArrayQueryRecurrent3<T1, T2, T3, _, R1, R2, R3> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent3<T1, T2, T3, A, R1, R2, R3>(sources, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 3 sources that have captured 3 results
        public static IArrayQueryRecurrent4<T1, T2, T3, A, R1, R2, R3, R4> SelectMany<T1, T2, T3, _, A, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T1, T2, T3, _, R1, R2, R3> sources, Func<object, Result<R4>> recurrentResult4Selector, Expression<Func<_, RelativeCell<R4>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent4<T1, T2, T3, A, R1, R2, R3, R4>(sources, recurrentResult4Selector(default).InitValue, resultSelector);

		// final select of 3 results from 3 recurrent sources that have captured 3 results
        public static IArrayTransform3<T1, T2, T3, R1, R2, R3> Select<T1, T2, T3, _, R1, R2, R3>(this IArrayQueryRecurrent3<T1, T2, T3, _, R1, R2, R3> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(sources, resultSelector);


		// final select of 4 results from 3 recurrent sources that have captured 3 results
        public static IArrayTransform4<T1, T2, T3, R1, R2, R3, R4> Select<T1, T2, T3, _, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T1, T2, T3, _, R1, R2, R3> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 3 recurrent sources that have captured 3 results
        public static IArrayTransform4<T1, T2, T3, R1, R2, R3, R4> SelectMany<T1, T2, T3, _, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T1, T2, T3, _, R1, R2, R3> sources, Func<object, Result<R4>> recurrentResult4Selector, Expression<Func<_, RelativeCell<R4>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(sources, recurrentResult4Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 3 sources with 4 results from a recurrent source
        public static IArrayQueryRecurrent4<T1, T2, T3, A, R1, R2, R3, R4> Select<T1, T2, T3, _, A, R1, R2, R3, R4>(this IArrayQueryRecurrent4<T1, T2, T3, _, R1, R2, R3, R4> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent4<T1, T2, T3, A, R1, R2, R3, R4>(sources, resultSelector); 


		// final select of 4 results from 3 recurrent sources that have captured 4 results
        public static IArrayTransform4<T1, T2, T3, R1, R2, R3, R4> Select<T1, T2, T3, _, R1, R2, R3, R4>(this IArrayQueryRecurrent4<T1, T2, T3, _, R1, R2, R3, R4> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(sources, resultSelector);

        #endregion

        #region 4 sources
        // intermediate select (variable definition) for the 4 sources with 1 results from a recurrent source
        public static IArrayQueryRecurrent<T1, T2, T3, T4, A, R> Select<T1, T2, T3, T4, _, A, R>(this IArrayQueryRecurrent<T1, T2, T3, T4, _, R> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent<T1, T2, T3, T4, A, R>(sources, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 4 sources that have captured 1 result
        public static IArrayQueryRecurrent2<T1, T2, T3, T4, A, R1, R2> SelectMany<T1, T2, T3, T4, _, A, R1, R2>(this IArrayQueryRecurrent<T1, T2, T3, T4, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent2<T1, T2, T3, T4, A, R1, R2>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 1 result from 4 recurrent sources that have captured 1 result
        public static IArrayTransform<T1, T2, T3, T4, R> Select<T1, T2, T3, T4, _, R>(this IArrayQueryRecurrent<T1, T2, T3, T4, _, R> sources, Expression<Func<_, R>> resultSelector)
              => new ArrayQuery<T1, T2, T3, T4, R>(sources, resultSelector);


		// final select of 2 results from 4 recurrent sources that have captured 1 result
        public static IArrayTransform2<T1, T2, T3, T4, R1, R2> Select<T1, T2, T3, T4, _, R1, R2>(this IArrayQueryRecurrent<T1, T2, T3, T4, _, R1> sources, Expression<Func<_, (R1, R2)>> resultSelector)
              => new ArrayQuery2<T1, T2, T3, T4, R1, R2>(sources, resultSelector);

        // final-recurrent select of 2 results from 4 recurrent sources that have captured 1 result
        public static IArrayTransform2<T1, T2, T3, T4, R1, R2> SelectMany<T1, T2, T3, T4, _, R1, R2>(this IArrayQueryRecurrent<T1, T2, T3, T4, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, R1, R2>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 3 results from 4 recurrent sources that have captured 1 result
        public static IArrayTransform3<T1, T2, T3, T4, R1, R2, R3> Select<T1, T2, T3, T4, _, R1, R2, R3>(this IArrayQueryRecurrent<T1, T2, T3, T4, _, R1> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(sources, resultSelector);

        // final-recurrent select of 3 results from 4 recurrent sources that have captured 1 result
        public static IArrayTransform3<T1, T2, T3, T4, R1, R2, R3> SelectMany<T1, T2, T3, T4, _, R1, R2, R3>(this IArrayQueryRecurrent<T1, T2, T3, T4, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 4 results from 4 recurrent sources that have captured 1 result
        public static IArrayTransform4<T1, T2, T3, T4, R1, R2, R3, R4> Select<T1, T2, T3, T4, _, R1, R2, R3, R4>(this IArrayQueryRecurrent<T1, T2, T3, T4, _, R1> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 4 recurrent sources that have captured 1 result
        public static IArrayTransform4<T1, T2, T3, T4, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, _, R1, R2, R3, R4>(this IArrayQueryRecurrent<T1, T2, T3, T4, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(sources, recurrentResult2Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 4 sources with 2 results from a recurrent source
        public static IArrayQueryRecurrent2<T1, T2, T3, T4, A, R1, R2> Select<T1, T2, T3, T4, _, A, R1, R2>(this IArrayQueryRecurrent2<T1, T2, T3, T4, _, R1, R2> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent2<T1, T2, T3, T4, A, R1, R2>(sources, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 4 sources that have captured 2 results
        public static IArrayQueryRecurrent3<T1, T2, T3, T4, A, R1, R2, R3> SelectMany<T1, T2, T3, T4, _, A, R1, R2, R3>(this IArrayQueryRecurrent2<T1, T2, T3, T4, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent3<T1, T2, T3, T4, A, R1, R2, R3>(sources, recurrentResult3Selector(default).InitValue, resultSelector);

		// final select of 2 results from 4 recurrent sources that have captured 2 results
        public static IArrayTransform2<T1, T2, T3, T4, R1, R2> Select<T1, T2, T3, T4, _, R1, R2>(this IArrayQueryRecurrent2<T1, T2, T3, T4, _, R1, R2> sources, Expression<Func<_, (R1, R2)>> resultSelector)
              => new ArrayQuery2<T1, T2, T3, T4, R1, R2>(sources, resultSelector);


		// final select of 3 results from 4 recurrent sources that have captured 2 results
        public static IArrayTransform3<T1, T2, T3, T4, R1, R2, R3> Select<T1, T2, T3, T4, _, R1, R2, R3>(this IArrayQueryRecurrent2<T1, T2, T3, T4, _, R1, R2> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(sources, resultSelector);

        // final-recurrent select of 3 results from 4 recurrent sources that have captured 2 results
        public static IArrayTransform3<T1, T2, T3, T4, R1, R2, R3> SelectMany<T1, T2, T3, T4, _, R1, R2, R3>(this IArrayQueryRecurrent2<T1, T2, T3, T4, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(sources, recurrentResult3Selector(default).InitValue, resultSelector);

		// final select of 4 results from 4 recurrent sources that have captured 2 results
        public static IArrayTransform4<T1, T2, T3, T4, R1, R2, R3, R4> Select<T1, T2, T3, T4, _, R1, R2, R3, R4>(this IArrayQueryRecurrent2<T1, T2, T3, T4, _, R1, R2> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 4 recurrent sources that have captured 2 results
        public static IArrayTransform4<T1, T2, T3, T4, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, _, R1, R2, R3, R4>(this IArrayQueryRecurrent2<T1, T2, T3, T4, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(sources, recurrentResult3Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 4 sources with 3 results from a recurrent source
        public static IArrayQueryRecurrent3<T1, T2, T3, T4, A, R1, R2, R3> Select<T1, T2, T3, T4, _, A, R1, R2, R3>(this IArrayQueryRecurrent3<T1, T2, T3, T4, _, R1, R2, R3> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent3<T1, T2, T3, T4, A, R1, R2, R3>(sources, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 4 sources that have captured 3 results
        public static IArrayQueryRecurrent4<T1, T2, T3, T4, A, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, _, A, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T1, T2, T3, T4, _, R1, R2, R3> sources, Func<object, Result<R4>> recurrentResult4Selector, Expression<Func<_, RelativeCell<R4>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent4<T1, T2, T3, T4, A, R1, R2, R3, R4>(sources, recurrentResult4Selector(default).InitValue, resultSelector);

		// final select of 3 results from 4 recurrent sources that have captured 3 results
        public static IArrayTransform3<T1, T2, T3, T4, R1, R2, R3> Select<T1, T2, T3, T4, _, R1, R2, R3>(this IArrayQueryRecurrent3<T1, T2, T3, T4, _, R1, R2, R3> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(sources, resultSelector);


		// final select of 4 results from 4 recurrent sources that have captured 3 results
        public static IArrayTransform4<T1, T2, T3, T4, R1, R2, R3, R4> Select<T1, T2, T3, T4, _, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T1, T2, T3, T4, _, R1, R2, R3> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 4 recurrent sources that have captured 3 results
        public static IArrayTransform4<T1, T2, T3, T4, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, _, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T1, T2, T3, T4, _, R1, R2, R3> sources, Func<object, Result<R4>> recurrentResult4Selector, Expression<Func<_, RelativeCell<R4>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(sources, recurrentResult4Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 4 sources with 4 results from a recurrent source
        public static IArrayQueryRecurrent4<T1, T2, T3, T4, A, R1, R2, R3, R4> Select<T1, T2, T3, T4, _, A, R1, R2, R3, R4>(this IArrayQueryRecurrent4<T1, T2, T3, T4, _, R1, R2, R3, R4> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent4<T1, T2, T3, T4, A, R1, R2, R3, R4>(sources, resultSelector); 


		// final select of 4 results from 4 recurrent sources that have captured 4 results
        public static IArrayTransform4<T1, T2, T3, T4, R1, R2, R3, R4> Select<T1, T2, T3, T4, _, R1, R2, R3, R4>(this IArrayQueryRecurrent4<T1, T2, T3, T4, _, R1, R2, R3, R4> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(sources, resultSelector);

        #endregion

        #region 5 sources
        // intermediate select (variable definition) for the 5 sources with 1 results from a recurrent source
        public static IArrayQueryRecurrent<T1, T2, T3, T4, T5, A, R> Select<T1, T2, T3, T4, T5, _, A, R>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, _, R> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent<T1, T2, T3, T4, T5, A, R>(sources, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 5 sources that have captured 1 result
        public static IArrayQueryRecurrent2<T1, T2, T3, T4, T5, A, R1, R2> SelectMany<T1, T2, T3, T4, T5, _, A, R1, R2>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent2<T1, T2, T3, T4, T5, A, R1, R2>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 1 result from 5 recurrent sources that have captured 1 result
        public static IArrayTransform<T1, T2, T3, T4, T5, R> Select<T1, T2, T3, T4, T5, _, R>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, _, R> sources, Expression<Func<_, R>> resultSelector)
              => new ArrayQuery<T1, T2, T3, T4, T5, R>(sources, resultSelector);


		// final select of 2 results from 5 recurrent sources that have captured 1 result
        public static IArrayTransform2<T1, T2, T3, T4, T5, R1, R2> Select<T1, T2, T3, T4, T5, _, R1, R2>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, _, R1> sources, Expression<Func<_, (R1, R2)>> resultSelector)
              => new ArrayQuery2<T1, T2, T3, T4, T5, R1, R2>(sources, resultSelector);

        // final-recurrent select of 2 results from 5 recurrent sources that have captured 1 result
        public static IArrayTransform2<T1, T2, T3, T4, T5, R1, R2> SelectMany<T1, T2, T3, T4, T5, _, R1, R2>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, R1, R2>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 3 results from 5 recurrent sources that have captured 1 result
        public static IArrayTransform3<T1, T2, T3, T4, T5, R1, R2, R3> Select<T1, T2, T3, T4, T5, _, R1, R2, R3>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, _, R1> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3>(sources, resultSelector);

        // final-recurrent select of 3 results from 5 recurrent sources that have captured 1 result
        public static IArrayTransform3<T1, T2, T3, T4, T5, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, _, R1, R2, R3>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 4 results from 5 recurrent sources that have captured 1 result
        public static IArrayTransform4<T1, T2, T3, T4, T5, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, _, R1, R2, R3, R4>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, _, R1> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 5 recurrent sources that have captured 1 result
        public static IArrayTransform4<T1, T2, T3, T4, T5, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, _, R1, R2, R3, R4>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>(sources, recurrentResult2Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 5 sources with 2 results from a recurrent source
        public static IArrayQueryRecurrent2<T1, T2, T3, T4, T5, A, R1, R2> Select<T1, T2, T3, T4, T5, _, A, R1, R2>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, _, R1, R2> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent2<T1, T2, T3, T4, T5, A, R1, R2>(sources, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 5 sources that have captured 2 results
        public static IArrayQueryRecurrent3<T1, T2, T3, T4, T5, A, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, _, A, R1, R2, R3>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent3<T1, T2, T3, T4, T5, A, R1, R2, R3>(sources, recurrentResult3Selector(default).InitValue, resultSelector);

		// final select of 2 results from 5 recurrent sources that have captured 2 results
        public static IArrayTransform2<T1, T2, T3, T4, T5, R1, R2> Select<T1, T2, T3, T4, T5, _, R1, R2>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, _, R1, R2> sources, Expression<Func<_, (R1, R2)>> resultSelector)
              => new ArrayQuery2<T1, T2, T3, T4, T5, R1, R2>(sources, resultSelector);


		// final select of 3 results from 5 recurrent sources that have captured 2 results
        public static IArrayTransform3<T1, T2, T3, T4, T5, R1, R2, R3> Select<T1, T2, T3, T4, T5, _, R1, R2, R3>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, _, R1, R2> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3>(sources, resultSelector);

        // final-recurrent select of 3 results from 5 recurrent sources that have captured 2 results
        public static IArrayTransform3<T1, T2, T3, T4, T5, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, _, R1, R2, R3>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3>(sources, recurrentResult3Selector(default).InitValue, resultSelector);

		// final select of 4 results from 5 recurrent sources that have captured 2 results
        public static IArrayTransform4<T1, T2, T3, T4, T5, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, _, R1, R2, R3, R4>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, _, R1, R2> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 5 recurrent sources that have captured 2 results
        public static IArrayTransform4<T1, T2, T3, T4, T5, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, _, R1, R2, R3, R4>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>(sources, recurrentResult3Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 5 sources with 3 results from a recurrent source
        public static IArrayQueryRecurrent3<T1, T2, T3, T4, T5, A, R1, R2, R3> Select<T1, T2, T3, T4, T5, _, A, R1, R2, R3>(this IArrayQueryRecurrent3<T1, T2, T3, T4, T5, _, R1, R2, R3> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent3<T1, T2, T3, T4, T5, A, R1, R2, R3>(sources, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 5 sources that have captured 3 results
        public static IArrayQueryRecurrent4<T1, T2, T3, T4, T5, A, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, _, A, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T1, T2, T3, T4, T5, _, R1, R2, R3> sources, Func<object, Result<R4>> recurrentResult4Selector, Expression<Func<_, RelativeCell<R4>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent4<T1, T2, T3, T4, T5, A, R1, R2, R3, R4>(sources, recurrentResult4Selector(default).InitValue, resultSelector);

		// final select of 3 results from 5 recurrent sources that have captured 3 results
        public static IArrayTransform3<T1, T2, T3, T4, T5, R1, R2, R3> Select<T1, T2, T3, T4, T5, _, R1, R2, R3>(this IArrayQueryRecurrent3<T1, T2, T3, T4, T5, _, R1, R2, R3> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3>(sources, resultSelector);


		// final select of 4 results from 5 recurrent sources that have captured 3 results
        public static IArrayTransform4<T1, T2, T3, T4, T5, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, _, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T1, T2, T3, T4, T5, _, R1, R2, R3> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 5 recurrent sources that have captured 3 results
        public static IArrayTransform4<T1, T2, T3, T4, T5, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, _, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T1, T2, T3, T4, T5, _, R1, R2, R3> sources, Func<object, Result<R4>> recurrentResult4Selector, Expression<Func<_, RelativeCell<R4>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>(sources, recurrentResult4Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 5 sources with 4 results from a recurrent source
        public static IArrayQueryRecurrent4<T1, T2, T3, T4, T5, A, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, _, A, R1, R2, R3, R4>(this IArrayQueryRecurrent4<T1, T2, T3, T4, T5, _, R1, R2, R3, R4> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent4<T1, T2, T3, T4, T5, A, R1, R2, R3, R4>(sources, resultSelector); 


		// final select of 4 results from 5 recurrent sources that have captured 4 results
        public static IArrayTransform4<T1, T2, T3, T4, T5, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, _, R1, R2, R3, R4>(this IArrayQueryRecurrent4<T1, T2, T3, T4, T5, _, R1, R2, R3, R4> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>(sources, resultSelector);

        #endregion

        #region 6 sources
        // intermediate select (variable definition) for the 6 sources with 1 results from a recurrent source
        public static IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, A, R> Select<T1, T2, T3, T4, T5, T6, _, A, R>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, _, R> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, A, R>(sources, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 6 sources that have captured 1 result
        public static IArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, A, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, _, A, R1, R2>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, A, R1, R2>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 1 result from 6 recurrent sources that have captured 1 result
        public static IArrayTransform<T1, T2, T3, T4, T5, T6, R> Select<T1, T2, T3, T4, T5, T6, _, R>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, _, R> sources, Expression<Func<_, R>> resultSelector)
              => new ArrayQuery<T1, T2, T3, T4, T5, T6, R>(sources, resultSelector);


		// final select of 2 results from 6 recurrent sources that have captured 1 result
        public static IArrayTransform2<T1, T2, T3, T4, T5, T6, R1, R2> Select<T1, T2, T3, T4, T5, T6, _, R1, R2>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, _, R1> sources, Expression<Func<_, (R1, R2)>> resultSelector)
              => new ArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2>(sources, resultSelector);

        // final-recurrent select of 2 results from 6 recurrent sources that have captured 1 result
        public static IArrayTransform2<T1, T2, T3, T4, T5, T6, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 3 results from 6 recurrent sources that have captured 1 result
        public static IArrayTransform3<T1, T2, T3, T4, T5, T6, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, _, R1, R2, R3>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, _, R1> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3>(sources, resultSelector);

        // final-recurrent select of 3 results from 6 recurrent sources that have captured 1 result
        public static IArrayTransform3<T1, T2, T3, T4, T5, T6, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2, R3>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 4 results from 6 recurrent sources that have captured 1 result
        public static IArrayTransform4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, _, R1, R2, R3, R4>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, _, R1> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 6 recurrent sources that have captured 1 result
        public static IArrayTransform4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2, R3, R4>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(sources, recurrentResult2Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 6 sources with 2 results from a recurrent source
        public static IArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, A, R1, R2> Select<T1, T2, T3, T4, T5, T6, _, A, R1, R2>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, _, R1, R2> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, A, R1, R2>(sources, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 6 sources that have captured 2 results
        public static IArrayQueryRecurrent3<T1, T2, T3, T4, T5, T6, A, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, _, A, R1, R2, R3>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent3<T1, T2, T3, T4, T5, T6, A, R1, R2, R3>(sources, recurrentResult3Selector(default).InitValue, resultSelector);

		// final select of 2 results from 6 recurrent sources that have captured 2 results
        public static IArrayTransform2<T1, T2, T3, T4, T5, T6, R1, R2> Select<T1, T2, T3, T4, T5, T6, _, R1, R2>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, _, R1, R2> sources, Expression<Func<_, (R1, R2)>> resultSelector)
              => new ArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2>(sources, resultSelector);


		// final select of 3 results from 6 recurrent sources that have captured 2 results
        public static IArrayTransform3<T1, T2, T3, T4, T5, T6, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, _, R1, R2, R3>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, _, R1, R2> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3>(sources, resultSelector);

        // final-recurrent select of 3 results from 6 recurrent sources that have captured 2 results
        public static IArrayTransform3<T1, T2, T3, T4, T5, T6, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2, R3>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3>(sources, recurrentResult3Selector(default).InitValue, resultSelector);

		// final select of 4 results from 6 recurrent sources that have captured 2 results
        public static IArrayTransform4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, _, R1, R2, R3, R4>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, _, R1, R2> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 6 recurrent sources that have captured 2 results
        public static IArrayTransform4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2, R3, R4>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(sources, recurrentResult3Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 6 sources with 3 results from a recurrent source
        public static IArrayQueryRecurrent3<T1, T2, T3, T4, T5, T6, A, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, _, A, R1, R2, R3>(this IArrayQueryRecurrent3<T1, T2, T3, T4, T5, T6, _, R1, R2, R3> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent3<T1, T2, T3, T4, T5, T6, A, R1, R2, R3>(sources, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 6 sources that have captured 3 results
        public static IArrayQueryRecurrent4<T1, T2, T3, T4, T5, T6, A, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, _, A, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T1, T2, T3, T4, T5, T6, _, R1, R2, R3> sources, Func<object, Result<R4>> recurrentResult4Selector, Expression<Func<_, RelativeCell<R4>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent4<T1, T2, T3, T4, T5, T6, A, R1, R2, R3, R4>(sources, recurrentResult4Selector(default).InitValue, resultSelector);

		// final select of 3 results from 6 recurrent sources that have captured 3 results
        public static IArrayTransform3<T1, T2, T3, T4, T5, T6, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, _, R1, R2, R3>(this IArrayQueryRecurrent3<T1, T2, T3, T4, T5, T6, _, R1, R2, R3> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3>(sources, resultSelector);


		// final select of 4 results from 6 recurrent sources that have captured 3 results
        public static IArrayTransform4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, _, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T1, T2, T3, T4, T5, T6, _, R1, R2, R3> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 6 recurrent sources that have captured 3 results
        public static IArrayTransform4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T1, T2, T3, T4, T5, T6, _, R1, R2, R3> sources, Func<object, Result<R4>> recurrentResult4Selector, Expression<Func<_, RelativeCell<R4>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(sources, recurrentResult4Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 6 sources with 4 results from a recurrent source
        public static IArrayQueryRecurrent4<T1, T2, T3, T4, T5, T6, A, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, _, A, R1, R2, R3, R4>(this IArrayQueryRecurrent4<T1, T2, T3, T4, T5, T6, _, R1, R2, R3, R4> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent4<T1, T2, T3, T4, T5, T6, A, R1, R2, R3, R4>(sources, resultSelector); 


		// final select of 4 results from 6 recurrent sources that have captured 4 results
        public static IArrayTransform4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, _, R1, R2, R3, R4>(this IArrayQueryRecurrent4<T1, T2, T3, T4, T5, T6, _, R1, R2, R3, R4> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(sources, resultSelector);

        #endregion

        #region 7 sources
        // intermediate select (variable definition) for the 7 sources with 1 results from a recurrent source
        public static IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, T7, A, R> Select<T1, T2, T3, T4, T5, T6, T7, _, A, R>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, T7, _, R> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, T7, A, R>(sources, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 7 sources that have captured 1 result
        public static IArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, T7, A, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, A, R1, R2>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, T7, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, T7, A, R1, R2>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 1 result from 7 recurrent sources that have captured 1 result
        public static IArrayTransform<T1, T2, T3, T4, T5, T6, T7, R> Select<T1, T2, T3, T4, T5, T6, T7, _, R>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, T7, _, R> sources, Expression<Func<_, R>> resultSelector)
              => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, R>(sources, resultSelector);


		// final select of 2 results from 7 recurrent sources that have captured 1 result
        public static IArrayTransform2<T1, T2, T3, T4, T5, T6, T7, R1, R2> Select<T1, T2, T3, T4, T5, T6, T7, _, R1, R2>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, T7, _, R1> sources, Expression<Func<_, (R1, R2)>> resultSelector)
              => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2>(sources, resultSelector);

        // final-recurrent select of 2 results from 7 recurrent sources that have captured 1 result
        public static IArrayTransform2<T1, T2, T3, T4, T5, T6, T7, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, T7, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 3 results from 7 recurrent sources that have captured 1 result
        public static IArrayTransform3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, T7, _, R1> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(sources, resultSelector);

        // final-recurrent select of 3 results from 7 recurrent sources that have captured 1 result
        public static IArrayTransform3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, T7, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(sources, recurrentResult2Selector(default).InitValue, resultSelector);

		// final select of 4 results from 7 recurrent sources that have captured 1 result
        public static IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3, R4>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, T7, _, R1> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 7 recurrent sources that have captured 1 result
        public static IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3, R4>(this IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, T7, _, R1> sources, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(sources, recurrentResult2Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 7 sources with 2 results from a recurrent source
        public static IArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, T7, A, R1, R2> Select<T1, T2, T3, T4, T5, T6, T7, _, A, R1, R2>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, T7, _, R1, R2> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, T7, A, R1, R2>(sources, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 7 sources that have captured 2 results
        public static IArrayQueryRecurrent3<T1, T2, T3, T4, T5, T6, T7, A, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, A, R1, R2, R3>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, T7, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent3<T1, T2, T3, T4, T5, T6, T7, A, R1, R2, R3>(sources, recurrentResult3Selector(default).InitValue, resultSelector);

		// final select of 2 results from 7 recurrent sources that have captured 2 results
        public static IArrayTransform2<T1, T2, T3, T4, T5, T6, T7, R1, R2> Select<T1, T2, T3, T4, T5, T6, T7, _, R1, R2>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, T7, _, R1, R2> sources, Expression<Func<_, (R1, R2)>> resultSelector)
              => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2>(sources, resultSelector);


		// final select of 3 results from 7 recurrent sources that have captured 2 results
        public static IArrayTransform3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, T7, _, R1, R2> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(sources, resultSelector);

        // final-recurrent select of 3 results from 7 recurrent sources that have captured 2 results
        public static IArrayTransform3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, T7, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(sources, recurrentResult3Selector(default).InitValue, resultSelector);

		// final select of 4 results from 7 recurrent sources that have captured 2 results
        public static IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3, R4>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, T7, _, R1, R2> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 7 recurrent sources that have captured 2 results
        public static IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3, R4>(this IArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, T7, _, R1, R2> sources, Func<object, Result<R3>> recurrentResult3Selector, Expression<Func<_, RelativeCell<R3>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(sources, recurrentResult3Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 7 sources with 3 results from a recurrent source
        public static IArrayQueryRecurrent3<T1, T2, T3, T4, T5, T6, T7, A, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, T7, _, A, R1, R2, R3>(this IArrayQueryRecurrent3<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent3<T1, T2, T3, T4, T5, T6, T7, A, R1, R2, R3>(sources, resultSelector); 


        // recurrent definition of an extra variable for the recurrent selection from 7 sources that have captured 3 results
        public static IArrayQueryRecurrent4<T1, T2, T3, T4, T5, T6, T7, A, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, A, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3> sources, Func<object, Result<R4>> recurrentResult4Selector, Expression<Func<_, RelativeCell<R4>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent4<T1, T2, T3, T4, T5, T6, T7, A, R1, R2, R3, R4>(sources, recurrentResult4Selector(default).InitValue, resultSelector);

		// final select of 3 results from 7 recurrent sources that have captured 3 results
        public static IArrayTransform3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3>(this IArrayQueryRecurrent3<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector)
              => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(sources, resultSelector);


		// final select of 4 results from 7 recurrent sources that have captured 3 results
        public static IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 7 recurrent sources that have captured 3 results
        public static IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3, R4>(this IArrayQueryRecurrent3<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3> sources, Func<object, Result<R4>> recurrentResult4Selector, Expression<Func<_, RelativeCell<R4>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(sources, recurrentResult4Selector(default).InitValue, resultSelector);
        // intermediate select (variable definition) for the 7 sources with 4 results from a recurrent source
        public static IArrayQueryRecurrent4<T1, T2, T3, T4, T5, T6, T7, A, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, T7, _, A, R1, R2, R3, R4>(this IArrayQueryRecurrent4<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3, R4> sources, Expression<Func<_, A>> resultSelector)
            where A: class
            => new ArrayQueryRecurrent4<T1, T2, T3, T4, T5, T6, T7, A, R1, R2, R3, R4>(sources, resultSelector); 


		// final select of 4 results from 7 recurrent sources that have captured 4 results
        public static IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3, R4>(this IArrayQueryRecurrent4<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3, R4> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
              => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(sources, resultSelector);

        #endregion

	}

}

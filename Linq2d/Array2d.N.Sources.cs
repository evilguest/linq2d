﻿using System;
using System.Linq.Expressions;

namespace Linq2d
{
    public static partial class Array2d
    {
        #region 1 source

        // intermediate-recurrent select of 1 result from 1 source
        public static IArrayQueryRecurrent<T, A, R> SelectMany<T, _, A, R>(this IArrayQuery<T, _> source, Func<object, Result<R>> recurrentSelector, Expression<Func<_, RelativeCell<R>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent<T, A, R>(source, recurrentSelector(default).InitValue, resultSelector);


		// final select of 1 results from 1 source
        public static IArrayQuery<T, R> Select<T, _, R>(this IArrayQuery<T, _> sources, Expression<Func<_, R>> resultSelector) 
            => new ArrayQuery<T, R>(sources, resultSelector);

        // final-recurrent select of 1 results from 1 source
        public static IArrayQuery<T, R> SelectMany<T, _, R>(this IArrayQuery<T, _> sources, Func<object, Result<R>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R>, R>> resultSelector) 
            => new ArrayQuery<T, R>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 1 results from 1+1 sources
        public static IArrayQuery<T1, T2, R> SelectMany<T1, T2, _, R>(this IArrayQuery<T1, _> source, Func<object, T2[,]> source2Selector, Expression<Func<_, Cell<T2>, R>> kernel)
            => new ArrayQuery<T1, T2, R>(source, source2Selector(default).Wrap(), kernel); 
        public static IArrayQuery<T1, T2, R> SelectMany<T1, T2, _, R>(this IArrayQuery<T1, _> source, Func<object, ArraySource<T2>> source2Selector, Expression<Func<_, Cell<T2>, R>> kernel)
            => new ArrayQuery<T1, T2, R>(source, source2Selector(default), kernel); 


		// final select of 2 results from 1 source
        public static IArrayTransform2<T, R1, R2> Select<T, _, R1, R2>(this IArrayQuery<T, _> sources, Expression<Func<_, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T, R1, R2>(sources, resultSelector);

        // final-recurrent select of 2 results from 1 source
        public static IArrayTransform2<T, R1, R2> SelectMany<T, _, R1, R2>(this IArrayQuery<T, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T, R1, R2>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 2 results from 1+1 sources
        public static IArrayQuery2<T1, T2, R1, R2> SelectMany<T1, T2, _, R1, R2>(this IArrayQuery<T1, _> source, Func<object, T2[,]> source2Selector, Expression<Func<_, Cell<T2>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, R1, R2>(source, source2Selector(default).Wrap(), kernel); 
        public static IArrayQuery2<T1, T2, R1, R2> SelectMany<T1, T2, _, R1, R2>(this IArrayQuery<T1, _> source, Func<object, ArraySource<T2>> source2Selector, Expression<Func<_, Cell<T2>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, R1, R2>(source, source2Selector(default), kernel); 


		// final select of 3 results from 1 source
        public static IArrayTransform3<T, R1, R2, R3> Select<T, _, R1, R2, R3>(this IArrayQuery<T, _> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T, R1, R2, R3>(sources, resultSelector);

        // final-recurrent select of 3 results from 1 source
        public static IArrayTransform3<T, R1, R2, R3> SelectMany<T, _, R1, R2, R3>(this IArrayQuery<T, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T, R1, R2, R3>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 3 results from 1+1 sources
        public static IArrayQuery3<T1, T2, R1, R2, R3> SelectMany<T1, T2, _, R1, R2, R3>(this IArrayQuery<T1, _> source, Func<object, T2[,]> source2Selector, Expression<Func<_, Cell<T2>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, R1, R2, R3>(source, source2Selector(default).Wrap(), kernel); 
        public static IArrayQuery3<T1, T2, R1, R2, R3> SelectMany<T1, T2, _, R1, R2, R3>(this IArrayQuery<T1, _> source, Func<object, ArraySource<T2>> source2Selector, Expression<Func<_, Cell<T2>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, R1, R2, R3>(source, source2Selector(default), kernel); 


		// final select of 4 results from 1 source
        public static IArrayTransform4<T, R1, R2, R3, R4> Select<T, _, R1, R2, R3, R4>(this IArrayQuery<T, _> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 1 source
        public static IArrayTransform4<T, R1, R2, R3, R4> SelectMany<T, _, R1, R2, R3, R4>(this IArrayQuery<T, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T, R1, R2, R3, R4>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 4 results from 1+1 sources
        public static IArrayQuery4<T1, T2, R1, R2, R3, R4> SelectMany<T1, T2, _, R1, R2, R3, R4>(this IArrayQuery<T1, _> source, Func<object, T2[,]> source2Selector, Expression<Func<_, Cell<T2>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, R1, R2, R3, R4>(source, source2Selector(default).Wrap(), kernel); 
        public static IArrayQuery4<T1, T2, R1, R2, R3, R4> SelectMany<T1, T2, _, R1, R2, R3, R4>(this IArrayQuery<T1, _> source, Func<object, ArraySource<T2>> source2Selector, Expression<Func<_, Cell<T2>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, R1, R2, R3, R4>(source, source2Selector(default), kernel); 


        #endregion

        #region 2 sources

        // intermediate-recurrent select of 1 result from 2 sources
        public static IArrayQueryRecurrent<T1, T2, A, R> SelectMany<T1, T2, _, A, R>(this IArrayQuery<T1, T2, _> sources, Func<object, Result<R>> recurrentSelector, Expression<Func<_, RelativeCell<R>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent<T1, T2, A, R>(sources, recurrentSelector(default).InitValue, resultSelector);


		// final select of 1 results from 2 sources
        public static IArrayQuery<T1, T2, R> Select<T1, T2, _, R>(this IArrayQuery<T1, T2, _> sources, Expression<Func<_, R>> resultSelector) 
            => new ArrayQuery<T1, T2, R>(sources, resultSelector);

        // final-recurrent select of 1 results from 2 sources
        public static IArrayQuery<T1, T2, R> SelectMany<T1, T2, _, R>(this IArrayQuery<T1, T2, _> sources, Func<object, Result<R>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R>, R>> resultSelector) 
            => new ArrayQuery<T1, T2, R>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 1 results from 2+1 sources
        public static IArrayQuery<T1, T2, T3, R> SelectMany<T1, T2, T3, _, R>(this IArrayQuery<T1, T2, _> sources, Func<object, T3[,]> source3Selector, Expression<Func<_, Cell<T3>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, R>(sources, source3Selector(default).Wrap(), kernel); 
        public static IArrayQuery<T1, T2, T3, R> SelectMany<T1, T2, T3, _, R>(this IArrayQuery<T1, T2, _> sources, Func<object, ArraySource<T3>> source3Selector, Expression<Func<_, Cell<T3>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, R>(sources, source3Selector(default), kernel); 


		// final select of 2 results from 2 sources
        public static IArrayTransform2<T1, T2, R1, R2> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> sources, Expression<Func<_, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, R1, R2>(sources, resultSelector);

        // final-recurrent select of 2 results from 2 sources
        public static IArrayTransform2<T1, T2, R1, R2> SelectMany<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, R1, R2>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 2 results from 2+1 sources
        public static IArrayQuery2<T1, T2, T3, R1, R2> SelectMany<T1, T2, T3, _, R1, R2>(this IArrayQuery<T1, T2, _> sources, Func<object, T3[,]> source3Selector, Expression<Func<_, Cell<T3>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, R1, R2>(sources, source3Selector(default).Wrap(), kernel); 
        public static IArrayQuery2<T1, T2, T3, R1, R2> SelectMany<T1, T2, T3, _, R1, R2>(this IArrayQuery<T1, T2, _> sources, Func<object, ArraySource<T3>> source3Selector, Expression<Func<_, Cell<T3>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, R1, R2>(sources, source3Selector(default), kernel); 


		// final select of 3 results from 2 sources
        public static IArrayTransform3<T1, T2, R1, R2, R3> Select<T1, T2, _, R1, R2, R3>(this IArrayQuery<T1, T2, _> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, R1, R2, R3>(sources, resultSelector);

        // final-recurrent select of 3 results from 2 sources
        public static IArrayTransform3<T1, T2, R1, R2, R3> SelectMany<T1, T2, _, R1, R2, R3>(this IArrayQuery<T1, T2, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, R1, R2, R3>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 3 results from 2+1 sources
        public static IArrayQuery3<T1, T2, T3, R1, R2, R3> SelectMany<T1, T2, T3, _, R1, R2, R3>(this IArrayQuery<T1, T2, _> sources, Func<object, T3[,]> source3Selector, Expression<Func<_, Cell<T3>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(sources, source3Selector(default).Wrap(), kernel); 
        public static IArrayQuery3<T1, T2, T3, R1, R2, R3> SelectMany<T1, T2, T3, _, R1, R2, R3>(this IArrayQuery<T1, T2, _> sources, Func<object, ArraySource<T3>> source3Selector, Expression<Func<_, Cell<T3>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(sources, source3Selector(default), kernel); 


		// final select of 4 results from 2 sources
        public static IArrayTransform4<T1, T2, R1, R2, R3, R4> Select<T1, T2, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, _> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 2 sources
        public static IArrayTransform4<T1, T2, R1, R2, R3, R4> SelectMany<T1, T2, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, R1, R2, R3, R4>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 4 results from 2+1 sources
        public static IArrayQuery4<T1, T2, T3, R1, R2, R3, R4> SelectMany<T1, T2, T3, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, _> sources, Func<object, T3[,]> source3Selector, Expression<Func<_, Cell<T3>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(sources, source3Selector(default).Wrap(), kernel); 
        public static IArrayQuery4<T1, T2, T3, R1, R2, R3, R4> SelectMany<T1, T2, T3, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, _> sources, Func<object, ArraySource<T3>> source3Selector, Expression<Func<_, Cell<T3>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(sources, source3Selector(default), kernel); 


        #endregion

        #region 3 sources

        // intermediate-recurrent select of 1 result from 3 sources
        public static IArrayQueryRecurrent<T1, T2, T3, A, R> SelectMany<T1, T2, T3, _, A, R>(this IArrayQuery<T1, T2, T3, _> sources, Func<object, Result<R>> recurrentSelector, Expression<Func<_, RelativeCell<R>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent<T1, T2, T3, A, R>(sources, recurrentSelector(default).InitValue, resultSelector);


		// final select of 1 results from 3 sources
        public static IArrayQuery<T1, T2, T3, R> Select<T1, T2, T3, _, R>(this IArrayQuery<T1, T2, T3, _> sources, Expression<Func<_, R>> resultSelector) 
            => new ArrayQuery<T1, T2, T3, R>(sources, resultSelector);

        // final-recurrent select of 1 results from 3 sources
        public static IArrayQuery<T1, T2, T3, R> SelectMany<T1, T2, T3, _, R>(this IArrayQuery<T1, T2, T3, _> sources, Func<object, Result<R>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R>, R>> resultSelector) 
            => new ArrayQuery<T1, T2, T3, R>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 1 results from 3+1 sources
        public static IArrayQuery<T1, T2, T3, T4, R> SelectMany<T1, T2, T3, T4, _, R>(this IArrayQuery<T1, T2, T3, _> sources, Func<object, T4[,]> source4Selector, Expression<Func<_, Cell<T4>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, R>(sources, source4Selector(default).Wrap(), kernel); 
        public static IArrayQuery<T1, T2, T3, T4, R> SelectMany<T1, T2, T3, T4, _, R>(this IArrayQuery<T1, T2, T3, _> sources, Func<object, ArraySource<T4>> source4Selector, Expression<Func<_, Cell<T4>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, R>(sources, source4Selector(default), kernel); 


		// final select of 2 results from 3 sources
        public static IArrayTransform2<T1, T2, T3, R1, R2> Select<T1, T2, T3, _, R1, R2>(this IArrayQuery<T1, T2, T3, _> sources, Expression<Func<_, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, R1, R2>(sources, resultSelector);

        // final-recurrent select of 2 results from 3 sources
        public static IArrayTransform2<T1, T2, T3, R1, R2> SelectMany<T1, T2, T3, _, R1, R2>(this IArrayQuery<T1, T2, T3, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, R1, R2>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 2 results from 3+1 sources
        public static IArrayQuery2<T1, T2, T3, T4, R1, R2> SelectMany<T1, T2, T3, T4, _, R1, R2>(this IArrayQuery<T1, T2, T3, _> sources, Func<object, T4[,]> source4Selector, Expression<Func<_, Cell<T4>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, R1, R2>(sources, source4Selector(default).Wrap(), kernel); 
        public static IArrayQuery2<T1, T2, T3, T4, R1, R2> SelectMany<T1, T2, T3, T4, _, R1, R2>(this IArrayQuery<T1, T2, T3, _> sources, Func<object, ArraySource<T4>> source4Selector, Expression<Func<_, Cell<T4>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, R1, R2>(sources, source4Selector(default), kernel); 


		// final select of 3 results from 3 sources
        public static IArrayTransform3<T1, T2, T3, R1, R2, R3> Select<T1, T2, T3, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, _> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(sources, resultSelector);

        // final-recurrent select of 3 results from 3 sources
        public static IArrayTransform3<T1, T2, T3, R1, R2, R3> SelectMany<T1, T2, T3, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 3 results from 3+1 sources
        public static IArrayQuery3<T1, T2, T3, T4, R1, R2, R3> SelectMany<T1, T2, T3, T4, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, _> sources, Func<object, T4[,]> source4Selector, Expression<Func<_, Cell<T4>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(sources, source4Selector(default).Wrap(), kernel); 
        public static IArrayQuery3<T1, T2, T3, T4, R1, R2, R3> SelectMany<T1, T2, T3, T4, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, _> sources, Func<object, ArraySource<T4>> source4Selector, Expression<Func<_, Cell<T4>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(sources, source4Selector(default), kernel); 


		// final select of 4 results from 3 sources
        public static IArrayTransform4<T1, T2, T3, R1, R2, R3, R4> Select<T1, T2, T3, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, _> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 3 sources
        public static IArrayTransform4<T1, T2, T3, R1, R2, R3, R4> SelectMany<T1, T2, T3, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 4 results from 3+1 sources
        public static IArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, _> sources, Func<object, T4[,]> source4Selector, Expression<Func<_, Cell<T4>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(sources, source4Selector(default).Wrap(), kernel); 
        public static IArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, _> sources, Func<object, ArraySource<T4>> source4Selector, Expression<Func<_, Cell<T4>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(sources, source4Selector(default), kernel); 


        #endregion

        #region 4 sources

        // intermediate-recurrent select of 1 result from 4 sources
        public static IArrayQueryRecurrent<T1, T2, T3, T4, A, R> SelectMany<T1, T2, T3, T4, _, A, R>(this IArrayQuery<T1, T2, T3, T4, _> sources, Func<object, Result<R>> recurrentSelector, Expression<Func<_, RelativeCell<R>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent<T1, T2, T3, T4, A, R>(sources, recurrentSelector(default).InitValue, resultSelector);


		// final select of 1 results from 4 sources
        public static IArrayQuery<T1, T2, T3, T4, R> Select<T1, T2, T3, T4, _, R>(this IArrayQuery<T1, T2, T3, T4, _> sources, Expression<Func<_, R>> resultSelector) 
            => new ArrayQuery<T1, T2, T3, T4, R>(sources, resultSelector);

        // final-recurrent select of 1 results from 4 sources
        public static IArrayQuery<T1, T2, T3, T4, R> SelectMany<T1, T2, T3, T4, _, R>(this IArrayQuery<T1, T2, T3, T4, _> sources, Func<object, Result<R>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R>, R>> resultSelector) 
            => new ArrayQuery<T1, T2, T3, T4, R>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 1 results from 4+1 sources
        public static IArrayQuery<T1, T2, T3, T4, T5, R> SelectMany<T1, T2, T3, T4, T5, _, R>(this IArrayQuery<T1, T2, T3, T4, _> sources, Func<object, T5[,]> source5Selector, Expression<Func<_, Cell<T5>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, R>(sources, source5Selector(default).Wrap(), kernel); 
        public static IArrayQuery<T1, T2, T3, T4, T5, R> SelectMany<T1, T2, T3, T4, T5, _, R>(this IArrayQuery<T1, T2, T3, T4, _> sources, Func<object, ArraySource<T5>> source5Selector, Expression<Func<_, Cell<T5>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, R>(sources, source5Selector(default), kernel); 


		// final select of 2 results from 4 sources
        public static IArrayTransform2<T1, T2, T3, T4, R1, R2> Select<T1, T2, T3, T4, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, _> sources, Expression<Func<_, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, R1, R2>(sources, resultSelector);

        // final-recurrent select of 2 results from 4 sources
        public static IArrayTransform2<T1, T2, T3, T4, R1, R2> SelectMany<T1, T2, T3, T4, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, R1, R2>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 2 results from 4+1 sources
        public static IArrayQuery2<T1, T2, T3, T4, T5, R1, R2> SelectMany<T1, T2, T3, T4, T5, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, _> sources, Func<object, T5[,]> source5Selector, Expression<Func<_, Cell<T5>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, R1, R2>(sources, source5Selector(default).Wrap(), kernel); 
        public static IArrayQuery2<T1, T2, T3, T4, T5, R1, R2> SelectMany<T1, T2, T3, T4, T5, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, _> sources, Func<object, ArraySource<T5>> source5Selector, Expression<Func<_, Cell<T5>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, R1, R2>(sources, source5Selector(default), kernel); 


		// final select of 3 results from 4 sources
        public static IArrayTransform3<T1, T2, T3, T4, R1, R2, R3> Select<T1, T2, T3, T4, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, _> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(sources, resultSelector);

        // final-recurrent select of 3 results from 4 sources
        public static IArrayTransform3<T1, T2, T3, T4, R1, R2, R3> SelectMany<T1, T2, T3, T4, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 3 results from 4+1 sources
        public static IArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, _> sources, Func<object, T5[,]> source5Selector, Expression<Func<_, Cell<T5>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3>(sources, source5Selector(default).Wrap(), kernel); 
        public static IArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, _> sources, Func<object, ArraySource<T5>> source5Selector, Expression<Func<_, Cell<T5>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3>(sources, source5Selector(default), kernel); 


		// final select of 4 results from 4 sources
        public static IArrayTransform4<T1, T2, T3, T4, R1, R2, R3, R4> Select<T1, T2, T3, T4, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, _> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 4 sources
        public static IArrayTransform4<T1, T2, T3, T4, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 4 results from 4+1 sources
        public static IArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, _> sources, Func<object, T5[,]> source5Selector, Expression<Func<_, Cell<T5>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>(sources, source5Selector(default).Wrap(), kernel); 
        public static IArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, _> sources, Func<object, ArraySource<T5>> source5Selector, Expression<Func<_, Cell<T5>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>(sources, source5Selector(default), kernel); 


        #endregion

        #region 5 sources

        // intermediate-recurrent select of 1 result from 5 sources
        public static IArrayQueryRecurrent<T1, T2, T3, T4, T5, A, R> SelectMany<T1, T2, T3, T4, T5, _, A, R>(this IArrayQuery<T1, T2, T3, T4, T5, _> sources, Func<object, Result<R>> recurrentSelector, Expression<Func<_, RelativeCell<R>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent<T1, T2, T3, T4, T5, A, R>(sources, recurrentSelector(default).InitValue, resultSelector);


		// final select of 1 results from 5 sources
        public static IArrayQuery<T1, T2, T3, T4, T5, R> Select<T1, T2, T3, T4, T5, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, _> sources, Expression<Func<_, R>> resultSelector) 
            => new ArrayQuery<T1, T2, T3, T4, T5, R>(sources, resultSelector);

        // final-recurrent select of 1 results from 5 sources
        public static IArrayQuery<T1, T2, T3, T4, T5, R> SelectMany<T1, T2, T3, T4, T5, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, _> sources, Func<object, Result<R>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R>, R>> resultSelector) 
            => new ArrayQuery<T1, T2, T3, T4, T5, R>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 1 results from 5+1 sources
        public static IArrayQuery<T1, T2, T3, T4, T5, T6, R> SelectMany<T1, T2, T3, T4, T5, T6, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, _> sources, Func<object, T6[,]> source6Selector, Expression<Func<_, Cell<T6>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, R>(sources, source6Selector(default).Wrap(), kernel); 
        public static IArrayQuery<T1, T2, T3, T4, T5, T6, R> SelectMany<T1, T2, T3, T4, T5, T6, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, _> sources, Func<object, ArraySource<T6>> source6Selector, Expression<Func<_, Cell<T6>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, R>(sources, source6Selector(default), kernel); 


		// final select of 2 results from 5 sources
        public static IArrayTransform2<T1, T2, T3, T4, T5, R1, R2> Select<T1, T2, T3, T4, T5, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, _> sources, Expression<Func<_, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, R1, R2>(sources, resultSelector);

        // final-recurrent select of 2 results from 5 sources
        public static IArrayTransform2<T1, T2, T3, T4, T5, R1, R2> SelectMany<T1, T2, T3, T4, T5, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, R1, R2>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 2 results from 5+1 sources
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, _> sources, Func<object, T6[,]> source6Selector, Expression<Func<_, Cell<T6>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2>(sources, source6Selector(default).Wrap(), kernel); 
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, _> sources, Func<object, ArraySource<T6>> source6Selector, Expression<Func<_, Cell<T6>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2>(sources, source6Selector(default), kernel); 


		// final select of 3 results from 5 sources
        public static IArrayTransform3<T1, T2, T3, T4, T5, R1, R2, R3> Select<T1, T2, T3, T4, T5, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, _> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3>(sources, resultSelector);

        // final-recurrent select of 3 results from 5 sources
        public static IArrayTransform3<T1, T2, T3, T4, T5, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 3 results from 5+1 sources
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, _> sources, Func<object, T6[,]> source6Selector, Expression<Func<_, Cell<T6>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3>(sources, source6Selector(default).Wrap(), kernel); 
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, _> sources, Func<object, ArraySource<T6>> source6Selector, Expression<Func<_, Cell<T6>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3>(sources, source6Selector(default), kernel); 


		// final select of 4 results from 5 sources
        public static IArrayTransform4<T1, T2, T3, T4, T5, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, _> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 5 sources
        public static IArrayTransform4<T1, T2, T3, T4, T5, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 4 results from 5+1 sources
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, _> sources, Func<object, T6[,]> source6Selector, Expression<Func<_, Cell<T6>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(sources, source6Selector(default).Wrap(), kernel); 
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, _> sources, Func<object, ArraySource<T6>> source6Selector, Expression<Func<_, Cell<T6>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(sources, source6Selector(default), kernel); 


        #endregion

        #region 6 sources

        // intermediate-recurrent select of 1 result from 6 sources
        public static IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, A, R> SelectMany<T1, T2, T3, T4, T5, T6, _, A, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> sources, Func<object, Result<R>> recurrentSelector, Expression<Func<_, RelativeCell<R>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, A, R>(sources, recurrentSelector(default).InitValue, resultSelector);


		// final select of 1 results from 6 sources
        public static IArrayQuery<T1, T2, T3, T4, T5, T6, R> Select<T1, T2, T3, T4, T5, T6, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> sources, Expression<Func<_, R>> resultSelector) 
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, R>(sources, resultSelector);

        // final-recurrent select of 1 results from 6 sources
        public static IArrayQuery<T1, T2, T3, T4, T5, T6, R> SelectMany<T1, T2, T3, T4, T5, T6, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> sources, Func<object, Result<R>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R>, R>> resultSelector) 
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, R>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 1 results from 6+1 sources
        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> sources, Func<object, T7[,]> source7Selector, Expression<Func<_, Cell<T7>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, R>(sources, source7Selector(default).Wrap(), kernel); 
        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> sources, Func<object, ArraySource<T7>> source7Selector, Expression<Func<_, Cell<T7>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, R>(sources, source7Selector(default), kernel); 


		// final select of 2 results from 6 sources
        public static IArrayTransform2<T1, T2, T3, T4, T5, T6, R1, R2> Select<T1, T2, T3, T4, T5, T6, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> sources, Expression<Func<_, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2>(sources, resultSelector);

        // final-recurrent select of 2 results from 6 sources
        public static IArrayTransform2<T1, T2, T3, T4, T5, T6, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 2 results from 6+1 sources
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> sources, Func<object, T7[,]> source7Selector, Expression<Func<_, Cell<T7>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2>(sources, source7Selector(default).Wrap(), kernel); 
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> sources, Func<object, ArraySource<T7>> source7Selector, Expression<Func<_, Cell<T7>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2>(sources, source7Selector(default), kernel); 


		// final select of 3 results from 6 sources
        public static IArrayTransform3<T1, T2, T3, T4, T5, T6, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3>(sources, resultSelector);

        // final-recurrent select of 3 results from 6 sources
        public static IArrayTransform3<T1, T2, T3, T4, T5, T6, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 3 results from 6+1 sources
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> sources, Func<object, T7[,]> source7Selector, Expression<Func<_, Cell<T7>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(sources, source7Selector(default).Wrap(), kernel); 
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> sources, Func<object, ArraySource<T7>> source7Selector, Expression<Func<_, Cell<T7>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(sources, source7Selector(default), kernel); 


		// final select of 4 results from 6 sources
        public static IArrayTransform4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 6 sources
        public static IArrayTransform4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(sources, recurrentResult1Selector(default).InitValue, resultSelector);


        // final select of 4 results from 6+1 sources
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> sources, Func<object, T7[,]> source7Selector, Expression<Func<_, Cell<T7>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(sources, source7Selector(default).Wrap(), kernel); 
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> sources, Func<object, ArraySource<T7>> source7Selector, Expression<Func<_, Cell<T7>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(sources, source7Selector(default), kernel); 


        #endregion

        #region 7 sources

        // intermediate-recurrent select of 1 result from 7 sources
        public static IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, T7, A, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, A, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> sources, Func<object, Result<R>> recurrentSelector, Expression<Func<_, RelativeCell<R>, A>> resultSelector) 
            where A: class
            => new ArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, T7, A, R>(sources, recurrentSelector(default).InitValue, resultSelector);


		// final select of 1 results from 7 sources
        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, R> Select<T1, T2, T3, T4, T5, T6, T7, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> sources, Expression<Func<_, R>> resultSelector) 
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, R>(sources, resultSelector);

        // final-recurrent select of 1 results from 7 sources
        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> sources, Func<object, Result<R>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R>, R>> resultSelector) 
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, R>(sources, recurrentResult1Selector(default).InitValue, resultSelector);



		// final select of 2 results from 7 sources
        public static IArrayTransform2<T1, T2, T3, T4, T5, T6, T7, R1, R2> Select<T1, T2, T3, T4, T5, T6, T7, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> sources, Expression<Func<_, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2>(sources, resultSelector);

        // final-recurrent select of 2 results from 7 sources
        public static IArrayTransform2<T1, T2, T3, T4, T5, T6, T7, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2>(sources, recurrentResult1Selector(default).InitValue, resultSelector);



		// final select of 3 results from 7 sources
        public static IArrayTransform3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> sources, Expression<Func<_, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(sources, resultSelector);

        // final-recurrent select of 3 results from 7 sources
        public static IArrayTransform3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(sources, recurrentResult1Selector(default).InitValue, resultSelector);



		// final select of 4 results from 7 sources
        public static IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> sources, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(sources, resultSelector);

        // final-recurrent select of 4 results from 7 sources
        public static IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> sources, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(sources, recurrentResult1Selector(default).InitValue, resultSelector);



        #endregion

	}

}

using System;
using System.Linq.Expressions;

namespace Linq2d
{
    public static class Array2d
    {
        #region control
        public static bool TryVectorize { get; set; } = true;
        public static bool EliminateCommonSubexpressions { get; set; } = true;
        public static bool PoolCSEVariables { get; set; } = true;
        public static bool SaveDynamicCode { get; set; } = false;
        public static bool MoveLoopInvariants {get; set; } = true;
        #endregion

        #region window functions
//        public static (Cell<T> tl, Cell<T> tr, Cell<T> bl, Cell<T> br) Window<T>(this Cell<T> cell, int size)
//            =>(cell, cell, cell, cell);
//        public static int Area<T>(this (Cell<T> tl, Cell<T> tr, Cell<T> bl, Cell<T> br) window)=>(window.br.X - window.tl.X) * (window.br.Y - window.tl.Y);
        
        #endregion

        #region One array recurrent
        public static IArrayQueryRecurrent<T, _, R> SelectMany<T, _, R>(this T[,] source, Func<object, Result<R>> secondSelector, Expression<Func<Cell<T>, RelativeCell<R>, _>> resultSelector)
        where _: class
            => new ArrayQueryRecurrent<T, _, R>(source.Wrap(), secondSelector(default).InitValue, resultSelector);
        public static IArrayQueryRecurrent<T, _, R> SelectMany<T, _, R>(this ArraySource<T> source, Func<object, Result<R>> secondSelector, Expression<Func<Cell<T>, RelativeCell<R>, _>> resultSelector)
        where _: class
            => new ArrayQueryRecurrent<T, _, R>(source, secondSelector(default).InitValue, resultSelector);
        #endregion


        #region One array two results recurrent one
        public static IArrayTransform2<T, R1, R2> SelectMany<T, R1, R2>(this T[,] source, Func<object, Result<R1>> secondSelector, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T, R1, R2>(source.Wrap(), secondSelector(default).InitValue, resultSelector);
        public static IArrayTransform2<T, R1, R2> SelectMany<T, R1, R2>(this ArraySource<T> source, Func<object, Result<R1>> secondSelector, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T, R1, R2>(source, secondSelector(default).InitValue, resultSelector);
        #endregion

        #region One array three results recurrent one
        public static IArrayTransform3<T, R1, R2, R3> SelectMany<T, R1, R2, R3>(this T[,] source, Func<object, Result<R1>> secondSelector, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T, R1, R2, R3>(source.Wrap(), secondSelector(default).InitValue, resultSelector);
        public static IArrayTransform3<T, R1, R2, R3> SelectMany<T, R1, R2, R3>(this ArraySource<T> source, Func<object, Result<R1>> secondSelector, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T, R1, R2, R3>(source, secondSelector(default).InitValue, resultSelector);
        #endregion

        #region One array four results recurrent one
        public static IArrayTransform4<T, R1, R2, R3, R4> SelectMany<T, R1, R2, R3, R4>(this T[,] source, Func<object, Result<R1>> secondSelector, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T, R1, R2, R3, R4>(source.Wrap(), secondSelector(default).InitValue, resultSelector);
        public static IArrayTransform4<T, R1, R2, R3, R4> SelectMany<T, R1, R2, R3, R4>(this ArraySource<T> source, Func<object, Result<R1>> secondSelector, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T, R1, R2, R3, R4>(source, secondSelector(default).InitValue, resultSelector);
        #endregion

        #region One array two results recurrent two
        public static IArrayTransform2<T, R1, R2> SelectMany<T, _, R1, R2>(this IArrayQueryRecurrent<T, _, R1> source, Func<object, Result<R2>> secondSelector, Expression<Func<_, RelativeCell<R2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T, R1, R2>(source, secondSelector(default).InitValue, resultSelector);
        #endregion

        #region Two arrays one result recurrent 
        public static IArrayTransform<T1, T2, R> SelectMany<T1, T2, _, R>(this IArrayQuery<T1, T2, _> source, Func<object, Result<R>> recurrentResultSelector, Expression<Func<_, RelativeCell<R>, R>> resultSelector)
            => new ArrayQuery<T1, T2, R>(source, recurrentResultSelector(default).InitValue, resultSelector);
        #endregion

        #region Three arrays one result recurrent 
        public static IArrayTransform<T1, T2, T3, R> SelectMany<T1, T2, T3, _, R>(this IArrayQuery<T1, T2, T3, _> source, Func<object, Result<R>> recurrentResultSelector, Expression<Func<_, RelativeCell<R>, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, R>(source, recurrentResultSelector(default).InitValue, resultSelector);
        #endregion
        #region 4 arrays one result recurrent 
        public static IArrayTransform<T1, T2, T3, T4, R> SelectMany<T1, T2, T3, T4, _, R>(this IArrayQuery<T1, T2, T3, T4, _> source, Func<object, Result<R>> recurrentResultSelector, Expression<Func<_, RelativeCell<R>, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, R>(source, recurrentResultSelector(default).InitValue, resultSelector);
        #endregion

        #region Two array two results recurent two
        public static IArrayTransform2<T1, T2, R1, R2> SelectMany<T1, T2, _, R1, R2>(this IArrayQueryRecurrent<T1, T2, _, R1> source, Func<object, Result<R2>> recurrentResult2Selector, Expression<Func<_, RelativeCell<R2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, R1, R2>(source, recurrentResult2Selector(default).InitValue, resultSelector);
        #endregion



        #region 1 array

        #region 1 array, 1 result

        public static IArrayQuery<T, R> Select<T, R>(this T[,] source, Expression<Func<Cell<T>, R>> resultSelector) 
            => new ArrayQuery<T, R>(source.Wrap(), resultSelector);
        public static IArrayQuery<T, R> Select<T, R>(this ArraySource<T> source, Expression<Func<Cell<T>, R>> resultSelector) 
            => new ArrayQuery<T, R>(source, resultSelector);
         public static IArrayTransform<T, R> SelectMany<T, R>(this T[,] source, Func<object, Result<R>> recurrentResult1Selector, Expression<Func<Cell<T>, RelativeCell<R>, R>> resultSelector) 
            => new ArrayQuery<T, R>(source.Wrap(), recurrentResult1Selector(default).InitValue, resultSelector);
        public static IArrayTransform<T, R> SelectMany<T, R>(this ArraySource<T> source, Func<object, Result<R>> recurrentResult1Selector, Expression<Func<Cell<T>, RelativeCell<R>, R>> resultSelector) 
            => new ArrayQuery<T, R>(source, recurrentResult1Selector(default).InitValue, resultSelector);
        public static IArrayQuery<T, R> Select<T, _, R>(this IArrayQuery<T, _> source, Expression<Func<_, R>> resultSelector)
            => new ArrayQuery<T, R>(source, resultSelector);


        #endregion

        #region 1 array, 2 results

        public static IArrayQuery2<T, R1, R2> Select<T, R1, R2>(this T[,] source, Expression<Func<Cell<T>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T, R1, R2>(source.Wrap(), resultSelector);
        public static IArrayQuery2<T, R1, R2> Select<T, R1, R2>(this ArraySource<T> source, Expression<Func<Cell<T>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T, R1, R2>(source, resultSelector);
         public static IArrayQueryRecurrent<T, _, R1> SelectMany<T, _, R1, R2>(this T[,] source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2)>> resultSelector) 
            => new ArrayQueryRecurrent<T, _, R1>(source.Wrap(), recurrentResult1Selector(default).InitValue, resultSelector);
        public static IArrayQueryRecurrent<T, _, R1> SelectMany<T, _, R1, R2>(this ArraySource<T> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2)>> resultSelector) 
            => new ArrayQueryRecurrent<T, _, R1>(source, recurrentResult1Selector(default).InitValue, resultSelector);
        
        public static IArrayQuery2<T, R1, R2> Select<T, _, R1, R2>(this IArrayQuery<T, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T, R1, R2>(source, resultSelector);


        public static IArrayQuery2<T, R1, R2> SelectMany<T, _, R1, R2>(this IArrayQuery<T, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T, R1, R2>(source, recurrentResult1Selector(default).InitValue, resultSelector);

//        public static IArrayQueryRecurrent<T, _, R1> SelectMany<T, _, R1, R2>(this IArrayQuery<T, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2)>> resultSelector)
//            => new ArrayQueryRecurrent<T, _, R1>(source, recurrentResult1Selector(default).InitValue, resultSelector);
        #endregion

        #region 1 array, 3 results

        public static IArrayQuery3<T, R1, R2, R3> Select<T, R1, R2, R3>(this T[,] source, Expression<Func<Cell<T>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T, R1, R2, R3>(source.Wrap(), resultSelector);
        public static IArrayQuery3<T, R1, R2, R3> Select<T, R1, R2, R3>(this ArraySource<T> source, Expression<Func<Cell<T>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T, R1, R2, R3>(source, resultSelector);
         public static IArrayQueryRecurrent2<T, _, R1, R2> SelectMany<T, _, R1, R2, R3>(this T[,] source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQueryRecurrent2<T, _, R1, R2>(source.Wrap(), recurrentResult1Selector(default).InitValue, resultSelector);
        public static IArrayQueryRecurrent2<T, _, R1, R2> SelectMany<T, _, R1, R2, R3>(this ArraySource<T> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQueryRecurrent2<T, _, R1, R2>(source, recurrentResult1Selector(default).InitValue, resultSelector);
        
        public static IArrayQuery3<T, R1, R2, R3> Select<T, _, R1, R2, R3>(this IArrayQuery<T, _> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T, R1, R2, R3>(source, resultSelector);


        public static IArrayQuery3<T, R1, R2, R3> SelectMany<T, _, R1, R2, R3>(this IArrayQuery<T, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T, R1, R2, R3>(source, recurrentResult1Selector(default).InitValue, resultSelector);

//        public static IArrayQueryRecurrent2<T, _, R1, R2> SelectMany<T, _, R1, R2, R3>(this IArrayQuery<T, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3)>> resultSelector)
//            => new ArrayQueryRecurrent2<T, _, R1, R2>(source, recurrentResult1Selector(default).InitValue, resultSelector);
        #endregion

        #region 1 array, 4 results

        public static IArrayQuery4<T, R1, R2, R3, R4> Select<T, R1, R2, R3, R4>(this T[,] source, Expression<Func<Cell<T>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T, R1, R2, R3, R4>(source.Wrap(), resultSelector);
        public static IArrayQuery4<T, R1, R2, R3, R4> Select<T, R1, R2, R3, R4>(this ArraySource<T> source, Expression<Func<Cell<T>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T, R1, R2, R3, R4>(source, resultSelector);
         public static IArrayQueryRecurrent3<T, _, R1, R2, R3> SelectMany<T, _, R1, R2, R3, R4>(this T[,] source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQueryRecurrent3<T, _, R1, R2, R3>(source.Wrap(), recurrentResult1Selector(default).InitValue, resultSelector);
        public static IArrayQueryRecurrent3<T, _, R1, R2, R3> SelectMany<T, _, R1, R2, R3, R4>(this ArraySource<T> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQueryRecurrent3<T, _, R1, R2, R3>(source, recurrentResult1Selector(default).InitValue, resultSelector);
        
        public static IArrayQuery4<T, R1, R2, R3, R4> Select<T, _, R1, R2, R3, R4>(this IArrayQuery<T, _> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T, R1, R2, R3, R4>(source, resultSelector);


        public static IArrayQuery4<T, R1, R2, R3, R4> SelectMany<T, _, R1, R2, R3, R4>(this IArrayQuery<T, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T, R1, R2, R3, R4>(source, recurrentResult1Selector(default).InitValue, resultSelector);

//        public static IArrayQueryRecurrent3<T, _, R1, R2, R3> SelectMany<T, _, R1, R2, R3, R4>(this IArrayQuery<T, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3, R4)>> resultSelector)
//            => new ArrayQueryRecurrent3<T, _, R1, R2, R3>(source, recurrentResult1Selector(default).InitValue, resultSelector);
        #endregion

        #endregion

        #region 2 arrays

        #region 2 arrays, 1 result

        public static IArrayQuery<T1, T2, R> Select<T1, T2, _, R>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, R>> resultSelector)
            => new ArrayQuery<T1, T2, R>(source, resultSelector);

        public static IArrayQuery<T1, T2, R> SelectMany<T1, T2, R>(this T1[,] source, Func<object, T2[,]> source2Selector, Expression<Func<Cell<T1>, Cell<T2>, R>> resultSelector)
            => new ArrayQuery<T1, T2, R>(source.Wrap(), source2Selector(default).Wrap(), resultSelector);
        public static IArrayQuery<T1, T2, R> SelectMany<T1, T2, R>(this T1[,] source, Func<object, ArraySource<T2>> source2Selector, Expression<Func<Cell<T1>, Cell<T2>,  R>> resultSelector)
            => new ArrayQuery<T1, T2, R>(source.Wrap(), source2Selector(default), resultSelector);
        public static IArrayQuery<T1, T2, R> SelectMany<T1, T2, R>(this ArraySource<T1> source, Func<object, T2[,]> source2Selector, Expression<Func<Cell<T1>, Cell<T2>, R>> resultSelector) 
            => new ArrayQuery<T1, T2, R>(source, source2Selector(default).Wrap(), resultSelector);
        public static IArrayQuery<T1, T2, R> SelectMany<T1, T2, R>(this ArraySource<T1> source, Func<object, ArraySource<T2>> source2Selector, Expression<Func<Cell<T1>, Cell<T2>, R>> resultSelector)
            => new ArrayQuery<T1, T2, R>(source, source2Selector(default), resultSelector);


        public static IArrayQuery<T1, T2, R> SelectMany<T1, T2, _, R>(this IArrayQuery<T1, _> source, Func<object, T2[,]> source2Selector, Expression<Func<_, Cell<T2>, R>> kernel)
            => new ArrayQuery<T1, T2, R>(source, source2Selector(default).Wrap(), kernel);

        public static IArrayQuery<T1, T2, R> SelectMany<T1, T2, _, R>(this IArrayQuery<T1, _> source, Func<object, ArraySource<T2>> source2Selector, Expression<Func<_, Cell<T2>, R>> kernel)
            => new ArrayQuery<T1, T2, R>(source, source2Selector(default), kernel);

        #endregion

        #region 2 arrays, 2 results

        public static IArrayQuery2<T1, T2, R1, R2> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, R1, R2>(source, resultSelector);

        public static IArrayQuery2<T1, T2, R1, R2> SelectMany<T1, T2, R1, R2>(this T1[,] source, Func<object, T2[,]> source2Selector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, R1, R2>(source.Wrap(), source2Selector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, R1, R2> SelectMany<T1, T2, R1, R2>(this T1[,] source, Func<object, ArraySource<T2>> source2Selector, Expression<Func<Cell<T1>, Cell<T2>,  (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, R1, R2>(source.Wrap(), source2Selector(default), resultSelector);
        public static IArrayQuery2<T1, T2, R1, R2> SelectMany<T1, T2, R1, R2>(this ArraySource<T1> source, Func<object, T2[,]> source2Selector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, R1, R2>(source, source2Selector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, R1, R2> SelectMany<T1, T2, R1, R2>(this ArraySource<T1> source, Func<object, ArraySource<T2>> source2Selector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, R1, R2>(source, source2Selector(default), resultSelector);


        public static IArrayQuery2<T1, T2, R1, R2> SelectMany<T1, T2, _, R1, R2>(this IArrayQuery<T1, _> source, Func<object, T2[,]> source2Selector, Expression<Func<_, Cell<T2>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, R1, R2>(source, source2Selector(default).Wrap(), kernel);

        public static IArrayQuery2<T1, T2, R1, R2> SelectMany<T1, T2, _, R1, R2>(this IArrayQuery<T1, _> source, Func<object, ArraySource<T2>> source2Selector, Expression<Func<_, Cell<T2>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, R1, R2>(source, source2Selector(default), kernel);

        public static IArrayQuery2<T1, T2, R1, R2> SelectMany<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, R1, R2>(source, recurrentResult1Selector(default).InitValue, resultSelector);

//        public static IArrayQueryRecurrent<T1, T2, _, R1> SelectMany<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2)>> resultSelector)
//            => new ArrayQueryRecurrent<T1, T2, _, R1>(source, recurrentResult1Selector(default).InitValue, resultSelector);
        #endregion

        #region 2 arrays, 3 results

        public static IArrayQuery3<T1, T2, R1, R2, R3> Select<T1, T2, _, R1, R2, R3>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, R1, R2, R3>(source, resultSelector);

        public static IArrayQuery3<T1, T2, R1, R2, R3> SelectMany<T1, T2, R1, R2, R3>(this T1[,] source, Func<object, T2[,]> source2Selector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, R1, R2, R3>(source.Wrap(), source2Selector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, R1, R2, R3> SelectMany<T1, T2, R1, R2, R3>(this T1[,] source, Func<object, ArraySource<T2>> source2Selector, Expression<Func<Cell<T1>, Cell<T2>,  (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, R1, R2, R3>(source.Wrap(), source2Selector(default), resultSelector);
        public static IArrayQuery3<T1, T2, R1, R2, R3> SelectMany<T1, T2, R1, R2, R3>(this ArraySource<T1> source, Func<object, T2[,]> source2Selector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T1, T2, R1, R2, R3>(source, source2Selector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, R1, R2, R3> SelectMany<T1, T2, R1, R2, R3>(this ArraySource<T1> source, Func<object, ArraySource<T2>> source2Selector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, R1, R2, R3>(source, source2Selector(default), resultSelector);


        public static IArrayQuery3<T1, T2, R1, R2, R3> SelectMany<T1, T2, _, R1, R2, R3>(this IArrayQuery<T1, _> source, Func<object, T2[,]> source2Selector, Expression<Func<_, Cell<T2>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, R1, R2, R3>(source, source2Selector(default).Wrap(), kernel);

        public static IArrayQuery3<T1, T2, R1, R2, R3> SelectMany<T1, T2, _, R1, R2, R3>(this IArrayQuery<T1, _> source, Func<object, ArraySource<T2>> source2Selector, Expression<Func<_, Cell<T2>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, R1, R2, R3>(source, source2Selector(default), kernel);

        public static IArrayQuery3<T1, T2, R1, R2, R3> SelectMany<T1, T2, _, R1, R2, R3>(this IArrayQuery<T1, T2, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, R1, R2, R3>(source, recurrentResult1Selector(default).InitValue, resultSelector);

//        public static IArrayQueryRecurrent2<T1, T2, _, R1, R2> SelectMany<T1, T2, _, R1, R2, R3>(this IArrayQuery<T1, T2, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3)>> resultSelector)
//            => new ArrayQueryRecurrent2<T1, T2, _, R1, R2>(source, recurrentResult1Selector(default).InitValue, resultSelector);
        #endregion

        #region 2 arrays, 4 results

        public static IArrayQuery4<T1, T2, R1, R2, R3, R4> Select<T1, T2, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, R1, R2, R3, R4>(source, resultSelector);

        public static IArrayQuery4<T1, T2, R1, R2, R3, R4> SelectMany<T1, T2, R1, R2, R3, R4>(this T1[,] source, Func<object, T2[,]> source2Selector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, R1, R2, R3, R4>(source.Wrap(), source2Selector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, R1, R2, R3, R4> SelectMany<T1, T2, R1, R2, R3, R4>(this T1[,] source, Func<object, ArraySource<T2>> source2Selector, Expression<Func<Cell<T1>, Cell<T2>,  (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, R1, R2, R3, R4>(source.Wrap(), source2Selector(default), resultSelector);
        public static IArrayQuery4<T1, T2, R1, R2, R3, R4> SelectMany<T1, T2, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, T2[,]> source2Selector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2, R3, R4)>> resultSelector) 
            => new ArrayQuery4<T1, T2, R1, R2, R3, R4>(source, source2Selector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, R1, R2, R3, R4> SelectMany<T1, T2, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, ArraySource<T2>> source2Selector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, R1, R2, R3, R4>(source, source2Selector(default), resultSelector);


        public static IArrayQuery4<T1, T2, R1, R2, R3, R4> SelectMany<T1, T2, _, R1, R2, R3, R4>(this IArrayQuery<T1, _> source, Func<object, T2[,]> source2Selector, Expression<Func<_, Cell<T2>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, R1, R2, R3, R4>(source, source2Selector(default).Wrap(), kernel);

        public static IArrayQuery4<T1, T2, R1, R2, R3, R4> SelectMany<T1, T2, _, R1, R2, R3, R4>(this IArrayQuery<T1, _> source, Func<object, ArraySource<T2>> source2Selector, Expression<Func<_, Cell<T2>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, R1, R2, R3, R4>(source, source2Selector(default), kernel);

        public static IArrayQuery4<T1, T2, R1, R2, R3, R4> SelectMany<T1, T2, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, R1, R2, R3, R4>(source, recurrentResult1Selector(default).InitValue, resultSelector);

//        public static IArrayQueryRecurrent3<T1, T2, _, R1, R2, R3> SelectMany<T1, T2, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3, R4)>> resultSelector)
//            => new ArrayQueryRecurrent3<T1, T2, _, R1, R2, R3>(source, recurrentResult1Selector(default).InitValue, resultSelector);
        #endregion

        #endregion

        #region 3 arrays

        #region 3 arrays, 1 result

        public static IArrayQuery<T1, T2, T3, R> Select<T1, T2, T3, _, R>(this IArrayQuery<T1, T2, T3, _> source, Expression<Func<_, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, R>(source, resultSelector);


        public static IArrayQuery<T1, T2, T3, R> SelectMany<T1, T2, T3, _, R>(this IArrayQuery<T1, T2, _> source, Func<object, T3[,]> source3Selector, Expression<Func<_, Cell<T3>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, R>(source, source3Selector(default).Wrap(), kernel);

        public static IArrayQuery<T1, T2, T3, R> SelectMany<T1, T2, T3, _, R>(this IArrayQuery<T1, T2, _> source, Func<object, ArraySource<T3>> source3Selector, Expression<Func<_, Cell<T3>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, R>(source, source3Selector(default), kernel);

        #endregion

        #region 3 arrays, 2 results

        public static IArrayQuery2<T1, T2, T3, R1, R2> Select<T1, T2, T3, _, R1, R2>(this IArrayQuery<T1, T2, T3, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, R1, R2>(source, resultSelector);


        public static IArrayQuery2<T1, T2, T3, R1, R2> SelectMany<T1, T2, T3, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Func<object, T3[,]> source3Selector, Expression<Func<_, Cell<T3>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, R1, R2>(source, source3Selector(default).Wrap(), kernel);

        public static IArrayQuery2<T1, T2, T3, R1, R2> SelectMany<T1, T2, T3, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Func<object, ArraySource<T3>> source3Selector, Expression<Func<_, Cell<T3>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, R1, R2>(source, source3Selector(default), kernel);

        public static IArrayQuery2<T1, T2, T3, R1, R2> SelectMany<T1, T2, T3, _, R1, R2>(this IArrayQuery<T1, T2, T3, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, R1, R2>(source, recurrentResult1Selector(default).InitValue, resultSelector);

//        public static IArrayQueryRecurrent<T1, T2, T3, _, R1> SelectMany<T1, T2, T3, _, R1, R2>(this IArrayQuery<T1, T2, T3, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2)>> resultSelector)
//            => new ArrayQueryRecurrent<T1, T2, T3, _, R1>(source, recurrentResult1Selector(default).InitValue, resultSelector);
        #endregion

        #region 3 arrays, 3 results

        public static IArrayQuery3<T1, T2, T3, R1, R2, R3> Select<T1, T2, T3, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, _> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(source, resultSelector);


        public static IArrayQuery3<T1, T2, T3, R1, R2, R3> SelectMany<T1, T2, T3, _, R1, R2, R3>(this IArrayQuery<T1, T2, _> source, Func<object, T3[,]> source3Selector, Expression<Func<_, Cell<T3>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(source, source3Selector(default).Wrap(), kernel);

        public static IArrayQuery3<T1, T2, T3, R1, R2, R3> SelectMany<T1, T2, T3, _, R1, R2, R3>(this IArrayQuery<T1, T2, _> source, Func<object, ArraySource<T3>> source3Selector, Expression<Func<_, Cell<T3>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(source, source3Selector(default), kernel);

        public static IArrayQuery3<T1, T2, T3, R1, R2, R3> SelectMany<T1, T2, T3, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(source, recurrentResult1Selector(default).InitValue, resultSelector);

//        public static IArrayQueryRecurrent2<T1, T2, T3, _, R1, R2> SelectMany<T1, T2, T3, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3)>> resultSelector)
//            => new ArrayQueryRecurrent2<T1, T2, T3, _, R1, R2>(source, recurrentResult1Selector(default).InitValue, resultSelector);
        #endregion

        #region 3 arrays, 4 results

        public static IArrayQuery4<T1, T2, T3, R1, R2, R3, R4> Select<T1, T2, T3, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, _> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(source, resultSelector);


        public static IArrayQuery4<T1, T2, T3, R1, R2, R3, R4> SelectMany<T1, T2, T3, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, _> source, Func<object, T3[,]> source3Selector, Expression<Func<_, Cell<T3>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(source, source3Selector(default).Wrap(), kernel);

        public static IArrayQuery4<T1, T2, T3, R1, R2, R3, R4> SelectMany<T1, T2, T3, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, _> source, Func<object, ArraySource<T3>> source3Selector, Expression<Func<_, Cell<T3>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(source, source3Selector(default), kernel);

        public static IArrayQuery4<T1, T2, T3, R1, R2, R3, R4> SelectMany<T1, T2, T3, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(source, recurrentResult1Selector(default).InitValue, resultSelector);

//        public static IArrayQueryRecurrent3<T1, T2, T3, _, R1, R2, R3> SelectMany<T1, T2, T3, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3, R4)>> resultSelector)
//            => new ArrayQueryRecurrent3<T1, T2, T3, _, R1, R2, R3>(source, recurrentResult1Selector(default).InitValue, resultSelector);
        #endregion

        #endregion

        #region 4 arrays

        #region 4 arrays, 1 result

        public static IArrayQuery<T1, T2, T3, T4, R> Select<T1, T2, T3, T4, _, R>(this IArrayQuery<T1, T2, T3, T4, _> source, Expression<Func<_, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, R>(source, resultSelector);


        public static IArrayQuery<T1, T2, T3, T4, R> SelectMany<T1, T2, T3, T4, _, R>(this IArrayQuery<T1, T2, T3, _> source, Func<object, T4[,]> source4Selector, Expression<Func<_, Cell<T4>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, R>(source, source4Selector(default).Wrap(), kernel);

        public static IArrayQuery<T1, T2, T3, T4, R> SelectMany<T1, T2, T3, T4, _, R>(this IArrayQuery<T1, T2, T3, _> source, Func<object, ArraySource<T4>> source4Selector, Expression<Func<_, Cell<T4>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, R>(source, source4Selector(default), kernel);

        #endregion

        #region 4 arrays, 2 results

        public static IArrayQuery2<T1, T2, T3, T4, R1, R2> Select<T1, T2, T3, T4, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, R1, R2>(source, resultSelector);


        public static IArrayQuery2<T1, T2, T3, T4, R1, R2> SelectMany<T1, T2, T3, T4, _, R1, R2>(this IArrayQuery<T1, T2, T3, _> source, Func<object, T4[,]> source4Selector, Expression<Func<_, Cell<T4>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, R1, R2>(source, source4Selector(default).Wrap(), kernel);

        public static IArrayQuery2<T1, T2, T3, T4, R1, R2> SelectMany<T1, T2, T3, T4, _, R1, R2>(this IArrayQuery<T1, T2, T3, _> source, Func<object, ArraySource<T4>> source4Selector, Expression<Func<_, Cell<T4>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, R1, R2>(source, source4Selector(default), kernel);

        public static IArrayQuery2<T1, T2, T3, T4, R1, R2> SelectMany<T1, T2, T3, T4, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, R1, R2>(source, recurrentResult1Selector(default).InitValue, resultSelector);

//        public static IArrayQueryRecurrent<T1, T2, T3, T4, _, R1> SelectMany<T1, T2, T3, T4, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2)>> resultSelector)
//            => new ArrayQueryRecurrent<T1, T2, T3, T4, _, R1>(source, recurrentResult1Selector(default).InitValue, resultSelector);
        #endregion

        #region 4 arrays, 3 results

        public static IArrayQuery3<T1, T2, T3, T4, R1, R2, R3> Select<T1, T2, T3, T4, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, _> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(source, resultSelector);


        public static IArrayQuery3<T1, T2, T3, T4, R1, R2, R3> SelectMany<T1, T2, T3, T4, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, _> source, Func<object, T4[,]> source4Selector, Expression<Func<_, Cell<T4>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(source, source4Selector(default).Wrap(), kernel);

        public static IArrayQuery3<T1, T2, T3, T4, R1, R2, R3> SelectMany<T1, T2, T3, T4, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, _> source, Func<object, ArraySource<T4>> source4Selector, Expression<Func<_, Cell<T4>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(source, source4Selector(default), kernel);

        public static IArrayQuery3<T1, T2, T3, T4, R1, R2, R3> SelectMany<T1, T2, T3, T4, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(source, recurrentResult1Selector(default).InitValue, resultSelector);

//        public static IArrayQueryRecurrent2<T1, T2, T3, T4, _, R1, R2> SelectMany<T1, T2, T3, T4, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3)>> resultSelector)
//            => new ArrayQueryRecurrent2<T1, T2, T3, T4, _, R1, R2>(source, recurrentResult1Selector(default).InitValue, resultSelector);
        #endregion

        #region 4 arrays, 4 results

        public static IArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4> Select<T1, T2, T3, T4, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, _> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(source, resultSelector);


        public static IArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, _> source, Func<object, T4[,]> source4Selector, Expression<Func<_, Cell<T4>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(source, source4Selector(default).Wrap(), kernel);

        public static IArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, _> source, Func<object, ArraySource<T4>> source4Selector, Expression<Func<_, Cell<T4>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(source, source4Selector(default), kernel);

        public static IArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(source, recurrentResult1Selector(default).InitValue, resultSelector);

//        public static IArrayQueryRecurrent3<T1, T2, T3, T4, _, R1, R2, R3> SelectMany<T1, T2, T3, T4, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, _> source, Func<object, Result<R1>> recurrentResult1Selector, Expression<Func<_, RelativeCell<R1>, (R1, R2, R3, R4)>> resultSelector)
//            => new ArrayQueryRecurrent3<T1, T2, T3, T4, _, R1, R2, R3>(source, recurrentResult1Selector(default).InitValue, resultSelector);
        #endregion

        #endregion

        public static ArraySource<T> With<T>(this T[,] source, T initValue) => new ArraySource<T>(source, initValue);
        public static ArraySource<T> With<T>(this T[,] source, OutOfBoundsStrategy<T> strategy) => new ArraySource<T>(source, strategy);
        public static ArraySource<T> With<T>(this T[,] source, OutOfBoundsStrategyUntyped strategy) => new ArraySource<T>(source, strategy);
    }

}
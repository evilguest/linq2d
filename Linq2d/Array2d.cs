using System;
using System.Linq.Expressions;

namespace Linq2d
{
//    public static class Array2d<T>
//    {
//        public static IArrayQuery<T, R> Cast<R>(T[,] source) 
//            => new ArrayQuery<T, R>(source.Wrap(), (t)=>(R)(object)t.Value);
//    }
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

        #region One array

        /// <summary>
        /// Plain select from the single 2d-array
        /// </summary>
        /// <typeparam name="T">Type of the source array element</typeparam>
        /// <typeparam name="R">Type of the return array element</typeparam>
        /// <param name="source">Source array</param>
        /// <param name="kernel">filter kernel, expressed in terms of the Cell<typeparamref name="T"/> type, i.e. allowing both direct and relative access to the "current cell" </param>
        /// <returns>Queryable array that can both be used for obtaining the filter result, and gettign the reusable transform function.</returns>
        /// <exception cref="IndexOutOfRangeException">Is thrown if the kernel attempts accessing elements outside of the array</exception>
        public static IArrayQuery<T, R> Select<T, R>(this T[,] source, Expression<Func<Cell<T>, R>> kernel) 
            => new ArrayQuery<T, R>(source.Wrap(), kernel);
        /// <summary>
        /// Plain select from the single 2d-array
        /// </summary>
        /// <typeparam name="T">Type of the source array element</typeparam>
        /// <typeparam name="R">Type of the return array element</typeparam>
        /// <param name="source">Wrapped source array. Allows to define the replacement value that's used when the kernel accesses an element outside of the source array.</param>
        /// <param name="kernel">filter kernel, expressed in terms of the Cell<typeparamref name="T"/> type, i.e. allowing both direct and relative access to the "current cell" </param>
        /// <returns>Queryable array that can both be used for obtaining the filter result, and gettign the reusable transform function.</returns>
        public static IArrayQuery<T, R> Select<T, R>(this ArraySource<T> source, Expression<Func<Cell<T>, R>> kernel) 
            => new ArrayQuery<T, R>(source, kernel);

        public static IArrayQuery<T, R> Select<T, _, R>(this IArrayQuery<T, _> source, Expression<Func<_, R>> kernel) 
            => new ArrayQuery<T, R>(source, kernel);

        #endregion

        #region One array recurrent
        public static IArrayTransform<T, R> SelectMany<T, R>(this T[,] source, Func<object, Result<R>> secondSelector, Expression<Func<Cell<T>, RelativeCell<R>, R>> resultSelector) 
            => new ArrayQuery<T, R>(source.Wrap(), secondSelector(default).InitValue, resultSelector);
        public static IArrayTransform<T, R> SelectMany<T, R>(this ArraySource<T> source, Func<object, Result<R>> secondSelector, Expression<Func<Cell<T>, RelativeCell<R>, R>> resultSelector)
            => new ArrayQuery<T, R>(source, secondSelector(default).InitValue, resultSelector);
        public static IArrayQueryRecurrent<T, R, A> SelectMany<T, R, A>(this T[,] source, Func<object, Result<R>> secondSelector, Expression<Func<Cell<T>, RelativeCell<R>, A>> resultSelector)
        where A: class
        where R: unmanaged
            => new ArrayQueryRecurrent<T, R, A>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQueryRecurrent<T, R, A> SelectMany<T, R, A>(this ArraySource<T> source, Func<object, Result<R>> secondSelector, Expression<Func<Cell<T>, RelativeCell<R>, A>> resultSelector)
        where A: class
        where R: unmanaged
            => new ArrayQueryRecurrent<T, R, A>(source, secondSelector(default), resultSelector);
        #endregion

        #region One array two results
        public static IArrayQuery2<T, R1, R2> Select<T, R1, R2>(this T[,] source, Expression<Func<Cell<T>, (R1, R2)>> kernel) 
            => new ArrayQuery2<T, R1, R2>(source.Wrap(), kernel);
        public static IArrayQuery2<T, R1, R2> Select<T, R1, R2>(this ArraySource<T> source, Expression<Func<Cell<T>, (R1, R2)>> kernel) 
            => new ArrayQuery2<T, R1, R2>(source, kernel);
        public static IArrayQuery2<T, R1, R2> Select<T, _, R1, R2>(this IArrayQuery<T, _> source, Expression<Func<_, (R1, R2)>> kernel) 
            => new ArrayQuery2<T, R1, R2>(source, kernel);
        #endregion

        #region One array two results recurrent one
        public static IArrayTransform2<T, R1, R2> SelectMany<T, R1, R2>(this T[,] source, Func<object, Result<R1>> secondSelector, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T, R1, R2>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayTransform2<T, R1, R2> SelectMany<T, R1, R2>(this ArraySource<T> source, Func<object, Result<R1>> secondSelector, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T, R1, R2>(source, secondSelector(default), resultSelector);
        #endregion

        #region One array two results recurrent two
        public static IArrayTransform2<T, R1, R2> SelectMany<T, _, R1, R2>(this IArrayQueryRecurrent<T, R1, _> source, Func<object, Result<R2>> secondSelector, Expression<Func<_, RelativeCell<R2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T, R1, R2>(source, secondSelector(default), resultSelector);
        #endregion

        #region One array three results
        public static IArrayTransform3<T, R1, R2, R3> Select<T, R1, R2, R3>(this T[,] source, Expression<Func<Cell<T>, (R1, R2, R3)>> kernel) 
            => new ArrayQuery3<T, R1, R2, R3>(source.Wrap(), kernel);
        public static IArrayQuery3<T, R1, R2, R3> Select<T, R1, R2, R3>(this ArraySource<T> source, Expression<Func<Cell<T>, (R1, R2, R3)>> kernel) 
            => new ArrayQuery3<T, R1, R2, R3>(source, kernel);
        public static IArrayQuery3<T, R1, R2, R3> Select<T, _, R1, R2, R3>(this IArrayQuery<T, _> source, Expression<Func<_, (R1, R2, R3)>> kernel) 
            => new ArrayQuery3<T, R1, R2, R3>(source, kernel);
        public static IArrayTransform3<T, R1, R2, R3> SelectMany<T, R1, R2, R3>(this T[,] source, Func<object, Result<R1>> secondSelector, Expression<Func<Cell<T>, RelativeCell<R1>, (R1, R2, R3)>> resultSelector) 
            => new ArrayQuery3<T, R1, R2, R3>(source.Wrap(), secondSelector(default), resultSelector);


        #endregion

        #region Two arrays
        #region Two arrays one result
        public static IArrayQuery<T1, T2, R> SelectMany<T1, T2, R>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, R>> resultSelector)
            => new ArrayQuery<T1, T2, R>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery<T1, T2, R> SelectMany<T1, T2, R>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, R>> resultSelector)
            => new ArrayQuery<T1, T2, R>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery<T1, T2, R> SelectMany<T1, T2, R>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, R>> resultSelector) 
            => new ArrayQuery<T1, T2, R>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery<T1, T2, R> SelectMany<T1, T2, R>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, R>> resultSelector)
            => new ArrayQuery<T1, T2, R>(source, secondSelector(default), resultSelector);

        public static IArrayQuery<T1, T2, R> Select<T1, T2, _, R>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, R>> resultSelector)
            => new ArrayQuery<T1, T2, R>(source, resultSelector);
        public static IArrayTransform<T1, T2, R> SelectMany<T1, T2, _, R>(this IArrayQuery<T1, T2, _> source, Func<object, Result<R>> secondSelector, Expression<Func<_, RelativeCell<R>, R>> resultSelector)
            => new ArrayQuery<T1, T2, R>(source, secondSelector(default), resultSelector);
        #endregion
        #region Two arrays two results
        public static IArrayQuery2<T1, T2, R1, R2> SelectMany<T1, T2, R1, R2>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, R1, R2>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, R1, R2> SelectMany<T1, T2, R1, R2>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, R1, R2>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery2<T1, T2, R1, R2> SelectMany<T1, T2, R1, R2>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, R1, R2>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, R1, R2> SelectMany<T1, T2, R1, R2>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, R1, R2>(source, secondSelector(default), resultSelector);

        public static IArrayQuery2<T1, T2, R1, R2> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, R1, R2>(source, resultSelector);
        public static IArrayTransform2<T1, T2, R1, R2> SelectMany<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Func<object, Result<R1>> secondSelector, Expression<Func<_, RelativeCell<R1>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, R1, R2>(source, secondSelector(default), resultSelector);
        #endregion
        #endregion

        #region 3 arrays
        public static IArrayQuery<T1, T2, T3, R> Select<T1, T2, T3, _, R>(this IArrayQuery<T1, T2, T3, _> source, Expression<Func<_, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, R>(source, resultSelector);

        public static IArrayQuery<T1, T2, T3, R> SelectMany<T1, T2, T3, _, R>(this IArrayQuery<T1, T2, _> source, Func<object, T3[,]> getSource, Expression<Func<_, Cell<T3>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, R>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery<T1, T2, T3, R> SelectMany<T1, T2, T3, _, R>(this IArrayQuery<T1, T2, _> source, Func<object, ArraySource<T3>> getSource, Expression<Func<_, Cell<T3>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, R>(source, getSource(default), kernel);

        public static IArrayTransform<T1, T2, T3, R> SelectMany<T1, T2, T3, _, R>(this IArrayQuery<T1, T2, T3, _> source, Func<object, Result<R>> secondSelector, Expression<Func<_, RelativeCell<R>, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, R>(source, secondSelector(default), resultSelector);

        #region 3 arrays, 2 results

        public static IArrayQuery2<T1, T2, T3, R1, R2> Select<T1, T2, T3, _, R1, R2>(this IArrayQuery<T1, T2, T3, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, R1, R2>(source, resultSelector);
        public static IArrayQuery2<T1, T2, T3, R1, R2> SelectMany<T1, T2, T3, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Func<object, T3[,]> getSource, Expression<Func<_, Cell<T3>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, R1, R2>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery2<T1, T2, T3, R1, R2> SelectMany<T1, T2, T3, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Func<object, ArraySource<T3>> getSource, Expression<Func<_, Cell<T3>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, R1, R2>(source, getSource(default), kernel);

/*
        public static IArrayQuery2<T1, T2, T3, R1, R2> SelectMany<T1, T2, T3, R1, R2>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, R1, R2>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, R1, R2> SelectMany<T1, T2, T3, R1, R2>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, R1, R2>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery2<T1, T2, T3, R1, R2> SelectMany<T1, T2, T3, R1, R2>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, R1, R2>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, R1, R2> SelectMany<T1, T2, T3, R1, R2>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, R1, R2>(source, secondSelector(default), resultSelector);

        public static IArrayQuery2<T1, T2, T3, R1, R2> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, R1, R2>(source, resultSelector);
*/
        #endregion
        #region 3 arrays, 3 results

        public static IArrayQuery3<T1, T2, T3, R1, R2, R3> Select<T1, T2, T3, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, _> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(source, resultSelector);
        public static IArrayQuery3<T1, T2, T3, R1, R2, R3> SelectMany<T1, T2, T3, _, R1, R2, R3>(this IArrayQuery<T1, T2, _> source, Func<object, T3[,]> getSource, Expression<Func<_, Cell<T3>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery3<T1, T2, T3, R1, R2, R3> SelectMany<T1, T2, T3, _, R1, R2, R3>(this IArrayQuery<T1, T2, _> source, Func<object, ArraySource<T3>> getSource, Expression<Func<_, Cell<T3>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(source, getSource(default), kernel);

/*
        public static IArrayQuery3<T1, T2, T3, R1, R2, R3> SelectMany<T1, T2, T3, R1, R2, R3>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, R1, R2, R3> SelectMany<T1, T2, T3, R1, R2, R3>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery3<T1, T2, T3, R1, R2, R3> SelectMany<T1, T2, T3, R1, R2, R3>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, R1, R2, R3> SelectMany<T1, T2, T3, R1, R2, R3>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(source, secondSelector(default), resultSelector);

        public static IArrayQuery3<T1, T2, T3, R1, R2, R3> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, R1, R2, R3>(source, resultSelector);
*/
        #endregion
        #region 3 arrays, 4 results

        public static IArrayQuery4<T1, T2, T3, R1, R2, R3, R4> Select<T1, T2, T3, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, _> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(source, resultSelector);
        public static IArrayQuery4<T1, T2, T3, R1, R2, R3, R4> SelectMany<T1, T2, T3, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, _> source, Func<object, T3[,]> getSource, Expression<Func<_, Cell<T3>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery4<T1, T2, T3, R1, R2, R3, R4> SelectMany<T1, T2, T3, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, _> source, Func<object, ArraySource<T3>> getSource, Expression<Func<_, Cell<T3>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(source, getSource(default), kernel);

/*
        public static IArrayQuery4<T1, T2, T3, R1, R2, R3, R4> SelectMany<T1, T2, T3, R1, R2, R3, R4>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, R1, R2, R3, R4> SelectMany<T1, T2, T3, R1, R2, R3, R4>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery4<T1, T2, T3, R1, R2, R3, R4> SelectMany<T1, T2, T3, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, R1, R2, R3, R4> SelectMany<T1, T2, T3, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(source, secondSelector(default), resultSelector);

        public static IArrayQuery4<T1, T2, T3, R1, R2, R3, R4> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, R1, R2, R3, R4>(source, resultSelector);
*/
        #endregion
        #endregion

        #region 4 arrays
        public static IArrayQuery<T1, T2, T3, T4, R> Select<T1, T2, T3, T4, _, R>(this IArrayQuery<T1, T2, T3, T4, _> source, Expression<Func<_, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, R>(source, resultSelector);

        public static IArrayQuery<T1, T2, T3, T4, R> SelectMany<T1, T2, T3, T4, _, R>(this IArrayQuery<T1, T2, T3, _> source, Func<object, T4[,]> getSource, Expression<Func<_, Cell<T4>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, R>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery<T1, T2, T3, T4, R> SelectMany<T1, T2, T3, T4, _, R>(this IArrayQuery<T1, T2, T3, _> source, Func<object, ArraySource<T4>> getSource, Expression<Func<_, Cell<T4>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, R>(source, getSource(default), kernel);

        public static IArrayTransform<T1, T2, T3, T4, R> SelectMany<T1, T2, T3, T4, _, R>(this IArrayQuery<T1, T2, T3, T4, _> source, Func<object, Result<R>> secondSelector, Expression<Func<_, RelativeCell<R>, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, R>(source, secondSelector(default), resultSelector);

        #region 4 arrays, 2 results

        public static IArrayQuery2<T1, T2, T3, T4, R1, R2> Select<T1, T2, T3, T4, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, R1, R2>(source, resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, R1, R2> SelectMany<T1, T2, T3, T4, _, R1, R2>(this IArrayQuery<T1, T2, T3, _> source, Func<object, T4[,]> getSource, Expression<Func<_, Cell<T4>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, R1, R2>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery2<T1, T2, T3, T4, R1, R2> SelectMany<T1, T2, T3, T4, _, R1, R2>(this IArrayQuery<T1, T2, T3, _> source, Func<object, ArraySource<T4>> getSource, Expression<Func<_, Cell<T4>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, R1, R2>(source, getSource(default), kernel);

/*
        public static IArrayQuery2<T1, T2, T3, T4, R1, R2> SelectMany<T1, T2, T3, T4, R1, R2>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, R1, R2>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, R1, R2> SelectMany<T1, T2, T3, T4, R1, R2>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, R1, R2>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, R1, R2> SelectMany<T1, T2, T3, T4, R1, R2>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, R1, R2>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, R1, R2> SelectMany<T1, T2, T3, T4, R1, R2>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, R1, R2>(source, secondSelector(default), resultSelector);

        public static IArrayQuery2<T1, T2, T3, T4, R1, R2> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, R1, R2>(source, resultSelector);
*/
        #endregion
        #region 4 arrays, 3 results

        public static IArrayQuery3<T1, T2, T3, T4, R1, R2, R3> Select<T1, T2, T3, T4, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, _> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(source, resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, R1, R2, R3> SelectMany<T1, T2, T3, T4, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, _> source, Func<object, T4[,]> getSource, Expression<Func<_, Cell<T4>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery3<T1, T2, T3, T4, R1, R2, R3> SelectMany<T1, T2, T3, T4, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, _> source, Func<object, ArraySource<T4>> getSource, Expression<Func<_, Cell<T4>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(source, getSource(default), kernel);

/*
        public static IArrayQuery3<T1, T2, T3, T4, R1, R2, R3> SelectMany<T1, T2, T3, T4, R1, R2, R3>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, R1, R2, R3> SelectMany<T1, T2, T3, T4, R1, R2, R3>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, R1, R2, R3> SelectMany<T1, T2, T3, T4, R1, R2, R3>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, R1, R2, R3> SelectMany<T1, T2, T3, T4, R1, R2, R3>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(source, secondSelector(default), resultSelector);

        public static IArrayQuery3<T1, T2, T3, T4, R1, R2, R3> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>(source, resultSelector);
*/
        #endregion
        #region 4 arrays, 4 results

        public static IArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4> Select<T1, T2, T3, T4, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, _> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(source, resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, _> source, Func<object, T4[,]> getSource, Expression<Func<_, Cell<T4>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, _> source, Func<object, ArraySource<T4>> getSource, Expression<Func<_, Cell<T4>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(source, getSource(default), kernel);

/*
        public static IArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, R1, R2, R3, R4>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, R1, R2, R3, R4>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(source, secondSelector(default), resultSelector);

        public static IArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4>(source, resultSelector);
*/
        #endregion
        #endregion

        #region 5 arrays
        public static IArrayQuery<T1, T2, T3, T4, T5, R> Select<T1, T2, T3, T4, T5, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, _> source, Expression<Func<_, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, R>(source, resultSelector);

        public static IArrayQuery<T1, T2, T3, T4, T5, R> SelectMany<T1, T2, T3, T4, T5, _, R>(this IArrayQuery<T1, T2, T3, T4, _> source, Func<object, T5[,]> getSource, Expression<Func<_, Cell<T5>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, R>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery<T1, T2, T3, T4, T5, R> SelectMany<T1, T2, T3, T4, T5, _, R>(this IArrayQuery<T1, T2, T3, T4, _> source, Func<object, ArraySource<T5>> getSource, Expression<Func<_, Cell<T5>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, R>(source, getSource(default), kernel);

        public static IArrayTransform<T1, T2, T3, T4, T5, R> SelectMany<T1, T2, T3, T4, T5, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, _> source, Func<object, Result<R>> secondSelector, Expression<Func<_, RelativeCell<R>, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, R>(source, secondSelector(default), resultSelector);

        #region 5 arrays, 2 results

        public static IArrayQuery2<T1, T2, T3, T4, T5, R1, R2> Select<T1, T2, T3, T4, T5, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, R1, R2>(source, resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, R1, R2> SelectMany<T1, T2, T3, T4, T5, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, _> source, Func<object, T5[,]> getSource, Expression<Func<_, Cell<T5>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, R1, R2>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery2<T1, T2, T3, T4, T5, R1, R2> SelectMany<T1, T2, T3, T4, T5, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, _> source, Func<object, ArraySource<T5>> getSource, Expression<Func<_, Cell<T5>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, R1, R2>(source, getSource(default), kernel);

/*
        public static IArrayQuery2<T1, T2, T3, T4, T5, R1, R2> SelectMany<T1, T2, T3, T4, T5, R1, R2>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, R1, R2>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, R1, R2> SelectMany<T1, T2, T3, T4, T5, R1, R2>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, R1, R2>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, R1, R2> SelectMany<T1, T2, T3, T4, T5, R1, R2>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, R1, R2>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, R1, R2> SelectMany<T1, T2, T3, T4, T5, R1, R2>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, R1, R2>(source, secondSelector(default), resultSelector);

        public static IArrayQuery2<T1, T2, T3, T4, T5, R1, R2> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, R1, R2>(source, resultSelector);
*/
        #endregion
        #region 5 arrays, 3 results

        public static IArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3> Select<T1, T2, T3, T4, T5, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, _> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3>(source, resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, _> source, Func<object, T5[,]> getSource, Expression<Func<_, Cell<T5>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, _> source, Func<object, ArraySource<T5>> getSource, Expression<Func<_, Cell<T5>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3>(source, getSource(default), kernel);

/*
        public static IArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, R1, R2, R3>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, R1, R2, R3>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, R1, R2, R3>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, R1, R2, R3>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3>(source, secondSelector(default), resultSelector);

        public static IArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3>(source, resultSelector);
*/
        #endregion
        #region 5 arrays, 4 results

        public static IArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, _> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>(source, resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, _> source, Func<object, T5[,]> getSource, Expression<Func<_, Cell<T5>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, _> source, Func<object, ArraySource<T5>> getSource, Expression<Func<_, Cell<T5>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>(source, getSource(default), kernel);

/*
        public static IArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, R1, R2, R3, R4>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, R1, R2, R3, R4>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>(source, secondSelector(default), resultSelector);

        public static IArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4>(source, resultSelector);
*/
        #endregion
        #endregion

        #region 6 arrays
        public static IArrayQuery<T1, T2, T3, T4, T5, T6, R> Select<T1, T2, T3, T4, T5, T6, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> source, Expression<Func<_, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, R>(source, resultSelector);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, R> SelectMany<T1, T2, T3, T4, T5, T6, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, _> source, Func<object, T6[,]> getSource, Expression<Func<_, Cell<T6>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, R>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, R> SelectMany<T1, T2, T3, T4, T5, T6, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, _> source, Func<object, ArraySource<T6>> getSource, Expression<Func<_, Cell<T6>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, R>(source, getSource(default), kernel);

        public static IArrayTransform<T1, T2, T3, T4, T5, T6, R> SelectMany<T1, T2, T3, T4, T5, T6, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> source, Func<object, Result<R>> secondSelector, Expression<Func<_, RelativeCell<R>, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, R>(source, secondSelector(default), resultSelector);

        #region 6 arrays, 2 results

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2> Select<T1, T2, T3, T4, T5, T6, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2>(source, resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, _> source, Func<object, T6[,]> getSource, Expression<Func<_, Cell<T6>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, _> source, Func<object, ArraySource<T6>> getSource, Expression<Func<_, Cell<T6>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2>(source, getSource(default), kernel);

/*
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, R1, R2>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, R1, R2>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, R1, R2>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, R1, R2>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2>(source, secondSelector(default), resultSelector);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2>(source, resultSelector);
*/
        #endregion
        #region 6 arrays, 3 results

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3>(source, resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, _> source, Func<object, T6[,]> getSource, Expression<Func<_, Cell<T6>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, _> source, Func<object, ArraySource<T6>> getSource, Expression<Func<_, Cell<T6>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3>(source, getSource(default), kernel);

/*
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, R1, R2, R3>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, R1, R2, R3>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, R1, R2, R3>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, R1, R2, R3>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3>(source, secondSelector(default), resultSelector);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3>(source, resultSelector);
*/
        #endregion
        #region 6 arrays, 4 results

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(source, resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, _> source, Func<object, T6[,]> getSource, Expression<Func<_, Cell<T6>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, _> source, Func<object, ArraySource<T6>> getSource, Expression<Func<_, Cell<T6>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(source, getSource(default), kernel);

/*
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(source, secondSelector(default), resultSelector);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>(source, resultSelector);
*/
        #endregion
        #endregion

        #region 7 arrays
        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, R> Select<T1, T2, T3, T4, T5, T6, T7, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> source, Expression<Func<_, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, R>(source, resultSelector);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> source, Func<object, T7[,]> getSource, Expression<Func<_, Cell<T7>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, R>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> source, Func<object, ArraySource<T7>> getSource, Expression<Func<_, Cell<T7>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, R>(source, getSource(default), kernel);

        public static IArrayTransform<T1, T2, T3, T4, T5, T6, T7, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> source, Func<object, Result<R>> secondSelector, Expression<Func<_, RelativeCell<R>, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, R>(source, secondSelector(default), resultSelector);

        #region 7 arrays, 2 results

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2> Select<T1, T2, T3, T4, T5, T6, T7, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2>(source, resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> source, Func<object, T7[,]> getSource, Expression<Func<_, Cell<T7>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> source, Func<object, ArraySource<T7>> getSource, Expression<Func<_, Cell<T7>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2>(source, getSource(default), kernel);

/*
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, R1, R2>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, R1, R2>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, R1, R2>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, R1, R2>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2>(source, secondSelector(default), resultSelector);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2>(source, resultSelector);
*/
        #endregion
        #region 7 arrays, 3 results

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(source, resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> source, Func<object, T7[,]> getSource, Expression<Func<_, Cell<T7>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> source, Func<object, ArraySource<T7>> getSource, Expression<Func<_, Cell<T7>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(source, getSource(default), kernel);

/*
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(source, secondSelector(default), resultSelector);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>(source, resultSelector);
*/
        #endregion
        #region 7 arrays, 4 results

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(source, resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> source, Func<object, T7[,]> getSource, Expression<Func<_, Cell<T7>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, _> source, Func<object, ArraySource<T7>> getSource, Expression<Func<_, Cell<T7>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(source, getSource(default), kernel);

/*
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(source, secondSelector(default), resultSelector);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>(source, resultSelector);
*/
        #endregion
        #endregion

        #region 8 arrays
        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, R> Select<T1, T2, T3, T4, T5, T6, T7, T8, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, _> source, Expression<Func<_, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, R>(source, resultSelector);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> source, Func<object, T8[,]> getSource, Expression<Func<_, Cell<T8>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, R>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> source, Func<object, ArraySource<T8>> getSource, Expression<Func<_, Cell<T8>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, R>(source, getSource(default), kernel);

        public static IArrayTransform<T1, T2, T3, T4, T5, T6, T7, T8, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, _> source, Func<object, Result<R>> secondSelector, Expression<Func<_, RelativeCell<R>, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, R>(source, secondSelector(default), resultSelector);

        #region 8 arrays, 2 results

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2> Select<T1, T2, T3, T4, T5, T6, T7, T8, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2>(source, resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> source, Func<object, T8[,]> getSource, Expression<Func<_, Cell<T8>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> source, Func<object, ArraySource<T8>> getSource, Expression<Func<_, Cell<T8>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2>(source, getSource(default), kernel);

/*
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2>(source, secondSelector(default), resultSelector);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2>(source, resultSelector);
*/
        #endregion
        #region 8 arrays, 3 results

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, T7, T8, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, _> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3>(source, resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> source, Func<object, T8[,]> getSource, Expression<Func<_, Cell<T8>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> source, Func<object, ArraySource<T8>> getSource, Expression<Func<_, Cell<T8>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3>(source, getSource(default), kernel);

/*
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3>(source, secondSelector(default), resultSelector);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3>(source, resultSelector);
*/
        #endregion
        #region 8 arrays, 4 results

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, T7, T8, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, _> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4>(source, resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> source, Func<object, T8[,]> getSource, Expression<Func<_, Cell<T8>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, _> source, Func<object, ArraySource<T8>> getSource, Expression<Func<_, Cell<T8>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4>(source, getSource(default), kernel);

/*
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4>(source, secondSelector(default), resultSelector);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4>(source, resultSelector);
*/
        #endregion
        #endregion

        #region 9 arrays
        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, R> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, _> source, Expression<Func<_, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, R>(source, resultSelector);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, _> source, Func<object, T9[,]> getSource, Expression<Func<_, Cell<T9>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, R>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, _> source, Func<object, ArraySource<T9>> getSource, Expression<Func<_, Cell<T9>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, R>(source, getSource(default), kernel);

        public static IArrayTransform<T1, T2, T3, T4, T5, T6, T7, T8, T9, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, _> source, Func<object, Result<R>> secondSelector, Expression<Func<_, RelativeCell<R>, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, R>(source, secondSelector(default), resultSelector);

        #region 9 arrays, 2 results

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2>(source, resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, _> source, Func<object, T9[,]> getSource, Expression<Func<_, Cell<T9>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, _> source, Func<object, ArraySource<T9>> getSource, Expression<Func<_, Cell<T9>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2>(source, getSource(default), kernel);

/*
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2>(source, secondSelector(default), resultSelector);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2>(source, resultSelector);
*/
        #endregion
        #region 9 arrays, 3 results

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, _> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3>(source, resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, _> source, Func<object, T9[,]> getSource, Expression<Func<_, Cell<T9>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, _> source, Func<object, ArraySource<T9>> getSource, Expression<Func<_, Cell<T9>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3>(source, getSource(default), kernel);

/*
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3>(source, secondSelector(default), resultSelector);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3>(source, resultSelector);
*/
        #endregion
        #region 9 arrays, 4 results

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, _> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4>(source, resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, _> source, Func<object, T9[,]> getSource, Expression<Func<_, Cell<T9>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, _> source, Func<object, ArraySource<T9>> getSource, Expression<Func<_, Cell<T9>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4>(source, getSource(default), kernel);

/*
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4>(source, secondSelector(default), resultSelector);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4>(source, resultSelector);
*/
        #endregion
        #endregion

        #region 10 arrays
        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _> source, Expression<Func<_, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R>(source, resultSelector);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, _> source, Func<object, T10[,]> getSource, Expression<Func<_, Cell<T10>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, _> source, Func<object, ArraySource<T10>> getSource, Expression<Func<_, Cell<T10>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R>(source, getSource(default), kernel);

        public static IArrayTransform<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _> source, Func<object, Result<R>> secondSelector, Expression<Func<_, RelativeCell<R>, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R>(source, secondSelector(default), resultSelector);

        #region 10 arrays, 2 results

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2>(source, resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, _> source, Func<object, T10[,]> getSource, Expression<Func<_, Cell<T10>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, _> source, Func<object, ArraySource<T10>> getSource, Expression<Func<_, Cell<T10>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2>(source, getSource(default), kernel);

/*
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2>(source, secondSelector(default), resultSelector);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2>(source, resultSelector);
*/
        #endregion
        #region 10 arrays, 3 results

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3>(source, resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, _> source, Func<object, T10[,]> getSource, Expression<Func<_, Cell<T10>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, _> source, Func<object, ArraySource<T10>> getSource, Expression<Func<_, Cell<T10>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3>(source, getSource(default), kernel);

/*
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3>(source, secondSelector(default), resultSelector);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3>(source, resultSelector);
*/
        #endregion
        #region 10 arrays, 4 results

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4>(source, resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, _> source, Func<object, T10[,]> getSource, Expression<Func<_, Cell<T10>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, _> source, Func<object, ArraySource<T10>> getSource, Expression<Func<_, Cell<T10>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4>(source, getSource(default), kernel);

/*
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4>(source, secondSelector(default), resultSelector);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4>(source, resultSelector);
*/
        #endregion
        #endregion

        #region 11 arrays
        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _> source, Expression<Func<_, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R>(source, resultSelector);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _> source, Func<object, T11[,]> getSource, Expression<Func<_, Cell<T11>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _> source, Func<object, ArraySource<T11>> getSource, Expression<Func<_, Cell<T11>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R>(source, getSource(default), kernel);

        public static IArrayTransform<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _> source, Func<object, Result<R>> secondSelector, Expression<Func<_, RelativeCell<R>, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R>(source, secondSelector(default), resultSelector);

        #region 11 arrays, 2 results

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2>(source, resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _> source, Func<object, T11[,]> getSource, Expression<Func<_, Cell<T11>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _> source, Func<object, ArraySource<T11>> getSource, Expression<Func<_, Cell<T11>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2>(source, getSource(default), kernel);

/*
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2>(source, secondSelector(default), resultSelector);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2>(source, resultSelector);
*/
        #endregion
        #region 11 arrays, 3 results

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3>(source, resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _> source, Func<object, T11[,]> getSource, Expression<Func<_, Cell<T11>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _> source, Func<object, ArraySource<T11>> getSource, Expression<Func<_, Cell<T11>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3>(source, getSource(default), kernel);

/*
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3>(source, secondSelector(default), resultSelector);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3>(source, resultSelector);
*/
        #endregion
        #region 11 arrays, 4 results

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4>(source, resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _> source, Func<object, T11[,]> getSource, Expression<Func<_, Cell<T11>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, _> source, Func<object, ArraySource<T11>> getSource, Expression<Func<_, Cell<T11>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4>(source, getSource(default), kernel);

/*
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4>(source, secondSelector(default), resultSelector);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4>(source, resultSelector);
*/
        #endregion
        #endregion

        #region 12 arrays
        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _> source, Expression<Func<_, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R>(source, resultSelector);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _> source, Func<object, T12[,]> getSource, Expression<Func<_, Cell<T12>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _> source, Func<object, ArraySource<T12>> getSource, Expression<Func<_, Cell<T12>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R>(source, getSource(default), kernel);

        public static IArrayTransform<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _> source, Func<object, Result<R>> secondSelector, Expression<Func<_, RelativeCell<R>, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R>(source, secondSelector(default), resultSelector);

        #region 12 arrays, 2 results

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2>(source, resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _> source, Func<object, T12[,]> getSource, Expression<Func<_, Cell<T12>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _> source, Func<object, ArraySource<T12>> getSource, Expression<Func<_, Cell<T12>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2>(source, getSource(default), kernel);

/*
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2>(source, secondSelector(default), resultSelector);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2>(source, resultSelector);
*/
        #endregion
        #region 12 arrays, 3 results

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3>(source, resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _> source, Func<object, T12[,]> getSource, Expression<Func<_, Cell<T12>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _> source, Func<object, ArraySource<T12>> getSource, Expression<Func<_, Cell<T12>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3>(source, getSource(default), kernel);

/*
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3>(source, secondSelector(default), resultSelector);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3>(source, resultSelector);
*/
        #endregion
        #region 12 arrays, 4 results

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4>(source, resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _> source, Func<object, T12[,]> getSource, Expression<Func<_, Cell<T12>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, _> source, Func<object, ArraySource<T12>> getSource, Expression<Func<_, Cell<T12>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4>(source, getSource(default), kernel);

/*
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4>(source, secondSelector(default), resultSelector);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4>(source, resultSelector);
*/
        #endregion
        #endregion

        #region 13 arrays
        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _> source, Expression<Func<_, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R>(source, resultSelector);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _> source, Func<object, T13[,]> getSource, Expression<Func<_, Cell<T13>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _> source, Func<object, ArraySource<T13>> getSource, Expression<Func<_, Cell<T13>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R>(source, getSource(default), kernel);

        public static IArrayTransform<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _> source, Func<object, Result<R>> secondSelector, Expression<Func<_, RelativeCell<R>, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R>(source, secondSelector(default), resultSelector);

        #region 13 arrays, 2 results

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2>(source, resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _> source, Func<object, T13[,]> getSource, Expression<Func<_, Cell<T13>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _> source, Func<object, ArraySource<T13>> getSource, Expression<Func<_, Cell<T13>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2>(source, getSource(default), kernel);

/*
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2>(source, secondSelector(default), resultSelector);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2>(source, resultSelector);
*/
        #endregion
        #region 13 arrays, 3 results

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3>(source, resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _> source, Func<object, T13[,]> getSource, Expression<Func<_, Cell<T13>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _> source, Func<object, ArraySource<T13>> getSource, Expression<Func<_, Cell<T13>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3>(source, getSource(default), kernel);

/*
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3>(source, secondSelector(default), resultSelector);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3>(source, resultSelector);
*/
        #endregion
        #region 13 arrays, 4 results

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4>(source, resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _> source, Func<object, T13[,]> getSource, Expression<Func<_, Cell<T13>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, _> source, Func<object, ArraySource<T13>> getSource, Expression<Func<_, Cell<T13>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4>(source, getSource(default), kernel);

/*
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4>(source, secondSelector(default), resultSelector);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4>(source, resultSelector);
*/
        #endregion
        #endregion

        #region 14 arrays
        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _> source, Expression<Func<_, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R>(source, resultSelector);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _> source, Func<object, T14[,]> getSource, Expression<Func<_, Cell<T14>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _> source, Func<object, ArraySource<T14>> getSource, Expression<Func<_, Cell<T14>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R>(source, getSource(default), kernel);

        public static IArrayTransform<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _> source, Func<object, Result<R>> secondSelector, Expression<Func<_, RelativeCell<R>, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R>(source, secondSelector(default), resultSelector);

        #region 14 arrays, 2 results

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2>(source, resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _> source, Func<object, T14[,]> getSource, Expression<Func<_, Cell<T14>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _> source, Func<object, ArraySource<T14>> getSource, Expression<Func<_, Cell<T14>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2>(source, getSource(default), kernel);

/*
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2>(source, secondSelector(default), resultSelector);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2>(source, resultSelector);
*/
        #endregion
        #region 14 arrays, 3 results

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3>(source, resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _> source, Func<object, T14[,]> getSource, Expression<Func<_, Cell<T14>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _> source, Func<object, ArraySource<T14>> getSource, Expression<Func<_, Cell<T14>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3>(source, getSource(default), kernel);

/*
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3>(source, secondSelector(default), resultSelector);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3>(source, resultSelector);
*/
        #endregion
        #region 14 arrays, 4 results

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4>(source, resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _> source, Func<object, T14[,]> getSource, Expression<Func<_, Cell<T14>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, _> source, Func<object, ArraySource<T14>> getSource, Expression<Func<_, Cell<T14>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4>(source, getSource(default), kernel);

/*
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4>(source, secondSelector(default), resultSelector);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4>(source, resultSelector);
*/
        #endregion
        #endregion

        #region 15 arrays
        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _> source, Expression<Func<_, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R>(source, resultSelector);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _> source, Func<object, T15[,]> getSource, Expression<Func<_, Cell<T15>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _> source, Func<object, ArraySource<T15>> getSource, Expression<Func<_, Cell<T15>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R>(source, getSource(default), kernel);

        public static IArrayTransform<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _> source, Func<object, Result<R>> secondSelector, Expression<Func<_, RelativeCell<R>, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R>(source, secondSelector(default), resultSelector);

        #region 15 arrays, 2 results

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2>(source, resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _> source, Func<object, T15[,]> getSource, Expression<Func<_, Cell<T15>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _> source, Func<object, ArraySource<T15>> getSource, Expression<Func<_, Cell<T15>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2>(source, getSource(default), kernel);

/*
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2>(source, secondSelector(default), resultSelector);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2>(source, resultSelector);
*/
        #endregion
        #region 15 arrays, 3 results

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3>(source, resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _> source, Func<object, T15[,]> getSource, Expression<Func<_, Cell<T15>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _> source, Func<object, ArraySource<T15>> getSource, Expression<Func<_, Cell<T15>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3>(source, getSource(default), kernel);

/*
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3>(source, secondSelector(default), resultSelector);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3>(source, resultSelector);
*/
        #endregion
        #region 15 arrays, 4 results

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4>(source, resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _> source, Func<object, T15[,]> getSource, Expression<Func<_, Cell<T15>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, _> source, Func<object, ArraySource<T15>> getSource, Expression<Func<_, Cell<T15>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4>(source, getSource(default), kernel);

/*
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4>(source, secondSelector(default), resultSelector);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4>(source, resultSelector);
*/
        #endregion
        #endregion

        #region 16 arrays
        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, _> source, Expression<Func<_, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R>(source, resultSelector);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _> source, Func<object, T16[,]> getSource, Expression<Func<_, Cell<T16>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _> source, Func<object, ArraySource<T16>> getSource, Expression<Func<_, Cell<T16>, R>> kernel)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R>(source, getSource(default), kernel);

        public static IArrayTransform<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, _, R>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, _> source, Func<object, Result<R>> secondSelector, Expression<Func<_, RelativeCell<R>, R>> resultSelector)
            => new ArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R>(source, secondSelector(default), resultSelector);

        #region 16 arrays, 2 results

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2>(source, resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _> source, Func<object, T16[,]> getSource, Expression<Func<_, Cell<T16>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, _, R1, R2>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _> source, Func<object, ArraySource<T16>> getSource, Expression<Func<_, Cell<T16>, (R1, R2)>> kernel)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2>(source, getSource(default), kernel);

/*
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2>(source, secondSelector(default), resultSelector);

        public static IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2>(source, resultSelector);
*/
        #endregion
        #region 16 arrays, 3 results

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, _> source, Expression<Func<_, (R1, R2, R3)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3>(source, resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _> source, Func<object, T16[,]> getSource, Expression<Func<_, Cell<T16>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, _, R1, R2, R3>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _> source, Func<object, ArraySource<T16>> getSource, Expression<Func<_, Cell<T16>, (R1, R2, R3)>> kernel)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3>(source, getSource(default), kernel);

/*
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3>(source, secondSelector(default), resultSelector);

        public static IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3>(source, resultSelector);
*/
        #endregion
        #region 16 arrays, 4 results

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4> Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, _> source, Expression<Func<_, (R1, R2, R3, R4)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4>(source, resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _> source, Func<object, T16[,]> getSource, Expression<Func<_, Cell<T16>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4>(source, getSource(default).Wrap(), kernel);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, _, R1, R2, R3, R4>(this IArrayQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, _> source, Func<object, ArraySource<T16>> getSource, Expression<Func<_, Cell<T16>, (R1, R2, R3, R4)>> kernel)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4>(source, getSource(default), kernel);

/*
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4>(this T1[,] source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4>(source.Wrap(), secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4>(this T1[,] source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4>(source.Wrap(), secondSelector(default), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, T2[,]> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector) 
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4>(source, secondSelector(default).Wrap(), resultSelector);
        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4> SelectMany<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4>(this ArraySource<T1> source, Func<object, ArraySource<T2>> secondSelector, Expression<Func<Cell<T1>, Cell<T2>, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4>(source, secondSelector(default), resultSelector);

        public static IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4> Select<T1, T2, _, R1, R2>(this IArrayQuery<T1, T2, _> source, Expression<Func<_, (R1, R2)>> resultSelector)
            => new ArrayQuery4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4>(source, resultSelector);
*/
        #endregion
        #endregion

        public static ArraySource<T> With<T>(this T[,] source, T initValue) => new ArraySource<T>(source, initValue);
        public static ArraySource<T> With<T>(this T[,] source, OutOfBoundsStrategy<T> strategy) => new ArraySource<T>(source, strategy);
        public static ArraySource<T> With<T>(this T[,] source, OutOfBoundsStrategyUntyped strategy) => new ArraySource<T>(source, strategy);
    }

}

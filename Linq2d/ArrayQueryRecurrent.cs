using System;
using System.Linq.Expressions;

namespace Linq2d
{
    #region 1 result


    internal class ArrayQueryRecurrent<T, A, R>: ArrayQuery<T, R>, IArrayQueryRecurrent<T, A, R>
    {
        public ArrayQueryRecurrent(ArraySource<T> source, R initValue, LambdaExpression kernel): base(source, initValue, kernel){}
        
        internal ArrayQueryRecurrent(
            /*IArrayQuery<T, _>*/ IArraySource<T> sources, 
            R initValue, 
            /*Expression<Func<_, R>>*/ LambdaExpression kernel): base(sources, initValue, kernel){}
        internal ArrayQueryRecurrent(
            /*IArrayQueryRecurrent<T, _, R>*/ IArraySource<T> source, 
            /*Expression<Func<_, A>>*/ LambdaExpression kernel): base(source, kernel){}
    }


    internal class ArrayQueryRecurrent<T1, T2, A, R>: ArrayQuery<T1, T2, R>, IArrayQueryRecurrent<T1, T2, A, R>
    {
        internal ArrayQueryRecurrent(
            /*IArrayQuery<T1, T2, _>*/ IArraySource<T1, T2> sources, 
            R initValue, 
            /*Expression<Func<_, R>>*/ LambdaExpression kernel): base(sources, initValue, kernel){}
        internal ArrayQueryRecurrent(
            /*IArrayQueryRecurrent<T1, T2, _, R>*/ IArraySource<T1, T2> source, 
            /*Expression<Func<_, A>>*/ LambdaExpression kernel): base(source, kernel){}
    }


    internal class ArrayQueryRecurrent<T1, T2, T3, A, R>: ArrayQuery<T1, T2, T3, R>, IArrayQueryRecurrent<T1, T2, T3, A, R>
    {
        internal ArrayQueryRecurrent(
            /*IArrayQuery<T1, T2, T3, _>*/ IArraySource<T1, T2, T3> sources, 
            R initValue, 
            /*Expression<Func<_, R>>*/ LambdaExpression kernel): base(sources, initValue, kernel){}
        internal ArrayQueryRecurrent(
            /*IArrayQueryRecurrent<T1, T2, T3, _, R>*/ IArraySource<T1, T2, T3> source, 
            /*Expression<Func<_, A>>*/ LambdaExpression kernel): base(source, kernel){}
    }


    internal class ArrayQueryRecurrent<T1, T2, T3, T4, A, R>: ArrayQuery<T1, T2, T3, T4, R>, IArrayQueryRecurrent<T1, T2, T3, T4, A, R>
    {
        internal ArrayQueryRecurrent(
            /*IArrayQuery<T1, T2, T3, T4, _>*/ IArraySource<T1, T2, T3, T4> sources, 
            R initValue, 
            /*Expression<Func<_, R>>*/ LambdaExpression kernel): base(sources, initValue, kernel){}
        internal ArrayQueryRecurrent(
            /*IArrayQueryRecurrent<T1, T2, T3, T4, _, R>*/ IArraySource<T1, T2, T3, T4> source, 
            /*Expression<Func<_, A>>*/ LambdaExpression kernel): base(source, kernel){}
    }

    #endregion
    #region 2 results


    internal class ArrayQueryRecurrent2<T, A, R1, R2>: ArrayQuery2<T, R1, R2>, IArrayQueryRecurrent2<T, A, R1, R2>
    {
        internal ArrayQueryRecurrent2(
            /*IArrayQuery<T, _>*/ IArraySource<T> sources, 
            R2 initValue, 
            /*Expression<Func<_, (R1, R2)>>*/ LambdaExpression kernel): base(sources, initValue, kernel){}
        internal ArrayQueryRecurrent2(
            /*IArrayQueryRecurrent2<T, _, R1, R2>*/ IArraySource<T> source, 
            /*Expression<Func<_, A>>*/ LambdaExpression kernel): base(source, kernel){}
    }


    internal class ArrayQueryRecurrent2<T1, T2, A, R1, R2>: ArrayQuery2<T1, T2, R1, R2>, IArrayQueryRecurrent2<T1, T2, A, R1, R2>
    {
        internal ArrayQueryRecurrent2(
            /*IArrayQuery<T1, T2, _>*/ IArraySource<T1, T2> sources, 
            R2 initValue, 
            /*Expression<Func<_, (R1, R2)>>*/ LambdaExpression kernel): base(sources, initValue, kernel){}
        internal ArrayQueryRecurrent2(
            /*IArrayQueryRecurrent2<T1, T2, _, R1, R2>*/ IArraySource<T1, T2> source, 
            /*Expression<Func<_, A>>*/ LambdaExpression kernel): base(source, kernel){}
    }


    internal class ArrayQueryRecurrent2<T1, T2, T3, A, R1, R2>: ArrayQuery2<T1, T2, T3, R1, R2>, IArrayQueryRecurrent2<T1, T2, T3, A, R1, R2>
    {
        internal ArrayQueryRecurrent2(
            /*IArrayQuery<T1, T2, T3, _>*/ IArraySource<T1, T2, T3> sources, 
            R2 initValue, 
            /*Expression<Func<_, (R1, R2)>>*/ LambdaExpression kernel): base(sources, initValue, kernel){}
        internal ArrayQueryRecurrent2(
            /*IArrayQueryRecurrent2<T1, T2, T3, _, R1, R2>*/ IArraySource<T1, T2, T3> source, 
            /*Expression<Func<_, A>>*/ LambdaExpression kernel): base(source, kernel){}
    }


    internal class ArrayQueryRecurrent2<T1, T2, T3, T4, A, R1, R2>: ArrayQuery2<T1, T2, T3, T4, R1, R2>, IArrayQueryRecurrent2<T1, T2, T3, T4, A, R1, R2>
    {
        internal ArrayQueryRecurrent2(
            /*IArrayQuery<T1, T2, T3, T4, _>*/ IArraySource<T1, T2, T3, T4> sources, 
            R2 initValue, 
            /*Expression<Func<_, (R1, R2)>>*/ LambdaExpression kernel): base(sources, initValue, kernel){}
        internal ArrayQueryRecurrent2(
            /*IArrayQueryRecurrent2<T1, T2, T3, T4, _, R1, R2>*/ IArraySource<T1, T2, T3, T4> source, 
            /*Expression<Func<_, A>>*/ LambdaExpression kernel): base(source, kernel){}
    }

    #endregion
    #region 3 results


    internal class ArrayQueryRecurrent3<T, A, R1, R2, R3>: ArrayQuery3<T, R1, R2, R3>, IArrayQueryRecurrent3<T, A, R1, R2, R3>
    {
        internal ArrayQueryRecurrent3(
            /*IArrayQuery<T, _>*/ IArraySource<T> sources, 
            R3 initValue, 
            /*Expression<Func<_, (R1, R2, R3)>>*/ LambdaExpression kernel): base(sources, initValue, kernel){}
        internal ArrayQueryRecurrent3(
            /*IArrayQueryRecurrent3<T, _, R1, R2, R3>*/ IArraySource<T> source, 
            /*Expression<Func<_, A>>*/ LambdaExpression kernel): base(source, kernel){}
    }


    internal class ArrayQueryRecurrent3<T1, T2, A, R1, R2, R3>: ArrayQuery3<T1, T2, R1, R2, R3>, IArrayQueryRecurrent3<T1, T2, A, R1, R2, R3>
    {
        internal ArrayQueryRecurrent3(
            /*IArrayQuery<T1, T2, _>*/ IArraySource<T1, T2> sources, 
            R3 initValue, 
            /*Expression<Func<_, (R1, R2, R3)>>*/ LambdaExpression kernel): base(sources, initValue, kernel){}
        internal ArrayQueryRecurrent3(
            /*IArrayQueryRecurrent3<T1, T2, _, R1, R2, R3>*/ IArraySource<T1, T2> source, 
            /*Expression<Func<_, A>>*/ LambdaExpression kernel): base(source, kernel){}
    }


    internal class ArrayQueryRecurrent3<T1, T2, T3, A, R1, R2, R3>: ArrayQuery3<T1, T2, T3, R1, R2, R3>, IArrayQueryRecurrent3<T1, T2, T3, A, R1, R2, R3>
    {
        internal ArrayQueryRecurrent3(
            /*IArrayQuery<T1, T2, T3, _>*/ IArraySource<T1, T2, T3> sources, 
            R3 initValue, 
            /*Expression<Func<_, (R1, R2, R3)>>*/ LambdaExpression kernel): base(sources, initValue, kernel){}
        internal ArrayQueryRecurrent3(
            /*IArrayQueryRecurrent3<T1, T2, T3, _, R1, R2, R3>*/ IArraySource<T1, T2, T3> source, 
            /*Expression<Func<_, A>>*/ LambdaExpression kernel): base(source, kernel){}
    }


    internal class ArrayQueryRecurrent3<T1, T2, T3, T4, A, R1, R2, R3>: ArrayQuery3<T1, T2, T3, T4, R1, R2, R3>, IArrayQueryRecurrent3<T1, T2, T3, T4, A, R1, R2, R3>
    {
        internal ArrayQueryRecurrent3(
            /*IArrayQuery<T1, T2, T3, T4, _>*/ IArraySource<T1, T2, T3, T4> sources, 
            R3 initValue, 
            /*Expression<Func<_, (R1, R2, R3)>>*/ LambdaExpression kernel): base(sources, initValue, kernel){}
        internal ArrayQueryRecurrent3(
            /*IArrayQueryRecurrent3<T1, T2, T3, T4, _, R1, R2, R3>*/ IArraySource<T1, T2, T3, T4> source, 
            /*Expression<Func<_, A>>*/ LambdaExpression kernel): base(source, kernel){}
    }

    #endregion
}
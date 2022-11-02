
namespace Linq2d
{


    #region 1 result


    public interface IArrayQueryRecurrent<T, _, R> : IArraySource<T>, IArrayTransform<T, R> {};

    public interface IArrayQueryRecurrent<T1, T2, _, R> : IArraySource<T1, T2>, IArrayTransform<T1, T2, R> {};

    public interface IArrayQueryRecurrent<T1, T2, T3, _, R> : IArraySource<T1, T2, T3>, IArrayTransform<T1, T2, T3, R> {};

    public interface IArrayQueryRecurrent<T1, T2, T3, T4, _, R> : IArraySource<T1, T2, T3, T4>, IArrayTransform<T1, T2, T3, T4, R> {};

    public interface IArrayQueryRecurrent<T1, T2, T3, T4, T5, _, R> : IArraySource<T1, T2, T3, T4, T5>, IArrayTransform<T1, T2, T3, T4, T5, R> {};

    public interface IArrayQueryRecurrent<T1, T2, T3, T4, T5, T6, _, R> : IArraySource<T1, T2, T3, T4, T5, T6>, IArrayTransform<T1, T2, T3, T4, T5, T6, R> {};

    #endregion

    #region 2 results


    public interface IArrayQueryRecurrent2<T, _, R1, R2> : IArraySource<T>, IArrayTransform2<T, R1, R2> {};

    public interface IArrayQueryRecurrent2<T1, T2, _, R1, R2> : IArraySource<T1, T2>, IArrayTransform2<T1, T2, R1, R2> {};

    public interface IArrayQueryRecurrent2<T1, T2, T3, _, R1, R2> : IArraySource<T1, T2, T3>, IArrayTransform2<T1, T2, T3, R1, R2> {};

    public interface IArrayQueryRecurrent2<T1, T2, T3, T4, _, R1, R2> : IArraySource<T1, T2, T3, T4>, IArrayTransform2<T1, T2, T3, T4, R1, R2> {};

    public interface IArrayQueryRecurrent2<T1, T2, T3, T4, T5, _, R1, R2> : IArraySource<T1, T2, T3, T4, T5>, IArrayTransform2<T1, T2, T3, T4, T5, R1, R2> {};

    public interface IArrayQueryRecurrent2<T1, T2, T3, T4, T5, T6, _, R1, R2> : IArraySource<T1, T2, T3, T4, T5, T6>, IArrayTransform2<T1, T2, T3, T4, T5, T6, R1, R2> {};

    #endregion

    #region 3 results


    public interface IArrayQueryRecurrent3<T, _, R1, R2, R3> : IArraySource<T>, IArrayTransform3<T, R1, R2, R3> {};

    public interface IArrayQueryRecurrent3<T1, T2, _, R1, R2, R3> : IArraySource<T1, T2>, IArrayTransform3<T1, T2, R1, R2, R3> {};

    public interface IArrayQueryRecurrent3<T1, T2, T3, _, R1, R2, R3> : IArraySource<T1, T2, T3>, IArrayTransform3<T1, T2, T3, R1, R2, R3> {};

    public interface IArrayQueryRecurrent3<T1, T2, T3, T4, _, R1, R2, R3> : IArraySource<T1, T2, T3, T4>, IArrayTransform3<T1, T2, T3, T4, R1, R2, R3> {};

    public interface IArrayQueryRecurrent3<T1, T2, T3, T4, T5, _, R1, R2, R3> : IArraySource<T1, T2, T3, T4, T5>, IArrayTransform3<T1, T2, T3, T4, T5, R1, R2, R3> {};

    public interface IArrayQueryRecurrent3<T1, T2, T3, T4, T5, T6, _, R1, R2, R3> : IArraySource<T1, T2, T3, T4, T5, T6>, IArrayTransform3<T1, T2, T3, T4, T5, T6, R1, R2, R3> {};

    #endregion

    #region 4 results


    public interface IArrayQueryRecurrent4<T, _, R1, R2, R3, R4> : IArraySource<T>, IArrayTransform4<T, R1, R2, R3, R4> {};

    public interface IArrayQueryRecurrent4<T1, T2, _, R1, R2, R3, R4> : IArraySource<T1, T2>, IArrayTransform4<T1, T2, R1, R2, R3, R4> {};

    public interface IArrayQueryRecurrent4<T1, T2, T3, _, R1, R2, R3, R4> : IArraySource<T1, T2, T3>, IArrayTransform4<T1, T2, T3, R1, R2, R3, R4> {};

    public interface IArrayQueryRecurrent4<T1, T2, T3, T4, _, R1, R2, R3, R4> : IArraySource<T1, T2, T3, T4>, IArrayTransform4<T1, T2, T3, T4, R1, R2, R3, R4> {};

    public interface IArrayQueryRecurrent4<T1, T2, T3, T4, T5, _, R1, R2, R3, R4> : IArraySource<T1, T2, T3, T4, T5>, IArrayTransform4<T1, T2, T3, T4, T5, R1, R2, R3, R4> {};

    public interface IArrayQueryRecurrent4<T1, T2, T3, T4, T5, T6, _, R1, R2, R3, R4> : IArraySource<T1, T2, T3, T4, T5, T6>, IArrayTransform4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> {};

    #endregion


}
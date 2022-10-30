
namespace Linq2d
{


    #region 1 result


    public interface IArrayQueryRecurrent<T, _, R> : /*IArraySource<T>, */IArrayQuery<T, R> {};

    public interface IArrayQueryRecurrent<T1, T2, _, R> : /*IArraySource<T1, T2>, */IArrayQuery<T1, T2, R> {};

    public interface IArrayQueryRecurrent<T1, T2, T3, _, R> : /*IArraySource<T1, T2, T3>, */IArrayQuery<T1, T2, T3, R> {};

    public interface IArrayQueryRecurrent<T1, T2, T3, T4, _, R> : /*IArraySource<T1, T2, T3, T4>, */IArrayQuery<T1, T2, T3, T4, R> {};

    #endregion

    #region 2 results


    public interface IArrayQueryRecurrent2<T, _, R1, R2> : /*IArraySource<T>, */IArrayQuery2<T, R1, R2> {};

    public interface IArrayQueryRecurrent2<T1, T2, _, R1, R2> : /*IArraySource<T1, T2>, */IArrayQuery2<T1, T2, R1, R2> {};

    public interface IArrayQueryRecurrent2<T1, T2, T3, _, R1, R2> : /*IArraySource<T1, T2, T3>, */IArrayQuery2<T1, T2, T3, R1, R2> {};

    public interface IArrayQueryRecurrent2<T1, T2, T3, T4, _, R1, R2> : /*IArraySource<T1, T2, T3, T4>, */IArrayQuery2<T1, T2, T3, T4, R1, R2> {};

    #endregion

    #region 3 results


    public interface IArrayQueryRecurrent3<T, _, R1, R2, R3> : /*IArraySource<T>, */IArrayQuery3<T, R1, R2, R3> {};

    public interface IArrayQueryRecurrent3<T1, T2, _, R1, R2, R3> : /*IArraySource<T1, T2>, */IArrayQuery3<T1, T2, R1, R2, R3> {};

    public interface IArrayQueryRecurrent3<T1, T2, T3, _, R1, R2, R3> : /*IArraySource<T1, T2, T3>, */IArrayQuery3<T1, T2, T3, R1, R2, R3> {};

    public interface IArrayQueryRecurrent3<T1, T2, T3, T4, _, R1, R2, R3> : /*IArraySource<T1, T2, T3, T4>, */IArrayQuery3<T1, T2, T3, T4, R1, R2, R3> {};

    #endregion

    #region 4 results


    public interface IArrayQueryRecurrent4<T, _, R1, R2, R3, R4> : /*IArraySource<T>, */IArrayQuery4<T, R1, R2, R3, R4> {};

    public interface IArrayQueryRecurrent4<T1, T2, _, R1, R2, R3, R4> : /*IArraySource<T1, T2>, */IArrayQuery4<T1, T2, R1, R2, R3, R4> {};

    public interface IArrayQueryRecurrent4<T1, T2, T3, _, R1, R2, R3, R4> : /*IArraySource<T1, T2, T3>, */IArrayQuery4<T1, T2, T3, R1, R2, R3, R4> {};

    public interface IArrayQueryRecurrent4<T1, T2, T3, T4, _, R1, R2, R3, R4> : /*IArraySource<T1, T2, T3, T4>, */IArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4> {};

    #endregion


}
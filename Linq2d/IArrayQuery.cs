
namespace Linq2d
{
    public interface IArrayQuery {};


    #region 1 result

    public interface IArrayQuery<R> : IArrayQuery, IArray<R>{};

    public interface IArrayQuery<T, R> : IArrayQuery<R>, IArraySource<T>, IArrayTransform<T, R>{};

    public interface IArrayQuery<T1, T2, R> : IArrayQuery<R>, IArraySource<T1, T2>, IArrayTransform<T1, T2, R>{};

    public interface IArrayQuery<T1, T2, T3, R> : IArrayQuery<R>, IArraySource<T1, T2, T3>, IArrayTransform<T1, T2, T3, R>{};

    public interface IArrayQuery<T1, T2, T3, T4, R> : IArrayQuery<R>, IArraySource<T1, T2, T3, T4>, IArrayTransform<T1, T2, T3, T4, R>{};

    public interface IArrayQuery<T1, T2, T3, T4, T5, R> : IArrayQuery<R>, IArraySource<T1, T2, T3, T4, T5>, IArrayTransform<T1, T2, T3, T4, T5, R>{};

    public interface IArrayQuery<T1, T2, T3, T4, T5, T6, R> : IArrayQuery<R>, IArraySource<T1, T2, T3, T4, T5, T6>, IArrayTransform<T1, T2, T3, T4, T5, T6, R>{};

    public interface IArrayQuery<T1, T2, T3, T4, T5, T6, T7, R> : IArrayQuery<R>, IArraySource<T1, T2, T3, T4, T5, T6, T7>, IArrayTransform<T1, T2, T3, T4, T5, T6, T7, R>{};

    #endregion

    #region 2 results

    public interface IArrayQuery2<R1, R2> : IArrayQuery, IArray<R1, R2>{};

    public interface IArrayQuery2<T, R1, R2> : IArrayQuery2<R1, R2>, IArraySource<T>, IArrayTransform2<T, R1, R2>{};

    public interface IArrayQuery2<T1, T2, R1, R2> : IArrayQuery2<R1, R2>, IArraySource<T1, T2>, IArrayTransform2<T1, T2, R1, R2>{};

    public interface IArrayQuery2<T1, T2, T3, R1, R2> : IArrayQuery2<R1, R2>, IArraySource<T1, T2, T3>, IArrayTransform2<T1, T2, T3, R1, R2>{};

    public interface IArrayQuery2<T1, T2, T3, T4, R1, R2> : IArrayQuery2<R1, R2>, IArraySource<T1, T2, T3, T4>, IArrayTransform2<T1, T2, T3, T4, R1, R2>{};

    public interface IArrayQuery2<T1, T2, T3, T4, T5, R1, R2> : IArrayQuery2<R1, R2>, IArraySource<T1, T2, T3, T4, T5>, IArrayTransform2<T1, T2, T3, T4, T5, R1, R2>{};

    public interface IArrayQuery2<T1, T2, T3, T4, T5, T6, R1, R2> : IArrayQuery2<R1, R2>, IArraySource<T1, T2, T3, T4, T5, T6>, IArrayTransform2<T1, T2, T3, T4, T5, T6, R1, R2>{};

    public interface IArrayQuery2<T1, T2, T3, T4, T5, T6, T7, R1, R2> : IArrayQuery2<R1, R2>, IArraySource<T1, T2, T3, T4, T5, T6, T7>, IArrayTransform2<T1, T2, T3, T4, T5, T6, T7, R1, R2>{};

    #endregion

    #region 3 results

    public interface IArrayQuery3<R1, R2, R3> : IArrayQuery, IArray<R1, R2, R3>{};

    public interface IArrayQuery3<T, R1, R2, R3> : IArrayQuery3<R1, R2, R3>, IArraySource<T>, IArrayTransform3<T, R1, R2, R3>{};

    public interface IArrayQuery3<T1, T2, R1, R2, R3> : IArrayQuery3<R1, R2, R3>, IArraySource<T1, T2>, IArrayTransform3<T1, T2, R1, R2, R3>{};

    public interface IArrayQuery3<T1, T2, T3, R1, R2, R3> : IArrayQuery3<R1, R2, R3>, IArraySource<T1, T2, T3>, IArrayTransform3<T1, T2, T3, R1, R2, R3>{};

    public interface IArrayQuery3<T1, T2, T3, T4, R1, R2, R3> : IArrayQuery3<R1, R2, R3>, IArraySource<T1, T2, T3, T4>, IArrayTransform3<T1, T2, T3, T4, R1, R2, R3>{};

    public interface IArrayQuery3<T1, T2, T3, T4, T5, R1, R2, R3> : IArrayQuery3<R1, R2, R3>, IArraySource<T1, T2, T3, T4, T5>, IArrayTransform3<T1, T2, T3, T4, T5, R1, R2, R3>{};

    public interface IArrayQuery3<T1, T2, T3, T4, T5, T6, R1, R2, R3> : IArrayQuery3<R1, R2, R3>, IArraySource<T1, T2, T3, T4, T5, T6>, IArrayTransform3<T1, T2, T3, T4, T5, T6, R1, R2, R3>{};

    public interface IArrayQuery3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3> : IArrayQuery3<R1, R2, R3>, IArraySource<T1, T2, T3, T4, T5, T6, T7>, IArrayTransform3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>{};

    #endregion

    #region 4 results

    public interface IArrayQuery4<R1, R2, R3, R4> : IArrayQuery, IArray<R1, R2, R3, R4>{};

    public interface IArrayQuery4<T, R1, R2, R3, R4> : IArrayQuery4<R1, R2, R3, R4>, IArraySource<T>, IArrayTransform4<T, R1, R2, R3, R4>{};

    public interface IArrayQuery4<T1, T2, R1, R2, R3, R4> : IArrayQuery4<R1, R2, R3, R4>, IArraySource<T1, T2>, IArrayTransform4<T1, T2, R1, R2, R3, R4>{};

    public interface IArrayQuery4<T1, T2, T3, R1, R2, R3, R4> : IArrayQuery4<R1, R2, R3, R4>, IArraySource<T1, T2, T3>, IArrayTransform4<T1, T2, T3, R1, R2, R3, R4>{};

    public interface IArrayQuery4<T1, T2, T3, T4, R1, R2, R3, R4> : IArrayQuery4<R1, R2, R3, R4>, IArraySource<T1, T2, T3, T4>, IArrayTransform4<T1, T2, T3, T4, R1, R2, R3, R4>{};

    public interface IArrayQuery4<T1, T2, T3, T4, T5, R1, R2, R3, R4> : IArrayQuery4<R1, R2, R3, R4>, IArraySource<T1, T2, T3, T4, T5>, IArrayTransform4<T1, T2, T3, T4, T5, R1, R2, R3, R4>{};

    public interface IArrayQuery4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> : IArrayQuery4<R1, R2, R3, R4>, IArraySource<T1, T2, T3, T4, T5, T6>, IArrayTransform4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>{};

    public interface IArrayQuery4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4> : IArrayQuery4<R1, R2, R3, R4>, IArraySource<T1, T2, T3, T4, T5, T6, T7>, IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>{};

    #endregion


}

using System;

namespace Linq2d
{
    #region 1 result

    public interface IArrayTransform<T, R> : IArray<R>
    {
        Func<T[,], R[,]> Transform { get; }
    }

    public interface IArrayTransform<T1, T2, R> : IArray<R>
    {
        Func<T1[,], T2[,], R[,]> Transform { get; }
    }

    public interface IArrayTransform<T1, T2, T3, R> : IArray<R>
    {
        Func<T1[,], T2[,], T3[,], R[,]> Transform { get; }
    }

    public interface IArrayTransform<T1, T2, T3, T4, R> : IArray<R>
    {
        Func<T1[,], T2[,], T3[,], T4[,], R[,]> Transform { get; }
    }

    public interface IArrayTransform<T1, T2, T3, T4, T5, R> : IArray<R>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], R[,]> Transform { get; }
    }

    public interface IArrayTransform<T1, T2, T3, T4, T5, T6, R> : IArray<R>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], R[,]> Transform { get; }
    }
    #endregion

    #region 2 results

    public interface IArrayTransform2<T, R1, R2> : IArray<R1, R2>
    {
        Func<T[,], (R1[,], R2[,])> Transform { get; }
    }

    public interface IArrayTransform2<T1, T2, R1, R2> : IArray<R1, R2>
    {
        Func<T1[,], T2[,], (R1[,], R2[,])> Transform { get; }
    }

    public interface IArrayTransform2<T1, T2, T3, R1, R2> : IArray<R1, R2>
    {
        Func<T1[,], T2[,], T3[,], (R1[,], R2[,])> Transform { get; }
    }

    public interface IArrayTransform2<T1, T2, T3, T4, R1, R2> : IArray<R1, R2>
    {
        Func<T1[,], T2[,], T3[,], T4[,], (R1[,], R2[,])> Transform { get; }
    }

    public interface IArrayTransform2<T1, T2, T3, T4, T5, R1, R2> : IArray<R1, R2>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], (R1[,], R2[,])> Transform { get; }
    }

    public interface IArrayTransform2<T1, T2, T3, T4, T5, T6, R1, R2> : IArray<R1, R2>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], (R1[,], R2[,])> Transform { get; }
    }
    #endregion

    #region 3 results

    public interface IArrayTransform3<T, R1, R2, R3> : IArray<R1, R2, R3>
    {
        Func<T[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }

    public interface IArrayTransform3<T1, T2, R1, R2, R3> : IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }

    public interface IArrayTransform3<T1, T2, T3, R1, R2, R3> : IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], T3[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }

    public interface IArrayTransform3<T1, T2, T3, T4, R1, R2, R3> : IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], T3[,], T4[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }

    public interface IArrayTransform3<T1, T2, T3, T4, T5, R1, R2, R3> : IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }

    public interface IArrayTransform3<T1, T2, T3, T4, T5, T6, R1, R2, R3> : IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }
    #endregion

    #region 4 results

    public interface IArrayTransform4<T, R1, R2, R3, R4> : IArray<R1, R2, R3, R4>
    {
        Func<T[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }

    public interface IArrayTransform4<T1, T2, R1, R2, R3, R4> : IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }

    public interface IArrayTransform4<T1, T2, T3, R1, R2, R3, R4> : IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], T3[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }

    public interface IArrayTransform4<T1, T2, T3, T4, R1, R2, R3, R4> : IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], T3[,], T4[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }

    public interface IArrayTransform4<T1, T2, T3, T4, T5, R1, R2, R3, R4> : IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }

    public interface IArrayTransform4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4> : IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }
    #endregion

}
using System;

namespace Linq2d
{

    #region Single result
    public interface IArrayTransform<T, R>: IArray<R>
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

    public interface IArrayTransform<T1, T2, T3, T4, T5, T6, T7, R> : IArray<R>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], R[,]> Transform { get; }
    }

    public interface IArrayTransform<T1, T2, T3, T4, T5, T6, T7, T8, R> : IArray<R>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], R[,]> Transform { get; }
    }

    public interface IArrayTransform<T1, T2, T3, T4, T5, T6, T7, T8, T9, R> : IArray<R>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], R[,]> Transform { get; }
    }

    public interface IArrayTransform<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R> : IArray<R>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], R[,]> Transform { get; }
    }

    public interface IArrayTransform<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R> : IArray<R>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], R[,]> Transform { get; }
    }

    public interface IArrayTransform<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R> : IArray<R>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], R[,]> Transform { get; }
    }

    public interface IArrayTransform<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R> : IArray<R>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], R[,]> Transform { get; }
    }

    public interface IArrayTransform<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R> : IArray<R>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], R[,]> Transform { get; }
    }

    public interface IArrayTransform<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R> : IArray<R>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], T15[,], R[,]> Transform { get; }
    }

    public interface IArrayTransform<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R> : IArray<R>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], T15[,], T16[,], R[,]> Transform { get; }
    }
    #endregion



    #region 2 results

    public interface IArrayTransform2<T, R1, R2>: IArray<R1, R2>
    {
        Func<T[,], (R1[,], R2[,])> Transform { get; }
    }


    public interface IArrayTransform2<T1, T2, R1, R2>: IArray<R1, R2>
    {
        Func<T1[,], T2[,], (R1[,], R2[,])> Transform { get; }
    }

    public interface IArrayTransform2<T1, T2, T3, R1, R2>: IArray<R1, R2>
    {
        Func<T1[,], T2[,], T3[,], (R1[,], R2[,])> Transform { get; }
    }

    public interface IArrayTransform2<T1, T2, T3, T4, R1, R2>: IArray<R1, R2>
    {
        Func<T1[,], T2[,], T3[,], T4[,], (R1[,], R2[,])> Transform { get; }
    }

    public interface IArrayTransform2<T1, T2, T3, T4, T5, R1, R2>: IArray<R1, R2>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], (R1[,], R2[,])> Transform { get; }
    }

    public interface IArrayTransform2<T1, T2, T3, T4, T5, T6, R1, R2>: IArray<R1, R2>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], (R1[,], R2[,])> Transform { get; }
    }

    public interface IArrayTransform2<T1, T2, T3, T4, T5, T6, T7, R1, R2>: IArray<R1, R2>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], (R1[,], R2[,])> Transform { get; }
    }

    public interface IArrayTransform2<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2>: IArray<R1, R2>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], (R1[,], R2[,])> Transform { get; }
    }

    public interface IArrayTransform2<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2>: IArray<R1, R2>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], (R1[,], R2[,])> Transform { get; }
    }

    public interface IArrayTransform2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2>: IArray<R1, R2>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], (R1[,], R2[,])> Transform { get; }
    }

    public interface IArrayTransform2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2>: IArray<R1, R2>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], (R1[,], R2[,])> Transform { get; }
    }

    public interface IArrayTransform2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2>: IArray<R1, R2>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], (R1[,], R2[,])> Transform { get; }
    }

    public interface IArrayTransform2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2>: IArray<R1, R2>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], (R1[,], R2[,])> Transform { get; }
    }

    public interface IArrayTransform2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2>: IArray<R1, R2>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], (R1[,], R2[,])> Transform { get; }
    }

    public interface IArrayTransform2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2>: IArray<R1, R2>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], T15[,], (R1[,], R2[,])> Transform { get; }
    }

    public interface IArrayTransform2<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2>: IArray<R1, R2>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], T15[,], T16[,], (R1[,], R2[,])> Transform { get; }
    }
    #endregion

    #region 3 results

    public interface IArrayTransform3<T, R1, R2, R3>: IArray<R1, R2, R3>
    {
        Func<T[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }


    public interface IArrayTransform3<T1, T2, R1, R2, R3>: IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }

    public interface IArrayTransform3<T1, T2, T3, R1, R2, R3>: IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], T3[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }

    public interface IArrayTransform3<T1, T2, T3, T4, R1, R2, R3>: IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], T3[,], T4[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }

    public interface IArrayTransform3<T1, T2, T3, T4, T5, R1, R2, R3>: IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }

    public interface IArrayTransform3<T1, T2, T3, T4, T5, T6, R1, R2, R3>: IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }

    public interface IArrayTransform3<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3>: IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }

    public interface IArrayTransform3<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3>: IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }

    public interface IArrayTransform3<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3>: IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }

    public interface IArrayTransform3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3>: IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }

    public interface IArrayTransform3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3>: IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }

    public interface IArrayTransform3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3>: IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }

    public interface IArrayTransform3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3>: IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }

    public interface IArrayTransform3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3>: IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }

    public interface IArrayTransform3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3>: IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], T15[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }

    public interface IArrayTransform3<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3>: IArray<R1, R2, R3>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], T15[,], T16[,], (R1[,], R2[,], R3[,])> Transform { get; }
    }
    #endregion

    #region 4 results

    public interface IArrayTransform4<T, R1, R2, R3, R4>: IArray<R1, R2, R3, R4>
    {
        Func<T[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }


    public interface IArrayTransform4<T1, T2, R1, R2, R3, R4>: IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }

    public interface IArrayTransform4<T1, T2, T3, R1, R2, R3, R4>: IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], T3[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }

    public interface IArrayTransform4<T1, T2, T3, T4, R1, R2, R3, R4>: IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], T3[,], T4[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }

    public interface IArrayTransform4<T1, T2, T3, T4, T5, R1, R2, R3, R4>: IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }

    public interface IArrayTransform4<T1, T2, T3, T4, T5, T6, R1, R2, R3, R4>: IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }

    public interface IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, R1, R2, R3, R4>: IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }

    public interface IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, T8, R1, R2, R3, R4>: IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }

    public interface IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1, R2, R3, R4>: IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }

    public interface IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R1, R2, R3, R4>: IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }

    public interface IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R1, R2, R3, R4>: IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }

    public interface IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R1, R2, R3, R4>: IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }

    public interface IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R1, R2, R3, R4>: IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }

    public interface IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R1, R2, R3, R4>: IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }

    public interface IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R1, R2, R3, R4>: IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], T15[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }

    public interface IArrayTransform4<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R1, R2, R3, R4>: IArray<R1, R2, R3, R4>
    {
        Func<T1[,], T2[,], T3[,], T4[,], T5[,], T6[,], T7[,], T8[,], T9[,], T10[,], T11[,], T12[,], T13[,], T14[,], T15[,], T16[,], (R1[,], R2[,], R3[,], R4[,])> Transform { get; }
    }
    #endregion

}
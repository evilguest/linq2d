namespace Linq2d
{
    public interface IArraySource<T>: IArrayQuery
    {
        public ArraySource<T> Source { get; }
    }

    public interface IArraySource<T1, T2>: IArrayQuery
    {
        public ArraySource<T1> Source1 { get; }
        public ArraySource<T2> Source2 { get; }
    }

    public interface IArraySource<T1, T2, T3>
        : IArraySource<T1, T2>
    {
        public ArraySource<T3> Source3 { get; }
    }
    public interface IArraySource<T1, T2, T3, T4>
        : IArraySource<T1, T2, T3>
    {
        public ArraySource<T4> Source4 { get; }
    }
    public interface IArraySource<T1, T2, T3, T4, T5>
        : IArraySource<T1, T2, T3, T4>
    {
        public ArraySource<T5> Source5 { get; }
    }
    public interface IArraySource<T1, T2, T3, T4, T5, T6>
        : IArraySource<T1, T2, T3, T4, T5>
    {
        public ArraySource<T6> Source6 { get; }
    }
    public interface IArraySource<T1, T2, T3, T4, T5, T6, T7>
        : IArraySource<T1, T2, T3, T4, T5, T6>
    {
        public ArraySource<T7> Source7 { get; }
    }
    public interface IArraySource<T1, T2, T3, T4, T5, T6, T7, T8>
        : IArraySource<T1, T2, T3, T4, T5, T6, T7>
    {
        public ArraySource<T8> Source8 { get; }
    }
    public interface IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9>
        : IArraySource<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        public ArraySource<T9> Source9 { get; }
    }
    public interface IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
        : IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        public ArraySource<T10> Source10 { get; }
    }
    public interface IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
        : IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    {
        public ArraySource<T11> Source11 { get; }
    }
    public interface IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
        : IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
    {
        public ArraySource<T12> Source12 { get; }
    }
    public interface IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
        : IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
    {
        public ArraySource<T13> Source13 { get; }
    }
    public interface IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
        : IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
    {
        public ArraySource<T14> Source14 { get; }
    }
    public interface IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
        : IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
    {
        public ArraySource<T15> Source15 { get; }
    }
    public interface IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
        : IArraySource<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
    {
        public ArraySource<T16> Source16 { get; }
    }
}
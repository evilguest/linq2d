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

    public interface IArraySource<T1, T2, T3> : IArraySource<T1, T2>
    {
        public ArraySource<T3> Source3 { get; }
    }
    public interface IArraySource<T1, T2, T3, T4> : IArraySource<T1, T2, T3>
    {
        public ArraySource<T4> Source4 { get; }
    }
}
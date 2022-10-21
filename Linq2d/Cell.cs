using System;
using System.Diagnostics.CodeAnalysis;

namespace Linq2d
{

    public abstract class Cell
    {
        public abstract int X { get; }
        public abstract int Y { get; }
        public abstract int H { get; }
        public abstract int W { get; }
    }

    public abstract class RelativeCell<T> : Cell
    {
        public abstract T this[int dx, int dy] { get; }
        public abstract Cell<T> Offset(int dx, int dy);

    }
    public abstract class Cell<T> : RelativeCell<T>
    {
        public T Value { get; }
        [ExcludeFromCodeCoverage]
        public static implicit operator T(Cell<T> _) => throw new InvalidOperationException("The Cell class is not supposed to be ever directly used. It is only designed to be a placeholder in the 2d array filters");
    }
}

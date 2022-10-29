using Linq2d.Expressions;
using System;
using System.Linq.Expressions;

namespace Linq2d
{
    public class ArraySource 
    {
        public Array Array { get; }
        public Type Type { get; }
        public OutOfBoundsStrategy OutOfBounds { get; }

        //public int Height => Array.GetLength(0);

        //public int Width => Array.GetLength(1);

        protected ArraySource(Array array, OutOfBoundsStrategy outOfBounds)
        {
            Array = array ?? throw new ArgumentNullException(nameof(array));
            OutOfBounds = outOfBounds ?? throw new ArgumentNullException(nameof(outOfBounds));
            Type = array.GetType().GetElementType();
            //if (!Type.IsAssignableFrom(outOfBounds.Type))
            //    throw new ArgumentException($"Wrong expression type {outOfBounds.Type}", nameof(outOfBounds));
        }
    }
    public sealed class ArraySource<T> : ArraySource
    {
        public new T[,] Array { get => (T[,])base.Array; }

        public ArraySource<T> Source => this;

        public ArraySource(T[,] source, T outOfBoundsValue) : base(source, OutOfBoundsStrategy.Substitute(outOfBoundsValue)) { }
        public ArraySource(T[,] source, OutOfBoundsStrategyUntyped outOfBoundsStrategy) : base(source, outOfBoundsStrategy) { }

        public ArraySource(T[,] source, OutOfBoundsStrategy<T> outOfBoundsStrategy) : base(source, outOfBoundsStrategy) { }
        public ArraySource(T[,] source) : base(source, OutOfBoundsStrategy.Default<T>()) { }

        //public T[,] ToArray() => Array;
    }
}
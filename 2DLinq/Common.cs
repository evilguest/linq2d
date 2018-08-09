using System.Collections;

namespace System.Linq.Processing2d
{
    public interface ICell<out T>
    {
        int X { get; }
        int Y { get; }
        int H { get; }
        int W { get; }
        T this[int dx, int dy] { get; }
    }

    public interface IArray2d
    {
        int GetLength(int dimension);
    }

    public interface IArray2d<T> : IArray2d
    {
        T this[int x, int y] { get; } // consider ref T
        //IArray2d<R> Cast<R>();  // Cast is one level deeper
        T[,] ToArray();
    }


    public struct ArrayWrapper<T> : IArray2d<T>, IQueryableArray2d<T>, IRelQueryableArray2d<T>, IStructuralEquatable
    {
        internal readonly T[,] _array;
        internal readonly IArray2d<T> _source;
        public ArrayWrapper(T[,] array)
        {
            _array = array ?? throw new ArgumentNullException(nameof(array));
            _source = null;
        }
        public ArrayWrapper(IArray2d<T> source)
        {
            _array = null;
            _source = source ?? throw new ArgumentNullException(nameof(source));
            while(_source is ArrayWrapper<T>)
            {
                var t2 = (ArrayWrapper<T>)_source;
                if (t2._array != null)
                {
                    _array = t2._array;
                    _source = null;
                    return;
                }
                else
                    _source = t2._source;
            }
        }


        public T this[int x, int y]
        {
            get {
                if (_array != null)
                    return _array[x, y];
                else
                    return _source[x, y];
            }
            //set => _array[x, y] = value;
        }

        public IArray2d<R> Cast<R>()
        {
            return new SlowDeferred.SingleSelectWrapper<T, R>(this, (r) => (R)Convert.ChangeType(r[0, 0], typeof(R)));
        }

        public int GetLength(int dimension) => _array.GetLength(dimension);

        public T[,] ToArray() => _array ?? _source.ToArray();


        public bool Equals(object other, IEqualityComparer comparer)
        {
            if (other is IArray2d<T> otherArray)
                return Equals(this, otherArray, comparer);
            else
                return false;
        }

        public int GetHashCode(IEqualityComparer comparer)
        {
            return comparer.GetHashCode(ToArray());
        }

        static private bool Equals(IArray2d<T> left, IArray2d<T> right, IEqualityComparer comparer)
        {
            if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
            {
                Console.WriteLine("Discrepancy found: left dimensions[{0}, {2}] don't match right dimensions[{1}, {3}]", left.GetLength(0), right.GetLength(0), left.GetLength(1), right.GetLength(1));
                return false;
            }
            for (int i = 0; i < left.GetLength(0); i++)
                for (int j = 0; j < left.GetLength(1); j++)
                    if (!comparer.Equals(left[i, j], right[i, j]))
                    {
                        Console.WriteLine("Discrepancy found: left[{0}, {1}] == {2} != {3} == right[{0}, {1}]", i, j, left[i, j], right[i, j]);
                        return false; // diff found
                    }
            return true;
        }

    }

    public interface IQueryableArray2d<T> : IArray2d<T>
    {
    }
    public interface IRelQueryableArray2d<T> : IArray2d<T>
    {
    }

    struct Cell<T> : ICell<T>
    {
        private readonly IArray2d<T> _array;
        private readonly int _h;
        private readonly int _w;
        internal int _x;
        internal int _y;

        public Cell(IArray2d<T> array) : this(array, 0, 0) { }
        public Cell(IArray2d<T> array, int x, int y)
        {
            _array = array ?? throw new ArgumentNullException(nameof(array));
            _h = array.GetLength(0);
            _w = array.GetLength(1);
            _x = x; _y = y;
        }

        public T this[int dx, int dy] => _array[_x + dx, _y + dy];

        public int X => _x;

        public int Y => _y;

        public int H => _h;

        public int W => _w;
    }

    public delegate R Kernel<in T, out R>(ICell<T> cell);
    public delegate R[,] Filter<T, R>(T[,] data);

    class KernelMeasure
    {
        public int xmin = 0, xmax = 0, ymin = 0, ymax = 0;
        public int rxmin = 0, rxmax = 0, rymin = 0, rymax = 0;
        public static KernelMeasure<T> Measure<T, R>(Kernel<T, R> kernel)
        {
            var km = new KernelMeasure<T>();
            kernel(km);
            return km;
        }
    }


    sealed class KernelMeasure<T> : KernelMeasure, ICell<T>
    {
        T ICell<T>.this[int x, int y]
        {
            get
            {
                xmin = Math.Min(xmin, x);
                xmax = Math.Max(xmax, x);
                ymin = Math.Min(ymin, y);
                ymax = Math.Max(ymax, y);
                return default;
            }
        }

        public int X => throw new NotImplementedException();

        public int Y => throw new NotImplementedException();

        public int H => throw new NotImplementedException();

        public int W => throw new NotImplementedException();

        public ArrayRecurrence<R> Recurrent<R>() => null;

    }

    public static class Recurrent
    {
        public static ArrayRecurrence<R> InitWith<R>(R initValue) => null;
        public static ArrayRecurrence<R> InitWith<R>(Func<int, int, R> initFunction) => null;
    }
    public static class Array2dHelper
    {
        //public static ArrayDirect<T> AsDirect<T>(this T[,] source) => new ArrayDirect<T>(source);
        public static ArrayRecurrence<R> Recurrent<T, R>(this T item) => null;

        //public static IQueryableArray2d<T> AsDirect<T>(this T[,] source) => new ArrayWrapper<T>(source);
        public static IRelQueryableArray2d<T> AsRelative<T>(this T[,] source, Bounds bounds) => new ArrayWrapper<T>(source);
        //public static IQueryableArray2d<T> AsDirect<T>(this IArray2d<T> source) => new ArrayWrapper<T>(source);
        public static IRelQueryableArray2d<T> AsRelative<T>(this IArray2d<T> source, Bounds bounds) => new ArrayWrapper<T>(source);

        public static T Max<T>(this IArray2d<T> source)
            where T : IComparable<T>
        {
            T max = source[0, 0];
            int height = source.GetLength(0);
            int width = source.GetLength(1);
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    if (max.CompareTo(source[i, j]) < 0)
                        max = source[i, j];
            return max;
        }

        public static T Max<T>(this T[,] source)
            where T : IComparable<T>
        {
            T max = source[0, 0];
            int height = source.GetLength(0);
            int width = source.GetLength(1);
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    if (max.CompareTo(source[i, j]) < 0)
                        max = source[i, j];
            return max;
        }

        //public static ReadOnlySpan<T> LockLine<T>(this T[,] array, int i)
        //    where T: unmanaged
        //    => LockLine<T>(array, i, 0, array.GetLength(1));

        //unsafe public static ReadOnlySpan<T> LockLine<T>(this T[,] array, int i, int j, int length)
        //    where T: unmanaged
        //{
        //    if (j + length > array.GetLength(1))
        //        throw new IndexOutOfRangeException("span ends after the line length");
        //    fixed (T* lineRef = &array[i, j])
        //        return new ReadOnlySpan<T>(lineRef, length);
        //}
    }

    public class ArrayRecurrence<R> { }

    public class Bounds
    {
        private Bounds() { }
        public static Bounds Skip {get;} = new Bounds();
    }
}
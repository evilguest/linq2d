using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

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


    public static class ArrayWrapper
    {
        public static ArrayWrapper<T> Wrap<T>(this IArray2d<T> array, BoundOptions bounds) => new ArrayWrapper<T>(array, bounds);
        public static ArrayWrapper<T> Wrap<T>(this T[,] array, BoundOptions bounds) => new ArrayWrapper<T>(array, bounds);
    }
    public struct ArrayWrapper<T> : IArray2d<T>, IQueryableArray2d<T>, IRelQueryableArray2d<T>, IStructuralEquatable
    {
        internal readonly T[,] _array;
        internal readonly IArray2d<T> _source;
        private readonly BoundOptions _boundOptions;

        public BoundOptions BoundOptions => _boundOptions;

        public ArrayWrapper(T[,] array, BoundOptions bounds)
        {
            _array = array ?? throw new ArgumentNullException(nameof(array));
            _boundOptions = bounds;
            _source = null;

        }
        public ArrayWrapper(IArray2d<T> source, BoundOptions bounds)
        {
            _array = null;
            _boundOptions = bounds;
            _source = source ?? throw new ArgumentNullException(nameof(source));
            while(_source is ArrayWrapper<T> t2)
            {
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
                //check the bounds
                if(x<0)
                {
                    if (y < 0)
                    {
                        if (_boundOptions[BoundsDirection.TopLeft] is Bounds<T> bt)
                            return bt._initValue;
                        if (_boundOptions[BoundsDirection.TopLeft] == Bounds.Wrap)
                        {
                            x = x % GetLength(0);
                            y = y % GetLength(1);
                        }
                        if (_boundOptions[BoundsDirection.TopLeft] == Bounds.Continue)
                        {
                            x = 0;
                            y = 0;
                        }
                    } else if (y >= GetLength(1))
                    {
                        if (_boundOptions[BoundsDirection.TopRight] is Bounds<T> bt)
                            return bt._initValue;
                        if (_boundOptions[BoundsDirection.TopRight] == Bounds.Wrap)
                        {
                            x = x % GetLength(0);
                            y = y % GetLength(1);
                        }
                        if (_boundOptions[BoundsDirection.TopRight] == Bounds.Continue)
                        {
                            x = 0;
                            y = GetLength(1)-1;
                        }

                    }
                    else
                    {
                        if (_boundOptions[BoundsDirection.Top] is Bounds<T> bt)
                            return bt._initValue;
                        if (_boundOptions[BoundsDirection.Top] == Bounds.Wrap)
                            x = x % GetLength(0);
                        if (_boundOptions[BoundsDirection.Top] == Bounds.Continue)
                            x = 0;
                    }
                } else if(x > GetLength(0))
                {
                    if (y < 0)
                    {
                        if (_boundOptions[BoundsDirection.BottomLeft] is Bounds<T> bt)
                            return bt._initValue;
                        if (_boundOptions[BoundsDirection.BottomLeft] == Bounds.Wrap)
                        {
                            x = x % GetLength(0);
                            y = y % GetLength(1);
                        }
                        if (_boundOptions[BoundsDirection.BottomLeft] == Bounds.Continue)
                        {
                            x = GetLength(0)-1;
                            y = 0;
                        }
                    }
                    else if (y >= GetLength(1))
                    {
                        if (_boundOptions[BoundsDirection.BottomRight] is Bounds<T> bt)
                            return bt._initValue;
                        if (_boundOptions[BoundsDirection.BottomRight] == Bounds.Wrap)
                        {
                            x = x % GetLength(0);
                            y = y % GetLength(1);
                        }
                        if (_boundOptions[BoundsDirection.BottomRight] == Bounds.Continue)
                        {
                            x = GetLength(0) - 1;
                            y = GetLength(1) - 1;
                        }

                    }
                    else
                    {
                        if (_boundOptions[BoundsDirection.Bottom] is Bounds<T> bt)
                            return bt._initValue;
                        if (_boundOptions[BoundsDirection.Bottom] == Bounds.Wrap)
                            x = x % GetLength(0);
                        if (_boundOptions[BoundsDirection.Bottom] == Bounds.Continue)
                            x = GetLength(0)-1;
                    }
                }
                else if (y < 0)
                {
                    if (_boundOptions[BoundsDirection.Left] is Bounds<T> bt)
                        return bt._initValue;
                    if (_boundOptions[BoundsDirection.Left] == Bounds.Wrap)
                        y = y % GetLength(1);
                    if (_boundOptions[BoundsDirection.Left] == Bounds.Continue)
                        y = 0;
                }
                else if (y >= GetLength(1))
                {
                    if (_boundOptions[BoundsDirection.Right] is Bounds<T> bt)
                        return bt._initValue;
                    if (_boundOptions[BoundsDirection.Right] == Bounds.Wrap)
                        y = y % GetLength(1);
                    if (_boundOptions[BoundsDirection.Right] == Bounds.Continue)
                        y = GetLength(1) - 1;
                }

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

        public int GetLength(int dimension) => _array != null ? _array.GetLength(dimension) : _source.GetLength(dimension);

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
    public enum BoundsDirection
    {
        Left, TopLeft, Top, TopRight, Right, BottomRight, Bottom, BottomLeft
    }

    public struct BoundOptions
    {
        private readonly Bounds[] _boundOptions;
        public static implicit operator BoundOptions(Bounds bounds) => new BoundOptions(bounds);

        public BoundOptions(Bounds all) : this(all, all) { }
        public BoundOptions(Bounds tl, Bounds br) : this(tl, tl, br, br) { }
        public BoundOptions(Bounds l, Bounds t, Bounds r, Bounds b) : this(l, l, t, t, r, r, b, b) { }
        public BoundOptions(Bounds l, Bounds tl, Bounds t, Bounds tr, Bounds r, Bounds br, Bounds b, Bounds bl) 
            => _boundOptions = new Bounds[] { l, tl, t, tr, r, br, b, bl };

        public Bounds this[BoundsDirection direction] { get => _boundOptions[(int)direction]; }
    }
    public interface IRelQueryableArray2d<T> : IArray2d<T>
    {
        BoundOptions BoundOptions { get; }
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
        public static KernelMeasure Measure<T, R>(Kernel<T, R> kernel, KernelMeasure m = null)
        {
            m = m ?? new KernelMeasure();
            var km = new KernelMeasure<T>(m);
            kernel(km);
            return m;
        }
        public static KernelMeasure Measure(LambdaExpression kernel, KernelMeasure m = null)
        {
            m = m ?? new KernelMeasure();
            // TODO: perform the analysis of the kernel lambda to see what's going on
            new KernelMeasureVisitor(kernel, m).Visit(kernel);
            return m;
        }
    }

    public class KernelMeasureVisitor : ExpressionVisitor
    {
        private readonly LambdaExpression _kernel;
        private readonly KernelMeasure _m;

        internal KernelMeasureVisitor(LambdaExpression kernel, KernelMeasure m)
        {
            _kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
            _m = m ?? throw new ArgumentNullException(nameof(m));
        }
        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression is ConstantExpression ce)
            {
                switch (node.Member)
                {
                    case FieldInfo fe: return Expression.Constant(fe.GetValue(ce.Value));
                    case PropertyInfo pe: return Expression.Constant(pe.GetValue(ce.Value));
                    default: throw new NotSupportedException($"The member is of unknown type ${node.Member.GetType().Name}");
                }
            }
            return base.VisitMember(node);
        }


        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Object is ParameterExpression pe && pe.Type.IsConstructedGenericType && pe.Type.GetGenericTypeDefinition() == typeof(ICell<>))
            {
                if (node.Method.Name == "get_Item")
                {
                    switch (Visit(node.Arguments[0]))
                    {
                        case ConstantExpression ce:
                            _m.xmax = Math.Max(_m.xmax, (int)ce.Value);
                            _m.xmin = Math.Min(_m.xmin, (int)ce.Value);
                            break;
                    }
                    switch (Visit(node.Arguments[1]))
                    {
                        case ConstantExpression ce:
                            _m.ymax = Math.Max(_m.ymax, (int)ce.Value);
                            _m.ymin = Math.Min(_m.ymin, (int)ce.Value);
                            break;
                    }
                }
            }

            return base.VisitMethodCall(node);
        }
    }
    sealed class KernelMeasure<T> : ICell<T>
    {
        private readonly T _item;
        private readonly KernelMeasure _measure;

        public KernelMeasure(KernelMeasure measure)
        {
            _measure = measure ?? throw new ArgumentNullException(nameof(measure));
            _item = PrepareItem();
        }
        T ICell<T>.this[int x, int y]
        {
            get
            {
                _measure.xmin = Math.Min(_measure.xmin, x);
                _measure.xmax = Math.Max(_measure.xmax, x);
                _measure.ymin = Math.Min(_measure.ymin, y);
                _measure.ymax = Math.Max(_measure.ymax, y);
                return _item;
            }
        }

        private T PrepareItem()
        {
            return (T)PrepareItem(typeof(T));
        }

        private object PrepareItem(Type itemType)
        {
            if (itemType == typeof(int))
                return 0;
            if (itemType == typeof(long))
                return 0L;
            if (itemType == typeof(byte))
                return (byte)0;
                            

            if (itemType.IsConstructedGenericType && itemType.GetGenericTypeDefinition() == typeof(ICell<T>).GetGenericTypeDefinition())
            {
                Type measureType = typeof(KernelMeasure<T>).GetGenericTypeDefinition().MakeGenericType(itemType.GetGenericArguments());
                return Activator.CreateInstance(measureType, _measure);
            }

            var c = itemType.GetConstructors().FirstOrDefault();
            if (c == null)
                return null;

            var paramValues = new ArrayList();
            foreach (var param in c.GetParameters())
                paramValues.Add(PrepareItem(param.ParameterType));
            return Activator.CreateInstance(itemType, paramValues.ToArray());
        }

        public int X => 0;

        public int Y => 0;

        public int H => 0;

        public int W => 0;

        //public ArrayRecurrence<R> Recurrent<R>() => null;

    }

    public static class Recurrent
    {
        public static ArrayRecurrence<R> InitWith<R>(R initValue) => null;
        public static ArrayRecurrence<R> InitWith<R>(Func<int, int, R> initFunction) => null;
    }
    public static class Array2dHelper
    {
        //public static ArrayDirect<T> AsDirect<T>(this T[,] source) => new ArrayDirect<T>(source);
//        public static ArrayRecurrence<R> Recurrent<T, R>(this T item) => null;

        //public static IQueryableArray2d<T> AsDirect<T>(this T[,] source) => new ArrayWrapper<T>(source);
        public static IRelQueryableArray2d<T> AsRelative<T>(this T[,] source, BoundOptions bounds) => source.Wrap(bounds);
        //public static IQueryableArray2d<T> AsDirect<T>(this IArray2d<T> source) => new ArrayWrapper<T>(source);
        public static IRelQueryableArray2d<T> AsRelative<T>(this IArray2d<T> source, BoundOptions bounds) => source.Wrap(bounds);

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
        protected Bounds() { }
        public static Bounds Skip {get;} = new Bounds();
        public static Bounds Continue { get; } = new Bounds();
        public static Bounds Wrap { get; } = new Bounds();
        public static Bounds<T> ReplaceWith<T>(T initValue) => new Bounds<T>(initValue);
    }
    public class Bounds<T> : Bounds
    {
        internal readonly T _initValue;

        public Bounds(T initValue) => _initValue = initValue;
    }
}
using System.Collections;
using System.Linq.Processing2d;

namespace System.Linq.Processing2d.SlowDeferred
{
    internal class SingleSelectWrapper<T, R> : SlowDeferredArrayWrapperBase<R>, IQueryableArray2d<R>
    {
        private readonly IArray2d<T> _source;
        private readonly Kernel<T, R> _kernel;
        private readonly int _h;
        private readonly int _w;
        private readonly KernelMeasure _km;

        public SingleSelectWrapper(IArray2d<T> source, Kernel<T, R> kernel)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
            _h = source.GetLength(0);
            _w = source.GetLength(1);
            _km = KernelMeasure.Measure(kernel);
            if(source is IRelQueryableArray2d<T> rqa)
            {
                if (rqa.BoundOptions[BoundsDirection.Top] != Bounds.Skip)
                    _km.xmin = 0;
                if (rqa.BoundOptions[BoundsDirection.Bottom] != Bounds.Skip)
                    _km.xmax = 0;
                if (rqa.BoundOptions[BoundsDirection.Left] != Bounds.Skip)
                    _km.ymin = 0;
                if (rqa.BoundOptions[BoundsDirection.Right] != Bounds.Skip)
                    _km.ymax = 0;
            }
        }

        public override R this[int x, int y]
        {
            get {
                if((x< -_km.xmin) || (x>=_h-_km.xmax) || (y<-_km.ymin) || (y>=_w-_km.ymax))
                    return default;
                else
                    return _kernel(new Cell<T>(_source, x, y));
            }
        }

        public override int GetLength(int dimension) =>_source.GetLength(dimension);
        

        public override R[,] ToArray()
        {
            var result = new R[_h, _w];
            for (int i = -_km.xmin; i < _h - _km.xmax; i++)
                for (int j = -_km.ymin; j < _w - _km.ymax; j++)
                    result[i, j] = this[i, j];
            return result;
        }
    }

    internal class DualSelectWrapper<T, A, R> : SlowDeferredArrayWrapperBase<R>, IQueryableArray2d<R>
    {
        private readonly IArray2d<T> _left;
        private readonly IArray2d<A> _right;
        private readonly Func<ICell<T>, ICell<A>, R> _kernel;
        private readonly int _xmin;
        private readonly int _xmax;
        private readonly int _ymin;
        private readonly int _ymax;
        private readonly int _h;
        private readonly int _w;

        public override R this[int x, int y]
        {
            get
            {
                var cell1 = new Cell<T>(_left, x, y);
                var cell2 = new Cell<A>(_right, x, y);
                return _kernel(cell1, cell2);
            }
        }

        public override int GetLength(int dimension)
        {
            switch(dimension)
            {
                case 0:return _h;
                case 1: return _w;
                default: throw new ArgumentOutOfRangeException(nameof(dimension), dimension, "only 0 or 1 can be specified");
            }
        }

        public override R[,] ToArray()
        {
            var result = new R[_h, _w];
            var cell1 = new Cell<T>(_left);
            var cell2 = new Cell<A>(_right);

            for (cell1._x = -_xmin; cell1._x < _h - _xmax; cell1._x++)
            {
                cell2._x = cell1._x;
                for (cell1._y = -_ymin; cell1._y < _w - _ymax; cell1._y++)
                {
                    cell2._y = cell1._y;
                    result[cell1._x, cell1._y] = _kernel(cell1, cell2);
                }
            }

            return result;
        }

        public DualSelectWrapper(IArray2d<T> left, IArray2d<A> right, Func<ICell<T>, ICell<A>, R> kernel)
        {
            _left = left ?? throw new ArgumentNullException(nameof(left));
            _right = right ?? throw new ArgumentNullException(nameof(right));
            _kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));

            _h = Math.Min(left.GetLength(0), right.GetLength(0));
            _w = Math.Min(left.GetLength(1), right.GetLength(1));

            var m = new KernelMeasure();
            var km1 = new KernelMeasure<T>(m);
            var km2 = new KernelMeasure<A>(m);
            kernel(km1, km2);
            _xmin = m.xmin;
            _xmax = m.xmax;
            _ymin = m.ymin;
            _ymax = m.ymax;
        }
        
    }

    internal class RecurrentSelectWrapper<T, R> : SlowDeferredArrayWrapperBase<R>, IQueryableArray2d<R>
    {
        private readonly IArray2d<T> _source;
        private readonly Func<ICell<T>, ICell<R>, R> _kernel;
        private readonly int _xmin;
        private readonly int _xmax;
        private readonly int _ymin;
        private readonly int _ymax;
        private readonly int _h;
        private readonly int _w;
        private R[,] _result;

        public RecurrentSelectWrapper(IArray2d<T> source, Func<ICell<T>, ICell<R>, R> kernel)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
            _h = source.GetLength(0);
            _w = source.GetLength(1);
            var m = new KernelMeasure();
            var km1 = new KernelMeasure<T>(m);
            var km2 = new KernelMeasure<R>(m);
            kernel(km1, km2);
            _xmin = m.xmin;
            _xmax = m.xmax;
            _ymin = m.ymin;
            _ymax = m.ymax;
        }

        public override R this[int x, int y] => ToArray()[x,y];

        public override int GetLength(int dimension) => _source.GetLength(dimension);

        public override R[,] ToArray()
        {
            if (_result == null)
            {
                _result = new R[_h, _w];
                var cell1 = new Cell<T>(_source);
                var cell2 = new Cell<R>(_result.Wrap(Bounds.Skip));

                for (cell1._x = -_xmin; cell1._x < _h - _xmax; cell1._x++)
                {
                    cell2._x = cell1._x;
                    for (cell1._y = -_ymin; cell1._y < _w - _ymax; cell1._y++)
                    {
                        cell2._y = cell1._y;
                        _result[cell1._x, cell1._y] = _kernel(cell1, cell2);
                    }
                }
            }
            return _result;
        }
    }


    internal abstract class SlowDeferredArrayWrapperBase<T>:IArray2d<T>, IStructuralEquatable
    {
        public abstract T this[int x, int y] { get; }

        public bool Equals(object other, IEqualityComparer comparer)
        {
            return ((IStructuralEquatable)ToArray()).Equals(other, comparer);
        }

        public int GetHashCode(IEqualityComparer comparer)
        {
            return ((IStructuralEquatable)ToArray()).GetHashCode(comparer);
        }

        public abstract int GetLength(int dimension);

        public abstract T[,] ToArray();
    }

    public static class SlowDeferredArray2d
    {
        public static IQueryableArray2d<R> Select<T, R>(this IQueryableArray2d<T> source, Func<T, R> kernel)=> new SingleSelectWrapper<T, R>(source, (cell) => kernel(cell[0, 0]));
        public static IQueryableArray2d<R> Select<T, R>(this IRelQueryableArray2d<T> source, Kernel<T, R> kernel)=> new SingleSelectWrapper<T, R>(source, kernel);
        public static IQueryableArray2d<R> SelectMany<T, A, R>(this T[,] source, Func<T, A[,]> secondSelector, Func<T, A, R> resultSelector) => new DualSelectWrapper<T, A, R>(source.Wrap(Bounds.Skip), secondSelector(default).Wrap(Bounds.Skip), (cellL, cellR) => resultSelector(cellL[0, 0], cellR[0, 0]));
        public static IQueryableArray2d<R> SelectMany<T, A, R>(this T[,] source, Func<T, IRelQueryableArray2d<A>> secondSelector, Func<T, ICell<A>, R> resultSelector) => new DualSelectWrapper<T, A, R>(source.Wrap(Bounds.Skip), secondSelector(default), (cellL, cellR) => resultSelector(cellL[0, 0], cellR));
        public static IQueryableArray2d<R> SelectMany<T, A, R>(this IQueryableArray2d<T> source, Func<T, IQueryableArray2d<A>> secondSelector, Func<T, A, R> resultSelector)=> new DualSelectWrapper<T, A, R>(source, secondSelector(default), (cellL, cellR) => resultSelector(cellL[0, 0], cellR[0, 0]));
        public static IQueryableArray2d<R> SelectMany<T, A, R>(this IQueryableArray2d<T> source, Func<T, IRelQueryableArray2d<A>> secondSelector, Func<T, ICell<A>, R> resultSelector)=> new DualSelectWrapper<T, A, R>(source, secondSelector(default), (cellL, cellR) => resultSelector(cellL[0, 0], cellR));
        public static IQueryableArray2d<R> SelectMany<T, A, R>(this IRelQueryableArray2d<T> source, Func<ICell<T>, IQueryableArray2d<A>> secondSelector, Func<ICell<T>, A, R> resultSelector)=> new DualSelectWrapper<T, A, R>(source, secondSelector(null), (cellL, cellR) => resultSelector(cellL, cellR[0, 0]));
        public static IQueryableArray2d<R> SelectMany<T, A, R>(this IRelQueryableArray2d<T> source, Func<ICell<T>, IRelQueryableArray2d<A>> secondSelector, Func<ICell<T>, ICell<A>, R> resultSelector)=> new DualSelectWrapper<T, A, R>(source, secondSelector(null), resultSelector);
        public static IArray2d<R> SelectMany<T, R>(this IRelQueryableArray2d<T> source, Func<ICell<T>, ArrayRecurrence<R>> secondSelector, Func<ICell<T>, ICell<R>, R> resultSelector)=> new RecurrentSelectWrapper<T, R>(source, resultSelector);
        public static IArray2d<R> SelectMany<T, R>(this T[,] source, Func<T, ArrayRecurrence<R>> secondSelector, Func<T, ICell<R>, R> resultSelector)=> new RecurrentSelectWrapper<T, R>(source.Wrap(Bounds.Skip), (cell1, cell2) => resultSelector(cell1[0, 0], cell2));

    }

    public class IntKernel : KernelBase<int>
    {
        public IntKernel(int vRadius, int hRadius) : base(vRadius, hRadius)
        {
        }

        static IntKernel()
        {
            _multiply = (a, b) => a * b;
            _add = (a, b) => a + b;
        }
    }

    public abstract class KernelBase<T>
        where T: unmanaged, IComparable<T>, IEquatable<T>
    {
        public KernelBase(int vRadius, int hRadius)
        {
            _kernel = new T[2*vRadius+1, 2*vRadius+1];
            _vRadius = vRadius;
            _hRadius = hRadius;
        }

        internal readonly T[,] _kernel;
        private readonly int _vRadius;
        private readonly int _hRadius;
        protected static Func<T, T, T> _multiply;
        protected static Func<T, T, T> _add;

        public T this[int x, int y] => _kernel[x,y];

        public IArray2d<R> Cast<R>()
        {
            throw new NotImplementedException();
        }

        public int GetLength(int dimension) => _kernel.GetLength(dimension);

        public T[,] ToArray() => _kernel;

        public static T operator *(KernelBase<T> kernel, ICell<T> cell)
        {
            T res = default;
            for (int i = 0; i < kernel.GetLength(0); i++)
                for (int j = 0; j < kernel.GetLength(1); j++)
                    res = _add(res, _multiply(cell[i - kernel._vRadius, j - kernel._hRadius], kernel[i, j]));

            return res;
        }
    }
}

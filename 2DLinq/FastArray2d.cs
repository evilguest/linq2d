using System.Linq.Expressions;

using static System.Linq.Expressions.Expression;

namespace System.Linq.Processing2d.Fast
{

    public struct FastArray2d<T> : IArray2d<T>
    {
        private readonly int _h;
        private readonly int _w;
        private readonly T[] _data; // temporary public

        public FastArray2d(IArray2d size) : this(size.GetLength(0), size.GetLength(1)) { }

        public FastArray2d(int height, int width)
        {
            _h = height;
            _w = width;
            _data = new T[height * width];
        }
        public FastArray2d(T[] data, int width)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (data.Length % width != 0)
                throw new ArgumentException("Data size doesn't match the width", nameof(data));
            _data = data;
            _w = width;
            _h = data.Length / _w;
        }

        public FastArray2d(T[,] data)
        {
            _h = data.GetLength(0);
            _w = data.GetLength(1);
            _data = new T[_h * _w];
            for (int i = 0; i < _h; i++)
                for (int j = 0; j < _w; j++)
                    this[i, j] = data[i, j];
        }

        public T this[int x, int y]
        {
            get => _data[x * _w + y];
            set => _data[x * _w + y] = value;
        }

        public int GetLength(int dimension)
        {
            switch(dimension)
            {
                case 0: return _h;
                case 1: return _w;
                default: throw new IndexOutOfRangeException("Unknown dimension requested");
            }
        }

        public IArray2d<R> Cast<R>()
        {
            var r = new FastArray2d<R>(this);
            for (int i = 0; i < GetLength(0); i++)
                for (int j = 0; j < GetLength(1); j++)
                    r[i, j] = (R)Convert.ChangeType(this[i, j], typeof(R));
            return r;
        }

        public unsafe T[,] ToArray()
        {
            var result = new T[_h, _w];
            for (int i = 0; i < GetLength(0); i++)
                for (int j = 0; j < GetLength(1); j++)
                    result[i, j] = this[i, j];
            return result;
        }
    }

}
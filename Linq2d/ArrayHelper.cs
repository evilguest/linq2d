using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Diagnostics.CodeAnalysis;
// using System.Text;

namespace Linq2d
{
    public static class ArrayHelper
    {
        public static T[,] Create<T>(int h, int w, int startX, int startY)
            => (T[,])Array.CreateInstance(typeof(T), [h, w], [startX, startY]);
        public static int Height<T>(this T[,] arr) => (arr ?? throw new ArgumentNullException(nameof(arr))).GetLength(0);
        public static int Width<T>(this T[,] arr) => (arr ?? throw new ArgumentNullException(nameof(arr))).GetLength(1);

        public static (int h, int w) EnsureSize(int minH, int minW, params Array[] arrays)
        {
            bool hasData = false;
            (int h, int w) size = (0, 0);
            var n = 1;
            foreach (var a in arrays)
            {
                if(a.GetLength(0)<minH || a.GetLength(1)<minW)
                    throw new ArgumentException($"Source {n} is too small. Expected[{minH},{minW}], found [{a.GetLength(0)},{ a.GetLength(1)}]", "source"+n);

                if (hasData)
                {
                    if (a.GetLength(0) != size.h || a.GetLength(1)!= size.w)
                        throw new ArgumentException($"Source {n} has wrong size. Expected [{size.h},{size.w}], found [{a.GetLength(0)},{a.GetLength(1)}]", "source"+n);
                }
                else
                {
                    size = (a.GetLength(0), a.GetLength(1));
                    hasData = true;
                }
                n++;
            }
            return hasData ? size : throw new InvalidOperationException("No sources are provided");
        }

        public static ArraySource<T> Wrap<T>(this T[,] array) => new(array);

        public static byte[,] Init1Diagonal(int size) => InitDiagonal(size, (byte)1);
        public static T[,] InitDiagonal<T>(int size, T value)
        {
            var result = new T[size, size];
            for (var i = 0; i < size; i++)
                result[i, i] = value;
            return result;
        }

        public static T[,] InitDiagonal<T>(int h, int w, T value)
        {
            var result = new T[h, w];
            for (var i = 0; i < Math.Max(h, w); i++)
                result[i % h, i % w] = value;
            return result;
        }

        public static T[,] InitAll<T>(int h, int w, T value)
        {
            var result = new T[h, w];
            for (var i = 0; i < h; i++)
                for (var j = 0; j < w; j++)
                    result[i, j] = value;
            return result;
        }
        public static long[,] InitAllRand(int h, int w, long seed)
            => InitAllRand(h, w, seed, x => x);
        public static int[,] InitAllRand(int h, int w, int seed)
            => InitAllRand(h, w, seed, x => x);
        public static T[,] InitAllRand<T>(int h, int w, int seed, Func<int, T> selector)
        {
            var r = new Random(seed);
            var result = new T[h, w];
            for (var i = 0; i < h; i++)
                for (var j = 0; j < w; j++)
                    result[i, j] = selector(r.Next());
            return result;
        }
        public static T[,] InitAllRand<T>(int h, int w, long seed, Func<long, T> selector)
        {
            var r = new Random((int)seed);
            var result = new T[h, w];
            for (var i = 0; i < h; i++)
                for (var j = 0; j < w; j++)
                    result[i, j] = selector(r.NextInt64());
            return result;
        }

        public static T[,] InitAllRand<T>(int h, int w, int seed1, int seed2, Func<int, int, T> selector)
        {
            var r1 = new Random(seed1);
            var r2 = new Random(seed2);
            var result = new T[h, w];
            for (var i = 0; i < h; i++)
                for (var j = 0; j < w; j++)
                    result[i, j] = selector(r1.Next(), r2.Next());
            return result;
        }

        public static T[,] InitAllRand<T>(int h, int w, long seed1, long seed2, Func<long, long, T> selector)
        {
            var r1 = new Random(seed1.GetHashCode());
            var r2 = new Random(seed2.GetHashCode());
            var result = new T[h, w];
            for (var i = 0; i < h; i++)
                for (var j = 0; j < w; j++)
                    result[i, j] = selector(r1.NextInt64(), r2.NextInt64());
            return result;
        }

        public static byte[,] InitAllRand(int h, int w, byte seed)
        {
            var r = new Random(seed);
            var result = new byte[h, w];
            for (var i = 0; i < h; i++)
                for (var j = 0; j < w; j++)
                    result[i, j] = (byte)r.Next();
            return result;
        }
        public static short[,] InitAllRand(int h, int w, short seed)
        {
            var r = new Random(seed);
            var result = new short[h, w];
            for (var i = 0; i < h; i++)
                for (var j = 0; j < w; j++)
                    result[i, j] = (short)r.Next();
            return result;
        }

        public static double[,] InitAllRandDouble(int h, int w, int seed)
        {
            var r = new Random(seed);
            var result = new double[h, w];
            for (var i = 0; i < h; i++)
                for (var j = 0; j < w; j++)
                    result[i, j] = r.NextDouble();
            return result;
        }

        public static T[,] InitAll<T>(int size, T value) => InitAll(size, size, value);

        /*
        public static string Dump<T>(this T[,] arr, string format)
        {
            var sb = new StringBuilder(arr.Length * format.Length);
            for(int i=0; i<arr.Height(); i++)
            {
                for (int j = 0; j < arr.Width(); j++)
                    sb.AppendFormat(format, arr[i, j]);
                sb.AppendLine();
            }
            Console.Write(sb);
            return sb.ToString();
        }
        */
    }

    /*
    public class ArrayComparer<T> : IEqualityComparer<T[,]>
    {

        public ArrayComparer(IEqualityComparer<T> comparer)
        {
            Comparer = comparer ?? EqualityComparer<T>.Default;
        }

        public IEqualityComparer<T> Comparer { get; }

        public bool Equals([AllowNull] T[,] x, [AllowNull] T[,] y)
        {
            if (x == null)
                return y == null;
            if (y == null)
                return x == null;
            if (x.GetLength(0) != y.GetLength(0) || x.GetLength(1) != y.GetLength(1))
                return false;
            for (int i = 0; i < x.Height(); ++i)
                for (int j = 0; j < x.Width(); ++j)
                    if (!Comparer.Equals(x[i, j], y[i, j]))
                        return false;
            return true;
        }

        public int GetHashCode([DisallowNull] T[,] array)
        {
            int hc = 0;
            for (int i = 0; i < array.Height(); ++i)
                for (int j = 0; j < array.Width(); ++j)
                    hc = unchecked(hc * 314159 + Comparer.GetHashCode(array[i, j]));
            return hc;
        }
    }*/
}

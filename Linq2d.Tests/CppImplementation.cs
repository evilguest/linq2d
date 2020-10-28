using System;
using System.Runtime.InteropServices;

namespace Linq2d.Tests
{
    public static unsafe class UnmanagedSauvola
    {
        public static (int[,], long[,]) PreIntegrate(byte[,] data)
        {
            var h = data.Height();
            var w = data.Width();
            var linear = new int[h, w];
            var square = new long[h, w];
            fixed (byte* source = &data[0, 0])
            fixed (int* pLinear = &linear[0, 0])
            fixed (long* pSquare = &square[0, 0])
            {
                var r = preIntegrate(h, w, source, pLinear, pSquare);
                switch (r)
                {
                    case 0: return (linear, square);
                    case -1: throw new InvalidOperationException("NULL input detected");
                    case -2: throw new InvalidOperationException("NULL output detected");
                    default: throw new InvalidOperationException($"Unexpected value {r} has been returned");
                }
            }

        }
        public static byte[,] Transform(byte[,] data, int wHalf, double k)
        {
            var h = data.Height();
            var w = data.Width();
            var result = new byte[h, w];
            fixed (byte* source = &data[0, 0])
            fixed (byte* target = &result[0, 0])
            {
                var r = sauvolaBinarize(h, w, source, target, wHalf, k);
                switch (r)
                {
                    case 0: return result;
                    case -1: throw new InvalidOperationException("NULL input detected");
                    case -2: throw new InvalidOperationException("NULL output detected");
                    default: throw new InvalidOperationException($"Unexpected value {r} has been returned");
                }
            }
        }

        [DllImport("SauvolaBinarizeCPP")]
        private static extern int sauvolaBinarize(int h, int w, byte* input, byte* output, int whalf, double K);
        [DllImport("SauvolaBinarizeCPP")]
        private static extern int preIntegrate(int h, int w, byte* input, int* linear, long* square);
    }
    public unsafe class UnmanagedC4
    {
        public static int[,] Transform(byte[,] data)
        {
            var h = data.Height();
            var w = data.Width();
            var result = new int[h, w];
            fixed (byte* source = &data[0, 0])
            fixed (int* target = &result[0, 0])
            {
                var r = c4filter(h, w, source, target);
                switch (r)
                {
                    case 0: return result;
                    case -1: throw new InvalidOperationException("NULL input detected");
                    case -2: throw new InvalidOperationException("NULL output detected");
                    default: throw new InvalidOperationException($"Unexpected value {r} has been returned");
                }
            }
        }

        [DllImport("SauvolaBinarizeCPP")]
        private static extern int c4filter(int h, int w, byte* input, int* output);
    }

}

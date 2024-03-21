using System.Runtime.InteropServices;

namespace Linq2d.Tests
{
    public static unsafe partial class UnmanagedSauvola
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
                return preIntegrate(h, w, source, pLinear, pSquare) switch
                {
                    0 => (linear, square),
                    -1 => throw new InvalidOperationException("NULL input detected"),
                    -2 => throw new InvalidOperationException("NULL output detected"),
                    _ => throw new InvalidOperationException($"Unexpected value {preIntegrate(h, w, source, pLinear, pSquare)} has been returned"),
                };
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
                return sauvolaBinarize(h, w, source, target, wHalf, k) switch
                {
                    0 => result,
                    -1 => throw new InvalidOperationException("NULL input detected"),
                    -2 => throw new InvalidOperationException("NULL output detected"),
                    _ => throw new InvalidOperationException($"Unexpected value {sauvolaBinarize(h, w, source, target, wHalf, k)} has been returned"),
                };
            }
        }

        [LibraryImport("SauvolaBinarizeCPP")]
        private static partial int sauvolaBinarize(int h, int w, byte* input, byte* output, int whalf, double K);
        [LibraryImport("SauvolaBinarizeCPP")]
        private static partial int preIntegrate(int h, int w, byte* input, int* linear, long* square);
    }
    public unsafe partial class UnmanagedC4
    {
        public static int[,] TransformAsm(byte[,] data)
        {
            var h = data.Height();
            var w = data.Width();
            var result = new int[h, w];
            fixed (byte* source = &data[0, 0])
            fixed (int* target = &result[0, 0])
            {
                return c4filterAsm(h, w, source, target) switch
                {
                    0 => result,
                    -1 => throw new InvalidOperationException("NULL input detected"),
                    -2 => throw new InvalidOperationException("NULL output detected"),
                    _ => throw new InvalidOperationException($"Unexpected value {c4filterAsm(h, w, source, target)} has been returned"),
                };
            }
        }
        public static int[,] Transform(byte[,] data)
        {
            var h = data.Height();
            var w = data.Width();
            var result = new int[h, w];
            fixed (byte* source = &data[0, 0])
            fixed (int* target = &result[0, 0])
            {
                return c4filter(h, w, source, target) switch
                {
                    0 => result,
                    -1 => throw new InvalidOperationException("NULL input detected"),
                    -2 => throw new InvalidOperationException("NULL output detected"),
                    _ => throw new InvalidOperationException($"Unexpected value {c4filter(h, w, source, target)} has been returned"),
                };
            }
        }

        [LibraryImport("SauvolaBinarizeCPP")]
        private static partial int c4filter(int h, int w, byte* input, int* output);
        [LibraryImport("SauvolaBinarizeCPP")]
        private static partial int c4filterAsm(int h, int w, byte* input, int* output);
    }

}

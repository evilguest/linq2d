using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Linq2d.Benchmarks
{
    [InProcess]
    public class SaturatedMultiplyDoubleBenchmark 
    {
        private double[,] array;
        private Func<double[,], double[,]> _transform;
        [GlobalSetup]
        public void Initialize()
        {
            var r = new Random(42);
            array = new double[1000, 1000];
            for (var i = 0; i < array.GetLength(0); i++)
                for (var j = 0; j < array.GetLength(1); j++)
                    array[i, j] = r.NextDouble();
            _transform = (from a in array select Math.Min(a * 1.5, 1.0)).Transform;

        }
        [Benchmark]
        public double[,] Linq2d()
            => (from a in array select Math.Min(a * 1.5, 1)).ToArray();
        [Benchmark]
        public double[,] Linq2dCached()
            => _transform(array);
        [Benchmark(Baseline = true)]
        public double[,] Cpp() => NativeSaturatedMultiplyDouble(array);

        private unsafe static double[,] NativeSaturatedMultiplyDouble(double[,] data)
        {
            var h = data.Height();
            var w = data.Width();
            var result = new double[h, w];
            fixed (double* source = &data[0, 0])
            fixed (double* target = &result[0, 0])
            {
                var r = SaturatedMultiplyDouble(h, w, source, target);
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
        private unsafe static extern int SaturatedMultiplyDouble(int h, int w, double* input, double* output);

    }
}

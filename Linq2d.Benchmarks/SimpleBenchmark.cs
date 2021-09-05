using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Linq2d.Benchmarks
{
    [InProcess]
    public class SimpleBenchmark 
    {
        private double[,] array;
        [GlobalSetup]
        public void Initialize()
        {
            var r = new Random(42);
            array = new double[1000, 1000];
            for (var i = 0; i < array.GetLength(0); i++)
                for (var j = 0; j < array.GetLength(1); j++)
                    array[i, j] = r.NextDouble();
        }
        [Benchmark]
        public double[,] Multiply()
            => (from a in array select Math.Min(a * 1.5, 1)).ToArray();
    }
}

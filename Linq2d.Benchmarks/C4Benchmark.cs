using BenchmarkDotNet.Attributes;
using Linq2d.Tests;
using Linq2d.Tests.Vectorization;
using System;

namespace Linq2d.Benchmarks
{

    [InProcess]
    public class C4Benchmark:ImageBenchmark
    {
        public C4Benchmark()
        {
        }

        [GlobalSetup]
        public override void Initialize()
        {
            base.Initialize();
            Array2d.TryVectorize = true;
            var q = GetQuery();
            _integrateVector = q.Transform;
            var ev = (IVectorizable)q;
            if(!ev.Vectorized)
                Console.Error.WriteLine($"C4 vectorization failed:\n{ev.Report()}");

            Array2d.TryVectorize = false;
            _integrateScalar = GetQuery().Transform;
        }

        private IArrayTransform<byte, int> GetQuery() => 
            from d in _data.With(OutOfBoundsStrategy.NearestNeighbour)
            select (d[-1, 0] + d[0, -1] + d[1, 0] + d[0, 1]) / 4;


        [Benchmark]
        public int[,] CppC4() => UnmanagedC4.Transform(_data);
        
        [Benchmark]
        public int[,] AsmC4() => UnmanagedC4.TransformAsm(_data);

        [Benchmark]
        public int[,] NaturalC4()
        {
            int w = _data.Width();
            int h = _data.Height();
            int[,] res = new int[h, w];
            // handle left corner
            res[0, 0] = (2 * _data[0, 0] + _data[0, 1] + _data[1, 0]) / 4;

            //handle first line
            for (var j = 1; j < w - 1; j++)
                res[0, j] = (
                                _data[0, j - 1]
                                + _data[0, j]
                                + _data[0, j + 1]
                                + _data[1, j]
                            ) / 4;
            // handle top right corner
            res[0, w - 1] = (2 * _data[0, w - 1] + _data[0, w - 2] + _data[1, w - 1]) / 4;

            //handle the other lines

            for (var i = 1; i < h - 1; i++)
            {
                // handle the first column
                res[i, 0] = (_data[i, 0] + _data[i - 1, 0] + _data[i + 1, 0] + _data[i, 1]) / 4;

                //handle the other columns
                for (var j = 1; j < w - 1; j++)
                    res[i, j] = (
                          _data[i - 1, j]
                        + _data[i, j - 1]
                        + _data[i + 1, j]
                        + _data[i, j + 1]
                        ) / 4;
                //handle the last column
                res[i, w - 1] = (_data[i, w - 1] + _data[i, w - 2] + _data[i + 1, w - 1] + _data[i - 1, w - 1]) / 4;
            }
            // handle the bottom left corner
            res[h - 1, 0] = (2 * _data[h - 1, 0] + _data[h - 2, 0] + _data[h - 1, 1]) / 4;

            //handle last line
            for (var j = 1; j < w - 1; j++)
                res[h - 1, j] = (
                                _data[h - 1, j - 1]
                                + _data[h - 1, j]
                                + _data[h - 1, j + 1]
                                + _data[h - 2, j]
                            ) / 4;
            // handle bottom right corner
            res[h - 1, w - 1] = (2 * _data[h - 1, w - 1] + _data[h - 1, w - 2] + _data[h - 2, w - 1]) / 4;
            return res;
        }

        [Benchmark(Baseline=true)]
        public unsafe int[,] UnsafeC4()
        {
            return SimpleFilters.C4NNUnsafeScalar(_data);
        }
        [Benchmark]
        public int[,] LinqC4()
        {
            Array2d.TryVectorize = true;
            return GetQuery().ToArray();
        }
        [Benchmark]
        public int[,] LinqC4VectorCached()
        {
            return _integrateVector(_data);
        }
    }
}

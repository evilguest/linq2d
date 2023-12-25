using BenchmarkDotNet.Attributes;
using Linq2d.Tests;
using Linq2d.Tests.Vectorization;
using Mono.Linq.Expressions;
using System;

namespace Linq2d.Benchmarks
{
    [InProcess]
    public class SauvolaBenchmark : ImageBenchmark
    {
        private Func<byte[,], (int[,], long[,])> _preIntegrateScalar;
        private Func<byte[,], int[,], long[,], byte[,]> _edgeDetectScalar;
        private Func<byte[,], (int[,], long[,])> _preIntegrateVector;
        private Func<byte[,], int[,], long[,], byte[,]> _edgeDetectVector;



        [GlobalSetup]
        public override void Initialize()
        {
            base.Initialize();
            Array2d.TryVectorize = true;
            _preIntegrateVector = GetIntegral().Transform;
            var edgeDetectVector = GetDetect(new int[0, 0], new long[0, 0]);
            _edgeDetectVector = edgeDetectVector.Transform;
            IVectorizable ev = ((IVectorizable)edgeDetectVector);
            if (!ev.Vectorized)
                Console.Error.WriteLine($"Sauvola Edge Detect vectorization failed:\n{ev.Report}");
            Array2d.TryVectorize = false; // force scalar
            _preIntegrateScalar = GetIntegral().Transform;
            _edgeDetectScalar = GetDetect(new int[0, 0], new long[0, 0]).Transform;

            var t = _edgeDetectVector(new byte[20, 20], new int[20, 20], new long[20, 20]); // force JIT
            t = _edgeDetectScalar(new byte[20, 20], new int[20, 20], new long[20, 20]); // force JIT
            //Console.WriteLine(t.Length);
        }

        [Benchmark]
        public byte[,] SafeSauvola()
        {
            return Tests.SauvolaBinarize.BaseBinarize(_data);
        }
        [Benchmark(Baseline = true)]
        public byte[,] UnsafeSauvolaScalar()
        {
            return UnsafeSauvola(_data, WHalf);
        }

        [Benchmark]
        public byte[,] CppSauvola() 
            => UnmanagedSauvola.Transform(_data, WHalf, K);

        [Benchmark]
        public byte[,] LinqSauvolaVector()
        {
            Array2d.TryVectorize = true;
            var (p, q) = GetIntegral().ToArrays();
            return GetDetect(p, q).ToArray();
        }
        [Benchmark]
        public byte[,] LinqSauvolaScalar()
        {
            Array2d.TryVectorize = false;
            var (p, q) = GetIntegral().ToArrays();
            return GetDetect(p, q).ToArray();
        }
        [Benchmark]
        public byte[,] CachedLinqSauvolaVector()
        {
            var (p, q) = _preIntegrateVector(_data);
            return _edgeDetectVector(_data, p, q);
        }
        [Benchmark]
        public byte[,] CachedLinqSauvolaScalar()
        {
            var (p, q) = _preIntegrateScalar(_data);
            return _edgeDetectScalar(_data, p, q);
        }

        private IArrayTransform2<byte, int, long> GetIntegral() =>
            from g in _data
            from ri in Result.InitWith(0)
            from rq in Result.InitWith(0L)
            select ValueTuple.Create(
                ri[-1, 0] + ri[0, -1] - ri[-1, -1] + g,
                rq[-1, 0] + rq[0, -1] - rq[-1, -1] + g * g);

        private IArrayTransform<byte, int, long, byte> GetDetect(int[,] p, long[,] sq) =>
            from g in _data
            from i in p.With(OutOfBoundsStrategy.Integral(0))
            from q in sq.With(OutOfBoundsStrategy.Integral(0L))
            let tl = i.Offset(-WHalf - 1, -WHalf - 1)
            let tr = i.Offset(-WHalf - 1, WHalf)
            let bl = i.Offset(WHalf, -WHalf - 1)
            let br = i.Offset(WHalf, WHalf)
            let tlq = q.Offset(-WHalf - 1, -WHalf - 1)
            let trq = q.Offset(-WHalf - 1, WHalf)
            let blq = q.Offset(WHalf, -WHalf - 1)
            let brq = q.Offset(WHalf, WHalf)
            let area = (br.X - tl.X) * (br.Y - tl.Y)
            let diff = br + tl - tr - bl 
            let sqdiff = brq + tlq - trq  - blq 
            let mean = (double)diff / area
            let std = Math.Sqrt((sqdiff - diff * mean) / (area - 1))

            let threshold = mean * (1 + K * ((std / 128) - 1))

            select g > threshold ? byte.MaxValue : byte.MinValue;

        [Params(5)] //[Params(1, 2, 3, 4, 5, 6, 7, 8)]
        public int WHalf { get; set; } = 5;
        public static readonly double K = 0.1;

        public static unsafe byte[,] UnsafeSauvola(byte[,] grayScale, int whalf)
        {
            int height = grayScale.Height();
            int width = grayScale.Width();
            var p = new int[height, width];
            var sq = new long[height, width];
            byte[,] result = new byte[height, width];

            fixed (byte* gPtr = &grayScale[0, 0])
            fixed (int* pPtr = &p[0, 0])
            fixed (long* sqPtr = &sq[0, 0])
            fixed (byte* rPtr = &result[0, 0])
            {
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                    {
                        pPtr[i * width + j] = gPtr[i * width + j];
                        sqPtr[i * width + j] = gPtr[i * width + j] * gPtr[i * width + j];
                        if (i > 0)
                        {
                            pPtr[i * width + j] += pPtr[(i - 1) * width + j];
                            sqPtr[i * width + j] += sqPtr[(i - 1) * width + j];
                        }
                        if (j > 0)
                        {
                            pPtr[i * width + j] += pPtr[i * width + j - 1];
                            sqPtr[i * width + j] += sqPtr[i * width + j - 1];
                        }
                        if (i > 0 && j > 0)
                        {
                            pPtr[i * width + j] -= pPtr[(i - 1) * width + j - 1];
                            sqPtr[i * width + j] -= sqPtr[(i - 1) * width + j - 1];
                        }
                    }
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                    {
                        var xmin = Math.Max(0, i - whalf);
                        var ymin = Math.Max(0, j - whalf);
                        var xmax = Math.Min(height - 1, i + whalf);
                        var ymax = Math.Min(width - 1, j + whalf);

                        var area = (xmax - xmin + 1) * (ymax - ymin + 1);

                        var diff = pPtr[width * xmax + ymax];
                        var sqdiff = sqPtr[width * xmax + ymax];
                        if (xmin > 0)
                        {
                            diff -= pPtr[width * (xmin - 1) + ymax];
                            sqdiff -= sqPtr[width * (xmin - 1) + ymax];
                        }
                        if (ymin > 0)
                        {
                            diff -= pPtr[width * xmax + ymin - 1];
                            sqdiff -= sqPtr[width * xmax + ymin - 1];
                        }
                        if (xmin > 0 && ymin > 0)
                        {
                            diff += pPtr[width * (xmin - 1) + ymin - 1];
                            sqdiff += sqPtr[width * (xmin - 1) + ymin - 1];
                        }
                        var mean = (double)diff / area;
                        var std = Math.Sqrt((sqdiff - diff * mean) / (area - 1));

                        var threshold = mean * (1 + K * ((std / 128) - 1));

                        rPtr[i * width + j] = gPtr[i * width + j] > threshold ? byte.MaxValue : byte.MinValue;
                    }
                return result;
            }
        }
    }
}

using ImageHelpers;
using Linq2d;
using System;
using System.IO;

namespace SauvolaBinarize
{
    class Program
    {
        static void Main(string[] args)
        {
            var WHalf = 6;
            var K = 0.3;
            var integrateQuery = from d in new byte[0,0]
                                 from rl in Result.InitWith(0)
                                 from rq in Result.InitWith(0L)
                                 select ValueTuple.Create(
                                   rl[-1, 0] + rl[0, -1] - rl[-1, -1] + d,
                                   rq[-1, 0] + rq[0, -1] - rq[-1, -1] + d * d);

            var thresholdQuery = from i in new int[0, 0].With(OutOfBoundsStrategy.Integral(0))
                                 from q in new long[0, 0].With(OutOfBoundsStrategy.Integral(0L))

                                 let itl = i.Offset(-WHalf - 1, -WHalf - 1)
                                 let itr = i.Offset(-WHalf - 1, WHalf)
                                 let ibl = i.Offset(WHalf, -WHalf - 1)
                                 let ibr = i.Offset(WHalf, WHalf)
                                 let sum = ibr + itl - itr - ibl

                                 let qtl = q.Offset(-WHalf - 1, -WHalf - 1)
                                 let qtr = q.Offset(-WHalf - 1, WHalf)
                                 let qbl = q.Offset(WHalf, -WHalf - 1)
                                 let qbr = q.Offset(WHalf, WHalf)
                                 let sumsq = qbr + qtl - qtr - qbl

                                 let area = (ibr.X - itl.X) * (ibr.Y - itl.Y)
                                 let mean = (double)sum / area
                                 let std = Math.Sqrt((sumsq - sum * mean) / (area - 1))
                                 select (byte)(mean * (1 + K * ((std / 128) - 1)));
            
            var detectQuery = from d in new byte[0,0]
                              from t in new byte[0,0]
                              select d > t ? byte.MaxValue : byte.MinValue;

            var integrate = integrateQuery.Transform;
            var getThreshold = thresholdQuery.Transform;
            var detect = detectQuery.Transform;

            foreach (var fileName in args)
            {
                var (data, palette) = IO.ReadImage(fileName);
                data = (from d in data
                        select palette[d]).ToArray();

                Array2d.TryVectorize = true;
                var (s, sq) = integrate(data);
                var t = getThreshold(s, sq);
                var r = detect(data, t);
                IO.WriteImage(Path.ChangeExtension(fileName, ".threshold.bmp"), t);
                IO.WriteImage(Path.ChangeExtension(fileName, ".linq.bmp"), r);
            }
        }
    }
}

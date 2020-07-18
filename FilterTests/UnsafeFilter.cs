using BenchmarkHelpers;
using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace FilterTests
{
    public unsafe class UnsafeFilter : ArrayFilterBase<int>, IArrayFilter<int>
    {
        public int[,] C2()
        {
            var h = Data.GetLength(0);
            var w = Data.GetLength(1);
            var result = new int[h, w];
            var W = w;
            h--; // decrease to use as the cycle limit
            w--; // decrease to use as the cycle limit

            fixed (int* psource = &Data[1, 1])
            fixed (int* presult = &result[1, 1])
            {
                var psourceptr = psource;
                var delta = presult - psource;

                for (int i = 1; i < h; i++)
                {
                    for (var j = 1; j < w; j++) // handle the row remainder with scalar operations
                    {
                        psourceptr[delta] = (psourceptr[-W] + psourceptr[W]) / 2;
                        psourceptr++;
                    }
                    psourceptr += 2;
                }
            }
            return result;

        }

        public int[,] C4()
        {
            var h = Data.GetLength(0);
            var w = Data.GetLength(1);
            var result = new int[h, w];
            var W = w;
            h--; // decrease to use as the cycle limit
            w--; // decrease to use as the cycle limit

            fixed (int* psource = &Data[1, 1])
            fixed (int* presult = &result[1, 1])
            {
                var psourceptr = psource;
                var delta = presult - psource;

                for (int i = 1; i < h; i++)
                {
                    for (var j = 1; j < w; j++) // handle the row remainder with scalar operations
                    {
                        psourceptr[delta] = (psourceptr[-W] + psourceptr[-1] + psourceptr[1] + psourceptr[W]) / 4;
                        psourceptr++;
                    }
                    psourceptr += 2;
                }
            }
            return result;

        }

        public int[,] C8()
        {
            var h = Data.GetLength(0);
            var w = Data.GetLength(1);
            var result = new int[h, w];

            fixed (int* psource = &Data[0, 0])
            fixed (int* presult = &result[0, 0])
            {
                var resultDelta = presult - psource;
                for (int i = 1; i < h - 1; i++)
                    for (var psourceptr = psource + i * w + 1; psourceptr < psource + i * w + w - 1; psourceptr++)
                        psourceptr[resultDelta] = (psourceptr[-w - 1] + psourceptr[-w] + psourceptr[-w + 1]
                                                 + psourceptr[-1]             +          psourceptr[1]
                                                 + psourceptr[ w - 1] + psourceptr[ w] + psourceptr[ w + 1]) / 8;
            }
            return result;
        }
    }
}

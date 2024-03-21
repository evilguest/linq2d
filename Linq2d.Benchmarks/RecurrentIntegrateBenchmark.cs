﻿using BenchmarkDotNet.Attributes;
using Linq2d.Tests.Vectorization;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

using Avx = System.Runtime.Intrinsics.X86.Avx;
using Avx2 = System.Runtime.Intrinsics.X86.Avx2;
namespace Linq2d.Benchmarks
{
    [InProcess]
    public class RecurrentIntegrateBenchmark: ImageBenchmark
    {

        [GlobalSetup]
        public override void Initialize()
        {
            base.Initialize();
            Array2d.TryVectorize = true;
            var vectorIntegrate = GetIntegrate();
            var _ = vectorIntegrate.Transform; // force compilation
            IVectorizable ev = ((IVectorizable)vectorIntegrate);
            if (!ev.Vectorized)
            {
                Console.Error.WriteLine("Recurrent integration failed:");
                Console.Error.WriteLine(ev.Report());
            }
            else 
              _integrateVector = vectorIntegrate.Transform;

            Array2d.TryVectorize = false;
            _integrateScalar = GetIntegrate().Transform;
        }

        private IArrayTransform<byte, int> GetIntegrate()
        {
            return (from d in _data
                    from r in Result.InitWith(0)
                    select d + r[0, -1] + r[-1, 0] - r[-1, -1]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Vector256<int> Aggregate(Vector256<int> t, int carry)
        {
            var shiftRight = RotateRight;
            var t2 = Avx2.PermuteVar8x32(t, shiftRight); t2 = t2.WithElement(0, carry);
            t = Avx2.Add(t, t2);
            t2 = Avx2.PermuteVar8x32(t2, shiftRight); t2 = t2.WithElement(0, 0);  t = Avx2.Add(t, t2);
            t2 = Avx2.PermuteVar8x32(t2, shiftRight); t = Avx2.Add(t, t2);
            t2 = Avx2.PermuteVar8x32(t2, shiftRight); t = Avx2.Add(t, t2);
            t2 = Avx2.PermuteVar8x32(t2, shiftRight); t = Avx2.Add(t, t2);
            t2 = Avx2.PermuteVar8x32(t2, shiftRight); t = Avx2.Add(t, t2);
            t2 = Avx2.PermuteVar8x32(t2, shiftRight); t = Avx2.Add(t, t2);
            t2 = Avx2.PermuteVar8x32(t2, shiftRight); t = Avx2.Add(t, t2);
            return t;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Vector256<int> Aggregate(Vector256<int> prev, Vector256<int> curr)
        {
            var shiftRight = RotateRight;
            Vector256<int> temp;
            temp = Avx2.PermuteVar8x32(curr, shiftRight); temp = temp.WithElement(0, prev.GetElement(7)); var r = temp;
            temp = Avx2.PermuteVar8x32(temp, shiftRight); temp = temp.WithElement(0, prev.GetElement(6)); r = Avx2.Add(r, temp);
            temp = Avx2.PermuteVar8x32(temp, shiftRight); temp = temp.WithElement(0, prev.GetElement(5)); r = Avx2.Add(r, temp);
            temp = Avx2.PermuteVar8x32(temp, shiftRight); temp = temp.WithElement(0, prev.GetElement(4)); r = Avx2.Add(r, temp);
            temp = Avx2.PermuteVar8x32(temp, shiftRight); temp = temp.WithElement(0, prev.GetElement(3)); r = Avx2.Add(r, temp);
            temp = Avx2.PermuteVar8x32(temp, shiftRight); temp = temp.WithElement(0, prev.GetElement(2)); r = Avx2.Add(r, temp);
            temp = Avx2.PermuteVar8x32(temp, shiftRight); temp = temp.WithElement(0, prev.GetElement(1)); r = Avx2.Add(r, temp);
            return r;
        }


        [Benchmark]
        unsafe public int[,] IntegrateUnsafeVector()
        {
            if (!Avx2.IsSupported)
                throw new InvalidOperationException("Avx2 is not supported - cannot perform vector operation");
            int w = _data.Width();
            int h = _data.Height();
            int[,] res = new int[h, w];

            fixed (byte* pSource = &_data[0, 0])
            fixed (int* pTarget = &res[0, 0])
            {
                var pSrc = pSource;
                var pTrg = pTarget;

                {// handle the first line
                    var j = 0;
                    var c = 0;
                    //handle vector part 
                    for (; j + Vector256<int>.Count <= w; j += Vector256<int>.Count)
                    {
                        var t = Avx2.ConvertToVector256Int32(pSrc);

                        t = Aggregate(t, c);

                        Avx.Store(pTrg, t);
                        c = t.GetElement(Vector256<int>.Count - 1);

                        pSrc += Vector256<int>.Count;
                        pTrg += Vector256<int>.Count;
                    }

                    // handle the tail
                    for (; j < w; j++)
                    {
                        c += *pSrc++;
                        *pTrg++ = c;
                    }
                }
                //handle the other lines

                for (var i = 1; i < h; i++)
                {
                    var j = 0;
                    var c = 0;
                    //handle vector part 
                    for (; j + Vector256<int>.Count <= w; j += Vector256<int>.Count)
                    {
                        var t = Avx2.ConvertToVector256Int32(pSrc);

                        t = Aggregate(t, c);

                        c = t.GetElement(Vector256<int>.Count - 1);

                        var p = Avx.LoadVector256(pTrg - w); // prev line vector

                        t = Avx2.Add(t, p);

                        Avx.Store(pTrg, t);
                        pSrc += Vector256<int>.Count;
                        pTrg += Vector256<int>.Count;
                    }

                    // handle the tail
                    for (; j < w; j++)
                    {
                        c += *pSrc++;

                        var q = *(pTrg - w);

                        *pTrg++ = c + q;
                    }
                }
            }
            return res;
        }

        [Benchmark] 
        public unsafe int[,] IntegrateUnsafeVectorBranched()
        {
            int w = _data.Width();
            int h = _data.Height();
            int[,] res = new int[h, w];

            Vector256<int> shiftRight = RotateRight;

            fixed (byte* pSource = &_data[0, 0])
            fixed (int* pTarget = &res[0, 0])
            {
                var pSrc = pSource;
                var pTrg = pTarget;

                for (var i = 0; i < h; i++)
                {
                    var j = 0;

                    var p = Vector256.CreateScalar(0);
                    var pr = Vector256.CreateScalar(0);
                    //handle vector part 
                    for (; j + Vector256<int>.Count <= w; j += Vector256<int>.Count)
                    {
                        var t = Avx2.ConvertToVector256Int32(pSrc); //(int)*(pSrc)

                        var s = Aggregate(p, t);    // this code block has to be 
                        p = t;                      // added to handle the in-line 
                        t = Avx2.Add(t, s);         // recursion: S[i]=a[i]+S[i-1]

                        if (j > 0)
                            t = Avx2.Add(t, pr);    // t += *(pTrg - 1);

                        if (i > 0)
                        {
                            t = Avx2.Add(t, Avx.LoadVector256(pTrg - w));
                            if (j > 0)
                                t = Avx2.Subtract(t, Avx.LoadVector256(pTrg - w - 8));
                        }

                        Avx.Store(pTrg, t);
                        pr = t;
                        pSrc += Vector256<int>.Count;
                        pTrg += Vector256<int>.Count;
                    }


                    // handle the tail
                    var pr2 = (j == 0 ? 0 : pr.GetElement(Vector256<int>.Count - 1)); //  Vector256.CreateScalar(0);
                    for (; j < w; j++)
                    {
                        var t = (int)*(pSrc);           // Avx2.ConvertToVector256Int32(pSrc);
                        if (j > 0)
                            t += pr2;                   //    t = Avx2.Add(t, pr);    
                        if (i > 0)
                        {
                            t += *(pTrg - w);           //  Avx2.Add(t, Avx.LoadVector256(pTrg - w));
                            if (j > 0)
                                t -= *(pTrg - w - 1);   //  Avx2.Subtract(t, Avx.LoadVector256(pTrg - w - 8));
                        }

                        *pTrg = t;                      // Avx2.Store(pTrg, t);
                        pr2 = t;                        // pr = t
                        pSrc++;
                        pTrg++;
                    }
                }
            }
            return res;
        }


        [Benchmark]
        public int[,] IntegrateLinqCachedScalar() => _integrateScalar(_data);
        [Benchmark]
        public int[,] IntegrateLinqCachedVector() => _integrateVector(_data);


        [Benchmark]
        public int[,] IntegrateLinqScalar()
        {
            Array2d.TryVectorize = false;
            return GetIntegrate().ToArray();
        }

        [Benchmark]
        public int[,] IntegrateLinqVector()
        {
            Array2d.TryVectorize = true;
            var q = GetIntegrate();
            if (q is IVectorizable v && !v.Vectorized)
                throw new InvalidOperationException($"Vectorization failed due to {v.Report()}");
            return q.ToArray();
        }

        /* private unsafe static void VerticalIntegrate(byte[,] source, int[,] target, int startColumn, int width)
        {
            var stride = target.Width();
            var height = source.Height();

            // handle first line: just copying over the source, nothing to add yet
            fixed (byte* pSource = &source[0, startColumn])
            fixed (int* pTarget = &target[0, startColumn])
            {
                var pSrc = pSource;
                var pTrg = pTarget;
                var j = 0;
                for (; j + Vector256<int>.Count <= width; j += Vector256<int>.Count)
                {
                    Avx2.Store(pTrg, Avx2.ConvertToVector256Int32(pSrc));
                    pSrc += Vector256<int>.Count;
                    pTrg += Vector256<int>.Count;
                }
                for (; j < width; j++)
                    *pTrg++ = *pSrc++;
            }

            // handle remaining lines: adding the previous line
            for (int i = 1; i < height; i++)
            {
                fixed (byte* pSource = &source[i, startColumn])
                fixed (int* pTarget = &target[i, startColumn])
                {
                    var pSrc = pSource;
                    var pTrg = pTarget;
                    int j;
                    for (j = 0; j + Vector256<int>.Count <= width; j += Vector256<int>.Count)
                    {
                        var s = Avx2.ConvertToVector256Int32(pSrc);
                        var t = Avx2.LoadVector256(pTrg - stride);
                        Avx2.Store(pTrg, Avx2.Add(s, t));
                        pSrc += Vector256<int>.Count;
                        pTrg += Vector256<int>.Count;
                    }
                    for (; j < width; j++)
                    {
                        *pTrg = *(pTrg - stride) + *pSrc;
                        pSrc++; pTrg++;
                    }

                }
            }
        }
        private unsafe static void HorizontalIntegrate(int[,] source, int[,] target, int startLine, int height)
        {
            Vector256<int> shiftRight = RightShift;

            var w = source.Width();

            fixed (int* pSource = &source[startLine, 0])
            fixed (int* pTarget = &target[startLine, 0])
            {
                var pSrc = pSource;
                var pTrg = pTarget;
                for (int i = 0; i < height; i++)
                {
                    var j = 0;
                    var c = 0;
                    //handle vector part 
                    for (; j + Vector256<int>.Count <= w; j += Vector256<int>.Count)
                    {
                        var t = Avx2.LoadVector256(pSrc);
                        t = Aggregate(t, c);
                        c = t.GetElement(7);
                        Avx2.Store(pTrg, t);
                        pSrc += Vector256<int>.Count;
                        pTrg += Vector256<int>.Count;
                    }

                    // handle the tail
                    for (; j < w; j++)
                    {
                        c += *pSrc++;
                        *pTrg++ = c;
                    }
                }
            }
        }
    */

        /*
        /// <summary>
        /// Divides w into numTasks bins so:
        /// - Each bin size (except, maybe, the last one) is divisible by basis
        /// - The bins are relatively to each other in size
        /// </summary>
        /// <param name="w"></param>
        /// <param name="num"></param>
        /// <param name="basis"></param>
        /// <returns></returns>
        private static int[] DivideEvenly(int w, int num, int basis)
        {
            var n = w / basis;
            var r1 = w % basis;
            var k = n / num;
            var r2 = n % num;

            var res = new int[num];

            int i = 0;
            while (i < r2)
                res[i++] = (k + 1) * basis;

            res[i++] = k * basis + r1;

            while (i < num)
                res[i++] = k * basis;
            return res;
        }

        [Benchmark] // 2 place; +25% to the winner
        unsafe public int[,] IntegrateAvx2Pass()
        {

            // we will do two passes, as it is multicore-friendlier than having a single-pass.

            int w = _data.Width();
            int h = _data.Height();

            // pass 1: top-to-bottom aggregation

            int[,] step1 = new int[h, w];

            var numTasks = Math.Min(w / Vector256<int>.Count, System.Environment.ProcessorCount);

            var taskWidths = DivideEvenly(w, numTasks, Vector256<int>.Count);
            var vTasks = new Task[numTasks];
            var position = 0;
            for (var i = 0; i < numTasks; i++)
            {
                var p = position; var width = taskWidths[i];
                vTasks[i] = Task.Run(() => VerticalIntegrate(_data, step1, p, width));
                position += taskWidths[i];
            }

            Task.WaitAll(vTasks);

            // Now in step1 we have the vertical-integrated array; so now it is time to perform the horizontal integration. 

            var step2 = new int[h, w];

            numTasks = Math.Min(h / Vector256<int>.Count, System.Environment.ProcessorCount);

            var taskHeights = DivideEvenly(h, numTasks, Vector256<int>.Count);
            var hTasks = new Task[numTasks];
            position = 0;
            for (var i = 0; i < numTasks; i++)
            {
                var p = position; var height = taskHeights[i];
                hTasks[i] = Task.Run(() => HorizontalIntegrate(step1, step2, p, height));
                position += taskHeights[i];
            }

            Task.WaitAll(hTasks);

            return step2;
        }
        */

        private static readonly unsafe Vector256<int> RotateRight = Vector256.Create(7, 0, 1, 2, 3, 4, 5, 6);


        /* [Benchmark] unsafe public int[,] IntegrateUnsafe2Pass() // 4 place; +114% to the winner
        {
            // pass 1: top-to-bottom aggregation
            int w = _data.Width();
            int h = _data.Height();
            int[,] step1 = new int[h, w];

            // handle first line: just copying over the source, nothing to add yet
            fixed (byte* pSource = &_data[0, 0])
            fixed (int* pTarget = &step1[0, 0])
            {
                var pSrc = pSource;
                var pTrg = pTarget;
                for (var j = 0; j < w; j++)
                    *pTrg++ = *pSrc++;
            }

            // handle remaining lines: adding the previous line
            for (int i = 1; i < h; i++)
            {
                fixed (byte* pSource = &_data[i, 0])
                fixed (int* pTarget = &step1[i, 0])
                {
                    var pSrc = pSource;
                    var pTrg = pTarget;
                    for (var j = 0; j < w; j++)
                    {
                        *pTrg = *(pTrg - w) + *pSrc++;
                        pTrg++;
                    }

                }
            }

            // end of pass1. Now in Step1 we have the vertical-integrated array; so now it is time to perform the horizontal integration. 
            var step2 = new int[h, w];

            fixed (int* pSource = &step1[0, 0])
            fixed (int* pTarget = &step2[0, 0])
            {
                var pSrc = pSource;
                var pTrg = pTarget;
                for (int i = 0; i < h; i++)
                {
                    var c = 0;
                    for (var j = c = 0; j < w; j++)
                        *pTrg++ = (c += *pSrc++);
                }
            }

            return step2;
        }
        */

        private static unsafe void IntegrateUnsafeScalar(byte[,] source, int[,] res)
        {
            int w = source.Width();
            int h = source.Height();
            fixed (byte* pSource = &source[0, 0])
            fixed (int* pTarget = &res[0, 0])
            {
                // handle left corner
                *pTarget = *pSource;

                //handle first line
                for (var j = 1; j < w; j++)
                    pTarget[j] = pSource[j] + pTarget[j - 1];

                //handle the other lines

                for (var i = 1; i < h; i++)
                {
                    // handle the first column
                    pTarget[i * w] = pSource[i * w] + pTarget[w * (i - 1)];
                        

                    //handle the other columns
                    for (var j = 1; j < w; j++)
                        pTarget[i * w + j] = 
                            pSource[i * w + j] 
                            + pTarget[i * w + j - 1]
                            + pTarget[(i - 1) * w + j]
                            - pTarget[(i - 1) * w + j - 1]
                            ;
                }
            }
        }
        [Benchmark(Baseline = true)] 
        public int[,] IntegrateUnsafeScalar() // 3 place; +32% to the winner
        {
            int[,] res = new int[_data.Height(), _data.Width()];
            IntegrateUnsafeScalar(_data, res);
            return res;
        }
    }

}

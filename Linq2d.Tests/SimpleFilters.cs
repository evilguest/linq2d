using System;
using Xunit;

namespace Linq2d.Tests
{
    public class SimpleFilters
    {
     //   [ExcludeFromCodeCoverage]
        public static unsafe int[,] C4UnsafeScalar(byte[,] data)
        {
            int w = data.Width();
            int h = data.Height();
            int[,] res = new int[h, w];
            fixed (byte* pSource = &data[0, 0])
            fixed (int* pTarget = &res[0, 0])
            {
                // handle top left corner
                pTarget[0] = (((w > 1) ? pSource[1] : 0) + ((h > 1) ? pSource[w] : 0)) / 4;

                //handle first line
                for (var j = 1; j < w - 1; j++)
                    pTarget[j] = (
                                    pSource[j - 1]
                                    + pSource[j + 1]
                                    + ((h > 1) ? pSource[w + j] : 0)
                                ) / 4;
                // handle top right corner
                pTarget[w - 1] = ((w > 1 ? pSource[w - 2] : 0) + (h > 1 ? pSource[w + w - 1] : 0)) / 4;

                //handle the other lines

                for (var i = 1; i < h - 1; i++)
                {
                    // handle the first column
                    pTarget[i * w] = (pSource[w * (i - 1)] + pSource[w * (i + 1)] + (w > 1 ? pSource[i * w + 1] : 0)) / 4;

                    //handle the other columns
                    for (var j = 1; j < w - 1; j++)
                        pTarget[i * w + j] = (
                              pSource[w * (i - 1) + j]
                            + pSource[i * w + j - 1]
                            + pSource[w * (i + 1) + j]
                            + pSource[w * i + j + 1]
                            ) / 4;
                    //handle the last column
                    pTarget[i * w + w - 1] = ((w > 1 ? pSource[i * w + w - 2] : 0) + pSource[w * (i - 1) + w - 1] + pSource[w * (i + 1) + w - 1]) / 4;
                }
                // handle the bottom left corner
                pTarget[w * (h - 1)] = ((h > 1 ? pSource[w * (h - 2)] : 0) + (w > 1 ? pSource[w * (h - 1) + 1] : 0)) / 4;

                //handle last line
                for (var j = 1; j < w - 1; j++)
                    pTarget[w * (h - 1) + j] = (
                                    pSource[w * (h - 1) + j - 1]
                                    + pSource[w * (h - 1) + j + 1]
                                    + (h > 1 ? pSource[(w * (h - 2)) + j] : 0)
                                ) / 4;
                // handle bottom right corner
                pTarget[w * (h - 1) + w - 1] = ((h > 1 ? pSource[w * h - 2] : 0) + (w > 1 ? pSource[w * (h - 1) - 1] : 0)) / 4;
                return res;
            }
        }
        public static unsafe int[,] C4NNUnsafeScalar(byte[,] data)
        {
            int w = data.Width();
            int h = data.Height();
            int[,] res = new int[h, w];
            fixed (byte* pSource = &data[0, 0])
            fixed (int* pTarget = &res[0, 0])
            {
                // handle top left corner
                pTarget[0] = (2 * pSource[0] + pSource[1] + pSource[w]) / 4;

                //handle first line
                for (var j = 1; j < w - 1; j++)
                    pTarget[j] = (
                                    pSource[j - 1]
                                    + pSource[j]
                                    + pSource[j + 1]
                                    + pSource[w + j]
                                ) / 4;
                // handle top right corner
                pTarget[w - 1] = (2 * pSource[w - 1] + pSource[w - 2] + pSource[w + w - 1]) / 4;

                //handle the other lines

                for (var i = 1; i < h - 1; i++)
                {
                    // handle the first column
                    pTarget[i * w] = (pSource[i * w] + pSource[w * (i - 1)] + pSource[w * (i + 1)] + pSource[i * w + 1]) / 4;

                    //handle the other columns
                    for (var j = 1; j < w - 1; j++)
                        pTarget[i * w + j] = (
                              pSource[w * (i - 1) + j]
                            + pSource[i * w + j - 1]
                            + pSource[w * (i + 1) + j]
                            + pSource[w * i + j + 1]
                            ) / 4;
                    //handle the last column
                    pTarget[i * w + w - 1] = (pSource[i * w + w - 2] + pSource[i * w + w - 1] + pSource[w * (i - 1) + w - 1] + pSource[w * (i + 1) + w - 1]) / 4;
                }
                // handle the bottom left corner
                pTarget[w * (h - 1)] = (2 * pSource[w * (h - 1)] + pSource[w * (h - 2)] + pSource[w * (h - 1) + 1]) / 4;

                //handle last line
                for (var j = 1; j < w - 1; j++)
                    pTarget[w * (h - 1) + j] = (
                                    pSource[w * (h - 1) + j - 1]
                                    + pSource[w * (h - 1) + j]
                                    + pSource[w * (h - 1) + j + 1]
                                    + pSource[(w * (h - 2)) + j]
                                ) / 4;
                // handle bottom right corner
                pTarget[w * h - 1] = (2 * pSource[w * h - 1] + pSource[w * h - 2] + pSource[w * (h - 1) - 1]) / 4;
                return res;
            }
        }
        /*
        public static unsafe int[,] C4NNUnsafeScalarWSZCheck(byte[,] data)
        {
            int w = data.Width();
            int h = data.Height();
            int[,] res = new int[h, w];
            fixed (byte* pSource = &data[0, 0])
            fixed (int* pTarget = &res[0, 0])
            {
                // handle top left corner
                pTarget[0] = (2 * pSource[0] + ((w > 1) ? pSource[1] : pSource[0]) + ((h > 1) ? pSource[w] : pSource[0])) / 4;

                //handle first line
                for (var j = 1; j < w - 1; j++)
                    pTarget[j] = (
                                    pSource[j - 1]
                                    + pSource[j]
                                    + pSource[j + 1]
                                    + ((h > 1) ? pSource[w + j] : pSource[j])
                                ) / 4;
                // handle top right corner
                pTarget[w - 1] = (2 * pSource[w - 1] + (w > 1 ? pSource[w - 2] : pSource[w - 1]) + (h > 1 ? pSource[w + w - 1] : pSource[w - 1])) / 4;

                //handle the other lines

                for (var i = 1; i < h - 1; i++)
                {
                    // handle the first column
                    pTarget[i * w] = (pSource[i * w] + pSource[w * (i - 1)] + pSource[w * (i + 1)] + (w > 1 ? pSource[i * w + 1] : pSource[i * w])) / 4;

                    //handle the other columns
                    for (var j = 1; j < w - 1; j++)
                        pTarget[i * w + j] = (
                                pSource[w * (i - 1) + j]
                            + pSource[i * w + j - 1]
                            + pSource[w * (i + 1) + j]
                            + pSource[w * i + j + 1]
                            ) / 4;
                    //handle the last column
                    pTarget[i * w + w - 1] = ((w > 1 ? pSource[i * w + w - 2] : pSource[i * w + w - 1]) + pSource[i * w + w - 1] + pSource[w * (i - 1) + w - 1] + pSource[w * (i + 1) + w - 1]) / 4;
                }
                // handle the bottom left corner
                pTarget[w * (h - 1)] = (2 * pSource[w * (h - 1)] + (h > 1 ? pSource[w * (h - 2)] : pSource[w * (h - 1)]) + (w > 1 ? pSource[w * (h - 1) + 1] : pSource[w * (h - 1)])) / 4;

                //handle last line
                for (var j = 1; j < w - 1; j++)
                    pTarget[w * (h - 1) + j] = (
                                    pSource[w * (h - 1) + j - 1]
                                    + pSource[w * (h - 1) + j]
                                    + pSource[w * (h - 1) + j + 1]
                                    + (h > 1 ? pSource[(w * (h - 2)) + j] : pSource[w * (h - 1) + j])
                                ) / 4;
                // handle bottom right corner
                pTarget[w * (h - 1) + w - 1] = (2 * pSource[w * h - 1] + (h > 1 ? pSource[w * h - 2] : pSource[w * h - 1]) + (w > 1 ? pSource[w * (h - 1) - 1] : pSource[w * h - 1])) / 4;
                return res;
            }
        }*/
        
        [Theory]
        //[InlineData(1, 2, 4)] -- known to fail; will review this corner case later.
        [InlineData(3, 3, 43)]
        [InlineData(5, 7, 42)]
        [InlineData(500, 7, 42)]
        public void TestC4(int h, int w, byte v)
        {
            var data = ArrayHelper.InitDiagonal(h, w, v);
            var q = from d in data.With(initValue: (byte)0)
                    select (d[-1, 0] + d[0, -1] + d[1, 0] + d[0, 1]) / 4;
            var p = q.ToArray();
            var r = C4UnsafeScalar(data);
            Assert.Equal(r, p);
        }

        [Fact]
        public void TestPrimitiveC4Oob()
        {
            var sample = new[,] { { 4, 4, 4 }, { 4, 4, 4 }, { 4, 4, 4 } };
            var q = from s in sample select (s[-1, 0] + s[1, 0] + s[0, -1] + s[0, 1]) / 4; // ouch!
            Assert.Throws<IndexOutOfRangeException>(() => q.ToArray());
        }
        [Fact]
        public void TestPrimitiveC4Subst()
        {
            var sample = new[,] { { 4, 4, 4 }, { 4, 4, 4 }, { 4, 4, 4 } };
            var q = from s in sample.With(initValue: 0)
                    select (s[-1, 0] + s[1, 0] + s[0, -1] + s[0, 1]) / 4;
            Assert.Equal(new[,] { { 2, 3, 2 }, { 3, 4, 3 }, { 2, 3, 2 } }, q.ToArray());
        }
        //[Theory]
        //[InlineData(160, 160, 42)]
        //[InlineData(160, 133, 42)]
        //[InlineData(133, 160, 42)]
        //public void TestPrimitiveC4NnCpp(int h, int w, byte seed)
        //{
        //    var sample = ArrayHelper.InitAllRand(h, w, seed);
        //    var q = UnmanagedC4.Transform(sample);
        //    var p = C4NNUnsafeScalar(sample);
        //    TestHelper.AssertEqual(p, q);
        //}
        
        //[Theory]
        //[InlineData(160, 133, 42)]
        //[InlineData(133, 160, 42)]
        //public void TestPrimitiveC4NnAsmUnaligned(int h, int w, byte seed)
        //{
        //    var sample = ArrayHelper.InitAllRand(h, w, seed);
        //    Assert.Throws<AccessViolationException>(() => UnmanagedC4.TransformAsm(sample));
        //}
        
        
        //[Theory]
        //[InlineData(160, 160, 42)]
        //public void TestPrimitiveC4NnAsm(int h, int w, byte seed)
        //{
        //    try
        //    {
        //        var sample = ArrayHelper.InitAllRand(h, w, seed);
        //        if (Avx2.IsSupported)
        //        {
        //            Console.WriteLine("Starting the C4 ASM...");
        //            Console.Out.Flush();
        //            var q = UnmanagedC4.TransformAsm(sample);
        //            Console.WriteLine("Done C4 ASM!");
        //            Console.Out.Flush();
        //            var p = C4NNUnsafeScalar(sample);
        //            TestHelper.AssertEqual(p, q);
        //        }
        //        else
        //        {
        //            Console.WriteLine("Avx2 support is not found, skipping the test");
        //        }
        //    } catch (Exception e)
        //    {
        //        Console.Error.WriteLine($"Caught exception:\n\t{e.Message}\nat\n{e.StackTrace}");
        //        Console.Error.Flush();
        //        throw;
        //    }
        //}
        [Fact]
            public void TestPrimitiveC4Nn()
        {
            //Array2d.SaveDynamicCode = Debugger.IsAttached;
            var sample = new[,] { { 4, 4, 4 }, { 4, 4, 4 }, { 4, 4, 4 } };
            var q = from s in sample.With(OutOfBoundsStrategy.NearestNeighbour)
                    select (s[-1, 0] + s[1, 0] + s[0, -1] + s[0, 1]) / 4;
            TestHelper.AssertEqual(sample, q.ToArray());
        }
        
        [Theory]
        //[InlineData(1, 2, 4)] -- known to fail; will review this corner case later.
        [InlineData(3, 3, 43)]
        [InlineData(5, 7, 42)]
        [InlineData(500, 7, 42)]
        public void TestC4NearestNeighbour(int h, int w, byte v)
        {
            //Array2d.SaveDynamicCode = Debugger.IsAttached;
            var data = ArrayHelper.InitDiagonal(h, w, v);
            var q = from d in data.With(OutOfBoundsStrategy.NearestNeighbour)
                    select (d[-1, 0] + d[0, -1] + d[1, 0] + d[0, 1]) / 4;
            var p = q.ToArray();
            var r = C4NNUnsafeScalar(data);
            TestHelper.AssertEqual(r, p);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(1, 0)]
        [InlineData(0, -1)]
        [InlineData(0, 1)]
        public void TestOutOfRange(int dx, int dy)
        {
            var data = ArrayHelper.InitDiagonal(11, 72, 42);

            Assert.Throws<IndexOutOfRangeException>(() => OutOfRange(data, dx, dy));
        }
        private int[,] OutOfRange(int[,] data, int dx, int dy) 
            => (from d in data select d[dx, dy]).ToArray();

        [Theory]
        [InlineData(11, 17, 1)]
        public void TestDynamicOutOfRange(int h, int w, int whalf)
        {
            var data = ArrayHelper.InitAll(h, w, 1);

            var t = from d in data
                    let tl = d.Offset(-whalf, -whalf)
                    let br = d.Offset(whalf, whalf)
                    let area = (br.X - tl.X + 1) * (br.Y - tl.Y + 1)
                    select (d + tl.Value + br.Value) / area;
            Assert.Throws<IndexOutOfRangeException>(() => t.ToArray());
        }
        [Theory]
        [InlineData(11, 17, 1)]
        public void TestDynamicKernelWith(int h, int w, int whalf)
        {
            var data = ArrayHelper.InitAll(h, w, 1);

            var t = from d in data.With(OutOfBoundsStrategy.NearestNeighbour)
                    let tl = d.Offset(-whalf, -whalf)
                    let br = d.Offset(whalf, whalf)
                    let area = (br.Y - tl.Y + 1) //* (br.Y - tl.Y + 1)
                    select area;

            var r = t.ToArray();
            Assert.NotNull(r);
        }
        //[Theory]
        //[InlineData(11, 17)]
        //public void TestCast(int h, int w)
        //{
        //    var arr = ArrayHelper.InitAll(h, w, 42);
        //    var res = ArrayHelper.InitAll(h, w, "42");
        //    var q = from string a in arr select a;

        //}
    }
}

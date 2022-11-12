using Linq2d.CodeGen.Intrinsics;
using System;
using Xunit;

namespace Linq2d.Tests
{
    public class Vectorization
    {
        [Fact]
        public void TestSuppressedVectorization()
        {
            Sse.Suppress = true;
            var source = ArrayHelper.InitAll(100, 110, 1);
            var q = from s in source select s * 2;
            Assert.Equal(ArrayHelper.InitAll(100, 110, 2), q.ToArray());
            IVectorizable iv = ((IVectorizable)q);
            Assert.False(iv.VectorizationResult.Success);
            Sse.Suppress = false;
        }
        [Fact]
        public void TestSuppressedAvx2()
        {
            Avx2.Suppress = true;
            var source = ArrayHelper.InitAll(100, 110, 1);
            var q = from s in source select s * 2;
            Assert.Equal(ArrayHelper.InitAll(100, 110, 2), q.ToArray());
            IVectorizable iv = (IVectorizable)q;
            Avx2.Suppress = false;
            Assert.True(iv.VectorizationResult.Success);
            Assert.Equal(4, iv.VectorizationResult.Step);
        }
        [Fact]
        public void TestSuppressedAvx()
        {
            Avx.Suppress = true;
            var source = ArrayHelper.InitAll(100, 110, 1);
            var q = from s in source select s * 2;
            Assert.Equal(ArrayHelper.InitAll(100, 110, 2), q.ToArray());
            IVectorizable iv = ((IVectorizable)q);
            Avx.Suppress = false;
            Assert.True(iv.VectorizationResult.Success);
            Assert.Equal(4, iv.VectorizationResult.Step);
        }

        [Fact]
        public void SimpleOperation()
        {
            var source = ArrayHelper.InitAll(100, 110, 1);
            var q = from s in source select s * 2;
            Assert.Equal(ArrayHelper.InitAll(100, 110, 2), q.ToArray());
            var iv = (IVectorizable)q;
            Assert.True(iv.VectorizationResult.Success);
        }

        [Fact]
        public void TwoResultsDifferentSize()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42);
            var q = from s in source
                    select ValueTuple.Create(s + 0, Math.Sqrt(s));
            var (r1, r2) = q.ToArrays();
            Assert.Equal(source, r1);
            Assert.Equal(Sqrt(source), r2);

            IVectorizable2 iv = (IVectorizable2)q;
            Assert.True(iv.VectorizationResults.Item1.Success);
            Assert.True(iv.VectorizationResults.Item2.Success);
        }

        private static double[,] Sqrt(int[,] source)
        {
            return (from s in source select Math.Sqrt(s)).ToArray();
        }

        [Fact]
        public void TestByteCopyOptimization32()
        {
            var source = ArrayHelper.InitAllRand(17, 17, (byte)42);
            Sse.Suppress = true;
            var q = from s in source select (byte)s;

            Assert.Equal(source, q.ToArray());
            Sse.Suppress = false;
            var iv = (IVectorizable)q;
            Assert.True(iv.VectorizationResult.Success);
            Assert.Equal(4, iv.VectorizationResult.Step);
        }
        [Fact]
        public void TestByteLiftOptimization32()
        {
            byte a = 42;
            var source = ArrayHelper.InitAll(17, 17, a);
            Sse.Suppress = true;
            var q = from s in source select a;

            Assert.Equal(source, q.ToArray());
            Sse.Suppress = false;

            var iv = (IVectorizable)q;
            Assert.True(iv.VectorizationResult.Success);
            Assert.Equal(4, iv.VectorizationResult.Step);
        }
        [Fact]
        public void TestByteCopyOptimization128()
        {
            var source = ArrayHelper.InitAllRand(17, 17, (byte)42);
            Avx.Suppress = true;
            var q = from s in source select (byte)s;

            Assert.Equal(source, q.ToArray());
            Avx.Suppress = false;
            var iv = (IVectorizable)q;
            Assert.True(iv.VectorizationResult.Success);
            Assert.Equal(16, iv.VectorizationResult.Step);
        }
        [Fact]
        public void TestByteLiftOptimization128()
        {
            byte a = 42;
            var source = ArrayHelper.InitAll(17, 17, a);
            Avx.Suppress = true;
            var q = from s in source select a;

            Assert.Equal(source, q.ToArray());
            Avx.Suppress = false;

            var iv = (IVectorizable)q;
            Assert.True(iv.VectorizationResult.Success);
            Assert.Equal(16, iv.VectorizationResult.Step);
        }
        [Fact]
        public void TestIntComparison128()
        {
            int a = 42;
            var source = ArrayHelper.InitAllRand(16, 16, 42);
            Avx.Suppress = true;
            var q = from s in source select s == a ? 1 : -1;
            var expect = ArrayHelper.InitAllRand(16, 16, 42, x => x == a ? 1 : -1);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            Avx.Suppress = false;
            Assert.True(iv.VectorizationResult.Success);
            Assert.Equal(4, iv.VectorizationResult.Step);
        }
        [Fact]
        public void TestUIntComparison128()
        {
            uint a = 42;
            var source = ArrayHelper.InitAllRand(16, 16, 42, x=>(uint)x);
            Avx.Suppress = true;
            var q = from s in source select s == a ? 1 : -1;
            var expect = ArrayHelper.InitAllRand(16, 16, 42, x => x == a ? 1 : -1);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            Avx.Suppress = false;
            Assert.True(iv.VectorizationResult.Success);
            Assert.Equal(4, iv.VectorizationResult.Step);
        }
        /*
        private unsafe int[,] IntComparisonVectorManual(int[,] source, int a)
        {
            int h = source.GetLength(0);
            int w = source.GetLength(1);
            var result = new int[h, w];
            fixed (int* pSource = &source[0, 0])
            fixed (int* pTarget = &result[0, 0])
                for (var i = 0; i < h; i++)
                    for (var j = 0; j < w; j += 4)
                        System.Runtime.Intrinsics.X86.Sse2.Store(pTarget + i * w + j,
                            System.Runtime.Intrinsics.X86.Sse41.BlendVariable(Vector128.Create(-1), Vector128.Create(1),
                                System.Runtime.Intrinsics.X86.Sse2.CompareEqual(
                                    System.Runtime.Intrinsics.X86.Sse2.LoadVector128(pSource + i * w + j),
                                    Vector128.Create(a))));
            return result; 
        }
        */
    }
}

using Linq2d.CodeGen.Intrinsics;
using System;
using Xunit;

namespace Linq2d.Tests.Vectorization
{
    public class Avx2:Base, IClassFixture<VectorizationStateFixture>
    {
        [Fact]
        public void IntArithmetics()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => s + s * 2 - 3);
            var q = from s in source select s + s * 2 - 3;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv.VectorizationResult, 8);
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
            AssertVectorised(iv.VectorizationResults.Item1, 8);
            AssertVectorised(iv.VectorizationResults.Item2, 4);
        }

        private static double[,] Sqrt(int[,] source)
        {
            return (from s in source select Math.Sqrt(s)).ToArray();
        }

        public void TestByteLiftOptimization()
        {
            byte a = 42;
            var source = ArrayHelper.InitAll(17, 17, a);
            Sse.Suppress = true;
            var q = from s in source select a;

            Assert.Equal(source, q.ToArray());
            Sse.Suppress = false;

            var iv = (IVectorizable)q;
            AssertVectorised(iv.VectorizationResult, 32);
        }
        private void TestConditionalInt256<T>(T t, T f, int sizeOfT)
            where T : unmanaged
        {
            int a = 42;
            var source = ArrayHelper.InitAllRand(16, 16, 42);
            var q = from s in source select s == a ? t : f;
            var expect = ArrayHelper.InitAllRand(16, 16, 42, x => x == a ? t : f);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv.VectorizationResult, 32 / Math.Max(sizeOfT, sizeof(int)));
        }
        private void TestConditionalLong256<T>(T t, T f, int sizeOfT)
            where T : unmanaged
        {
            long a = 42;
            var source = ArrayHelper.InitAllRand(16, 16, 42, x => (long)x);
            var q = from s in source select s == a ? t : f;
            var expect = ArrayHelper.InitAllRand(16, 16, 42, x => x == a ? t : f);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv.VectorizationResult, 32 / sizeOfT);
        }

        [Fact]
        public void TestDoubleConditional256() => TestConditionalLong256(Math.PI, Math.E, sizeof(double));
        [Fact]
        public void TestFloatConditional256() => TestConditionalInt256((float)Math.PI, (float)Math.E, sizeof(float));
        [Fact]
        public void TestLongConditional256() => TestConditionalLong256<long>(7, -42, sizeof(long));
        [Fact]
        public void TestULongConditional256() => TestConditionalLong256<ulong>(7, 42, sizeof(ulong));
        [Fact]
        public void TestShortConditional256() => TestConditionalInt256<short>(7, -42, sizeof(short));
        [Fact]
        public void TestUShortConditional256() => TestConditionalInt256<ushort>(7, 42, sizeof(ushort));

    }
}

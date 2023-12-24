using Linq2d.CodeGen.Intrinsics;
using System;
using Xunit;

namespace Linq2d.Tests.Vectorization
{
    public class Avx : Base, IClassFixture<SuppressAvx2Fixture>
    {
        [Fact]
        public void FloatMultiplication()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => s * 2f);
            var q = from s in source select s * 2f;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv, 8);
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

            IVectorizable iv = (IVectorizable)q;
            AssertVectorised(iv, 8);
        }

        private static double[,] Sqrt(int[,] source)
        {
            return (from s in source select Math.Sqrt(s)).ToArray();
        }

        [Fact]
        public void TestByteCopyOptimization()
        {
            var source = ArrayHelper.InitAllRand(17, 17, (byte)42);
            var q = from s in source select (byte)s;

            Assert.Equal(source, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv, 32);
        }
        [Fact]
        public void TestByteLiftOptimization()
        {
            byte a = 42;
            var source = ArrayHelper.InitAll(17, 17, a);
            var q = from s in source select a;

            Assert.Equal(source, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorised(iv, 32);
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
            AssertVectorised(iv, 16 / Math.Max(sizeOfT, sizeof(int)));
        }
        private void TestConditionalLong256<T>(T t, T f, int sizeOfT)
            where T : unmanaged
        {
            long a = 42;
            var source = ArrayHelper.InitAllRand(16, 16, 42, x=>(long)x);
            var q = from s in source select s == a ? t : f;
            var expect = ArrayHelper.InitAllRand(16, 16, 42, x => x == a ? t : f);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv, 16 / sizeOfT);
        }

        [Fact]
        public void TestDoubleConditional256() => TestConditionalLong256(Math.PI, Math.E, sizeof(double));
        [Fact]
        public void TestFloatConditional256() => TestConditionalInt256((float)Math.PI, (float)Math.E, sizeof(float));
        [Fact]
        public void TestLongConditional256() => TestConditionalLong256<long>(7, -42, sizeof(long));
        [Fact]
        public void TestULongConditional256() => TestConditionalLong256<ulong>(7, 42, sizeof(ulong));
    }
}

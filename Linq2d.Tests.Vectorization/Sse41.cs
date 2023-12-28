using Linq2d.CodeGen.Intrinsics;
using System;
using Xunit;

namespace Linq2d.Tests.Vectorization
{
    public class Sse41 : Base, IClassFixture<SuppressAvxFixture>
    {
        [Fact]
        public void IntArithmetics()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => s + s * 2 - 3);
            var q = from s in source select s + s * 2 - 3;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
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
            AssertVectorized(iv, 16);
        }
        [Fact]
        public void TestByteLiftOptimization()
        {
            byte a = 42;
            var source = ArrayHelper.InitAll(17, 17, a);
            var q = from s in source select a;

            Assert.Equal(source, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 16);
        }
        [Fact]
        public void TestIntComparison128()
        {
            int a = 42;
            var source = ArrayHelper.InitAllRand(16, 16, 42);
            var q = from s in source select s == a ? 1 : -1;
            var expect = ArrayHelper.InitAllRand(16, 16, 42, x => x == a ? 1 : -1);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void TestUIntComparison128()
        {
            uint a = 42;
            var source = ArrayHelper.InitAllRand(16, 16, 42, x=>(uint)x);
            var q = from s in source select s == a ? (uint)1 : 7;
            var expect = ArrayHelper.InitAllRand(16, 16, 42, x => x == a ? (uint)1 : 7);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }


        private void TestConditionalInt128<T>(T t, T f, int sizeOfT)
            where T: unmanaged
        {
            int a = 42;
            var source = ArrayHelper.InitAllRand(16, 16, 42);
            var q = from s in source select s == a ? t : f;
            var expect = ArrayHelper.InitAllRand(16, 16, 42, x => x == a ? t : f);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 16/sizeOfT);
        }
        private void TestConditionalLong128<T>(T t, T f, int sizeOfT)
            where T : unmanaged
        {
            long a = 42;
            var source = ArrayHelper.InitAllRand(16, 16, 42, x=>(long)x);
            var q = from s in source select s == a ? t : f;
            var expect = ArrayHelper.InitAllRand(16, 16, 42, x => x == a ? t : f);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 16 / sizeOfT);
        }
        [Fact]
        public void TestDoubleConditional128() => TestConditionalLong128(Math.PI, Math.E, sizeof(double));
        [Fact]
        public void TestFloatConditional128() => TestConditionalInt128((float)Math.PI, (float)Math.E, sizeof(float));
        [Fact]
        public void TestLongConditional128() => TestConditionalLong128<long>(7, -42, sizeof(long));

    }
}

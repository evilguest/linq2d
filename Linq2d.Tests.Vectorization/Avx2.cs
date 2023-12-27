using System;
using Xunit;

namespace Linq2d.Tests.Vectorization
{
    public class Avx2:Base, IClassFixture<SuppressAvx512Fixture>
    {
        [Fact]
        public void IntArithmetics()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => s + s * 2 - 3);
            var q = from s in source select s + s * 2 - 3;
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
        public void TestByteLiftOptimization()
        {
            byte a = 42;
            var source = ArrayHelper.InitAll(17, 17, a);
//            CodeGen.Intrinsics.Sse.Suppress = true;
            var q = from s in source select a;

            Assert.Equal(source, q.ToArray());
//            CodeGen.Intrinsics.Sse.Suppress = false;

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
            AssertVectorised(iv, 32 / Math.Max(sizeOfT, sizeof(int)));
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
            AssertVectorised(iv, 32 / sizeOfT);
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

        [Fact]
        public void ShortToIntConversion()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => (short)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, x=> (int)(short)x);
            var q = from s in source
                    select (int)s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv, 8);
        }

        [Fact]
        public void ShortNegation()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => (short)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => (short)-s);
            var q = from s in source
                    select (short)-s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv, 8);
        }

        [Fact]
        public void ShortAddition()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => (short)x);
            var source2 = ArrayHelper.InitAllRand(100, 110, 42, x => (short)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => (short)(s+s));
            var q = from s1 in source1
                    from s2 in source2
                    select (short)(s1 + s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv, 16);
        }
    }
}

using System;
using Xunit;

namespace Linq2d.Tests.Vectorization
{
    public class None: Base, IClassFixture<SuppressVectorizationFixture>
    {
        [Fact]
        public void TestSuppressedVectorization()
        {
            var source = ArrayHelper.InitAll(100, 110, 1);
            var q = from s in source select s * 2;
            Assert.Equal(ArrayHelper.InitAll(100, 110, 2), q.ToArray());
            IVectorizable iv = ((IVectorizable)q);
            Assert.False(iv.Vectorized);
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
            Assert.False(iv.Vectorized);
        }

        private static double[,] Sqrt(int[,] source) => (from s in source select Math.Sqrt(s)).ToArray();

        [Fact]
        public void TestByteCopyOptimization32()
        {
            var source = ArrayHelper.InitAllRand(17, 17, (byte)42);
            var q = from s in source select (byte)s;

            Assert.Equal(source, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv, 4);
        }
        [Fact]
        public void TestByteLiftOptimization32()
        {
            byte a = 42;
            var source = ArrayHelper.InitAll(17, 17, a);
            var q = from s in source select a;

            Assert.Equal(source, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorised(iv, 4);
        }
    }
}

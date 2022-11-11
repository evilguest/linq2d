using Linq2d.CodeGen;
using System;
using Xunit;

namespace Linq2d.Tests
{
    public class Vectorization
    {
        [Fact]
        public void TestSuppressedVectorization()
        {
            CodeGen.Intrinsics.Sse.Suppress = true;
            var source = ArrayHelper.InitAll(100, 110, 1);
            var q = from s in source select s * 2;
            Assert.Equal(ArrayHelper.InitAll(100, 110, 2), q.ToArray());
            IVectorizable iv = ((IVectorizable)q);
            Assert.False(iv.VectorizationResult.Success);
            CodeGen.Intrinsics.Sse.Suppress = false;
        }
        [Fact]
        public void TestSuppressedAvx2()
        {
            CodeGen.Intrinsics.Avx2.Suppress = true;
            var source = ArrayHelper.InitAll(100, 110, 1);
            var q = from s in source select s * 2;
            Assert.Equal(ArrayHelper.InitAll(100, 110, 2), q.ToArray());
            IVectorizable iv = (IVectorizable)q;
            Assert.True(iv.VectorizationResult.Success);
            Assert.Equal(4, iv.VectorizationResult.Step);
            CodeGen.Intrinsics.Avx2.Suppress = false;
        }
        [Fact]
        public void TestSuppressedAvx()
        {
            CodeGen.Intrinsics.Avx.Suppress = true;
            var source = ArrayHelper.InitAll(100, 110, 1);
            var q = from s in source select s * 2;
            Assert.Equal(ArrayHelper.InitAll(100, 110, 2), q.ToArray());
            IVectorizable iv = ((IVectorizable)q);
            Assert.True(iv.VectorizationResult.Success);
            Assert.Equal(4, iv.VectorizationResult.Step);
            CodeGen.Intrinsics.Avx.Suppress = false;
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
    }
}

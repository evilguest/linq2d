﻿
namespace Linq2d.Tests.Vectorization
{
    public class Avx512F:Base, IClassFixture<VectorizationStateFixture>
    {
        [Fact]
        public void IntArithmetics()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => s + s - 3);
            var q = from s in source select s + s - 3;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            if (CodeGen.Intrinsics.Avx512F.IsSupported)
            {
                AssertVectorized(iv, 16);
            }
            else
            {
                AssertVectorized(iv, 8);
            }
            Console.WriteLine(iv.Report());
        }
    }
}

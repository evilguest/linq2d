using Linq2d.MathHelpers;
using Xunit;

namespace Linq2d.Tests.Vectorization
{
    public class Sse2 : Base, IClassFixture<SuppressSse41Fixture>
    {
        [Fact]
        public void ShortFastArithmetics()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => (short)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => Fast.Subtract(Fast.Add(2, Fast.Multiply((short)s, 2)), (short)s));
            var q = from s1 in source 
                    from s2 in source
                    select Fast.Subtract(Fast.Add(2, Fast.Multiply(s2, 2)), s1);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv.VectorizationResult, 8);
        }
        [Fact]
        public void ShortArithmetics()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => (short)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => (short)(2 + s * 2 - s));
            var q = from s1 in source
                    from s2 in source
                    select (short)(2 + s2 * 2 - s1);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            Assert.False(iv.VectorizationResult.Success);
        }
    }
}

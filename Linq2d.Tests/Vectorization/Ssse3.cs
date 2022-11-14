using Linq2d.MathHelpers;
using Xunit;

namespace Linq2d.Tests.Vectorization
{
    public class Ssse3 : Base, IClassFixture<SuppressSse41Fixture>
    {
        [Fact]
        public void ShortNegation()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => (short)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => Fast.Negate((short)s));
            var q = from s in source
                    select Fast.Negate(s);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv.VectorizationResult, 8);
        }
    }
}

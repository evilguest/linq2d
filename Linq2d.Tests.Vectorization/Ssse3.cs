using Xunit;

namespace Linq2d.Tests.Vectorization
{
    public class Ssse3 : Base, IClassFixture<SuppressSse41Fixture>
    {
        [Fact]
        public void ShortNegation()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => (short)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => (short) -(short)s);
            var q = from s in source
                    select (short)-s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }

        [Fact]
        public void IntToShortConversion()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, x => (short)x);
            var q = from s in source
                    select (short)s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

    }
}

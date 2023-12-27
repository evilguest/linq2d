using Xunit;

namespace Linq2d.Tests.Vectorization
{
    public class Sse: Base, IClassFixture<SuppressSse2Fixture>
    {
        [Fact]
        public void FloatAddition()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => (float)x/3);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, x => 1.0f+(float)x / 3);
            var q = from s in source
                    select 1.0f + s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv, 4);
        }

        [Fact]
        public void FloatSubtraction()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => x / 3f);
            var source2 = ArrayHelper.InitAllRand(100, 110, 42, x => x / 17f);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, x => x /3f - x /17f);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 - s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv, 4);
        }
        [Fact]
        public void FloatMultiplication()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => x / 3f);
            var source2 = ArrayHelper.InitAllRand(100, 110, 42, x => x / 5f);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, x => (x / 3f) * (x / 5f));
            var q = from s1 in source1
                    from s2 in source2
                    select s1 * s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv, 4);
        }
        [Fact]
        public void FloatDivision()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => 3.14f * x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, x => 42f / (3.14f * x));
            var q = from s in source
                    select 42 / s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv, 4);
        }
        [Fact]
        public void FloatEquality()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => x / 3f);
            var source2 = ArrayHelper.InitAllRand(100, 110, 42, x => x / 5f);
            var expect = ArrayHelper.InitAll(100, 110, false);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 == s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv, 4);
        }
    }
}

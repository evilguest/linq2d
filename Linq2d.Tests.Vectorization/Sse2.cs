using Xunit;

namespace Linq2d.Tests.Vectorization
{
    public class Sse2 : Base, IClassFixture<SuppressSsse3Fixture>
    {
        [Fact]
        public void ShortArithmetics()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => (short)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => (short)((short)(2 + (short)((short)s + 2)) - (short)s));
            var q = from s1 in source 
                    from s2 in source
                    select (short)((short)(2+(short)(s2+2))-s1);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv, 4);
        }
        [Fact]
        public void UShortArithmetics()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => (ushort)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => (ushort)((ushort)(2 + (ushort)((ushort)s + 2)) - (ushort)s));
            var q = from s1 in source
                    from s2 in source
                    select (ushort)((ushort)(2 + (ushort)(s2 + 2)) - s1);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv, 4);
        }
        [Fact]
        public void ShortConversion()
        {
            unchecked
            {
                var source = ArrayHelper.InitAllRand(100, 110, 42);
                var expect = ArrayHelper.InitAllRand(100, 110, 42, x => (short)x);
                var q = from s in source
                        select (short)s;
                Assert.Equal(expect, q.ToArray());
                var iv = (IVectorizable)q;
                AssertVectorised(iv, 4);
            }
        }
        [Fact]
        public void UShortConversion()
        {
            unchecked
            {
                var source = ArrayHelper.InitAllRand(100, 110, 42, x => (uint)x);
                var expect = ArrayHelper.InitAllRand(100, 110, 42, x => (ushort)x);
                var q = from s in source
                        select (ushort)s;
                Assert.Equal(expect, q.ToArray());
                var iv = (IVectorizable)q;
                AssertVectorised(iv, 4);
            }
        }
        [Fact]
        public void ShortNegation()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => (short)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => (short)(-(short)s));
            var q = from s in source
                    select (short)-s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv, 4);
        }

        [Fact]
        public void BoolConstShort()
        {
            var source = ArrayHelper.InitAllRand(70, 3, 42, x => (short)x);
            //var source2 = ArrayHelper.InitAllRand(70, 3, 42, x => (short)x);
            var expect = ArrayHelper.InitAll(70, 3, true);
            var q = from s in source
                    select (s == s);
            //Array2d.SaveDynamicCode = true;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv, 8);
        }

        [Fact]
        public void ShortEquality()
        {
            var source = ArrayHelper.InitAllRand(70, 3, 42, x => (short)x);
            //var source2 = ArrayHelper.InitAllRand(70, 3, 42, x => (short)x);
            var expect = ArrayHelper.InitAll(70, 3, true);
            var q = from s1 in source
                    from s2 in source
                    select (s1 == s2);
            //Array2d.SaveDynamicCode = true;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv, 8);
        }
        [Fact]
        public void UShortNot()
        {
            var source = ArrayHelper.InitAllRand(70, 3, 42, x => (ushort)x);
            var expect = ArrayHelper.InitAllRand(70, 3, 42, x => (ushort)~(ushort)x);
            var q = from s in source
                    select (ushort)~s;
            //Array2d.SaveDynamicCode = true;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv, 8);
        }
        [Fact]
        public void ShortNotIndirect()
        {
            var source1 = ArrayHelper.InitAllRand(70, 3, 42, x => (short)x);
            var source2 = ArrayHelper.InitAllRand(70, 3, 42, x => (short)x);
            var expect = ArrayHelper.InitAll(70, 3, false);
            var q = from s1 in source1
                    from s2 in source2
                    select !(s1 == s2);
            Array2d.SaveDynamicCode = true;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorised(iv, 8);
        }
    }
}

using System;
using Xunit;

namespace Linq2d.Tests
{
    public class TwoArrayArithmetics
    {
        [Fact]
        public void TestFailOnWrongSize()
        {
            var aData = ArrayHelper.InitAllRand(1, 2, 42);
            var bData = ArrayHelper.InitAllRand(2, 1, 42);
            Assert.Throws<ArgumentException>(() => (from a in aData from b in bData select a+b).ToArray());

            var cData = ArrayHelper.InitAllRand(1, 2, 42);
            Assert.Throws<ArgumentException>(() => (from a in aData.With(0) select a[-1, -1] + a[1,1]).ToArray());
        }

        [Theory]
        [InlineData(1, 2, 3, 4)]
        [InlineData(10, 20, 123, 4343)]
        [InlineData(20, 10, 6756, 453)]
        public void TestAdd(int h, int w, int aValue, int bValue)
        {
            var aData = ArrayHelper.InitAll(h, w, aValue);
            var bData = ArrayHelper.InitAll(h, w, bValue);
            var abData = ArrayHelper.InitAll(h, w, aValue + bValue);
            var q = from a in aData
                    from b in bData
                    select a + b;
            Assert.Equal(abData, q.ToArray());

            q = from a in aData.With(0)
                from b in bData
                select a + b;
            Assert.Equal(abData, q.ToArray());

            q = from a in aData
                from b in bData.With(0)
                select a + b;
            Assert.Equal(abData, q.ToArray());

            q = from a in aData.With(0)
                from b in bData.With(0)
                select a + b;
            Assert.Equal(abData, q.ToArray());

            q = from a in aData
                from b in bData
                let c = a + b
                select c;
            Assert.Equal(abData, q.ToArray());
        }

        [Theory]
        [InlineData(1, 2, 3, 4)]
        [InlineData(10, 20, 123, 4343)]
        [InlineData(20, 10, 6756, 453)]
        public void TestSubtract(int h, int w, int aValue, int bValue)
        {
            var aData = ArrayHelper.InitAll(h, w, aValue);
            var bData = ArrayHelper.InitAll(h, w, bValue);
            var abData = ArrayHelper.InitAll(h, w, aValue - bValue);
            var q = from a in aData
                    from b in bData
                    select a - b;
            Assert.Equal(abData, q.ToArray());
        }

        [Theory]
        [InlineData(1, 2, 3, 4)]
        [InlineData(10, 20, 123, 4343)]
        [InlineData(20, 10, 6756, 453)]
        public void TestMultiply(int h, int w, int aValue, int bValue)
        {
            var aData = ArrayHelper.InitAll(h, w, aValue);
            var bData = ArrayHelper.InitAll(h, w, bValue);
            var abData = ArrayHelper.InitAll(h, w, aValue * bValue);
            var q = from a in aData
                    from b in bData
                    select a * b;
            Assert.Equal(abData, q.ToArray());
        }

    }
}

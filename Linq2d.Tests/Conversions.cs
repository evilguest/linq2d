using Xunit;

namespace Linq2d.Tests
{
    public class Conversions
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void TestConversion_ByteInt(int size)
        {
            var bytes = ArrayHelper.Init1Diagonal(size);
            var q = from b in bytes select b + 0;
            Assert.Equal(ArrayHelper.InitDiagonal(size, 1), q.ToArray());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void TestConversion_ShortInt(int size)
        {
            var shorts = ArrayHelper.InitDiagonal(size, (short)1);
            var q = from s in shorts select s + 0;
            Assert.Equal(ArrayHelper.InitDiagonal(size, 1), q.ToArray());
        }

    }
}

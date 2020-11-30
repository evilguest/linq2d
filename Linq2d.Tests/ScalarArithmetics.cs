using Xunit;

namespace Linq2d.Tests
{
    /*
    public class ScalarArithmetics
    {
        [Theory]
        [InlineData(1, 2, 4)]
        [InlineData(8, 17, 11)]
        [InlineData(10, 123, 4343)]
        [InlineData(200, 6756, 453)]
        public void TestMultiply(int size, int l, int r)
        {
            var sample = ArrayHelper.InitAll(size, l * r);
            var q = from d in ArrayHelper.InitAll(size, l)
                    select d * r;
            Assert.Equal(sample, q.ToArray());
        }
        [Theory]
        [InlineData(1, 2, 4)]
        [InlineData(10, 123, 4343)]
        [InlineData(200, 6756, 453)]

        public void TestAdd(int size, int a, int s)
        {
            var sample = ArrayHelper.InitAll(size, a + s);
            var q = from d in ArrayHelper.InitAll(size, a)
                    select d + s;
            Assert.Equal(sample, q.ToArray());
        }

        [Fact]
        public void Square()
        {
            var sample = ArrayHelper.InitAll(10, 20, 11);
            var q = from d in sample
                    select d * d;
            Assert.Equal(ArrayHelper.InitAll(10, 20, 121), q.ToArray());
        }

        [Fact]
        public void PrimitiveAdd()
        {
            var sample = new[,] { { 1, 2 }, { 3, 4 } };
            var q = from s in sample select s + 1;
            Assert.Equal(new[,] { { 2, 3 }, { 4, 5 } }, q.ToArray());
        }
    }*/
}

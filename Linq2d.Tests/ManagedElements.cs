using Xunit;

namespace Linq2d.Tests
{
    public class ManagedElements
    {
        [Theory]
        [InlineData(10, 234, "Hello, ", "world!")]
        public void TestStringToString(int h, int w, string a, string b)
        {
            var s = ArrayHelper.InitAll(h, w, a + b);
            var q = from d in ArrayHelper.InitAll(h, w, a)
                    select d + b;
            Assert.Equal(s, q.ToArray());
        }

        [Theory]
        [InlineData(10, 234, "Hello, ", 1)]
        public void TestIntToString(int h, int w, string a, int b)
        {
            var s = ArrayHelper.InitAll(h, w, a + b);
            var q = from d in ArrayHelper.InitAll(h, w, b)
                    select a + d;
            Assert.Equal(s, q.ToArray());
        }
        [Theory]
        [InlineData(10, 234, "Hello, world")]
        public void TestString(int h, int w, string a)
        {
            var s = ArrayHelper.InitAll(h, w, a);
            var q = from d in ArrayHelper.InitAll(h, w, 0)
                    select a;
            Assert.Equal(s, q.ToArray());
        }

    }
}

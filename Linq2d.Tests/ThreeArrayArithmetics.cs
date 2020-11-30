using Xunit;

namespace Linq2d.Tests
{
    /*
    public class ThreeArrayArithmetics
    {
        [Theory]
        [InlineData(1,2,3,4,5)]
        public void TestSimpleAdd(int h, int w, int a, int b, int c)
        {
            var s = ArrayHelper.InitAll(h, w, a + b + c);
            var aa = ArrayHelper.InitAll(h, w, a);
            var ba = ArrayHelper.InitAll(h, w, b);
            var ca = ArrayHelper.InitAll(h, w, c);
            var q = from x in aa
                    from y in ba
                    from z in ca
                    select x + y + z;
            Assert.Equal(s, q.ToArray());
        }
    }
    */
}

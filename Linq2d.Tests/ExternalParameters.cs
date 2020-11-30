using Xunit;

namespace Linq2d.Tests
{
    /*
    public class ExternalParameters
    {
        [Fact]
        void TestClosureDataAccess()
        {
            var a = 17;
            var b = 15;
            var sample = ArrayHelper.InitAll(10, a+b);
            var q = from d in ArrayHelper.InitAll(10, a)
                    select d + b;
            Assert.Equal(sample, q.ToArray());
        }
        [Fact]
        void TestClosureOffsetAccess()
        {
            var dx = 0;
            var sample = ArrayHelper.InitAll(10, 42);
            var q = from d in sample
                    select d[dx, 0];
            Assert.Equal(sample, q.ToArray());
        }
        [Fact]
        void TestArrayMemberAccess()
        {
            var param = new[] { 0, 10 };
            var sample = ArrayHelper.InitAll(10, 42);
            var q = from d in sample
                    select d[param[0], 0]+param[1];
            Assert.Equal(ArrayHelper.InitAll(10, 52), q.ToArray());
        }
    }*/
}

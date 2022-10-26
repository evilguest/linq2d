using System;
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

        [Theory]
        [InlineData(1, 2, 42)]
        [InlineData(1000, 20, 42)]
        public void Test2ResultsStringToString(int h, int w, int seed)
        {
            const string hi = "Hello, ";
            const string bye = "Goodbye, ";
            const string world = "world ";
            var helloex = ArrayHelper.InitAllRand(h, w, seed, x => hi + world + x + "!");
            var goodbyex = ArrayHelper.InitAllRand(h, w, seed, x => bye + world + x + "!");
            var (hello, goodbye) = (from s in ArrayHelper.InitAllRand(h, w, seed, x => world + x + "!")
                                    select ValueTuple.Create(hi + s, bye + s)).ToArrays();
            TestHelper.AssertEqual(helloex, hello);
            TestHelper.AssertEqual(goodbyex, goodbye);
        }

    }
}

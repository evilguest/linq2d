using System;
using Xunit;

namespace Linq2d.Tests
{
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

            var p = from x in aa
                    from y in ba
                    from z in ca
                    from r in Result.InitWith(0)
                    select x + y + z + r[-1, -1];
            Assert.Equal(s, p.ToArray());
        }

        [Theory]
        [InlineData(1, 2, 3, 4, 5)]
        public void TestThreeArrays2Results(int h, int w, int a, int b, int c)
        {
            var ex1 = ArrayHelper.InitAll(h, w, a + b);
            var ex2 = ArrayHelper.InitAll(h, w, b - c);
            var aa = ArrayHelper.InitAll(h, w, a);
            var ba = ArrayHelper.InitAll(h, w, b);
            var ca = ArrayHelper.InitAll(h, w, c);
            var q = from x in aa
                    from y in ba
                    from z in ca
                    select ValueTuple.Create(x + y, y - z);
            var (r1, r2) = q.ToArrays();
            Assert.Equal(ex1, r1);
            Assert.Equal(ex2, r2);


            var p = from x in aa
                    from y in ba
                    from z in ca
                    from r in Result.InitWith(0)
                    select ValueTuple.Create(x + y, y - z);
            (r1, r2) = p.ToArrays();

            Assert.Equal(ex1, r1);
            Assert.Equal(ex2, r2);
        }


    }
}


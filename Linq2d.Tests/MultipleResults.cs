using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Linq2d.Tests
{
    public class MultipleResults
    {
        [Theory]
        [InlineData(1, 2, 42, 3)]
        [InlineData(1000, 20, 42, 3)]
        public void TestTwoResults(int h, int w, int seed, int divisor)
        {
            var divex = (from d in ArrayHelper.InitAllRand(h, w, seed) select d / divisor).ToArray();
            var modex = (from d in ArrayHelper.InitAllRand(h, w, seed) select d % divisor).ToArray();
            var (div, mod) = (from d in ArrayHelper.InitAllRand(h, w, seed)
                              select ValueTuple.Create(d / divisor, d % divisor)).ToArrays();
            TestHelper.AssertEqual(divex, div);
            TestHelper.AssertEqual(modex, mod);

        }

        [Theory]
        [InlineData(2, 200, 42)]
        [InlineData(3, 4, 42)]
        [InlineData(1000, 20, 42)]
        public void TestTwoResultsOneRecursive(int h, int w, int seed)
        {
            var xex = from d in ArrayHelper.InitAllRand(h, w, seed) 
                      from r in Result.SubstBy(0)
                      select d + r[-1, 0];

            var q = from d in ArrayHelper.InitAllRand(h, w, seed)
                    from r in Result.SubstBy(0)
                    select ValueTuple.Create(d + r[-1, 0], d + 0);

            var (x, y) = q.ToArrays();
            TestHelper.AssertEqual(xex.ToArray(), x);
        }

        [Theory]
        [InlineData(1, 200, 42)]
        [InlineData(1000, 20, 42)]
        public void TestTwoResultsTwoRecursive(int h, int w, int seed)
        {
            var xex = from d in ArrayHelper.InitAllRand(h, w, seed)
                      from r in Result.SubstBy(0)
                      select d + r[0, -1];

            var t = xex.Transform;

            var yex = from d in ArrayHelper.InitAllRand(h, w, seed)
                      from r in Result.SubstBy(0)
                      select d + r[-1, 0];

            var q = from d in ArrayHelper.InitAllRand(h, w, seed)
                    from r1 in Result.SubstBy(0)
                    from r2 in Result.SubstBy(0)
                    select ValueTuple.Create(d + r1[0,-1], d + r2[-1, 0]);

            var (x, y) = q.ToArrays();

            TestHelper.AssertEqual(xex.ToArray(), x);
            TestHelper.AssertEqual(yex.ToArray(), y);
        }
    }
}

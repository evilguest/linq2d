using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Linq2d.Tests
{
 /*
  * public class Vectorization
    {
        [Fact]
        public void SimpleOperation()
        {
            var source = ArrayHelper.InitAll(100, 110, 1);
            var q = from s in source select s * 2;
            Assert.Equal(ArrayHelper.InitAll(100, 110, 2), q.ToArray());
            Assert.True(((IVectorizable)q).Vectorized);
        }

        [Fact]
        public void TwoResultsDifferentSize()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42);
            var q = from s in source
                    select ValueTuple.Create(s + 0, Math.Sqrt(s));
            var (r1, r2) = q.ToArrays();
            Assert.Equal(source, r1);
            Assert.Equal(Sqrt(source), r2);

            Assert.True(((IVectorizable)q).Vectorized);
        }

        private static double[,] Sqrt(int[,] source)
        {
            return (from s in source select Math.Sqrt(s)).ToArray();
        }
    }
 */
}

using System;
using Xunit;

namespace Linq2d.Tests
{
    public class KernelVariations
    {
        [Fact]
        public void TestUnmeasurableKernel()
        {
            var arr = ArrayHelper.InitDiagonal(3, 1);
            var res = from a in arr.With(OutOfBoundsStrategy.NearestNeighbour) select a[-a, 0];
            Assert.Throws<IndexOutOfRangeException>(() => res.ToArray());
            res = from a in arr select a[-a, 0];
            Assert.Throws<IndexOutOfRangeException>(() => res.ToArray());
        }
    }
}

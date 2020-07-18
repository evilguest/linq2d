using BenchmarkHelpers;
using System.Linq.Processing2d;
using System.Linq.Processing2d.Slow;

namespace FilterTests
{
    class SlowLinqFilter : ArrayFilterBase<int>, IArrayFilter<int>
    {
        public int[,] C4() => from d in Data.AsRelative(Bounds.Skip) select (d[-1, 0] + d[0, -1] + d[0, 1] + d[1, 0]) / 4;

        public int[,] C8() => from d in Data.AsRelative(Bounds.Skip)
                                     select (d[-1, -1] + d[-1, 0] + d[-1, 1]
                                           + d[ 0, -1]       +      d[ 0, 1]
                                           + d[ 1, -1] + d[ 1, 0] + d[ 1, 1]) / 8;
    }
}

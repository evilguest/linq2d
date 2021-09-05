using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Linq2d.Tests
{
    public class DoubleTests
    {
        [Fact]
        public void TestMulMin()
        {
            var array = ArrayHelper.InitAllRandDouble(1000, 1000, 42);
            array = (from a in array select Math.Min(a * 1.5, 1.0)).ToArray();
        }

    }
}

using System;
using Xunit;

namespace Linq2d.Tests
{
    public class ArraySource
    {
        [Fact] 
        public void TestNullReferences()
        {
            int[,] nullArr = null;
            Assert.Throws<ArgumentNullException>(() => from a in nullArr select a.Value);
            var arr = new int[1, 1];
            OutOfBoundsStrategy<int> s = null;
            Assert.Throws<ArgumentNullException>(() => from a in arr.With(s) select a.Value);
        }
    }
}

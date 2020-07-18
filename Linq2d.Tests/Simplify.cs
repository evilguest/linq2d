using Linq2d.Expressions;
using System;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using Xunit;

namespace Linq2d.Tests
{
    public class Simplify
    {
        [Fact]
        public void TestRangeIminusOneLessThanW()
        {
            Expression<Func<int, int, bool>> func = (int i, int w) => (i - 1 < w);
            var t = Arithmetic.Simplify(func.Body, Ranges.No.Add(func.Parameters[0], null, func.Parameters[1]));
            Assert.True(t is ConstantExpression ce && ce.Value.Equals(true));
        }
        [Fact]
        public void TestRangeIminusTwoLessThanWMinusOne()
        {
            Expression<Func<int, int, bool>> func = (int i, int w) => (i - 2 < w - 1);
            var t = Arithmetic.Simplify(func.Body, Ranges.No.Add(func.Parameters[0], null, func.Parameters[1]));
            Assert.True(t is ConstantExpression ce && ce.Value.Equals(true));
        }
        [Fact]
        public void TestRangeIplusOneLessThanWMinusOne()
        {
            Expression<Func<int, int, bool>> func = (int i, int w) => (i + 1 < w - 1);
            var t = Arithmetic.Simplify(func.Body, Ranges.No.Add(func.Parameters[0], null, Expression.Subtract(func.Parameters[1], Expression.Constant(3))));
            Assert.True(t is ConstantExpression ce && ce.Value.Equals(true));
        }
    }
}

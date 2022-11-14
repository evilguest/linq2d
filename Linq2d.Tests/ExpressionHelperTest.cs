using System;
using System.Linq.Expressions;
using Xunit;

using Linq2d.Expressions;

namespace Linq2d.Tests
{
    public class ExpressionHelperTest
    {
        [Fact]
        public void TestExpressionHelper()
        {
            Expression<Func<int, int>> e = x => 42;
            Assert.Throws<ArgumentException>(() => e.Call(Expression.Constant(42)));
            Assert.Throws<ArgumentException>(() => ExpressionHelper.New(e, Expression.Constant(42)));
        }
    }
}
 
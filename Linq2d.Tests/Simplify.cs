using Linq2d.Expressions;
using System;
using System.Linq.Expressions;
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
    

        //[Fact]
        //public void TestDualFiltering()
        //{
        //    var arr = new[] { "a", "b", "c", "d", "e" };
        //    var q = from p in arr where p.i % 2 == 0 select p;

        //    var q2 = from (int, int) p in arr select p.i;

        //}

    }

/*    public static class Enu
    {
        public static IEnumerable<(T item, int i)> Where<T>(this IEnumerable<T> input, Predicate<(T item, int i)> predicate)
        {
            var i = 0;
            var en = input.GetEnumerator();
            while (en.MoveNext())
                yield return (en.Current, i++);
        }
        public static IEnumerable<(T item, int i)> Cast<T>(this IEnumerable input)
        {
            var i = 0;
            foreach (var a in input.Cast<T>())
                yield return (a, i++);
        }
    }*/
}

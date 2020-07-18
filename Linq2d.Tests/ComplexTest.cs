using Linq2d.ComplexVector;
using System;
using System.Numerics;
using System.Runtime.Intrinsics;
using Xunit;

namespace Linq2d.Tests
{
    public class ComplexTest
    {
        [Fact]
        public void BasicComplexTest()
        {
            var a = new Complex(1, 2);
            var b = new Complex(3, 4);
            var av = new Complex[] { a, a, a }.AsSpan();
            var bv = new Complex[] { b, b, b }.AsSpan();

            var s = a + b;
            var sv = ComplexAvx.Add(av, bv);
            Assert.Equal(s, sv[0]);
            var d = b - a;
            var dv = ComplexAvx.Subtract(bv, av);
            Assert.Equal(d, dv[0]);
            var m = a * b; // (-5, 10)
            var mv = ComplexAvx.Multiply(av, bv);
            Assert.Equal(m, mv[0]);
        }
    }
}

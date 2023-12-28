using Xunit;

namespace Linq2d.Tests.Vectorization
{
    public class Sse2 : Base, IClassFixture<SuppressSsse3Fixture>
    {
        [Fact]
        public void ShortArithmetics()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => (short)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => (short)((short)(2 + (short)((short)s + 2)) - (short)s));
            var q = from s1 in source 
                    from s2 in source
                    select (short)((short)(2+(short)(s2+2))-s1);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void UShortArithmetics()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => (ushort)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => (ushort)((ushort)(2 + (ushort)((ushort)s + 2)) - (ushort)s));
            var q = from s1 in source
                    from s2 in source
                    select (ushort)((ushort)(2 + (ushort)(s2 + 2)) - s1);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void ShortConversion()
        {
            unchecked
            {
                var source = ArrayHelper.InitAllRand(100, 110, 42);
                var expect = ArrayHelper.InitAllRand(100, 110, 42, x => (short)x);
                var q = from s in source
                        select (short)s;
                Assert.Equal(expect, q.ToArray());
                var iv = (IVectorizable)q;
                AssertVectorized(iv, 4);
            }
        }
        [Fact]
        public void UShortConversion()
        {
            unchecked
            {
                var source = ArrayHelper.InitAllRand(100, 110, 42, x => (uint)x);
                var expect = ArrayHelper.InitAllRand(100, 110, 42, x => (ushort)x);
                var q = from s in source
                        select (ushort)s;
                Assert.Equal(expect, q.ToArray());
                var iv = (IVectorizable)q;
                AssertVectorized(iv, 4);
            }
        }
        [Fact]
        public void ShortNegation()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => (short)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => (short)(-(short)s));
            var q = from s in source
                    select (short)-s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

        [Fact]
        public void BoolConstShort()
        {
            var source = ArrayHelper.InitAllRand(70, 30, 42, x => (short)x);
            var expect = ArrayHelper.InitAll(70, 30, true);
            var q = from s in source
                    select (s == s);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }

        [Fact]
        public void ShortEquality()
        {
            var source1 = ArrayHelper.InitAllRand(70, 30, 42, x => (short)x);
            var source2 = ArrayHelper.InitAllRand(70, 30, 42, x => x > 0 ? (short)x : (short)-x);
            var expect = ArrayHelper.InitAllRand(70, 30, 42, x => x >= 0);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 == s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void UShortEquality()
        {
            var source1 = ArrayHelper.InitAllRand(70, 30, 42, x => (ushort)x);
            var source2 = ArrayHelper.InitAllRand(70, 30, 42, x => (ushort)(x & 0xFFFE));
            var expect = ArrayHelper.InitAllRand(70, 30, 42, x => x % 2 == 0);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 == s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void UShortNot()
        {
            var source = ArrayHelper.InitAllRand(70, 30, 42, x => (ushort)x);
            var expect = ArrayHelper.InitAllRand(70, 30, 42, x => (ushort)~(ushort)x);
            var q = from s in source
                    select (ushort)~s;
            //Array2d.SaveDynamicCode = true;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public unsafe void ShortNotIndirect()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (short)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (short)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x, y)=> !((short)x == (short)y) && true || false);
            var q = from s1 in source1
                    from s2 in source2
                    select !(s1 == s2) && true || false;
            //Array2d.SaveDynamicCode = true;
            //fixed(bool* pe = &expect[0,0])
            //    fixed(bool*pr = &r[0,0])
            //{
            //    Console.WriteLine($"{*(byte*)pe} == {*(byte*)pr}");
            //}
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void ShortAdd()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (short)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (short)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x, y) => (short)(x + y));
            var q = from s1 in source1
                    from s2 in source2
                    select (short)(s1 + s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }

        [Fact]
        public void UShortAdd()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (ushort)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (ushort)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x, y) => (ushort)(x + y));
            var q = from s1 in source1
                    from s2 in source2
                    select (ushort)(s1 + s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void ShortMultiply()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (short)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (short)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x, y) => (short)(x * y));
            var q = from s1 in source1
                    from s2 in source2
                    select (short)(s1 * s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }

        [Fact]
        public void UShortMultiply()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (ushort)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (ushort)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x, y) => (ushort)(x * y));
            var q = from s1 in source1
                    from s2 in source2
                    select (ushort)(s1 * s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }

        [Fact]
        public void ShortSubtract()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (short)(x));
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (short)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x,y) => (short)((short)x - (short)y));
            var q = from s1 in source1
                    from s2 in source2
                    select (short)(s1 - s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }

        [Fact]
        public void UShortSubtract()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (ushort)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (ushort)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x,y) => (ushort)((ushort)x - (ushort)y));
            var q = from s1 in source1
                    from s2 in source2
                    select (ushort)(s1 - s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }

        [Fact]
        public void ShortOr()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (short)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (short)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x,y) => (short)((short)x | (short)y));
            var q = from s1 in source1
                    from s2 in source2
                    select (short)(s1 | s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }

        [Fact]
        public void UShortOr()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (ushort)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (ushort)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x, y) => (ushort)((ushort)x | (ushort)y));
            var q = from s1 in source1
                    from s2 in source2
                    select (ushort)(s1 | s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void ShortXor()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (short)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (short)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x,y) => (short)((short)x ^ (short)y));
            var q = from s1 in source1
                    from s2 in source2
                    select (short)(s1 ^ s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }

        [Fact]
        public void UShortXor()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (ushort)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (ushort)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x, y) => (ushort)((ushort)x ^ (ushort)y));
            var q = from s1 in source1
                    from s2 in source2
                    select (ushort)(s1 ^ s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }

        [Fact]
        public void ShortAnd()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (short)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (short)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x, y) => (short)((short)x & (short)y));
            var q = from s1 in source1
                    from s2 in source2
                    select (short)(s1 & s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }

        [Fact]
        public void UShortAnd()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (ushort)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (ushort)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x, y) => (ushort)(x & y));
            var q = from s1 in source1
                    from s2 in source2
                    select (ushort)(s1 & s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void ShortShiftRight()
        {
            var source = ArrayHelper.InitAllRand(70, 40, 42, x => (short)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, x => (short)((short)x >> 3));
            var q = from s in source
                    select (short)(s >> 3);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }

        [Fact]
        public void UShortShiftRight()
        {
            var source = ArrayHelper.InitAllRand(70, 40, 42, x => (ushort)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, x => (ushort)((ushort)x >> 3));
            var q = from s in source
                    select (ushort)(s >> 3);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }

        [Fact]
        public void ShortShiftLeft()
        {
            var source = ArrayHelper.InitAllRand(70, 40, 42, x => (short)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, x => (short)((short)x << 3));
            var q = from s in source
                    select (short)(s << 3);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }

        [Fact]
        public void UShortShiftLeft()
        {
            var source = ArrayHelper.InitAllRand(70, 40, 42, x => (ushort)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, x => (ushort)((ushort)x << 3));
            var q = from s in source
                    select (ushort)(s << 3);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void ShortShiftRightVariable()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (short)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (short)(x % 16));
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x,y) => (short)((short)x >> (y % 16)));
            var q = from s1 in source1
                    from s2 in source2
                    select (short)(s1 >> s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            Assert.False(iv.Vectorized);
        }
        //[Fact]
        //public void ShortShiftRightA()
        //{
        //    var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (short)x);
        //    var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (byte)(x % 16));
        //    var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x, y) => (short)((short)x >> (y % 16)));
        //    var q = from s1 in source1
        //            from s2 in source2
        //            select (short)(s1 >>> s2);
        //    Assert.Equal(expect, q.ToArray());
        //    var iv = (IVectorizable)q;
        //    AssertVectorised(iv, 8);
        //}


        [Fact]
        public void UIntAdd()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (uint)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (uint)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x, y) => (uint)(x + y));
            var q = from s1 in source1
                    from s2 in source2
                    select (uint)(s1 + s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

        [Fact]
        public void IntOr()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x,y) => x | y);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 | s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

        [Fact]
        public void UIntOr()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x=>(uint)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x=>(uint)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x, y) => (uint)(x | y));
            var q = from s1 in source1
                    from s2 in source2
                    select s1 | s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void IntEquality()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42);
            var source2 = ArrayHelper.InitAllRand(70, 40, 42, x => x < 0 ? -x : x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, x => x >= 0); // we're inverting the sign only for the negative x'es.
            var q = from s1 in source1
                    from s2 in source2
                    select s1 == s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

        [Fact]
        public void UIntEquality()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (uint)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 42, x => (uint)(x & 0xFFFFFFFE));
            var expect = ArrayHelper.InitAllRand(70, 40, 42, x => x % 2 == 0); // we're flipping the low bit only on odd numbers
            var q = from s1 in source1
                    from s2 in source2
                    select s1 == s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

        [Fact]
        public void UIntSubtract()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (uint)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (uint)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x, y) => (uint)x - (uint)y);
            var q = from s1 in source1
                    from s2 in source2
                    select (uint)s1 - (uint)s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void IntXor()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x, y) => x ^ y);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 ^ s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

        [Fact]
        public void UIntXor()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (uint)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (uint)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x, y) => (uint)(x ^ y));
            var q = from s1 in source1
                    from s2 in source2
                    select s1 ^ s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void IntAnd()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x, y) => x & y);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 & s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

        [Fact]
        public void UIntAnd()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (uint)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (uint)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x, y) => (uint)(x & y));
            var q = from s1 in source1
                    from s2 in source2
                    select s1 & s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

        [Fact]
        public void IntShiftRight()
        {
            var source = ArrayHelper.InitAllRand(70, 40, 42);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, x => x >> 3);
            var q = from s in source
                    select s >> 3;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

        [Fact]
        public void UIntShiftRight()
        {
            var source = ArrayHelper.InitAllRand(70, 40, 42, x => (uint)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, x => (uint)x >> 3);
            var q = from s in source
                    select s >> 3;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

        [Fact]
        public void IntShiftLeft()
        {
            var source = ArrayHelper.InitAllRand(70, 40, 42);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, x => x << 3);
            var q = from s in source
                    select s << 3;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

        [Fact]
        public void UIntShiftLeft()
        {
            var source = ArrayHelper.InitAllRand(70, 40, 42, x => (uint)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, x => (uint)x << 3);
            var q = from s in source
                    select s << 3;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

        [Fact]
        public void IntFloatConversions()
        {
            var source = ArrayHelper.InitAllRand(70, 40, 42);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, x => (int)(x / 3f * 2));
            var q = from s in source
                    select (int) (s / 3f * 2);
            //Array2d.SaveDynamicCode = true;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void LongAdd()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42L);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17L);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, 17L, (x, y) => x + y);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 + s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }

        [Fact]
        public void ULongAdd()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42L, x => (ulong)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17L, x => (ulong)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, 17L, (x, y) => (ulong) (x + y));
            var q = from s1 in source1
                    from s2 in source2
                    select s1 + s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }

        [Fact]
        public void ULongEquality()
        {
            var source1 = ArrayHelper.InitAllRand(70, 30, 42L, x => (ulong)x);
            var source2 = ArrayHelper.InitAllRand(70, 30, 17L, x => (ulong)x);
            var expect = ArrayHelper.InitAllRand(70, 30, 42L, 17L, (x, y) => (ulong)x == (ulong)y);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 == s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }

        [Fact]
        public void ULongSubtract()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42L, x => (ulong)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17L, x => (ulong)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, 17L, (x, y) => (ulong)x - (ulong)y);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 - s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }

        [Fact]
        public void LongSubtract()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42L);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17L);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, 17L, (x, y) => x - y);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 - s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }
        [Fact]
        public void LongOr()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42L);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17L);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, 17L, (x, y) => x | y);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 | s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }

        [Fact]
        public void ULongOr()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42L, x => (ulong)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17L, x => (ulong)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, 17L, (x, y) => (ulong)(x | y));
            var q = from s1 in source1
                    from s2 in source2
                    select s1 | s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }

        [Fact]
        public void LongXor()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42L);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17L);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, 17L, (x, y) => x ^ y);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 ^ s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }

        [Fact]
        public void ULongXor()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42L, x => (ulong)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17L, x => (ulong)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, 17L, (x, y) => (ulong)(x ^ y));
            var q = from s1 in source1
                    from s2 in source2
                    select s1 ^ s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }

        [Fact]
        public void LongAnd()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42L);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17L);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, 17L, (x, y) => x & y);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 & s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }

        [Fact]
        public void ULongAnd()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42L, x => (ulong)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17L, x => (ulong)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, 17L, (x, y) => (ulong)(x & y));
            var q = from s1 in source1
                    from s2 in source2
                    select s1 & s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }
        [Fact]
        public void LongShiftRight()
        {
            var source = ArrayHelper.InitAllRand(70, 40, 42L);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, x => x >> 3);
            var q = from s in source
                    select s >> 3;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }

        [Fact]
        public void ULongShiftRight()
        {
            var source = ArrayHelper.InitAllRand(70, 40, 42L, x => (ulong)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, x => (ulong)x >> 3);
            var q = from s in source
                    select s >> 3;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }

        [Fact]
        public void LongShiftLeft()
        {
            var source = ArrayHelper.InitAllRand(70, 40, 42L);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, x => x << 3);
            var q = from s in source
                    select s << 3;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }

        [Fact]
        public void ULongShiftLeft()
        {
            var source = ArrayHelper.InitAllRand(70, 40, 42L, x => (ulong)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, x => (ulong)x << 3);
            var q = from s in source
                    select s << 3;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }

        [Fact]
        public void DoubleEquality()
        {
            var source1 = ArrayHelper.InitAllRand(70, 30, 42L, x => x*1.17);
            var source2 = ArrayHelper.InitAllRand(70, 30, 42L, x => Math.Abs(x*1.17));
            var expect = ArrayHelper.InitAllRand(70, 30, 42L, x => x >= 0);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 == s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }
        [Fact]
        public void DoubleAdd()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42L, x => x*1.17);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17L, x => x*1.13);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, 17L, (x, y) => (x*1.17 + y*1.13));
            var q = from s1 in source1
                    from s2 in source2
                    select s1 + s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }
        [Fact]
        public void DoubleSubtract()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42L, x => x * 1.17);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17L, x => x * 1.13);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, 17L, (x, y) => (x * 1.17 - y * 1.13));
            var q = from s1 in source1
                    from s2 in source2
                    select s1 - s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }
        [Fact]
        public void DoubleDivide()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42L, x => x * 1.17);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17L, x => x * 1.13);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, 17L, (x, y) => ((x * 1.17) / (y * 1.13)));
            var q = from s1 in source1
                    from s2 in source2
                    select s1 / s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }
        [Fact]
        public void DoubleMul()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42L, x => x * 1.17);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17L, x => x * 1.13);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, 17L, (x, y) => ((x * 1.17) * (y * 1.13)));
            var q = from s1 in source1
                    from s2 in source2
                    select s1 * s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }
        [Fact]
        public void DoubleMax()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42L, x => x * 1.17);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17L, x => x * 1.13);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, 17L, (x, y) => Math.Max(x * 1.17, y * 1.13));
            var q = from s1 in source1
                    from s2 in source2
                    select Math.Max(s1,s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }
        [Fact]
        public void DoubleMin()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42L, x => x * 1.17);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17L, x => x * 1.13);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, 17L, (x, y) => Math.Min(x * 1.17, y * 1.13));
            var q = from s1 in source1
                    from s2 in source2
                    select Math.Min(s1, s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }
        [Fact]
        public void SbyteCopy()
        {
            var source = ArrayHelper.InitAllRand(70, 40, 42, x => (sbyte)x);
            var q = from s in source
                    select (sbyte)s;
            Assert.Equal(source, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 16);
        }
    }
}

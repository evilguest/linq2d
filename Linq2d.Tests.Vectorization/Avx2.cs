using System;
using Xunit;

namespace Linq2d.Tests.Vectorization
{
    public class Avx2:Base, IClassFixture<SuppressAvx512Fixture>
    {
        [Fact]
        public void LongArithmetics()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42L);
            var expect = ArrayHelper.InitAllRand(100, 110, 42L, s => s + s - 3);
            var q = from s in source select s + s - 3;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void ULongArithmetics()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42L, x=>(ulong)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42L, s => (ulong)s + (ulong)s - 3);
            var q = from s in source select s + s - 3;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

        [Fact]
        public void IntArithmetics()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => s + s * 2 - 3);
            var q = from s in source select s + s * 2 - 3;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void UIntArithmetics()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => (uint)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => (uint)s + (uint)s * 2 - 3);
            var q = from s in source select s + s * 2 - 3;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
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
            AssertVectorized(iv, 8);
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
            AssertVectorized(iv, 8);
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

            IVectorizable iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }

        private static double[,] Sqrt(int[,] source)
        {
            return (from s in source select Math.Sqrt(s)).ToArray();
        }

        [Fact]
        public void TestByteLiftOptimization()
        {
            byte a = 42;
            var source = ArrayHelper.InitAll(17, 17, a);
//            CodeGen.Intrinsics.Sse.Suppress = true;
            var q = from s in source select a;

            Assert.Equal(source, q.ToArray());
//            CodeGen.Intrinsics.Sse.Suppress = false;

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 32);
        }
        private void TestConditionalInt256<T>(T t, T f, int sizeOfT)
            where T : unmanaged
        {
            int a = 42;
            var source = ArrayHelper.InitAllRand(16, 16, 42);
            var q = from s in source select s == a ? t : f;
            var expect = ArrayHelper.InitAllRand(16, 16, 42, x => x == a ? t : f);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 32 / Math.Max(sizeOfT, sizeof(int)));
        }
        private void TestConditionalLong256<T>(T t, T f, int sizeOfT)
            where T : unmanaged
        {
            long a = 42;
            var source = ArrayHelper.InitAllRand(16, 16, 42, x => (long)x);
            var q = from s in source select s == a ? t : f;
            var expect = ArrayHelper.InitAllRand(16, 16, 42, x => x == a ? t : f);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 32 / sizeOfT);
        }

        [Fact]
        public void TestDoubleConditional256() => TestConditionalLong256(Math.PI, Math.E, sizeof(double));
        [Fact]
        public void TestFloatConditional256() => TestConditionalInt256((float)Math.PI, (float)Math.E, sizeof(float));
        [Fact]
        public void TestLongConditional256() => TestConditionalLong256<long>(7, -42, sizeof(long));
        [Fact]
        public void TestULongConditional256() => TestConditionalLong256<ulong>(7, 42, sizeof(ulong));
        [Fact]
        public void TestShortConditional256() => TestConditionalInt256<short>(7, -42, sizeof(short));
        [Fact]
        public void TestUShortConditional256() => TestConditionalInt256<ushort>(7, 42, sizeof(ushort));

        [Fact]
        public void ShortToIntConversion()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => (short)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, x=> (int)(short)x);
            var q = from s in source
                    select (int)s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }

        [Fact]
        public void ShortNegation()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => (short)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => (short)-s);
            var q = from s in source
                    select (short)-s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 16);
        }

        [Fact]
        public void ShortAddition()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => (short)x);
            var source2 = ArrayHelper.InitAllRand(100, 110, 42, x => (short)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => (short)(s+s));
            var q = from s1 in source1
                    from s2 in source2
                    select (short)(s1 + s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 16);
        }
        [Fact]
        public void UShortAddition()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => (ushort)x);
            var source2 = ArrayHelper.InitAllRand(100, 110, 42, x => (ushort)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => (ushort)(s + s));
            var q = from s1 in source1
                    from s2 in source2
                    select (ushort)(s1 + s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 16);
        }
        [Fact]
        public void ByteAddition()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => (byte)x);
            var source2 = ArrayHelper.InitAllRand(100, 110, 42, x => (byte)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => (byte)(s + s));
            var q = from s1 in source1
                    from s2 in source2
                    select (byte)(s1 + s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 32);
        }
        [Fact]
        public void SByteAddition()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => (sbyte)x);
            var source2 = ArrayHelper.InitAllRand(100, 110, 42, x => (sbyte)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => (sbyte)(s + s));
            var q = from s1 in source1
                    from s2 in source2
                    select (sbyte)(s1 + s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 32);
        }
        [Fact]
        public void ByteLongAddition()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42L, x => (byte)x);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17L);
            var expect = ArrayHelper.InitAllRand(100, 110, 42L, 17L, (s1, s2) => (byte)s1 + s2);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 + s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void SByteLongAddition()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42L, x => (sbyte)x);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17L);
            var expect = ArrayHelper.InitAllRand(100, 110, 42L, 17L, (s1, s2) => (sbyte)s1 + s2);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 + s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
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
            AssertVectorized(iv, 4);
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
            AssertVectorized(iv, 4);
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
            AssertVectorized(iv, 4);
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
            AssertVectorized(iv, 4);
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
            AssertVectorized(iv, 4);
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
            AssertVectorized(iv, 4);
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
            AssertVectorized(iv, 4);
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
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void ULongEquality()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42L, x => (ulong)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 42L, x => (ulong)x & 0xFFFFFFFFFFFFFFFE);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, x => x % 2 == 0); // we're flipping the low bit only on odd numbers
            var q = from s1 in source1
                    from s2 in source2
                    select s1 == s2;
            Assert.Equal(expect, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
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
            AssertVectorized(iv, 4);
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
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void IntNegation()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => -s);
            var q = from s in source
                    select -s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
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
            AssertVectorized(iv, 8);
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
            AssertVectorized(iv, 8);
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
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void SByteEquality()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x=>(sbyte)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 42, x => x < 0 ? (sbyte)-x : (sbyte)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, x => x >= 0); // we're inverting the sign only for the negative x'es.
            var q = from s1 in source1
                    from s2 in source2
                    select s1 == s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 32);
        }

        [Fact]
        public void ByteEquality()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (byte)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 42, x => (byte)(x & 0xFFFFFFFE));
            var expect = ArrayHelper.InitAllRand(70, 40, 42, x => x % 2 == 0); // we're flipping the low bit only on odd numbers
            var q = from s1 in source1
                    from s2 in source2
                    select s1 == s2;
            Assert.Equal(expect, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 32);
        }
        [Fact]
        public void ShortEquality()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (short)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 42, x => x < 0 ? (short)-x : (short)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, x => x >= 0); // we're inverting the sign only for the negative x'es.
            var q = from s1 in source1
                    from s2 in source2
                    select s1 == s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 16);
        }

        [Fact]
        public void UshortEquality()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (ushort)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 42, x => (ushort)(x & 0xFFFFFFFE));
            var expect = ArrayHelper.InitAllRand(70, 40, 42, x => x % 2 == 0); // we're flipping the low bit only on odd numbers
            var q = from s1 in source1
                    from s2 in source2
                    select s1 == s2;
            Assert.Equal(expect, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 16);
        }

        [Fact]
        public void SByteNegation()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => (sbyte)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => (sbyte)-s);
            var q = from s in source
                    select (sbyte)-s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 32);
        }

        [Fact]
        public void IntMixedArith()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42, x => (byte)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17, x => (sbyte)x);
            var source3 = ArrayHelper.InitAllRand(70, 40, 42, x => (short)x);
            var source4 = ArrayHelper.InitAllRand(70, 40, 17, x => (ushort)x);
            var source0 = ArrayHelper.InitAllRand(70, 40, 42);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, 17, (x, y) => (byte)x + (sbyte)y + (short)x + (ushort)y + x);
            var q = from s in source0
                    from s1 in source1
                    from s2 in source2
                    from s3 in source3
                    from s4 in source4
                    select s1 + s2 + s3 + s4 + s;
            Assert.Equal(expect, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void LongMixedArith()
        {
            var source1 = ArrayHelper.InitAllRand(70, 40, 42L, x => (byte)x);
            var source2 = ArrayHelper.InitAllRand(70, 40, 17L, x => (sbyte)x);
            var source3 = ArrayHelper.InitAllRand(70, 40, 42L, x => (short)x);
            var source4 = ArrayHelper.InitAllRand(70, 40, 17L, x => (ushort)x);
            var source5 = ArrayHelper.InitAllRand(70, 40, 42L, x => (int)x);
            var source6 = ArrayHelper.InitAllRand(70, 40, 17L, x => (uint)x);
            var source0 = ArrayHelper.InitAllRand(70, 40, 42L);
            var expect = ArrayHelper.InitAllRand(70, 40, 42L, 17L, (x, y) => (byte)x + (sbyte)y + (short)x + (ushort)y + (int)x + (uint)y + x);
            var q = from s in source0
                    from s1 in source1
                    from s2 in source2
                    from s3 in source3
                    from s4 in source4
                    from s5 in source5
                    from s6 in source6
                    select s1 + s2 + s3 + s4 + s5 + s6 + s;
            Assert.Equal(expect, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void IntToShortConversion()
        {
            var source = ArrayHelper.InitAllRand(70, 40, 42);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, x=>(short)x);
            var q = from s in source
                    select (short)s;
            Assert.Equal(expect, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void ShortByteArith()
        {
            var source = ArrayHelper.InitAllRand(70, 40, 42, x=>(byte)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, x => (short)(byte)x);
            var q = from s in source
                    select (short)s;
            Assert.Equal(expect, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 16);
        }
        [Fact]
        public void ShortSByteArith()
        {
            var source = ArrayHelper.InitAllRand(70, 40, 42, x => (sbyte)x);
            var expect = ArrayHelper.InitAllRand(70, 40, 42, x => (short)(sbyte)x);
            var q = from s in source
                    select (short)s;
            Assert.Equal(expect, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 16);
        }
    }
}

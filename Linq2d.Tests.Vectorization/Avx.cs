namespace Linq2d.Tests.Vectorization
{
    //[Collection("Vectorization")]
    public class Avx : Base, IClassFixture<SuppressAvx2Fixture>
    {
        [Fact]
        public void FloatAddition()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x=>x*1.17f);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17, x=>x*1.13f);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, 17, (x, y) => (x*1.17f)+(y*1.13f));
            var q = from s1 in source1 
                    from s2 in source2
                    select s1 + s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void FloatSubtraction()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => x * 1.17f);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17, x => x * 1.13f);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, 17, (x, y) => (x * 1.17f) - (y * 1.13f));
            var q = from s1 in source1
                    from s2 in source2
                    select s1 - s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void DoubleSubtraction()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42L, x => x * 1.17);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17L, x => x * 1.13);
            var expect = ArrayHelper.InitAllRand(100, 110, 42L, 17L, (x, y) => (x * 1.17) - (y * 1.13));
            var q = from s1 in source1
                    from s2 in source2
                    select s1 - s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

        [Fact]
        public void FloatMultiplication()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => x * 1.17f);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17, x => x * 1.13f);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, 17, (x, y) => (x * 1.17f) * (y * 1.13f));
            var q = from s1 in source1
                    from s2 in source2
                    select s1 * s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void FloatDivision()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => x * 1.17f);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17, x => x * 1.13f);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, 17, (x, y) => (x * 1.17f) / (y * 1.13f));
            var q = from s1 in source1
                    from s2 in source2
                    select s1 / s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void FloatNegation()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => x * 1.17f);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, x => x * -1.17f);
            var q = from s in source
                    select -s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }


        [Fact]
        public void FloatGreaterThan()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => x * 1.17f);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17, x => x * 1.13f);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, 17, (x, y) => x * 1.17f > y * 1.13f ? (uint)42 : (uint)17);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 > s2 ? (uint)42 : (uint)17;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void FloatLessThan()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => x * 1.17f);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17, x => x * 1.13f);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, 17, (x, y) => (x * 1.17f) < (y * 1.13f) ? 42 : 17);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 < s2 ? 42 : 17;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void FloatGreaterThanOrEqual()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => x * 1.17f);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17, x => x * 1.13f);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, 17, (x, y) => x * 1.17f >= y * 1.13f ? 42 : 17);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 >= s2 ? 42 : 17;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void FloatEquality()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => x * 1.17f);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17, x => x * 1.13f);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, 17, (x, y) => x * 1.17f == y * 1.13f ? 42 : 17);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 == s2 ? 42 : 17;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void FloatLessThanOrEqual()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => x * 1.17f);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17, x => x * 1.13f);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, 17, (x, y) => x * 1.17f <= y * 1.13f ? 42 : 17);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 <= s2 ? 42 : 17;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }

        [Fact]
        public void DoubleEquality()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => x * 1.17);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17, x => x * 1.13);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, 17, (x, y) => x * 1.17 == y * 1.13 ? 42L : 17L);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 == s2 ? 42L : 17L;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void DoubleGreaterThan()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42L, x => x * 1.17);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17L, x => x * 1.13);
            var expect = ArrayHelper.InitAllRand(100, 110, 42L, 17L, (x, y) => x * 1.17 > y * 1.13 ? 42UL : 17UL);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 > s2 ? 42UL : 17UL;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void DoubleLessThan()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42L, x => x * 1.17);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17L, x => x * 1.13);
            var expect = ArrayHelper.InitAllRand(100, 110, 42L, 17L, (x, y) => (x * 1.17) < (y * 1.13) ? 42L : 17L);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 < s2 ? 42L : 17L;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void DoubleGreaterThanOrEqual()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42L, x => x * 1.17);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17L, x => x * 1.13);
            var expect = ArrayHelper.InitAllRand(100, 110, 42L, 17L, (x, y) => x * 1.17 >= y * 1.13 ? 42UL : 17UL);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 >= s2 ? 42UL : 17UL;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
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
        public void TestByteToDouble()
        {
            var source = ArrayHelper.InitAllRand(170, 170, (byte)42);
            var expect = ArrayHelper.InitAllRand(170, 170, 42, x => 1.13 * (byte)x);

            var q = from s in source select 1.13*s;

            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

        [Fact]
        public void TestSByteToDouble()
        {
            var source = ArrayHelper.InitAllRand(170, 170, 42, x=>(sbyte)x);
            var expect = ArrayHelper.InitAllRand(170, 170, 42, x => 1.13 * (sbyte)x);

            var q = from s in source select 1.13 * s;

            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

        [Fact]
        public void TestShortToDouble()
        {
            var source = ArrayHelper.InitAllRand(170, 170, 42, x => (short)x);
            var expect = ArrayHelper.InitAllRand(170, 170, 42, x => 1.13 * (short)x);

            var q = from s in source select 1.13 * s;

            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void TestUshortToDouble()
        {
            var source = ArrayHelper.InitAllRand(170, 170, 42, x => (ushort)x);
            var expect = ArrayHelper.InitAllRand(170, 170, 42, x => 1.13 * (ushort)x);

            var q = from s in source select 1.13 * s;

            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }


        [Fact]
        public void TestByteCopyOptimization()
        {
            var source = ArrayHelper.InitAllRand(100, 110, (byte)42);
            var q = from s in source select (byte)s;

            Assert.Equal(source, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 32);
        }
        [Fact]
        public void TestByteLiftOptimization()
        {
            byte a = 42;
            var source = ArrayHelper.InitAll(17, 17, a);
            var q = from s in source select a;

            Assert.Equal(source, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 32);
        }
        [Fact]
        public void ByteLoadStore()
        {
            var source = ArrayHelper.InitAllRand(110, 100, 42, x=>(byte)x);
            var q = from s in source select s.Value;

            Assert.Equal(source, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 32);
        }
        [Fact]
        public void SByteLoadStore()
        {
            var source = ArrayHelper.InitAllRand(110, 100, 42, x => (sbyte)x);
            var q = from s in source select s.Value;

            Assert.Equal(source, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 32);
        }
        [Fact]
        public void ShortLoadStore()
        {
            var source = ArrayHelper.InitAllRand(110, 100, 42, x => (short)x);
            var q = from s in source select s.Value;

            Assert.Equal(source, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 16);
        }
        [Fact]
        public void UShortLoadStore()
        {
            var source = ArrayHelper.InitAllRand(110, 100, 42, x => (ushort)x);
            var q = from s in source select s.Value;

            Assert.Equal(source, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 16);
        }

                [Fact]
        public void IntLoadStore()
        {
            var source = ArrayHelper.InitAllRand(110, 100, 42);
            var q = from s in source select s.Value;

            Assert.Equal(source, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void UIntLoadStore()
        {
            var source = ArrayHelper.InitAllRand(110, 100, 42, x => (uint)x);
            var q = from s in source select s.Value;

            Assert.Equal(source, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void ULongLoadStore()
        {
            var source = ArrayHelper.InitAllRand(110, 100, 42L, x => (ulong)x);
            var q = from s in source select s.Value;

            Assert.Equal(source, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

        private void TestConditionalInt256<T>(T t, T f, int sizeOfT)
            where T : unmanaged
        {
            var source = ArrayHelper.InitAllRand(16, 16, 42);
            int a = source[0, 0];

            var q = from s in source select s == a ? t : f;
            var expect = ArrayHelper.InitAllRand(16, 16, 42, x => x == a ? t : f);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 16 / Math.Max(sizeOfT, sizeof(int)));
        }
        private void TestConditionalLong256<T>(T t, T f, int sizeOfT)
            where T : unmanaged
        {
            var source = ArrayHelper.InitAllRand(16, 16, 42L);
            long a = source[0, 0];
            var q = from s in source select s == a ? t : f;
            var expect = ArrayHelper.InitAllRand(16, 16, 42L, x => x == a ? t : f);
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
        public void ConvertDoubleToInt()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => x * 1.17);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, x => (int)(x * 1.17)); 
            var q = from s in source
                    select (int)s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void ConvertFloatToInt()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => x * 1.17f);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, x => (int)(x * 1.17f));
            var q = from s in source
                    select (int)s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void LiftIntToFloat()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, x => (float)x);
            var q = from s in source
                    select (float)s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void ConvertIntToFloat()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => x * 0.89f);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, x => (float)(int)(x * 0.89f));
            var q = from s in source
                    select (float)(int)s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void ConvertIntToDouble()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, x => (double)(x+x));
            var q = from s in source
                    select (double)((s+s));
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void ConvertDoubleToFloat()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => x * 1.17);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, x => (float)(x * 1.17));
            var q = from s in source
                    select (float)s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void ConvertFloatToDouble()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42, x => x * 1.17f);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, x => (double)(x * 1.17f));
            var q = from s in source
                    select (double)s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void DoubleMax()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42L, x => x * 1.17);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17L, x => x * 1.13);
            var expect = ArrayHelper.InitAllRand(100, 110, 42L, 17L, (x, y) => Math.Max(x * 1.17, y * 1.13));
            var q = from s1 in source1
                    from s2 in source2
                    select Math.Max(s1, s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void DoubleMin()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42L, x => x * 1.17);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17L, x => x * 1.13);
            var expect = ArrayHelper.InitAllRand(100, 110, 42L, 17L, (x, y) => Math.Min(x * 1.17, y * 1.13));
            var q = from s1 in source1
                    from s2 in source2
                    select Math.Min(s1, s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void DoubleNegation()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42L, x => x * 1.17);
            var expect = ArrayHelper.InitAllRand(100, 110, 42L, x => x * -1.17);
            var q = from s in source
                    select -s;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
    }
}

namespace Linq2d.Tests.Vectorization
{
    public class Sse41 : Base, IClassFixture<SuppressAvxFixture>
    {
        [Fact]
        public void IntArithmetics()
        {
            var source = ArrayHelper.InitAllRand(100, 110, 42);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, s => s + s * 2 - 3);
            var q = from s in source select s + s * 2 - 3;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

        [Fact]
        public void UIntMul()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => (uint)x);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17, x => (uint)x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, 17, (x, y) => (uint)(x*y));
            var q = from s1 in source1
                    from s2 in source2
                    select s1 * s2;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }

        [Fact]
        public void IntMax()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17);

            var expect = ArrayHelper.InitAllRand(100, 110, 42, 17, Math.Max);
            var q = from s1 in source1
                    from s2 in source2
                    select Math.Max(s1, s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void IntMin()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17);

            var expect = ArrayHelper.InitAllRand(100, 110, 42, 17, Math.Min);
            var q = from s1 in source1
                    from s2 in source2
                    select Math.Min(s1, s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void UIntMax()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => (uint)x);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17, x => (uint)x);

            var expect = ArrayHelper.InitAllRand(100, 110, 42, 17, (x, y)=>Math.Max((uint)x,(uint)y));
            var q = from s1 in source1
                    from s2 in source2
                    select Math.Max(s1, s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void UIntMin()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => (uint)x);
            var source2 = ArrayHelper.InitAllRand(100, 110, 17, x => (uint)x);

            var expect = ArrayHelper.InitAllRand(100, 110, 42, 17, (x, y) => Math.Min((uint)x, (uint)y));
            var q = from s1 in source1
                    from s2 in source2
                    select Math.Min(s1, s2);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        //private static double[,] Sqrt(int[,] source)
        //{
        //    return (from s in source select Math.Sqrt(s)).ToArray();
        //}


        [Fact] 
        public void ShortConditional()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x=>(short)x);
            var source2 = ArrayHelper.InitAllRand(100, 110, 42, x => x > 0 ? (short)x : (short)-x);
            var expect = ArrayHelper.InitAllRand(100, 110, 42, x => x >= 0 ? (short)11 : (short)-17);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 == s2 ? (short)11 : (short)-17;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        [Fact]
        public void UShortConditional()
        {
            var source1 = ArrayHelper.InitAllRand(100, 110, 42, x => (ushort)x);
            var source2 = ArrayHelper.InitAllRand(100, 110, 42, x => (ushort)(x & 0xFFFE));
            var expect = ArrayHelper.InitAllRand(100, 110, 42,  x =>  x%2==0? (ushort)11 : (ushort)17);
            var q = from s1 in source1
                    from s2 in source2
                    select s1 == s2 ? (ushort)11 : (ushort)17;
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 8);
        }
        
        [Fact]
        public void TestByteCopyOptimization()
        {
            var source = ArrayHelper.InitAllRand(17, 17, (byte)42);
            var q = from s in source select (byte)s;

            Assert.Equal(source, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 16);
        }
        [Fact]
        public void TestByteLiftOptimization()
        {
            byte a = 42;
            var source = ArrayHelper.InitAll(17, 17, a);
            var q = from s in source select a;

            Assert.Equal(source, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 16);
        }

        [Fact] 
        public void TestByteToIntConversion() 
        {
            var source = ArrayHelper.InitAllRand(17, 17, 42, x => (byte)x);
            var expect = ArrayHelper.InitAllRand(17, 17, 42, x => x & 0xFF);
            var q = from s in source select (int)s;

            Assert.Equal(expect, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void TestSByteToIntConversion()
        {
            var source = ArrayHelper.InitAllRand(17, 17, 42, x => (sbyte)x);
            var expect = ArrayHelper.InitAllRand(17, 17, 42, x => (int)(sbyte)x);
            var q = from s in source select (int)s;

            Assert.Equal(expect, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void TestShortToIntConversion()
        {
            var source = ArrayHelper.InitAllRand(17, 17, 42, x => (short)x);
            var expect = ArrayHelper.InitAllRand(17, 17, 42, x => (int)(short)x);
            var q = from s in source select (int)s;

            Assert.Equal(expect, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void TestUShortToIntConversion()
        {
            var source = ArrayHelper.InitAllRand(17, 17, 42, x => (ushort)x);
            var expect = ArrayHelper.InitAllRand(17, 17, 42, x => (int)(ushort)x);
            var q = from s in source select (int)s;

            Assert.Equal(expect, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void TestByteToLongConversion()
        {
            var source = ArrayHelper.InitAllRand(17, 17, 42L, x => (byte)x);
            var expect = ArrayHelper.InitAllRand(17, 17, 42L, x => x & 0xFF);
            var q = from s in source select (long)s;

            Assert.Equal(expect, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }
        [Fact]
        public void TestSByteToLongConversion()
        {
            var source = ArrayHelper.InitAllRand(17, 17, 42, x => (sbyte)x);
            var expect = ArrayHelper.InitAllRand(17, 17, 42, x => (long)(sbyte)x);
            var q = from s in source select (long)s;

            Assert.Equal(expect, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }
        [Fact]
        public void TestShortToLongConversion()
        {
            var source = ArrayHelper.InitAllRand(17, 17, 42, x => (short)x);
            var expect = ArrayHelper.InitAllRand(17, 17, 42, x => (long)(short)x);
            var q = from s in source select (long)s;

            Assert.Equal(expect, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }
        [Fact]
        public void TestUShortToLongConversion()
        {
            var source = ArrayHelper.InitAllRand(17, 17, 42, x => (ushort)x);
            var expect = ArrayHelper.InitAllRand(17, 17, 42, x => (long)(ushort)x);
            var q = from s in source select (long)s;

            Assert.Equal(expect, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }
        [Fact]
        public void TestIntToLongConversion()
        {
            var source = ArrayHelper.InitAllRand(17, 17, 42);
            var expect = ArrayHelper.InitAllRand(17, 17, 42, x => (long)x);
            var q = from s in source select (long)s;

            Assert.Equal(expect, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }
        [Fact]
        public void TestUIntToLongConversion()
        {
            var source = ArrayHelper.InitAllRand(17, 17, 42, x => (uint)x);
            var expect = ArrayHelper.InitAllRand(17, 17, 42, x => (long)(uint)x);
            var q = from s in source select (long)s;

            Assert.Equal(expect, q.ToArray());

            var iv = (IVectorizable)q;
            AssertVectorized(iv, 2);
        }
        [Fact]
        public void TestIntComparison128()
        {
            int a = 42;
            var source = ArrayHelper.InitAllRand(16, 16, 42);
            var q = from s in source select s == a ? 1 : -1;
            var expect = ArrayHelper.InitAllRand(16, 16, 42, x => x == a ? 1 : -1);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }
        [Fact]
        public void TestUIntComparison128()
        {
            uint a = 42;
            var source = ArrayHelper.InitAllRand(16, 16, 42, x=>(uint)x);
            var q = from s in source select s == a ? (uint)1 : 7;
            var expect = ArrayHelper.InitAllRand(16, 16, 42, x => x == a ? (uint)1 : 7);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 4);
        }


        private void TestConditionalInt128<T>(T t, T f, int sizeOfT)
            where T: unmanaged
        {
            int a = 42;
            var source = ArrayHelper.InitAllRand(16, 16, 42);
            var q = from s in source select s == a ? t : f;
            var expect = ArrayHelper.InitAllRand(16, 16, 42, x => x == a ? t : f);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 16/sizeOfT);
        }
        private void TestConditionalLong128<T>(T t, T f, int sizeOfT)
            where T : unmanaged
        {
            long a = 42;
            var source = ArrayHelper.InitAllRand(16, 16, 42, x=>(long)x);
            var q = from s in source select s == a ? t : f;
            var expect = ArrayHelper.InitAllRand(16, 16, 42, x => x == a ? t : f);
            Assert.Equal(expect, q.ToArray());
            var iv = (IVectorizable)q;
            AssertVectorized(iv, 16 / sizeOfT);
        }
        [Fact]
        public void TestDoubleConditional128() => TestConditionalLong128(Math.PI, Math.E, sizeof(double));
        [Fact]
        public void TestFloatConditional128() => TestConditionalInt128((float)Math.PI, (float)Math.E, sizeof(float));
        [Fact]
        public void TestLongConditional128() => TestConditionalLong128<long>(7, -42, sizeof(long));

        [Fact]
        public void TestULongConditional128() => TestConditionalLong128<ulong>(7, 42, sizeof(ulong));

    }
}

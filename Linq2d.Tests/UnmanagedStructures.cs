using System;
using Xunit;

namespace Linq2d.Tests
{
    /*
    public class UnmanagedStructures
    {
        [Theory]
        [InlineData(17,13,67546,35324)]
        public void TestIntRecordToInt(int h, int w, int a, int b)
        {
            var s = ArrayHelper.InitAll(h, w, a + b);
            var data = ArrayHelper.InitAll(h, w, (a, b));
            var q = from d in data select d[0, 0].a + d[0, 0].b;
            Assert.Equal(s, q.ToArray());

        }
        [Theory]
        [InlineData(17, 13, 67546, 234)]
        public void TestMixedRecordToInt(int h, int w, int a, byte b)
        {
            var data = ArrayHelper.InitAll(h, w, (a, b));
            var s = MixedRecordToIntUnsafe(data);
            var q = from d in data select d[0, 0].a + d[0, 0].b;
            Assert.Equal(s, q.ToArray());
        }

        [Theory]
        [InlineData(17, 13, 67546, 24534)]
        public void TestIntToIntRecord(int h, int w, int a, int b)
        {
            var data = ArrayHelper.InitAll(h, w, a);

            var (ex1, ex2) = TestIntToIntRecordUnsafe(data, b);
            var q = from d in data select ValueTuple.Create(d / b, d % b);

            var (r1, r2) = q.ToArrays();
            Assert.Equal(ex1, r1);
            Assert.Equal(ex2, r2);

        }


        private static unsafe int[,] MixedRecordToIntUnsafe((int a, byte b)[,] data)
        {
            var res = new int[data.Height(), data.Width()];
            fixed ((int a, byte b) * pSrc = &data[0, 0])
            fixed (int* pTrg = &res[0, 0])
            {
                for (int i = 0; i < data.Height(); i++)
                    for (int j = 0; j < data.Width(); j++)
                        pTrg[i * data.Width() + j] = pSrc[i * data.Width() + j].a + pSrc[i * data.Width() + j].b;
            }
            return res;
        }
        private static unsafe int[,] IntRecordToIntUnsafe((int a, int b)[,] data)
        {
            var res = new int[data.Height(), data.Width()];
            fixed ((int a, int b) * pSrc = &data[0, 0])
            fixed (int* pTrg = &res[0, 0])
            {
                for (int i = 0; i < data.Height(); i++)
                    for (int j = 0; j < data.Width(); j++)
                        pTrg[i * data.Width() + j] = pSrc[i * data.Width() + j].a + pSrc[i * data.Width() + j].b;
            }
            return res;
        }
        private static unsafe (int [,] a, int[,] b) TestIntToIntRecordUnsafe(int[,] data, int d)
        {
            var a = new int[data.Height(), data.Width()];
            var b = new int[data.Height(), data.Width()];
            fixed (int* pSrc = &data[0, 0])
            fixed (int* pTrg1 = &a[0, 0])
            fixed (int* pTrg2 = &b[0, 0])
                for (int i = 0; i < data.Height(); i++)
                    for (int j = 0; j < data.Width(); j++)
                    {
                        pTrg1[i * data.Width() + j] = pSrc[i * data.Width() + j] / d;
                        pTrg2[i * data.Width() + j] = pSrc[i * data.Width() + j] % d;
                    }
            return (a, b);
        }
        //void Test()
        //{
        //    var q1 = from t in new Test<int>()
        //             from (a,b) in TestHelper1.Sbst("Hello", 42)
        //             select (k.Item1, k.Item2);
        //    var q2 = from p in new Test<string>() select "world";
        //    var q3 = from t in new Test<int>() select "world";
        //    var q4 = from p in new Test<string>() select (1, 2);


        //}
    }*/
/*    public struct Test<T> { }
    public static class TestHelper1
    {
        public static (R1, R2) Sbst<R1, R2>(R1 a, R2 b) => (default, default);
        public static R[,] Select<T, R>(this Test<T> test, Func<T, R> selector)
            => new R[1, 1] { { selector(default) } };
            
        public static ValueTuple<R1[,], R2[,]> Select<T, R1, R2>(this Test<T> test, Func<T, ValueTuple<R1, R2>> selector)
        {
            var t = selector(default);
            return (new R1[1, 1] { { t.Item1 } }, new R2[1, 1] { { t.Item2 } });
        }
        public static (R1[,], R2[,]) SelectMany<T, R1, R2>(this Test<T> test, Func<object, (R1, R2)> subst, Func<T, (R1, R2), (R1, R2)> selector)
        {
            var t = selector(default);
            return (new R1[1, 1] { { t.Item1 } }, new R2[1, 1] { { t.Item2 } });
        }
    }*/
}

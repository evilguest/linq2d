using Xunit;

namespace Linq2d.Tests
{
    public class RecurrentFiltering
    {
        private static int[,] SimpleIntegrate(byte[,] _data)
        {
            int[,] res = new int[_data.Height(), _data.Width()];
            // handle left corner
            res[0, 0] = _data[0, 0];

            //handle first line
            for (var j = 1; j < _data.Width(); j++)
                res[0, j] = _data[0, j] + res[0, j - 1]
                    ;

            //handle the other lines

            for (var i = 1; i < _data.Height(); i++)
            {
                // handle the first column
                res[i, 0] = _data[i, 0] + res[i - 1, 0]
                    ;

                //handle the other columns
                for (var j = 1; j < _data.Width(); j++)
                    res[i, j] = _data[i, j]
                        + res[i, j - 1]
                        + res[i - 1, j]
                        - res[i - 1, j - 1]
                        ;
            }
            return res;
        }
        private static int[,] SimpleIntegrate(int[,] _data)
        {
            int[,] res = new int[_data.Height(), _data.Width()];
            // handle left corner
            res[0, 0] = _data[0, 0];

            //handle first line
            for (var j = 1; j < _data.Width(); j++)
                res[0, j] = _data[0, j] + res[0, j - 1]
                    ;

            //handle the other lines

            for (var i = 1; i < _data.Height(); i++)
            {
                // handle the first column
                res[i, 0] = _data[i, 0] + res[i - 1, 0]
                    ;

                //handle the other columns
                for (var j = 1; j < _data.Width(); j++)
                    res[i, j] = _data[i, j]
                        + res[i, j - 1]
                        + res[i - 1, j]
                        - res[i - 1, j - 1]
                        ;
            }
            return res;
        }


        [Theory]
        [InlineData(1, 2, 4)]
        [InlineData(3, 3, 43)]
        [InlineData(5, 7, 42)]
        [InlineData(3425, 733, 42)]

        public void TestIntegral(int h, int w, byte b)
        {
            var data = ArrayHelper.InitDiagonal(h, w, b);
            var q = from d in data
                    from r in Result.InitWith(0)
                    select d + r[-1, 0] + r[0, -1] - r[-1, -1];
            int[,] expected = SimpleIntegrate(data);
            int[,] actual = q.ToArray();
            TestHelper.AssertEqual(expected, actual);
        }

        [Theory]
        [InlineData(1, 2, 4, 23)]
        [InlineData(3, 3, 43, 12)]
        [InlineData(5, 7, 42, 12987)]
        [InlineData(3425, 733, 42, -15466)]
        public void TestDoubleIntegral(int h, int w, byte a, short b)
        {
            var dataLeft = ArrayHelper.InitDiagonal(h, w, a);
            var dataRight = ArrayHelper.InitAll(h, w, b);
            var sum = (from left in dataLeft
                       from right in dataRight
                       select left + right
                       ).ToArray();

            var q = from left in dataLeft
                    from right in dataRight
                    from r in Result.InitWith(0)
                    select left + right + r[-1, 0] + r[0, -1] - r[-1, -1];
            TestHelper.AssertEqual(SimpleIntegrate(sum), q.ToArray());
        }

        [Fact]
        public void TestPrimitiveRecursion()
        {
            var sample = new[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            var q = from s in sample
                    from r in Result.InitWith(0)
                    select s + r[-1, 0];
            Assert.Equal(new[,] { { 1, 2, 3 }, { 5, 7, 9 }, { 12, 15, 18 } }, q.ToArray());
        }
    }
}

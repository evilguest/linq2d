using System;
using System.Diagnostics;

namespace Linq2d.Tests
{
    class TestHelper
    {
        [DebuggerStepThrough]
        public static void AssertEqual<T>(T[,] expected, T[,] actual)
            where T: IEquatable<T>
        {
            var (h, w) = ArrayHelper.EnsureSize(0, 0, expected, actual);
            for (var i = 0; i < h; i++)
                for (var j = 0; j < w; j++)
                    if (!expected[i, j].Equals(actual[i, j]))
                        throw new Exception($"{expected[i, j]} == expected[{i},{j}] != actual[{i},{j}] == {actual[i, j]} ");

        }
    }
}

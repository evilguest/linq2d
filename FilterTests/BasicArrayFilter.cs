using System.Linq.Processing2d;

namespace FilterTests
{
    public class BasicArrayFilter : ArrayFilterBase, IArrayFilter<int>
    {
        public int[,] C4()
        {
            var h = Data.GetLength(0);
            var w = Data.GetLength(1);
            var result = new int[h, w];
            for (int i = 1; i < h - 1; i++)
                for (int j = 1; j < w - 1; j++)
                    result[i, j] = (Data[i, j - 1] + Data[i, j + 1] + Data[i - 1, j] + Data[i + 1, j]) / 4;
            return result;
        }

        public int[,] C8()
        {
            var h = Data.GetLength(0);
            var w = Data.GetLength(1);
            var result = new int[h, w];

            for (int i = 1; i < h - 1; i++)
                for (int j = 1; j < w - 1; j++)
                    result[i, j] = (Data[i - 1, j - 1] + Data[i - 1, j] + Data[i - 1, j + 1] 
                                  + Data[i    , j - 1]          +         Data[i    , j + 1]
                                  + Data[i + 1, j - 1] + Data[i + 1, j] + Data[i + 1, j + 1]) / 8;
            return result;
        }
    }
}

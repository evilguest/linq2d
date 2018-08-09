using System;

namespace FilterTests
{
    public class ArrayFilterBase : IFilter<int>
    {
        protected int[,] Data { get; private set; }

        public void Initialize(int[,] data)
            => Data = data ?? throw new ArgumentNullException(nameof(data));

    }
}

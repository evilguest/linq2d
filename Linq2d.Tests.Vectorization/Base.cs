﻿namespace Linq2d.Tests.Vectorization
{
    [Collection("Vectorization")]
    public class Base
    {
        protected static void AssertVectorized(IVectorizable v, int step)
        {
            Assert.True(v.Vectorized, v.Report());
            Assert.Equal(step, v.MaxSuccessfulStep());
        }
    }
}

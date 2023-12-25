using Xunit.Sdk;
using Xunit;

namespace Linq2d.Tests.Vectorization
{
    public class Base
    {
        protected static void AssertVectorised(IVectorizable v, int step)
        {
            if (!v.Vectorized)
                throw new TrueException(v.Report(), false);
            Assert.Equal(step, v.MaxSuccessfulStep());
        }
    }
}

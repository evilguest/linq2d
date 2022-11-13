using Linq2d.Expressions;
using Xunit.Sdk;
using Xunit;
using Mono.Linq.Expressions;

namespace Linq2d.Tests.Vectorization
{
    public class Base
    {
        protected static void AssertVectorised(VectorizationResult vr, int step)
        {
            if (!vr.Success)
                throw new TrueException($"{vr.Reason} due to:\n{vr.BlockedBy.ToCSharpCode()}", false);
            Assert.Equal(step, vr.Step);
        }
    }
}

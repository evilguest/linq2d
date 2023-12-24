using Linq2d.Expressions;
using Xunit.Sdk;
using Xunit;
using Mono.Linq.Expressions;
using System.Linq;

namespace Linq2d.Tests.Vectorization
{
    public class Base
    {
        protected static void AssertVectorised(IVectorizable v, int step)
        {
            if (!v.Vectorized)
            {
                var report = string.Join(";\n", v.VectorizationResults.Where(vr => !vr.Success)
                    .Select(vr => $"{vr.Reason} with step {vr.Step} due to:\n{vr.BlockedBy.ToCSharpCode()}"));
                throw new TrueException(report, false);
            }
            Assert.Equal(step, v.VectorizationResults.Where(vr=>vr.Success).Select(vr=>vr.Step).Max());
        }
    }
}

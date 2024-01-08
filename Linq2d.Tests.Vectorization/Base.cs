namespace Linq2d.Tests.Vectorization
{
    [Collection("Vectorization")]
    public class Base
    {
        protected static void AssertVectorized(IVectorizable v, int step)
        {
            Assert.True(v.Vectorized, v.Report());
            var ourResult = v.VectorizationResults.Where(r => r.Step == step).First();
            Assert.True(step == v.MaxSuccessfulStep(), $"Expected step={step}, but {ourResult.Reason}:\n{ourResult.BlockedBy}");
        }
    }
}

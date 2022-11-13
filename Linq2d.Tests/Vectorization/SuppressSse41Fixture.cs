namespace Linq2d.Tests.Vectorization
{
    internal class SuppressSse41Fixture : VectorizationStateFixture
    {
        public SuppressSse41Fixture()
        {
            CodeGen.Intrinsics.Sse41.Suppress = true;
        }
        public override void Dispose()
        {
            CodeGen.Intrinsics.Sse41.Suppress = false;
            base.Dispose();
        }
    }
}
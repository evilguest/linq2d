namespace Linq2d.Tests.Vectorization
{
    internal class SuppressSsse3Fixture : VectorizationStateFixture
    {
        public SuppressSsse3Fixture()
        {
            CodeGen.Intrinsics.Ssse3.Suppress = true;
        }
        public override void Dispose()
        {
            CodeGen.Intrinsics.Ssse3.Suppress = false;
            base.Dispose();
        }
    }
}
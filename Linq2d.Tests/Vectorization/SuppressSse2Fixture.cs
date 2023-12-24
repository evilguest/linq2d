namespace Linq2d.Tests.Vectorization
{
    internal class SuppressSse2Fixture : VectorizationStateFixture
    {
        public SuppressSse2Fixture()
        {
            CodeGen.Intrinsics.Sse2.Suppress = true;
        }
        public override void Dispose()
        {
            CodeGen.Intrinsics.Sse2.Suppress = false;
            base.Dispose();
        }
    }
}
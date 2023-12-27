namespace Linq2d.Tests.Vectorization
{
    internal class SuppressVectorizationFixture: VectorizationStateFixture
    {
        public SuppressVectorizationFixture() {
            CodeGen.Intrinsics.Sse.Suppress = true;
        }

        public override void Dispose()
        {
            CodeGen.Intrinsics.Sse.Suppress = false;
            base.Dispose();
        }
    }
}

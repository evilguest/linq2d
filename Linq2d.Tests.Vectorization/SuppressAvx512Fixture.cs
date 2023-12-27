namespace Linq2d.Tests.Vectorization
{
    internal class SuppressAvx512Fixture : VectorizationStateFixture
    {
        public SuppressAvx512Fixture()
        {
            CodeGen.Intrinsics.Avx512F.Suppress = true;
        }

        public override void Dispose()
        {
            CodeGen.Intrinsics.Avx512F.Suppress = false;
            base.Dispose();
        }
    }
}

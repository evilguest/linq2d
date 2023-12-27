
namespace Linq2d.Tests.Vectorization
{
    internal class SuppressAvxFixture: VectorizationStateFixture
    {
        public SuppressAvxFixture() 
        {
            CodeGen.Intrinsics.Avx.Suppress = true;
        }

        public override void Dispose()
        {
            CodeGen.Intrinsics.Avx.Suppress = false;
            base.Dispose();
        }
    }
}

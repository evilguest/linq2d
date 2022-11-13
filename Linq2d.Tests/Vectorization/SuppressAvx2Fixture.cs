using System;

namespace Linq2d.Tests.Vectorization
{
    internal class SuppressAvx2Fixture: VectorizationStateFixture
    {
        public SuppressAvx2Fixture() {
            CodeGen.Intrinsics.Avx2.Suppress = true;
        }

        public override void Dispose()
        {
            CodeGen.Intrinsics.Avx2.Suppress = false;
            base.Dispose();
        }
    }
}

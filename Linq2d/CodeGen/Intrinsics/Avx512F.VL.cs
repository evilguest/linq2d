using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Base = System.Runtime.Intrinsics.X86.Avx512F;

namespace Linq2d.CodeGen.Intrinsics
{
    static partial class Avx512F
    {
        public static class VL
        {
            private static bool _suppress = false;
            public static bool Suppress
            {
                get => _suppress;
                set
                {
                    _suppress = value;
                    VectorData.Init();
                }
            }
            public static bool IsSupported { get => Base.IsSupported && !Suppress; }

        }
    }
}
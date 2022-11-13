using System.Runtime.Intrinsics;
using Base = System.Runtime.Intrinsics.X86.Ssse3;

namespace Linq2d.CodeGen.Intrinsics
{
    static class Ssse3
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
        public static bool IsSupported { get => Base.IsSupported && !Suppress && Sse2.IsSupported; }

        internal static Vector128<short> Negate(Vector128<short> arg) => Base.Sign(arg, arg);
    }
}

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

        #region vector-4
        private static readonly Vector128<byte> _shuffle = Vector128.Create(0, 1, 4, 5, 8, 9, 12, 13, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0);
        public static Vector64<short> ConvertToVector64Int16(Vector128<int> arg) => Base.Shuffle(arg.AsByte(), _shuffle).GetLower().AsInt16();
        public static Vector64<ushort> ConvertToVector64Int16(Vector128<uint> arg) => Base.Shuffle(arg.AsByte(), _shuffle).GetLower().AsUInt16();
        #endregion

        #region vector-8
        internal static Vector128<short> Negate(Vector128<short> arg) => Base.Sign(arg, Vector128<short>.AllBitsSet);
        #endregion
    }
}

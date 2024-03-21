using Linq2d.CodeGen.Fake;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Base = System.Runtime.Intrinsics.X86.Avx512BW;

namespace Linq2d.CodeGen.Intrinsics
{
    static class Avx512BW
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector512<byte> Add(Vector512<byte> left, Vector512<byte> right) => Base.Add(left, right);
        internal static Vector512<sbyte> Add(Vector512<sbyte> left, Vector512<sbyte> right) => Base.Add(left, right);
        internal static Vector512<short> Add(Vector512<short> left, Vector512<short> right) => Base.Add(left, right);
        internal static Vector512<ushort> Add(Vector512<ushort> left, Vector512<ushort> right) => Base.Add(left, right);

        internal static Vector512<byte> CompareEqual(Vector512<byte> left, Vector512<byte> right) => Base.CompareEqual(left, right);
        internal static Vector512<byte> CompareEqual(Vector512<sbyte> left, Vector512<sbyte> right) => Base.CompareEqual(left, right).AsByte();
        internal static Vector512<ushort> CompareEqual(Vector512<short> left, Vector512<short> right) => Base.CompareEqual(left, right).AsUInt16();
        internal static Vector512<ushort> CompareEqual(Vector512<ushort> left, Vector512<ushort> right) => Base.CompareEqual(left, right);
    }
}

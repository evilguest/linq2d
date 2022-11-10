using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Base = System.Runtime.Intrinsics.X86.Avx2;

namespace Linq2d.CodeGen.Intrinsics
{
    static class Avx2
    {

        public static bool IsSupported { get => Base.IsSupported; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector256<byte> Add(Vector256<byte> arg1, Vector256<byte> arg2) => Base.Add(arg1, arg2);
    }
}

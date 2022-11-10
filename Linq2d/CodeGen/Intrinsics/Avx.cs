using Linq2d.CodeGen.Fake;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Base = System.Runtime.Intrinsics.X86.Avx;

namespace Linq2d.CodeGen.Intrinsics
{
    static class Avx
    {

        public static bool IsSupported { get => Base.IsSupported; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector256<byte> LoadVector256(byte* address) => Base.LoadVector256(address);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector256<sbyte> LoadVector256(sbyte* address) => Base.LoadVector256(address);
        internal static unsafe void Store(byte* address, Vector256<byte> data) => Base.Store(address, data);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(sbyte* address, Vector256<sbyte> data) => Base.Store(address, data);
    }
}

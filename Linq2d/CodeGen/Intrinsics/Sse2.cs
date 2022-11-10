using Linq2d.CodeGen.Fake;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Base = System.Runtime.Intrinsics.X86.Sse2;

namespace Linq2d.CodeGen.Intrinsics
{
    static class Sse2
    {

        public static bool IsSupported { get => Base.IsSupported; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<long> Add(Vector128<long> arg1, Vector128<long> arg2) => Base.Add(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ulong> Add(Vector128<ulong> arg1, Vector128<ulong> arg2) => Base.Add(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<double> Add(Vector128<double> arg1, Vector128<double> arg2) => Base.Add(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<double> Divide(Vector128<double> arg1, Vector128<double> arg2) => Base.Divide(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector128<long> LoadVector128(long* address) => Base.LoadVector128(address);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector128<ulong> LoadVector128(ulong* address) => Base.LoadVector128(address);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector128<double> LoadVector128(double* address) => Base.LoadVector128(address);

        internal static Vector128<double> Max(Vector128<double> arg1, Vector128<double> arg2) => Base.Max(arg1, arg2);

        internal static Vector128<double> Min(Vector128<double> arg1, Vector128<double> arg2) => Base.Min(arg1, arg2);

        internal static Vector128<double> Multiply(Vector128<double> arg1, Vector128<double> arg2) => Base.Multiply(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<long> Or(Vector128<long> arg1, Vector128<long> arg2) => Base.Or(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ulong> Or(Vector128<ulong> arg1, Vector128<ulong> arg2) => Base.Or(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<long> And(Vector128<long> arg1, Vector128<long> arg2) => Base.And(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ulong> And(Vector128<ulong> arg1, Vector128<ulong> arg2) => Base.And(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(long* address, Vector128<long> data) => Base.Store(address, data);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(ulong* address, Vector128<ulong> data) => Base.Store(address, data);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(double* address, Vector128<double> data) => Base.Store(address, data);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<long> Subtract(Vector128<long> arg1, Vector128<long> arg2) => Base.Subtract(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ulong> Subtract(Vector128<ulong> arg1, Vector128<ulong> arg2) => Base.Subtract(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<double> Subtract(Vector128<double> arg1, Vector128<double> arg2) => Base.Subtract(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<long> Xor(Vector128<long> arg1, Vector128<long> arg2) => Base.Xor(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ulong> Xor(Vector128<ulong> arg1, Vector128<ulong> arg2) => Base.Xor(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<long> ShiftRightArithmetic(Vector128<long> arg1, byte arg2) => 
            Base.Or(Base.ShiftRightLogical(arg1, arg2), Base.ShiftLeftLogical(Vector128<long>.AllBitsSet, (byte)(sizeof(long) * 8 - arg2)));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ulong> ShiftRightLogical(Vector128<ulong> arg1, byte arg2) => Base.ShiftRightLogical(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<long> ShiftLeftLogical(Vector128<long> arg1, byte arg2) => Base.ShiftLeftLogical(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ulong> ShiftLeftLogical(Vector128<ulong> arg1, byte arg2) => Base.ShiftLeftLogical(arg1, arg2);
    }
}

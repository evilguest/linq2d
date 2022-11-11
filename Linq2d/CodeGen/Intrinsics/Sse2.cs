using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Base = System.Runtime.Intrinsics.X86.Sse2;

namespace Linq2d.CodeGen.Intrinsics
{
    static class Sse2
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
        public static bool IsSupported { get => Base.IsSupported && !Suppress && Sse.IsSupported; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<short> Add(Vector128<short> arg1, Vector128<short> arg2) => Base.Add(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ushort> Add(Vector128<ushort> arg1, Vector128<ushort> arg2) => Base.Add(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<int> Add(Vector128<int> arg1, Vector128<int> arg2) => Base.Add(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<uint> Add(Vector128<uint> arg1, Vector128<uint> arg2) => Base.Add(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<long> Add(Vector128<long> arg1, Vector128<long> arg2) => Base.Add(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ulong> Add(Vector128<ulong> arg1, Vector128<ulong> arg2) => Base.Add(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<double> Add(Vector128<double> arg1, Vector128<double> arg2) => Base.Add(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<double> Divide(Vector128<double> arg1, Vector128<double> arg2) => Base.Divide(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector128<short> LoadVector128(short* address) => Base.LoadVector128(address);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector128<ushort> LoadVector128(ushort* address) => Base.LoadVector128(address);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector128<int> LoadVector128(int* address) => Base.LoadVector128(address);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector128<uint> LoadVector128(uint* address) => Base.LoadVector128(address);
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
        internal static Vector128<short> Or(Vector128<short> arg1, Vector128<short> arg2) => Base.Or(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ushort> Or(Vector128<ushort> arg1, Vector128<ushort> arg2) => Base.Or(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<int> Or(Vector128<int> arg1, Vector128<int> arg2) => Base.Or(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<uint> Or(Vector128<uint> arg1, Vector128<uint> arg2) => Base.Or(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<long> Or(Vector128<long> arg1, Vector128<long> arg2) => Base.Or(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ulong> Or(Vector128<ulong> arg1, Vector128<ulong> arg2) => Base.Or(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<short> And(Vector128<short> arg1, Vector128<short> arg2) => Base.And(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ushort> And(Vector128<ushort> arg1, Vector128<ushort> arg2) => Base.And(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<int> And(Vector128<int> arg1, Vector128<int> arg2) => Base.And(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<uint> And(Vector128<uint> arg1, Vector128<uint> arg2) => Base.And(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<long> And(Vector128<long> arg1, Vector128<long> arg2) => Base.And(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ulong> And(Vector128<ulong> arg1, Vector128<ulong> arg2) => Base.And(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(long* address, Vector128<long> data) => Base.Store(address, data);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(ulong* address, Vector128<ulong> data) => Base.Store(address, data);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(int* address, Vector128<int> data) => Base.Store(address, data);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(uint* address, Vector128<uint> data) => Base.Store(address, data);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(ushort* address, Vector128<ushort> data) => Base.Store(address, data);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(short* address, Vector128<short> data) => Base.Store(address, data);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(double* address, Vector128<double> data) => Base.Store(address, data);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<short> Subtract(Vector128<short> arg1, Vector128<short> arg2) => Base.Subtract(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ushort> Subtract(Vector128<ushort> arg1, Vector128<ushort> arg2) => Base.Subtract(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<int> Subtract(Vector128<int> arg1, Vector128<int> arg2) => Base.Subtract(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<uint> Subtract(Vector128<uint> arg1, Vector128<uint> arg2) => Base.Subtract(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<long> Subtract(Vector128<long> arg1, Vector128<long> arg2) => Base.Subtract(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ulong> Subtract(Vector128<ulong> arg1, Vector128<ulong> arg2) => Base.Subtract(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<double> Subtract(Vector128<double> arg1, Vector128<double> arg2) => Base.Subtract(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<short> Xor(Vector128<short> arg1, Vector128<short> arg2) => Base.Xor(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ushort> Xor(Vector128<ushort> arg1, Vector128<ushort> arg2) => Base.Xor(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<int> Xor(Vector128<int> arg1, Vector128<int> arg2) => Base.Xor(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<uint> Xor(Vector128<uint> arg1, Vector128<uint> arg2) => Base.Xor(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<long> Xor(Vector128<long> arg1, Vector128<long> arg2) => Base.Xor(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ulong> Xor(Vector128<ulong> arg1, Vector128<ulong> arg2) => Base.Xor(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<short> ShiftRightArithmetic(Vector128<short> arg1, byte arg2) => Base.ShiftRightArithmetic(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<int> ShiftRightArithmetic(Vector128<int> arg1, byte arg2) => Base.ShiftRightArithmetic(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<long> ShiftRightArithmetic(Vector128<long> arg1, byte arg2) =>
            Base.Or(Base.ShiftRightLogical(arg1, arg2), Base.ShiftRightArithmetic(Base.And(arg1, Vector128.Create(long.MinValue)).As<long, int>(), arg2).As<int, long>());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ushort> ShiftRightLogical(Vector128<ushort> arg1, byte arg2) => Base.ShiftRightLogical(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<uint> ShiftRightLogical(Vector128<uint> arg1, byte arg2) => Base.ShiftRightLogical(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ulong> ShiftRightLogical(Vector128<ulong> arg1, byte arg2) => Base.ShiftRightLogical(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<short> ShiftLeftLogical(Vector128<short> arg1, byte arg2) => Base.ShiftLeftLogical(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ushort> ShiftLeftLogical(Vector128<ushort> arg1, byte arg2) => Base.ShiftLeftLogical(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<int> ShiftLeftLogical(Vector128<int> arg1, byte arg2) => Base.ShiftLeftLogical(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<uint> ShiftLeftLogical(Vector128<uint> arg1, byte arg2) => Base.ShiftLeftLogical(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<long> ShiftLeftLogical(Vector128<long> arg1, byte arg2) => Base.ShiftLeftLogical(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<ulong> ShiftLeftLogical(Vector128<ulong> arg1, byte arg2) => Base.ShiftLeftLogical(arg1, arg2);

        internal static Vector128<short> MultiplyLow(Vector128<short> arg1, Vector128<short> arg2) => Base.MultiplyLow(arg1, arg2);
        internal static Vector128<ushort> MultiplyLow(Vector128<ushort> arg1, Vector128<ushort> arg2) => Base.MultiplyLow(arg1, arg2);

        internal static Vector128<float> ConvertToVector128Single(Vector128<int> vector128) => Base.ConvertToVector128Single(vector128);

        internal static Vector128<int> ConvertToVector128Int32(Vector128<float> arg) => Base.ConvertToVector128Int32(arg);
    }
}

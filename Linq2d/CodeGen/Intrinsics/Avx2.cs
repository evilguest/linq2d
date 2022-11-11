using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Base = System.Runtime.Intrinsics.X86.Avx2;

namespace Linq2d.CodeGen.Intrinsics
{
    static class Avx2
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
        public static bool IsSupported { get => Base.IsSupported && !Suppress & Avx.IsSupported; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector256<byte> Add(Vector256<byte> arg1, Vector256<byte> arg2) => Base.Add(arg1, arg2);
        internal static Vector256<int> Add(Vector256<int> arg1, Vector256<int> arg2) => Base.Add(arg1, arg2);
        internal static Vector256<uint> Add(Vector256<uint> arg1, Vector256<uint> arg2) => Base.Add(arg1, arg2);
        internal static Vector256<long> Add(Vector256<long> arg1, Vector256<long> arg2) => Base.Add(arg1, arg2);
        internal static Vector256<ulong> Add(Vector256<ulong> arg1, Vector256<ulong> arg2) => Base.Add(arg1, arg2);

        internal static unsafe Vector256<long> ConvertToVector256Int64(byte* address) => Base.ConvertToVector256Int64(address);
        internal static unsafe Vector256<long> ConvertToVector256Int64(sbyte* address) => Base.ConvertToVector256Int64(address);
        internal static unsafe Vector256<long> ConvertToVector256Int64(short* address) => Base.ConvertToVector256Int64(address);
        internal static unsafe Vector256<long> ConvertToVector256Int64(ushort* address) => Base.ConvertToVector256Int64(address);
        internal static unsafe Vector256<long> ConvertToVector256Int64(int* address) => Base.ConvertToVector256Int64(address);
        internal static unsafe Vector256<long> ConvertToVector256Int64(uint* address) => Base.ConvertToVector256Int64(address);
        internal static unsafe Vector256<long> ConvertToVector256Int64(Vector128<int> arg) => Base.ConvertToVector256Int64(arg);
        internal static unsafe Vector256<long> ConvertToVector256Int64(Vector128<uint> arg) => Base.ConvertToVector256Int64(arg);

        internal static Vector256<int> Subtract(Vector256<int> arg1, Vector256<int> arg2) => Base.Subtract(arg1, arg2);
        internal static Vector256<uint> Subtract(Vector256<uint> arg1, Vector256<uint> arg2) => Base.Subtract(arg1, arg2);
        internal static Vector256<long> Subtract(Vector256<long> arg1, Vector256<long> arg2) => Base.Subtract(arg1, arg2);
        internal static Vector256<ulong> Subtract(Vector256<ulong> arg1, Vector256<ulong> arg2) => Base.Subtract(arg1, arg2);

        internal static Vector256<long> Xor(Vector256<long> arg1, Vector256<long> arg2) => Base.Xor(arg1, arg2);
        internal static Vector256<ulong> Xor(Vector256<ulong> arg1, Vector256<ulong> arg2) => Base.Xor(arg1, arg2);
        internal static Vector256<long> Or(Vector256<long> arg1, Vector256<long> arg2) => Base.Or(arg1, arg2);
        internal static Vector256<ulong> Or(Vector256<ulong> arg1, Vector256<ulong> arg2) => Base.Or(arg1, arg2);
        internal static Vector256<long> And(Vector256<long> arg1, Vector256<long> arg2) => Base.And(arg1, arg2);
        internal static Vector256<ulong> And(Vector256<ulong> arg1, Vector256<ulong> arg2) => Base.And(arg1, arg2);
        internal static Vector256<int> ShiftRightArithmetic(Vector256<int> arg1, byte arg2) => Base.ShiftRightArithmetic(arg1, arg2);

        internal static Vector256<long> ShiftRightArithmetic(Vector256<long> arg1, byte arg2) =>
            Base.Or(Base.ShiftRightLogical(arg1, arg2), Base.ShiftRightArithmetic(Base.And(arg1, Vector256.Create(long.MinValue)).As<long, int>(), arg2).As<int, long>());

        internal static Vector256<uint> ShiftRightLogical(Vector256<uint> arg1, byte arg2) => Base.ShiftRightLogical(arg1, arg2);
        internal static Vector256<ulong> ShiftRightLogical(Vector256<ulong> arg1, byte arg2) => Base.ShiftRightLogical(arg1, arg2);

        internal static Vector256<int> ShiftLeftLogical(Vector256<int> arg1, byte arg2) => Base.ShiftLeftLogical(arg1, arg2);
        internal static Vector256<uint> ShiftLeftLogical(Vector256<uint> arg1, byte arg2) => Base.ShiftLeftLogical(arg1, arg2);
        internal static Vector256<long> ShiftLeftLogical(Vector256<long> arg1, byte arg2) => Base.ShiftLeftLogical(arg1, arg2);
        internal static Vector256<ulong> ShiftLeftLogical(Vector256<ulong> arg1, byte arg2) => Base.ShiftLeftLogical(arg1, arg2);

        internal static Vector256<long> BlendVariable(Vector256<long> arg1, Vector256<long> arg2, Vector256<long> arg3) => Base.BlendVariable(arg1, arg2, arg3);
        internal static Vector256<ulong> BlendVariable(Vector256<ulong> arg1, Vector256<ulong> arg2, Vector256<ulong> arg3) => Base.BlendVariable(arg1, arg2, arg3);

        internal static unsafe Vector256<int> ConvertToVector256Int32(byte* address) => Base.ConvertToVector256Int32(address);
        internal static unsafe Vector256<int> ConvertToVector256Int32(sbyte* address) => Base.ConvertToVector256Int32(address);
        internal static unsafe Vector256<int> ConvertToVector256Int32(short* address) => Base.ConvertToVector256Int32(address);
        internal static unsafe Vector256<int> ConvertToVector256Int32(ushort* address) => Base.ConvertToVector256Int32(address);

        internal static Vector256<int> MultiplyLow(Vector256<int> arg1, Vector256<int> arg2) => Base.MultiplyLow(arg1, arg2);
        internal static Vector256<uint> MultiplyLow(Vector256<uint> arg1, Vector256<uint> arg2) => Base.MultiplyLow(arg1, arg2);

    }
}

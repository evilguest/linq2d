using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Base = System.Runtime.Intrinsics.X86.Avx2;

#pragma warning disable CA1857
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
        public static bool IsSupported { get => Base.IsSupported && !Suppress && Avx.IsSupported; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector256<byte> Add(Vector256<byte> left, Vector256<byte> right) => Base.Add(left, right);
        internal static Vector256<sbyte> Add(Vector256<sbyte> left, Vector256<sbyte> right) => Base.Add(left, right);
        internal static Vector256<short> Add(Vector256<short> left, Vector256<short> right) => Base.Add(left, right);
        internal static Vector256<ushort> Add(Vector256<ushort> left, Vector256<ushort> right) => Base.Add(left, right);
        internal static Vector256<int> Add(Vector256<int> left, Vector256<int> right) => Base.Add(left, right);
        internal static Vector256<uint> Add(Vector256<uint> left, Vector256<uint> right) => Base.Add(left, right);
        internal static Vector256<long> Add(Vector256<long> left, Vector256<long> right) => Base.Add(left, right);
        internal static Vector256<ulong> Add(Vector256<ulong> left, Vector256<ulong> right) => Base.Add(left, right);

        internal static unsafe Vector256<long> ConvertToVector256Int64(byte* address) => Base.ConvertToVector256Int64(address);
        internal static unsafe Vector256<long> ConvertToVector256Int64(sbyte* address) => Base.ConvertToVector256Int64(address);
        internal static unsafe Vector256<long> ConvertToVector256Int64(short* address) => Base.ConvertToVector256Int64(address);
        internal static unsafe Vector256<long> ConvertToVector256Int64(ushort* address) => Base.ConvertToVector256Int64(address);
        internal static unsafe Vector256<long> ConvertToVector256Int64(int* address) => Base.ConvertToVector256Int64(address);
        internal static unsafe Vector256<long> ConvertToVector256Int64(uint* address) => Base.ConvertToVector256Int64(address);
        internal static unsafe Vector256<long> ConvertToVector256Int64(Vector128<int> arg) => Base.ConvertToVector256Int64(arg);
        internal static unsafe Vector256<long> ConvertToVector256Int64(Vector128<uint> arg) => Base.ConvertToVector256Int64(arg);

        internal static Vector256<int> Subtract(Vector256<int> left, Vector256<int> right) => Base.Subtract(left, right);
        internal static Vector256<uint> Subtract(Vector256<uint> left, Vector256<uint> right) => Base.Subtract(left, right);
        internal static Vector256<long> Subtract(Vector256<long> left, Vector256<long> right) => Base.Subtract(left, right);
        internal static Vector256<ulong> Subtract(Vector256<ulong> left, Vector256<ulong> right) => Base.Subtract(left, right);

        internal static Vector256<long> Xor(Vector256<long> left, Vector256<long> right) => Base.Xor(left, right);
        internal static Vector256<ulong> Xor(Vector256<ulong> left, Vector256<ulong> right) => Base.Xor(left, right);
        internal static Vector256<long> Or(Vector256<long> left, Vector256<long> right) => Base.Or(left, right);
        internal static Vector256<ulong> Or(Vector256<ulong> left, Vector256<ulong> right) => Base.Or(left, right);
        internal static Vector256<long> And(Vector256<long> left, Vector256<long> right) => Base.And(left, right);
        internal static Vector256<ulong> And(Vector256<ulong> left, Vector256<ulong> right) => Base.And(left, right);
        internal static Vector256<int> ShiftRightArithmetic(Vector256<int> left, byte right) => Base.ShiftRightArithmetic(left, right);

        internal static Vector256<long> ShiftRightArithmetic(Vector256<long> left, byte right) =>
            Base.Or(Base.ShiftRightLogical(left, right), Base.ShiftRightArithmetic(Base.And(left, Vector256.Create(long.MinValue)).As<long, int>(), right).As<int, long>());

        internal static Vector256<uint> ShiftRightLogical(Vector256<uint> left, byte right) => Base.ShiftRightLogical(left, right);
        internal static Vector256<ulong> ShiftRightLogical(Vector256<ulong> left, byte right) => Base.ShiftRightLogical(left, right);

        internal static Vector256<int> ShiftLeftLogical(Vector256<int> left, byte right) => Base.ShiftLeftLogical(left, right);
        internal static Vector256<uint> ShiftLeftLogical(Vector256<uint> left, byte right) => Base.ShiftLeftLogical(left, right);
        internal static Vector256<long> ShiftLeftLogical(Vector256<long> left, byte right) => Base.ShiftLeftLogical(left, right);
        internal static Vector256<ulong> ShiftLeftLogical(Vector256<ulong> left, byte right) => Base.ShiftLeftLogical(left, right);

        internal static Vector256<long> BlendVariable(Vector256<long> left, Vector256<long> right, Vector256<ulong> mask) 
            => Base.BlendVariable(left, right, mask.AsInt64());
        internal static Vector256<ulong> BlendVariable(Vector256<ulong> left, Vector256<ulong> right, Vector256<ulong> mask) 
            => Base.BlendVariable(left, right, mask);

        internal static Vector256<byte> CompareEqual(Vector256<byte> left, Vector256<byte> right) => Base.CompareEqual(left, right);
        internal static Vector256<byte> CompareEqual(Vector256<sbyte> left, Vector256<sbyte> right) => Base.CompareEqual(left, right).AsByte();
        internal static Vector256<ushort> CompareEqual(Vector256<short> left, Vector256<short> right) => Base.CompareEqual(left, right).AsUInt16();
        internal static Vector256<ushort> CompareEqual(Vector256<ushort> left, Vector256<ushort> right) => Base.CompareEqual(left, right);
        internal static Vector256<uint> CompareEqual(Vector256<int> left, Vector256<int> right) => Base.CompareEqual(left, right).AsUInt32();
        internal static Vector256<uint> CompareEqual(Vector256<uint> left, Vector256<uint> right) => Base.CompareEqual(left, right);
        internal static Vector256<ulong> CompareEqual(Vector256<long> left, Vector256<long> right) => Base.CompareEqual(left, right).AsUInt64();
        internal static Vector256<ulong> CompareEqual(Vector256<ulong> left, Vector256<ulong> right) => Base.CompareEqual(left, right);
        internal static unsafe Vector256<int> ConvertToVector256Int32(byte* address) => Base.ConvertToVector256Int32(address);
        internal static unsafe Vector256<int> ConvertToVector256Int32(sbyte* address) => Base.ConvertToVector256Int32(address);
        internal static unsafe Vector256<int> ConvertToVector256Int32(short* address) => Base.ConvertToVector256Int32(address);
        internal static unsafe Vector256<int> ConvertToVector256Int32(ushort* address) => Base.ConvertToVector256Int32(address);
//        private static readonly Vector256<byte> _shuffle = Vector256.Create(2, 3, 6, 7, 10, 11, 14, 15, 18, 19, 22, 23, 26, 27, 30, 31, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0);

        private static readonly Vector128<byte> _shuffle128 = Vector128.Create(0, 1, 4, 5, 8, 9, 12, 13, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0);
        internal static Vector128<short> ConvertToVector128Int16(Vector256<int> arg)
                => Vector128.Create(Base.Shuffle(arg.GetLower().AsByte(), _shuffle128).GetLower(),
                                    Base.Shuffle(arg.GetUpper().AsByte(), _shuffle128).GetLower()).AsInt16();
        internal static Vector128<ushort> ConvertToVector128Int16(Vector256<uint> arg)
            => Vector128.Create(Base.Shuffle(arg.GetLower().AsByte(), _shuffle128).GetLower(),
                                Base.Shuffle(arg.GetUpper().AsByte(), _shuffle128).GetLower()).AsUInt16();
        //internal static Vector128<ushort> ConvertToVector128Int16(Vector256<uint> arg) => Base.Shuffle(arg.AsByte(), _shuffle).GetLower().AsUInt16();
        internal static Vector256<int> MultiplyLow(Vector256<int> left, Vector256<int> right) => Base.MultiplyLow(left, right);
        internal static Vector256<uint> MultiplyLow(Vector256<uint> left, Vector256<uint> right) => Base.MultiplyLow(left, right);
        internal static Vector256<sbyte> Negate(Vector256<sbyte> arg) => Base.Sign(arg, Vector256<sbyte>.AllBitsSet);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector256<int> Negate(Vector256<int> arg) => Base.Sign(arg, Vector256<int>.AllBitsSet);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<short> Negate(Vector256<short> a) => Base.Sign(a, Vector256<short>.AllBitsSet);

        internal static unsafe Vector256<short> ConvertToVector256Int16(byte* address) => Base.ConvertToVector256Int16(address);
        internal static unsafe Vector256<short> ConvertToVector256Int16(sbyte* address) => Base.ConvertToVector256Int16(address);
    }
}

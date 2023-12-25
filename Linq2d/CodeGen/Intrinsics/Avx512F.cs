using System.Runtime.Intrinsics;
using Base = System.Runtime.Intrinsics.X86.Avx512F;

#pragma warning disable CA1857

namespace Linq2d.CodeGen.Intrinsics
{
    static partial class Avx512F
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

        #region Vector-8
        internal static Vector512<double> Add(Vector512<double> left, Vector512<double> right) => Base.Add(left, right);
        internal static Vector512<long> Add(Vector512<long> left, Vector512<long> right) => Base.Add(left, right);
        internal static Vector512<ulong> Add(Vector512<ulong> left, Vector512<ulong> right) => Base.Add(left, right);
        internal static unsafe Vector512<long> ConvertToVector512Int64(Vector128<short> arg) => Base.ConvertToVector512Int64(arg);
        internal static unsafe Vector512<long> ConvertToVector512Int64(Vector128<ushort> arg) => Base.ConvertToVector512Int64(arg);
        internal static unsafe Vector512<long> ConvertToVector512Int64(Vector256<int> arg) => Base.ConvertToVector512Int64(arg);
        internal static unsafe Vector512<long> ConvertToVector512Int64(Vector256<uint> arg) => Base.ConvertToVector512Int64(arg);
        internal static Vector512<long> Subtract(Vector512<long> left, Vector512<long> right) => Base.Subtract(left, right);
        internal static Vector512<ulong> Subtract(Vector512<ulong> left, Vector512<ulong> right) => Base.Subtract(left, right);
        internal static Vector512<double> Subtract(Vector512<double> left, Vector512<double> right) => Base.Subtract(left, right);

        internal static Vector512<long> Xor(Vector512<long> left, Vector512<long> right) => Base.Xor(left, right);
        internal static Vector512<ulong> Xor(Vector512<ulong> left, Vector512<ulong> right) => Base.Xor(left, right);
        internal static Vector512<long> Or(Vector512<long> left, Vector512<long> right) => Base.Or(left, right);
        internal static Vector512<ulong> Or(Vector512<ulong> left, Vector512<ulong> right) => Base.Or(left, right);
        internal static Vector512<long> And(Vector512<long> left, Vector512<long> right) => Base.And(left, right);
        internal static Vector512<ulong> And(Vector512<ulong> left, Vector512<ulong> right) => Base.And(left, right);
        internal static Vector512<long> ShiftRightArithmetic(Vector512<long> left, byte right) => Base.ShiftRightArithmetic(left, right);
        internal static Vector512<ulong> ShiftRightLogical(Vector512<ulong> left, byte right) => Base.ShiftRightLogical(left, right);
        internal static Vector512<long> ShiftLeftLogical(Vector512<long> left, byte right) => Base.ShiftLeftLogical(left, right);
        internal static Vector512<ulong> ShiftLeftLogical(Vector512<ulong> left, byte right) => Base.ShiftLeftLogical(left, right);
        internal static Vector512<long> BlendVariable(Vector512<long> left, Vector512<long> right, Vector512<ulong> mask)
            => Base.BlendVariable(left, right, mask.AsInt64());
        internal static Vector512<ulong> BlendVariable(Vector512<ulong> left, Vector512<ulong> right, Vector512<ulong> mask)
            => Base.BlendVariable(left, right, mask);
        internal static Vector512<ulong> CompareEqual(Vector512<long> left, Vector512<long> right) => Base.CompareEqual(left, right).AsUInt64();
        internal static Vector512<ulong> CompareEqual(Vector512<ulong> left, Vector512<ulong> right) => Base.CompareEqual(left, right);
        #endregion
        #region Vector-16
        internal static Vector512<float> Add(Vector512<float> left, Vector512<float> right) => Base.Add(left, right);
        internal static Vector512<int> Add(Vector512<int> left, Vector512<int> right) => Base.Add(left, right);
        internal static Vector512<uint> Add(Vector512<uint> left, Vector512<uint> right) => Base.Add(left, right);
        internal static Vector512<int> Subtract(Vector512<int> left, Vector512<int> right) => Base.Subtract(left, right);
        internal static Vector512<uint> Subtract(Vector512<uint> left, Vector512<uint> right) => Base.Subtract(left, right);
        internal static Vector512<int> ShiftRightArithmetic(Vector512<int> left, byte right) => Base.ShiftRightArithmetic(left, right);
        internal static Vector512<uint> ShiftRightLogical(Vector512<uint> left, byte right) => Base.ShiftRightLogical(left, right);
        internal static Vector512<int> ShiftLeftLogical(Vector512<int> left, byte right) => Base.ShiftLeftLogical(left, right);
        internal static Vector512<uint> ShiftLeftLogical(Vector512<uint> left, byte right) => Base.ShiftLeftLogical(left, right);
        internal static Vector512<uint> CompareEqual(Vector512<int> left, Vector512<int> right) => Base.CompareEqual(left, right).AsUInt32();
        internal static Vector512<uint> CompareEqual(Vector512<uint> left, Vector512<uint> right) => Base.CompareEqual(left, right);
        internal static Vector256<ushort> ConvertToVector256UInt16(Vector512<uint> arg) => Base.ConvertToVector256UInt16(arg);
        internal static Vector512<int> MultiplyLow(Vector512<int> left, Vector512<int> right) => Base.MultiplyLow(left, right);
        internal static Vector512<uint> MultiplyLow(Vector512<uint> left, Vector512<uint> right) => Base.MultiplyLow(left, right);
        #endregion 


        //internal static Vector512<uint> ShiftRightArithmetic(Vector512<uint> left, byte right) => Base.ShiftRightArithmetic(left, right);
        //internal static Vector512<ulong> ShiftRightArithmetic(Vector512<ulong> left, byte right) => Base.ShiftRightArithmetic(left, right);
        //internal static Vector256<long> ShiftRightArithmetic(Vector256<long> left, byte right) => Base.ShiftRightArithmetic(left, right);




        //internal static unsafe Vector512<int> ConvertToVector512Int32(byte* address) => Base.ConvertToVector512Int32(address);
        //internal static unsafe Vector512<int> ConvertToVector512Int32(sbyte* address) => Base.ConvertToVector512Int32(address);
        //internal static unsafe Vector512<int> ConvertToVector512Int32(short* address) => Base.ConvertToVector512Int32(address);
        //internal static unsafe Vector512<int> ConvertToVector512Int32(ushort* address) => Base.ConvertToVector512Int32(address);
        //internal static Vector512<sbyte> Negate(Vector512<sbyte> arg) => Base.Sign(arg, Vector512<sbyte>.AllBitsSet);

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //internal static Vector512<int> Negate(Vector512<int> arg) => Base.Sign(arg, Vector512<int>.AllBitsSet);
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector512<short> Negate(Vector512<short> a) => Base.Sign(a, Vector512<short>.AllBitsSet);

    }
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Base = System.Runtime.Intrinsics.X86.Sse2;

#pragma warning disable CA1857

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

        #region Vector-16

        
        internal static unsafe Vector128<byte> LoadVector128(byte* address) => Base.LoadVector128(address);
        
        internal static unsafe Vector128<sbyte> LoadVector128(sbyte* address) => Base.LoadVector128(address);
        internal static unsafe void Store(byte* address, Vector128<byte> data) => Base.Store(address, data);
        internal static unsafe void Store(sbyte* address, Vector128<sbyte> data) => Base.Store(address, data);
//        internal static Vector128<byte> Create(bool arg) => Vector128.Create(arg ? byte.MaxValue : byte.MinValue);
        #endregion

        #region Vector-8
        internal static unsafe void Store(bool* address, Vector128<ushort> data) =>
            Vector64.Store(Vector64.Create(
                (byte)data[0], (byte)data[1],
                (byte)data[2], (byte)data[3],
                (byte)data[4], (byte)data[5],
                (byte)data[6], (byte)data[7]) & Vector64.Create((byte)1), (byte*)address);
        //internal static Vector128<ushort> Create(bool arg) => Vector128.Create(arg ? ushort.MaxValue : ushort.MinValue);

        internal static Vector128<short> Add(Vector128<short> left, Vector128<short> right) => Base.Add(left, right);
        
        internal static Vector128<ushort> Add(Vector128<ushort> left, Vector128<ushort> right) => Base.Add(left, right);
        
        internal static unsafe Vector128<short> LoadVector128(short* address) => Base.LoadVector128(address);
        
        internal static unsafe Vector128<ushort> LoadVector128(ushort* address) => Base.LoadVector128(address);
        
        internal static Vector128<short> Or(Vector128<short> left, Vector128<short> right) => Base.Or(left, right);

        
        internal static Vector128<ushort> Or(Vector128<ushort> left, Vector128<ushort> right) => Base.Or(left, right);
        
        internal static Vector128<short> And(Vector128<short> left, Vector128<short> right) => Base.And(left, right);
        
        internal static Vector128<ushort> And(Vector128<ushort> left, Vector128<ushort> right) => Base.And(left, right);
        internal static Vector128<ushort> Not(Vector128<ushort> v) => Base.AndNot(v, Vector128.Create(ushort.MaxValue));

        internal static unsafe void Store(ushort* address, Vector128<ushort> data) => Base.Store(address, data);
        
        internal static unsafe void Store(short* address, Vector128<short> data) => Base.Store(address, data);
        
        internal static Vector128<short> Subtract(Vector128<short> left, Vector128<short> right) => Base.Subtract(left, right);
        
        internal static Vector128<ushort> Subtract(Vector128<ushort> left, Vector128<ushort> right) => Base.Subtract(left, right);
        internal static Vector128<short> Xor(Vector128<short> left, Vector128<short> right) => Base.Xor(left, right);

        internal static Vector128<ushort> Xor(Vector128<ushort> left, Vector128<ushort> right) => Base.Xor(left, right);
        internal static Vector128<short> ShiftRightArithmetic(Vector128<short> left, byte right) => Base.ShiftRightArithmetic(left, right);
        internal static Vector128<ushort> ShiftRightLogical(Vector128<ushort> left, byte right) => Base.ShiftRightLogical(left, right);
        internal static Vector128<short> ShiftLeftLogical(Vector128<short> left, byte right) => Base.ShiftLeftLogical(left, right);
        internal static Vector128<ushort> ShiftLeftLogical(Vector128<ushort> left, byte right) => Base.ShiftLeftLogical(left, right);
        internal static Vector128<short> MultiplyLow(Vector128<short> left, Vector128<short> right) => Base.MultiplyLow(left, right);
        internal static Vector128<ushort> MultiplyLow(Vector128<ushort> left, Vector128<ushort> right) => Base.MultiplyLow(left, right);
        internal static Vector128<ushort> CompareEqual(Vector128<short> left, Vector128<short> right) => Base.CompareEqual(left, right).AsUInt16();
        internal static Vector128<ushort> DoCompareEqual(Vector128<ushort> left, Vector128<ushort> right) => Base.CompareEqual(left, right);
        #endregion

        #region Vector-4

        internal static unsafe Vector64<short> LoadVector64(short* address) => Vector64.Create(*(long*)address).AsInt16();
        internal static unsafe Vector64<ushort> LoadVector64(ushort* address) => Vector64.Create(*(long*)address).AsUInt16();
        internal static unsafe void Store(short* destination, Vector64<short> data) => data.Store(destination);
        internal static unsafe void Store(ushort* address, Vector64<ushort> data) => data.Store(address);
        internal static Vector128<int> ConvertToVector128Int32(Vector64<short> v) => Vector128.Create(v[0], v[1], v[2], v[3]);
        internal static Vector128<int> ConvertToVector128Int32(Vector64<ushort> v) => Vector128.Create(v[0], v[1], v[2], v[3]);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector64<short> ConvertToVector64Int16(Vector128<int> v)
        {
            var t = v.AsInt16();
            return Vector64.Create(t[0], t[2], t[4], t[6]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector64<ushort> ConvertToVector64UInt16(Vector128<int> v)
        {
            var t = v.AsUInt16();
            return Vector64.Create(t[0], t[2], t[4], t[6]);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector64<ushort> ConvertToVector64UInt16(Vector128<uint> v)
        {
            var t = v.AsUInt16();
            return Vector64.Create(t[0], t[2], t[4], t[6]);
        }

        internal static Vector128<int> Add(Vector128<int> left, Vector128<int> right) => Base.Add(left, right);
        
        internal static Vector128<uint> Add(Vector128<uint> left, Vector128<uint> right) => Base.Add(left, right);

        internal static unsafe Vector128<int> LoadVector128(int* address) => Base.LoadVector128(address);
        
        internal static unsafe Vector128<uint> LoadVector128(uint* address) => Base.LoadVector128(address);

        internal static Vector128<int> Or(Vector128<int> left, Vector128<int> right) => Base.Or(left, right);

        
        internal static Vector128<uint> Or(Vector128<uint> left, Vector128<uint> right) => Base.Or(left, right);
        
        internal static Vector128<int> And(Vector128<int> left, Vector128<int> right) => Base.And(left, right);
        
        internal static Vector128<uint> And(Vector128<uint> left, Vector128<uint> right) => Base.And(left, right);
        
        internal static unsafe void Store(int* address, Vector128<int> data) => Base.Store(address, data);
        
        internal static unsafe void Store(uint* address, Vector128<uint> data) => Base.Store(address, data);
        internal static Vector128<int> Subtract(Vector128<int> left, Vector128<int> right) => Base.Subtract(left, right);
        internal static Vector128<uint> Subtract(Vector128<uint> left, Vector128<uint> right) => Base.Subtract(left, right);

        internal static Vector128<int> Negate(Vector128<int> vector) => Base.Subtract(Vector128.Create(0), vector);

        internal static Vector128<int> Xor(Vector128<int> left, Vector128<int> right) => Base.Xor(left, right);
        internal static Vector128<uint> Xor(Vector128<uint> left, Vector128<uint> right) => Base.Xor(left, right);
        internal static Vector128<int> ShiftRightArithmetic(Vector128<int> left, byte right) => Base.ShiftRightArithmetic(left, right);
        internal static Vector128<uint> ShiftRightLogical(Vector128<uint> left, byte right) => Base.ShiftRightLogical(left, right);

        internal static Vector128<int> ShiftLeftLogical(Vector128<int> left, byte right) => Base.ShiftLeftLogical(left, right);

        internal static Vector128<uint> ShiftLeftLogical(Vector128<uint> left, byte right) => Base.ShiftLeftLogical(left, right);



        internal static Vector128<float> ConvertToVector128Single(Vector128<int> vector128) => Base.ConvertToVector128Single(vector128);

        internal static Vector128<int> ConvertToVector128Int32WithTruncation(Vector128<float> arg) => Base.ConvertToVector128Int32WithTruncation(arg);

        internal static Vector128<uint> CompareEqual(Vector128<int> left, Vector128<int> right) => Base.CompareEqual(left, right).AsUInt32();
        internal static Vector128<uint> CompareEqual(Vector128<uint> left, Vector128<uint> right) => Base.CompareEqual(left, right);
        #endregion

        #region Vector-2

        internal static unsafe void Store(bool* address, Vector128<ulong> data) =>
            *(ushort*)address = (ushort)((data[1] << 8 | data[0]) & 0x101);
        internal static Vector128<long> Add(Vector128<long> left, Vector128<long> right) => Base.Add(left, right);
        
        internal static Vector128<ulong> Add(Vector128<ulong> left, Vector128<ulong> right) => Base.Add(left, right);
        
        internal static Vector128<double> Add(Vector128<double> left, Vector128<double> right) => Base.Add(left, right);

        
        internal static Vector128<double> Divide(Vector128<double> left, Vector128<double> right) => Base.Divide(left, right);
        
        internal static unsafe Vector128<long> LoadVector128(long* address) => Base.LoadVector128(address);
        
        internal static unsafe Vector128<ulong> LoadVector128(ulong* address) => Base.LoadVector128(address);
        
        internal static unsafe Vector128<double> LoadVector128(double* address) => Base.LoadVector128(address);

        internal static Vector128<double> Max(Vector128<double> left, Vector128<double> right) => Base.Max(left, right);

        internal static Vector128<double> Min(Vector128<double> left, Vector128<double> right) => Base.Min(left, right);

        internal static Vector128<double> Multiply(Vector128<double> left, Vector128<double> right) => Base.Multiply(left, right);
        
        internal static Vector128<long> Or(Vector128<long> left, Vector128<long> right) => Base.Or(left, right);

        
        internal static Vector128<ulong> Or(Vector128<ulong> left, Vector128<ulong> right) => Base.Or(left, right);
        
        internal static Vector128<long> And(Vector128<long> left, Vector128<long> right) => Base.And(left, right);
        
        internal static Vector128<ulong> And(Vector128<ulong> left, Vector128<ulong> right) => Base.And(left, right);
        
        internal static unsafe void Store(long* address, Vector128<long> data) => Base.Store(address, data);
        
        internal static unsafe void Store(ulong* address, Vector128<ulong> data) => Base.Store(address, data);
        
        internal static unsafe void Store(double* address, Vector128<double> data) => Base.Store(address, data);
        internal static Vector128<ulong> CompareEqual(Vector128<long> left, Vector128<long> right) => Base.CompareEqual(left.AsDouble(), right.AsDouble()).AsUInt64();
        internal static Vector128<ulong> CompareEqual(Vector128<ulong> left, Vector128<ulong> right) => Base.CompareEqual(left.AsDouble(), right.AsDouble()).AsUInt64();
        internal static Vector128<ulong> CompareEqual(Vector128<double> left, Vector128<double> right) => Base.CompareEqual(left, right).AsUInt64();
        internal static Vector128<long> Subtract(Vector128<long> left, Vector128<long> right) => Base.Subtract(left, right);

        internal static Vector128<ulong> Subtract(Vector128<ulong> left, Vector128<ulong> right) => Base.Subtract(left, right);

        internal static Vector128<double> Subtract(Vector128<double> left, Vector128<double> right) => Base.Subtract(left, right);
        internal static Vector128<long> Xor(Vector128<long> left, Vector128<long> right) => Base.Xor(left, right);

        internal static Vector128<ulong> Xor(Vector128<ulong> left, Vector128<ulong> right) => Base.Xor(left, right);
        internal static Vector128<long> ShiftRightArithmetic(Vector128<long> left, byte right) =>
            Base.Or(Base.ShiftRightLogical(left, right), Base.ShiftRightArithmetic(Base.And(left, Vector128.Create(long.MinValue)).As<long, int>(), right).As<int, long>());
        internal static Vector128<ulong> ShiftRightLogical(Vector128<ulong> left, byte right) => Base.ShiftRightLogical(left, right);
        internal static Vector128<long> ShiftLeftLogical(Vector128<long> left, byte right) => Base.ShiftLeftLogical(left, right);

        internal static Vector128<ulong> ShiftLeftLogical(Vector128<ulong> left, byte right) => Base.ShiftLeftLogical(left, right);


        #endregion

    }
}

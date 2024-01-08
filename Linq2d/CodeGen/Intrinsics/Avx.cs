using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Base = System.Runtime.Intrinsics.X86.Avx;

#pragma warning disable CA1857

namespace Linq2d.CodeGen.Intrinsics
{
    static class Avx
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
        public static bool IsSupported { get => Base.IsSupported && !Suppress && Sse41.IsSupported; }

        internal static Vector256<float> Add(Vector256<float> left, Vector256<float> right) => Base.Add(left, right);
        internal static Vector256<double> Add(Vector256<double> left, Vector256<double> right) => Base.Add(left, right);

        internal static Vector256<double> BlendVariable(Vector256<double> left, Vector256<double> right, Vector256<ulong> mask)
            => Base.BlendVariable(left, right, mask.AsDouble());
        internal static Vector256<long> BlendVariable(Vector256<long> left, Vector256<long> right, Vector256<ulong> mask)
            => Base.BlendVariable(left.AsDouble(), right.AsDouble(), mask.AsDouble()).AsInt64();
        internal static Vector256<ulong> BlendVariable(Vector256<ulong> left, Vector256<ulong> right, Vector256<ulong> mask)
            => Base.BlendVariable(left.AsDouble(), right.AsDouble(), mask.AsDouble()).AsUInt64();
        internal static Vector256<float> BlendVariable(Vector256<float> left, Vector256<float> right, Vector256<uint> mask)
            => Base.BlendVariable(left, right, mask.AsSingle());
        internal static Vector256<int> BlendVariable(Vector256<int> left, Vector256<int> right, Vector256<uint> mask)
            => Base.BlendVariable(left.AsSingle(), right.AsSingle(), mask.AsSingle()).AsInt32();
        internal static Vector256<uint> BlendVariable(Vector256<uint> left, Vector256<uint> right, Vector256<uint> mask)
            => Base.BlendVariable(left.AsSingle(), right.AsSingle(), mask.AsSingle()).AsUInt32();

        //internal static Vector256<byte> Compare(Vector256<float> l, Vector256<float> r, System.Runtime.Intrinsics.X86.FloatComparisonMode orderedLessThanSignaling)
        //    => Base.Compare(l, r, orderedLessThanSignaling).AsByte();

        //internal static Vector256<uint> CompareGreaterThan(Vector256<float> l, Vector256<float> r)
        //    => Base.CompareGreaterThan(l, r).AsUInt32();
        internal static Vector256<uint> CompareLessThan(Vector256<float> l, Vector256<float> r)
            => Base.CompareLessThan(l, r).AsUInt32();
        //internal static Vector256<uint> CompareGreaterThanOrEqual(Vector256<float> l, Vector256<float> r)
        //    => Base.CompareGreaterThanOrEqual(l, r).AsUInt32();
        internal static Vector256<uint> CompareLessThanOrEqual(Vector256<float> l, Vector256<float> r)
            => Base.CompareLessThanOrEqual(l, r).AsUInt32();

        //internal static Vector256<byte> Compare(Vector256<double> l, Vector256<double> r, System.Runtime.Intrinsics.X86.FloatComparisonMode orderedLessThanSignaling) 
        //    => Base.Compare(l, r, orderedLessThanSignaling).AsByte();

        internal static Vector256<uint> CompareEqual(Vector256<float> left, Vector256<float> right) => Base.CompareEqual(left, right).AsUInt32();
        internal static Vector256<ulong> CompareEqual(Vector256<double> left, Vector256<double> right) => Base.CompareEqual(left, right).AsUInt64();
        internal static Vector256<ulong> CompareEqual(Vector256<long> left, Vector256<long> right) => Base.CompareEqual(left.AsDouble(), right.AsDouble()).AsUInt64();
        internal static Vector128<int> ConvertToVector128Int32WithTruncation(Vector256<double> arg) => Base.ConvertToVector128Int32WithTruncation(arg);

        internal static Vector128<float> ConvertToVector128Single(Vector256<double> arg) => Base.ConvertToVector128Single(arg);

        internal static Vector256<double> ConvertToVector256Double(Vector128<float> arg) => Base.ConvertToVector256Double(arg);
        internal static Vector256<double> ConvertToVector256Double(Vector128<int> arg) => Base.ConvertToVector256Double(arg);

        internal static Vector256<int> ConvertToVector256Int32WithTruncation(Vector256<float> arg) => Base.ConvertToVector256Int32WithTruncation(arg);

        internal static Vector256<float> ConvertToVector256Single(Vector256<int> arg) => Base.ConvertToVector256Single(arg);
        internal static unsafe Vector256<float> ConvertToVector256Single(int* address) => Base.ConvertToVector256Single(*(Vector256<int>*)address);

        internal static Vector256<float> Divide(Vector256<float> left, Vector256<float> right) => Base.Divide(left, right);
        internal static Vector256<double> Divide(Vector256<double> left, Vector256<double> right) => Base.Divide(left, right);

        internal static unsafe Vector256<byte> LoadVector256(byte* address) => Base.LoadVector256(address);
        internal static unsafe Vector256<sbyte> LoadVector256(sbyte* address) => Base.LoadVector256(address);
        internal static unsafe Vector256<short> LoadVector256(short* address) => Base.LoadVector256(address);
        internal static unsafe Vector256<ushort> LoadVector256(ushort* address) => Base.LoadVector256(address);
        internal static unsafe Vector256<int> LoadVector256(int* address) => Base.LoadVector256(address);
        internal static unsafe Vector256<uint> LoadVector256(uint* address) => Base.LoadVector256(address);
        internal static unsafe Vector256<long> LoadVector256(long* address) => Base.LoadVector256(address);
        internal static unsafe Vector256<ulong> LoadVector256(ulong* address) => Base.LoadVector256(address);
        internal static unsafe Vector256<float> LoadVector256(float* address) => Base.LoadVector256(address);
        internal static unsafe Vector256<double> LoadVector256(double* address) => Base.LoadVector256(address);
        internal static Vector256<double> Max(Vector256<double> left, Vector256<double> right) => Base.Max(left, right);
        internal static Vector256<double> Min(Vector256<double> left, Vector256<double> right) => Base.Min(left, right);
        internal static Vector256<float> Multiply(Vector256<float> left, Vector256<float> right) => Base.Multiply(left, right);
        internal static Vector256<double> Multiply(Vector256<double> left, Vector256<double> right) => Base.Multiply(left, right);

        internal static Vector256<float> Negate(Vector256<float> a) => Base.Subtract(Vector256<float>.Zero, a);

        internal static Vector256<double> Sqrt(Vector256<double> arg) => Base.Sqrt(arg);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(bool* address, Vector256<byte> data) => Base.Store((byte*)address, Base.And(data.AsDouble(), Vector256.Create((byte)1).AsDouble()).AsByte());
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(byte* address, Vector256<byte> data) => Base.Store(address, data);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(sbyte* address, Vector256<sbyte> data) => Base.Store(address, data);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(short* address, Vector256<short> data) => Base.Store(address, data);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(ushort* address, Vector256<ushort> data) => Base.Store(address, data);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(int* address, Vector256<int> data) => Base.Store(address, data);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(uint* address, Vector256<uint> data) => Base.Store(address, data);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(long* address, Vector256<long> data) => Base.Store(address, data);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(ulong* address, Vector256<ulong> data) => Base.Store(address, data);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(float* address, Vector256<float> data) => Base.Store(address, data);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(double* address, Vector256<double> data) => Base.Store(address, data);
        internal static Vector256<float> Subtract(Vector256<float> left, Vector256<float> right) => Base.Subtract(left, right);

        internal static Vector256<double> Subtract(Vector256<double> left, Vector256<double> right) => Base.Subtract(left, right);
        public static Vector256<ulong> LessThan(Vector256<double> left, Vector256<double> right) => Base.CompareLessThan(left, right).AsUInt64();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector256<ulong> GreaterThan(Vector256<double> left, Vector256<double> right) => Base.CompareGreaterThan(left, right).AsUInt64();
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<ulong> LessThanOrEqual(Vector256<double> left, Vector256<double> right) => Base.CompareLessThanOrEqual(left, right).AsUInt64();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector256<ulong> GreaterThanOrEqual(Vector256<double> left, Vector256<double> right) => Base.CompareGreaterThanOrEqual(left, right).AsUInt64();
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static Vector256<double> ConvertToVector256Double(byte* address) => Base.ConvertToVector256Double(Sse41.ConvertToVector128Int32(address));
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector256<double> ConvertToVector256Double(Vector256<long> data) => Vector256.Create((double)data.GetElement(0), data.GetElement(1), data.GetElement(2), data.GetElement(3));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static Vector256<double> ConvertToVector256Double(sbyte* address) => Base.ConvertToVector256Double(Sse41.ConvertToVector128Int32(address));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static Vector256<double> ConvertToVector256Double(short* address) => Base.ConvertToVector256Double(Sse41.ConvertToVector128Int32(address));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static Vector256<double> ConvertToVector256Double(ushort* address) => Base.ConvertToVector256Double(Sse41.ConvertToVector128Int32(address));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static Vector256<double> ConvertToVector256Double(int* address) => Base.ConvertToVector256Double(Sse2.LoadVector128(address));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> Negate(Vector256<double> a) => Subtract(Vector256<double>.Zero, a);
    }
}

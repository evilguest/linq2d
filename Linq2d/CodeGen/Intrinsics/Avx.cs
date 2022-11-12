using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Base = System.Runtime.Intrinsics.X86.Avx;

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

        internal static Vector256<float> Add(Vector256<float> arg1, Vector256<float> arg2) => Base.Add(arg1, arg2);
        internal static Vector256<double> Add(Vector256<double> arg1, Vector256<double> arg2) => Base.Add(arg1, arg2);

        internal static Vector256<double> BlendVariable(Vector256<double> left, Vector256<double> right, Vector256<byte> mask)
            => Base.BlendVariable(left, right, mask.AsDouble());

        internal static Vector256<double> Compare(Vector256<double> l, Vector256<double> r, System.Runtime.Intrinsics.X86.FloatComparisonMode orderedLessThanSignaling) 
            => Base.Compare(l, r, orderedLessThanSignaling);

        internal static Vector128<int> ConvertToVector128Int32(Vector256<double> arg) => Base.ConvertToVector128Int32(arg);

        internal static Vector128<float> ConvertToVector128Single(Vector256<double> arg) => Base.ConvertToVector128Single(arg);

        internal static Vector256<double> ConvertToVector256Double(Vector128<float> arg) => Base.ConvertToVector256Double(arg);
        internal static Vector256<double> ConvertToVector256Double(Vector128<int> arg) => Base.ConvertToVector256Double(arg);

        internal static Vector256<int> ConvertToVector256Int32(Vector256<float> arg) => Base.ConvertToVector256Int32(arg);

        internal static Vector256<float> ConvertToVector256Single(Vector256<int> arg) => Base.ConvertToVector256Single(arg);

        internal static Vector256<float> Divide(Vector256<float> arg1, Vector256<float> arg2) => Base.Divide(arg1, arg2);
        internal static Vector256<double> Divide(Vector256<double> arg1, Vector256<double> arg2) => Base.Divide(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector256<byte> LoadVector256(byte* address) => Base.LoadVector256(address);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector256<sbyte> LoadVector256(sbyte* address) => Base.LoadVector256(address);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector256<int> LoadVector256(int* address) => Base.LoadVector256(address);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector256<uint> LoadVector256(uint* address) => Base.LoadVector256(address);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector256<long> LoadVector256(long* address) => Base.LoadVector256(address);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector256<ulong> LoadVector256(ulong* address) => Base.LoadVector256(address);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector256<float> LoadVector256(float* address) => Base.LoadVector256(address);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector256<double> LoadVector256(double* address) => Base.LoadVector256(address);

        internal static Vector256<double> Max(Vector256<double> arg1, Vector256<double> arg2) => Base.Max(arg1, arg2);

        internal static Vector256<double> Min(Vector256<double> arg1, Vector256<double> arg2) => Base.Min(arg1, arg2);

        internal static Vector256<float> Multiply(Vector256<float> arg1, Vector256<float> arg2) => Base.Multiply(arg1, arg2);
        internal static Vector256<double> Multiply(Vector256<double> arg1, Vector256<double> arg2) => Base.Multiply(arg1, arg2);

        internal static Vector256<double> Sqrt(Vector256<double> arg) => Base.Sqrt(arg);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(byte* address, Vector256<byte> data) => Base.Store(address, data);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(sbyte* address, Vector256<sbyte> data) => Base.Store(address, data);
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
        internal static Vector256<float> Subtract(Vector256<float> arg1, Vector256<float> arg2) => Base.Subtract(arg1, arg2);

        internal static Vector256<double> Subtract(Vector256<double> arg1, Vector256<double> arg2) => Base.Subtract(arg1, arg2);
    }
}

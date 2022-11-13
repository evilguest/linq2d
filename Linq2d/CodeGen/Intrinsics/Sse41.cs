using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Base = System.Runtime.Intrinsics.X86.Sse41;

namespace Linq2d.CodeGen.Intrinsics
{
    static class Sse41
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
        public static bool IsSupported { get => Base.IsSupported && !Suppress & Ssse3.IsSupported; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        internal static Vector128<short> BlendVariable(Vector128<short> left, Vector128<short> right, Vector128<ushort> mask) => Base.BlendVariable(left, right, mask.AsInt16());
        internal static Vector128<ushort> BlendVariable(Vector128<ushort> left, Vector128<ushort> right, Vector128<ushort> mask) => Base.BlendVariable(left, right, mask);
        internal static Vector128<int> BlendVariable(Vector128<int> left, Vector128<int> right, Vector128<uint> mask) => Base.BlendVariable(left, right, mask.AsInt32());
        internal static Vector128<uint> BlendVariable(Vector128<uint> left, Vector128<uint> right, Vector128<uint> mask) => Base.BlendVariable(left, right, mask);
        internal static Vector128<long> BlendVariable(Vector128<long> left, Vector128<long> right, Vector128<ulong> mask) => Base.BlendVariable(left, right, mask.AsInt64());
        internal static Vector128<ulong> BlendVariable(Vector128<ulong> left, Vector128<ulong> right, Vector128<ulong> mask) => Base.BlendVariable(left, right, mask);
        internal static Vector128<float> BlendVariable(Vector128<float> left, Vector128<float> right, Vector128<uint> mask) => Base.BlendVariable(left, right, mask.AsSingle());
        internal static Vector128<double> BlendVariable(Vector128<double> left, Vector128<double> right, Vector128<ulong> mask) => Base.BlendVariable(left, right, mask.AsDouble());

        internal static unsafe Vector128<int> ConvertToVector128Int32(byte* address) => Base.ConvertToVector128Int32(address);
        internal static unsafe Vector128<int> ConvertToVector128Int32(sbyte* address) => Base.ConvertToVector128Int32(address);
        internal static unsafe Vector128<int> ConvertToVector128Int32(short* address) => Base.ConvertToVector128Int32(address);
        internal static unsafe Vector128<int> ConvertToVector128Int32(ushort* address) => Base.ConvertToVector128Int32(address);

        internal static unsafe Vector128<long> ConvertToVector128Int64(byte* address) => Base.ConvertToVector128Int64(address);
        internal static unsafe Vector128<long> ConvertToVector128Int64(sbyte* address) => Base.ConvertToVector128Int64(address);
        internal static unsafe Vector128<long> ConvertToVector128Int64(short* address) => Base.ConvertToVector128Int64(address);
        internal static unsafe Vector128<long> ConvertToVector128Int64(ushort* address) => Base.ConvertToVector128Int64(address);
        internal static unsafe Vector128<long> ConvertToVector128Int64(int* address) => Base.ConvertToVector128Int64(address);
        internal static unsafe Vector128<long> ConvertToVector128Int64(uint* address) => Base.ConvertToVector128Int64(address);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]


        internal static Vector128<int> Max(Vector128<int> arg1, Vector128<int> arg2) => Base.Max(arg1, arg2);
        internal static Vector128<uint> Max(Vector128<uint> arg1, Vector128<uint> arg2) => Base.Max(arg1, arg2);

        internal static Vector128<int> Min(Vector128<int> arg1, Vector128<int> arg2) => Base.Min(arg1, arg2);
        internal static Vector128<uint> Min(Vector128<uint> arg1, Vector128<uint> arg2) => Base.Min(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<uint> MultiplyLow(Vector128<uint> arg1, Vector128<uint> arg2) => Base.MultiplyLow(arg1, arg2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<int> MultiplyLow(Vector128<int> arg1, Vector128<int> arg2) => Base.MultiplyLow(arg1, arg2);

    }
}

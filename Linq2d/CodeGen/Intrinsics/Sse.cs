using System.Numerics;
using System.Runtime.Intrinsics;
using System.Runtime.CompilerServices;
using Base = System.Runtime.Intrinsics.X86.Sse;

namespace Linq2d.CodeGen.Intrinsics
{
    static class Sse
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

        #region Vector-4
        internal static unsafe Vector64<short> LoadVector64(short* address) => Vector64.Create(*(long*)address).AsInt16();
        internal static unsafe Vector64<ushort> LoadVector64(ushort* address) => Vector64.Create(*(long*)address).AsUInt16();
        internal static unsafe void Store(short* address, Vector64<short> data) => data.Store(address);
        internal static unsafe void Store(ushort* address, Vector64<ushort> data) => data.Store(address);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<float> Add(Vector128<float> arg1, Vector128<float> arg2) => Base.Add(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<float> Divide(Vector128<float> arg1, Vector128<float> arg2) => Base.Divide(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector128<float> LoadVector128(float* address) => Base.LoadVector128(address);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<float> Multiply(Vector128<float> arg1, Vector128<float> arg2) => Base.Multiply(arg1, arg2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void Store(float* address, Vector128<float> data) => Base.Store(address, data);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<float> Subtract(Vector128<float> arg1, Vector128<float> arg2) => Base.Subtract(arg1, arg2);
        internal static Vector128<uint> CompareEqual(Vector128<float> arg1, Vector128<float> arg2) => Base.CompareEqual(arg1, arg2).AsUInt32();

        internal static Vector128<int> ConvertToVector128Int16(Vector64<short> v) => Vector128.Create(v[0], v[1], v[2], v[3]);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector64<short> ConvertToVector64Int16(Vector128<int> v)
        {
            var t = v.AsInt16();
            return Vector64.Create(t[0], t[2], t[4], t[6]);
        }

        #endregion

    }
}

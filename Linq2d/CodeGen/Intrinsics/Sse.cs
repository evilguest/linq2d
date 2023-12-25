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
        internal static unsafe void Store(bool* destination, Vector128<uint> data) => *(Vector128<uint>*)destination = data;

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

        #endregion

    }
}

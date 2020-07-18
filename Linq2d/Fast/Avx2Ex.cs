using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace System.Linq.Array2d.Fast
{
    //
    public abstract unsafe class Avx2Ex 
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<int> ConvertToVector256Int32(in Vector64<byte> source)
        {
            fixed (Vector64<byte>* srcPtr = &source)
                return Avx2.ConvertToVector256Int32((byte*)srcPtr);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<int> SetFirstN(Vector256<int> source, int value, int count)
            => Avx2.Blend(source, Vector256.CreateScalar(value), (byte)(1 << count - 1));

        public static void Store(ref Vector256<int> target, Vector256<int> source)
        {
            fixed (Vector256<int>* trgPtr = &target)
                Avx2.Store((int*)trgPtr, source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector256<int> LoadVector256(in Vector256<int> source)
        {
            fixed (Vector256<int>* srcPtr = &source) 
                return Avx2.LoadVector256((int*)srcPtr);
        }
    }

}

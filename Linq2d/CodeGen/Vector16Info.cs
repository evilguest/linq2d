using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;


using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Linq2d.CodeGen
{
    public unsafe class Vector16Info : VectorInfo
    {
        public Vector16Info()
        {
            if (Sse.IsSupported)
            {
            }
            if (Sse2.IsSupported)
            {
                InitType128<byte>();
                InitType256<sbyte>();

                InitStore<byte>(Sse2.Store);
                InitStore<sbyte>(Sse2.Store);

                InitLoadAndConvert<byte>(Sse2.LoadVector128);
                InitLoadAndConvert<sbyte>(Sse2.LoadVector128);
            }
            if (Sse3.IsSupported)
            {
            }
            if (Sse41.IsSupported)
            {

            }
            if (Sse42.IsSupported)
            {

            }
            if (Avx.IsSupported)
            {
                InitType256<short>();
                InitType256<ushort>();

                InitStore<short, Vector256<short>>(Avx.Store);
                InitStore<ushort, Vector256<ushort>>(Avx.Store);

                InitLoadAndConvert<short>(Avx.LoadVector256);
                InitLoadAndConvert<ushort>(Avx.LoadVector256);
            }
            if (Avx2.IsSupported)
            {
                InitLoadAndConvert<byte, short>(Avx2.ConvertToVector256Int16);
                InitLoadAndConvert<sbyte, short>(Avx2.ConvertToVector256Int16);
                InitUnary256<short>(ExpressionType.Negate, Negate);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<short> Negate(Vector256<short> a) => Avx2.MultiplyLow(a, Vector256.Create((short)-1));
    }
}

using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;


using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Linq2d.CodeGen
{
    public unsafe class Vector16Info : VectorInfo
    {
        protected override void InitSse2()
        {
            InitType128<byte>();
            InitType256<sbyte>();

            InitLift<byte>(Vector128.Create);
            InitLift<sbyte>(Vector128.Create);

            InitStore<byte>(Sse2.Store);
            InitStore<sbyte>(Sse2.Store);

            InitLoadAndConvert<byte>(Sse2.LoadVector128);
            InitLoadAndConvert<sbyte>(Sse2.LoadVector128);
        }
        protected override void InitAvx()
        {
            InitType256<short>();
            InitType256<ushort>();

            InitLift<short>(Vector256.Create);
            InitLift<ushort>(Vector256.Create);

            InitStore<short, Vector256<short>>(Avx.Store);
            InitStore<ushort, Vector256<ushort>>(Avx.Store);

            InitLoadAndConvert<short>(Avx.LoadVector256);
            InitLoadAndConvert<ushort>(Avx.LoadVector256);
        }

        protected override void InitAvx2()
        {
            InitLoadAndConvert<byte, short>(Avx2.ConvertToVector256Int16);
            InitLoadAndConvert<sbyte, short>(Avx2.ConvertToVector256Int16);
            InitUnary256<short>(ExpressionType.Negate, Negate);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<short> Negate(Vector256<short> a) => Avx2.MultiplyLow(a, Vector256.Create((short)-1));
    }
}

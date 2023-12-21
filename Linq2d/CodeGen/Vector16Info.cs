using System.Linq.Expressions;
using System.Runtime.Intrinsics;
using Linq2d.CodeGen.Intrinsics;

namespace Linq2d.CodeGen
{
    public unsafe class Vector16Info : VectorInfoNet8
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

            InitBinary256<short>(ExpressionType.Add, Avx2.Add);

        }
        protected override void InitAvx512F()
        {
            InitBinary512<int>(ExpressionType.Add, Avx512F.Add);
            InitBinary512<uint>(ExpressionType.Add, Avx512F.Add);
            InitBinary512<float>(ExpressionType.Add, Avx512F.Add);
            InitBinary512<int>(ExpressionType.Subtract, Avx512F.Subtract);
            InitBinary512<uint>(ExpressionType.Subtract, Avx512F.Subtract);
        }

    }
}

using System.Linq.Expressions;
using System.Runtime.Intrinsics;

namespace Linq2d.CodeGen
{
    using Intrinsics;

    public unsafe class Vector32Info : VectorInfo
    {
        protected override void InitAvx()
        {
            InitType256<byte>();
            InitType256<sbyte>();

            InitLift<byte>(Vector256.Create);
            InitLift<sbyte>(Vector256.Create);

            InitStore<byte, Vector256<byte>>(Avx.Store);
            InitStore<sbyte, Vector256<sbyte>>(Avx.Store);

            InitLoadAndConvert<byte>(Avx.LoadVector256);
            InitLoadAndConvert<sbyte>(Avx.LoadVector256);
        }

        protected override void InitAvx2()
        {
            InitBinary256<byte>(ExpressionType.Add, Avx2.Add);
        }
    }
}

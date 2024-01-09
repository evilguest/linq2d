using System.Linq.Expressions;
using System.Runtime.Intrinsics;
using Linq2d.CodeGen.Intrinsics;

namespace Linq2d.CodeGen
{
    public unsafe class Vector32Info : VectorInfo
    {
        protected override void InitAvx()
        {
            InitType256<byte>();
            InitType256<sbyte>();

            InitLift<byte>(Vector256.Create);
            InitLift<sbyte>(Vector256.Create);

            InitStore<byte, Vector256<byte>>(Avx.Store);
            InitStore<bool, Vector256<byte>>(Avx.Store);
            InitStore<sbyte, Vector256<sbyte>>(Avx.Store);

            InitLoadAndConvert<byte>(Avx.LoadVector256);
            InitLoadAndConvert<sbyte>(Avx.LoadVector256);
        }

        protected override void InitAvx2()
        {
            InitBinary256<byte>(ExpressionType.Add, Avx2.Add);
            InitBinary256<sbyte>(ExpressionType.Add, Avx2.Add);
            InitUnary256<sbyte>(ExpressionType.Negate, Avx2.Negate);

            InitBinary256<byte>(ExpressionType.Equal, Avx2.CompareEqual);
            InitBinary256<sbyte, byte>(ExpressionType.Equal, Avx2.CompareEqual);
        }
    }
}

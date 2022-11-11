using System;
using System.Linq.Expressions;

namespace Linq2d.CodeGen
{
    using Intrinsics;
    using System.Runtime.Intrinsics;

    public unsafe class Vector2Info : VectorInfo
    {
        protected override void InitSse2()
        {
            InitType128<long>();
            InitType128<ulong>();
            InitType128<double>();

            InitLift<long>(Vector128.Create);
            InitLift<ulong>(Vector128.Create);
            InitLift<double>(Vector128.Create);

            InitStore<long>(Sse2.Store);
            InitStore<ulong>(Sse2.Store);
            InitStore<double>(Sse2.Store);

            InitLoadAndConvert<long>(Sse2.LoadVector128);
            InitLoadAndConvert<ulong>(Sse2.LoadVector128);
            InitLoadAndConvert<double>(Sse2.LoadVector128);

            InitBinary128<long>(ExpressionType.Add, Sse2.Add);
            InitBinary128<ulong>(ExpressionType.Add, Sse2.Add);
            InitBinary128<double>(ExpressionType.Add, Sse2.Add);

            InitBinary128<long>(ExpressionType.Subtract, Sse2.Subtract);
            InitBinary128<ulong>(ExpressionType.Subtract, Sse2.Subtract);
            InitBinary128<double>(ExpressionType.Subtract, Sse2.Subtract);

            InitBinary128<double>(ExpressionType.Multiply, Sse2.Multiply);
            InitBinary128<double>(ExpressionType.Divide, Sse2.Divide);

            InitBinary128<double>(Math.Max, Sse2.Max);
            InitBinary128<double>(Math.Min, Sse2.Min);

            InitBinary128<long>(ExpressionType.ExclusiveOr, Sse2.Xor);
            InitBinary128<ulong>(ExpressionType.ExclusiveOr, Sse2.Xor);

            InitBinary128<long>(ExpressionType.Or, Sse2.Or);
            InitBinary128<ulong>(ExpressionType.Or, Sse2.Or);

            InitBinary128<long>(ExpressionType.And, Sse2.And);
            InitBinary128<ulong>(ExpressionType.And, Sse2.And);

            InitBinary128Forced<long, byte, int>(ExpressionType.RightShift, Sse2.ShiftRightArithmetic); // ?? should be arithmetic
            InitBinary128Forced<ulong, byte, int>(ExpressionType.RightShift, Sse2.ShiftRightLogical);

            InitBinary128Forced<long, byte, int>(ExpressionType.LeftShift, Sse2.ShiftLeftLogical);
            InitBinary128Forced<ulong, byte, int>(ExpressionType.LeftShift, Sse2.ShiftLeftLogical);
        }
        protected override void InitSse41()
        {
            InitLoadAndConvert<byte, long>(Sse41.ConvertToVector128Int64);
            InitLoadAndConvert<sbyte, long>(Sse41.ConvertToVector128Int64);
            InitLoadAndConvert<short, long>(Sse41.ConvertToVector128Int64);
            InitLoadAndConvert<ushort, long>(Sse41.ConvertToVector128Int64);
            InitLoadAndConvert<int, long>(Sse41.ConvertToVector128Int64);
            InitLoadAndConvert<uint, long>(Sse41.ConvertToVector128Int64);

            InitConditional128<double>(Sse41.BlendVariable);
            InitConditional128<long>(Sse41.BlendVariable);
            InitConditional128<ulong>(Sse41.BlendVariable);
        }

    }
}

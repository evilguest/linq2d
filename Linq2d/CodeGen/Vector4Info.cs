using System;

using System.Runtime.Intrinsics;

using System.Linq.Expressions;
using Linq2d.CodeGen.Fake;

namespace Linq2d.CodeGen
{
    using Intrinsics;
    public unsafe class Vector4Info : VectorInfo
    {
        protected override void InitSse()
        {
            InitType128<float>();
            InitLift<float>(Vector128.Create);

            InitStore<float>(Sse.Store);
            InitStore<int>(Sse.Store);
            InitStore<uint>(Sse.Store);
            InitStore<bool, Vector128<uint>>(Sse.Store);
            InitLoadAndConvert<float>(Sse.LoadVector128);



            InitLift<float>(Vector128.Create);

            InitBinary128<float, uint>(ExpressionType.Equal, Sse.CompareEqual);
            InitBinary128<float>(ExpressionType.Add, Sse.Add);
            InitBinary128<float>(ExpressionType.Subtract, Sse.Subtract);
            InitBinary128<float>(ExpressionType.Multiply, Sse.Multiply);
            InitBinary128<float>(ExpressionType.Divide, Sse.Divide);

            InitType128<int>();
            InitLift<int>(Vector128.Create);
        }

        protected override void InitSse2()
        {
            InitType64<short>();
            InitLoadAndConvert<short>(Sse2.LoadVector64);
            InitLift<short>(Vector64.Create);
            InitConvert<short, int>(Sse2.ConvertToVector128Int32);
            InitConvert<ushort, int>(Sse2.ConvertToVector128Int32);
            InitConvert128to64<int, short>(Sse2.ConvertToVector64Int16);
            InitStore64<short>(Sse2.Store);

            InitType64<ushort>();
            InitLoadAndConvert<ushort>(Sse2.LoadVector64);
            InitConvert128to64<int, ushort>(Sse2.ConvertToVector64UInt16);
            InitConvert128to64<uint, ushort>(Sse2.ConvertToVector64UInt16);
            InitLift<ushort>(Vector64.Create);
            InitStore64<ushort>(Sse2.Store);
            InitType128<int>();
            InitType128<uint>();

            InitLift<int>(Vector128.Create);
            InitLift<uint>(Vector128.Create);

            InitStore<int>(Sse2.Store);
            InitStore<uint>(Sse2.Store);

            InitLoadAndConvert<int>(Sse2.LoadVector128);
            InitLoadAndConvert<uint>(Sse2.LoadVector128);

            InitConvert128<int, float>(Sse2.ConvertToVector128Single);
            InitConvert128<float, int>(Sse2.ConvertToVector128Int32WithTruncation);


            InitUnary128<int>(ExpressionType.Negate, Sse2.Negate);

            InitBinary128<int>(ExpressionType.Add, Sse2.Add);
            InitBinary128<uint>(ExpressionType.Add, Sse2.Add);
            InitBinary128<int>(ExpressionType.Subtract, Sse2.Subtract);
            InitBinary128<uint>(ExpressionType.Subtract, Sse2.Subtract);

            InitBinary128<int>(ExpressionType.ExclusiveOr, Sse2.Xor);
            InitBinary128<uint>(ExpressionType.ExclusiveOr, Sse2.Xor);

            InitBinary128<int>(ExpressionType.Or, Sse2.Or);
            InitBinary128<uint>(ExpressionType.Or, Sse2.Or);

            InitBinary128<int>(ExpressionType.And, Sse2.And);
            InitBinary128<uint>(ExpressionType.And, Sse2.And);

            InitBinary128<int, uint>(ExpressionType.Equal, Sse2.CompareEqual);
            InitBinary128<uint>(ExpressionType.Equal, Sse2.CompareEqual);

            InitBinary128Forced<int, byte, int>(ExpressionType.RightShift, Sse2.ShiftRightArithmetic);
            InitBinary128Forced<uint, byte, int>(ExpressionType.RightShift, Sse2.ShiftRightLogical);

            InitBinary128Forced<int, byte, int>(ExpressionType.LeftShift, Sse2.ShiftLeftLogical);
            InitBinary128Forced<uint, byte, int>(ExpressionType.LeftShift, Sse2.ShiftLeftLogical);
        }

        protected override void InitSsse3()
        {
            InitConvert128to64<uint, ushort>(Ssse3.ConvertToVector64Int16);
            InitConvert128to64<int, short>(Ssse3.ConvertToVector64Int16);
        }
        protected override void InitSse41()
        {
            InitLoadAndConvert<byte, int>(Sse41.ConvertToVector128Int32);
            InitLoadAndConvert<sbyte, int>(Sse41.ConvertToVector128Int32);
            InitLoadAndConvert<short, int>(Sse41.ConvertToVector128Int32);
            InitLoadAndConvert<ushort, int>(Sse41.ConvertToVector128Int32);

            InitLoadAndConvert((byte* address) => Sse2.ConvertToVector128Single(Sse41.ConvertToVector128Int32(address)));
            InitLoadAndConvert((sbyte* address) => Sse2.ConvertToVector128Single(Sse41.ConvertToVector128Int32(address)));
            InitLoadAndConvert((short* address) => Sse2.ConvertToVector128Single(Sse41.ConvertToVector128Int32(address)));
            InitLoadAndConvert((ushort* address) => Sse2.ConvertToVector128Single(Sse41.ConvertToVector128Int32(address)));

            InitBinary128<int>(ExpressionType.Multiply, Sse41.MultiplyLow);
            InitBinary128<uint>(ExpressionType.Multiply, Sse41.MultiplyLow);


            InitBinary128<int>(Math.Max, Sse41.Max);
            InitBinary128<int>(Math.Min, Sse41.Min);
            InitBinary128<uint>(Math.Max, Sse41.Max);
            InitBinary128<uint>(Math.Min, Sse41.Min);

            InitConditional128<int, uint>(Sse41.BlendVariable);
            InitConditional128<uint, uint>(Sse41.BlendVariable);
            InitConditional128<float, uint>(Sse41.BlendVariable);
        }
        protected override void InitAvx()
        {
            InitType256<long>();
            InitType256<ulong>();
            InitType256<double>();

            InitLift<long>(Vector256.Create);
            InitLift<ulong>(Vector256.Create);
            InitLift<double>(Vector256.Create);

            InitStore256<long>(Avx.Store);
            InitStore256<ulong>(Avx.Store);
            InitStore256<double>(Avx.Store);

            InitLoadAndConvert<long>(Avx.LoadVector256);
            InitLoadAndConvert<ulong>(Avx.LoadVector256);
            InitLoadAndConvert<double>(Avx.LoadVector256);
            InitLoadAndConvert<byte, double>(Avx.ConvertToVector256Double);
            InitLoadAndConvert<sbyte, double>(Avx.ConvertToVector256Double);
            InitLoadAndConvert<short, double>(Avx.ConvertToVector256Double);
            InitLoadAndConvert<ushort, double>(Avx.ConvertToVector256Double);
            InitLoadAndConvert<int, double>(Avx.ConvertToVector256Double);

            InitConvert<double, float>(Avx.ConvertToVector128Single);
            InitConvert<float, double>(Avx.ConvertToVector256Double);
            InitConvert<int, double>(Avx.ConvertToVector256Double);
            InitConvert<double, int>(Avx.ConvertToVector128Int32WithTruncation);
            //InitConvert256<long, double>(ConvertToVector256Double);
            // conversion from long to double requires the AVX-512 support
            // https://www.felixcloutier.com/x86/vcvtqq2pd
            InitUnary256<double>(Math.Sqrt, Avx.Sqrt);

            InitBinary256<double>(ExpressionType.Add, Avx.Add);
            InitBinary256<double>(ExpressionType.Subtract, Avx.Subtract);
            InitBinary256<double>(ExpressionType.Multiply, Avx.Multiply);
            InitBinary256<double>(ExpressionType.Divide, Avx.Divide);

            InitBinary256<double, ulong>(ExpressionType.Equal, Avx.CompareEqual);
            InitBinary256<double, ulong>(ExpressionType.LessThan, Avx.LessThan);
            InitBinary256<double, ulong>(ExpressionType.LessThanOrEqual, Avx.LessThanOrEqual);
            InitBinary256<double, ulong>(ExpressionType.GreaterThan, Avx.GreaterThan);
            InitBinary256<double, ulong>(ExpressionType.GreaterThanOrEqual, Avx.GreaterThanOrEqual);

            InitBinary256<double>(Math.Max, Avx.Max);
            InitBinary256<double>(Math.Min, Avx.Min);

            InitConditional256<double, ulong>(Avx.BlendVariable);
            InitConditional<Vector256<byte>, Vector32<byte>>(Vector32.DoubleConditional);

            InitUnary256<double>(ExpressionType.Negate, Avx.Negate);
        }

        protected override void InitAvx2()
        {
            InitLoadAndConvert<byte, long>(Avx2.ConvertToVector256Int64);
            InitLoadAndConvert<sbyte, long>(Avx2.ConvertToVector256Int64);
            InitLoadAndConvert<short, long>(Avx2.ConvertToVector256Int64);
            InitLoadAndConvert<ushort, long>(Avx2.ConvertToVector256Int64);
            InitLoadAndConvert<int, long>(Avx2.ConvertToVector256Int64);
            InitLoadAndConvert<uint, long>(Avx2.ConvertToVector256Int64);

            InitConvert<int, long>(Avx2.ConvertToVector256Int64);
            InitConvert<uint, long>(Avx2.ConvertToVector256Int64);

            InitBinary256<long>(ExpressionType.Add, Avx2.Add);
            InitBinary256<ulong>(ExpressionType.Add, Avx2.Add);

            InitBinary256<long>(ExpressionType.Subtract, Avx2.Subtract);
            InitBinary256<ulong>(ExpressionType.Subtract, Avx2.Subtract);

            InitBinary256<long>(ExpressionType.ExclusiveOr, Avx2.Xor);
            InitBinary256<ulong>(ExpressionType.ExclusiveOr, Avx2.Xor);

            InitBinary256<long>(ExpressionType.Or, Avx2.Or);
            InitBinary256<ulong>(ExpressionType.Or, Avx2.Or);

            InitBinary256<long>(ExpressionType.And, Avx2.And);
            InitBinary256<ulong>(ExpressionType.And, Avx2.And);

            InitBinary256<long, ulong>(ExpressionType.Equal, Avx2.CompareEqual);
            InitBinary256<ulong>(ExpressionType.Equal, Avx2.CompareEqual);

            InitBinary256Forced<long, byte, int>(ExpressionType.RightShift, Avx2.ShiftRightArithmetic);
            InitBinary256Forced<ulong, byte, int>(ExpressionType.RightShift, Avx2.ShiftRightLogical);

            InitBinary256Forced<long, byte, int>(ExpressionType.LeftShift, Avx2.ShiftLeftLogical);
            InitBinary256Forced<ulong, byte, int>(ExpressionType.LeftShift, Avx2.ShiftLeftLogical);

            InitConditional256<long, ulong>(Avx2.BlendVariable);
            InitConditional256<ulong, ulong>(Avx2.BlendVariable);
        }

        public Vector4Info()
        {
            InitType32<byte>();
            InitLift<byte>(Vector32.Create);

            InitStore<byte>(Vector32.Store);
            InitLoadAndConvert<byte>(Vector32.Load);
            Available = true;
        }
    }
}

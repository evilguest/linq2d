﻿using System.Runtime.Intrinsics;

using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Linq2d.CodeGen
{
    using Intrinsics;
    using Linq2d.MathHelpers;
    using System;

    public unsafe class Vector8Info : VectorInfo
    {
        protected override void InitSse2()
        {
            InitType128<short>();
            InitType128<ushort>();

            InitLift<short>(Vector128.Create);
            InitLift<ushort>(Vector128.Create);

            InitStore<short>(Sse2.Store);
            InitStore<ushort>(Sse2.Store);

            InitLoadAndConvert<short>(Sse2.LoadVector128);
            InitLoadAndConvert<ushort>(Sse2.LoadVector128);

            InitBinary128<short>(Fast.Add, Sse2.Add);  // C# does not generate the + expressions for short 
            InitBinary128<ushort>(Fast.Add, Sse2.Add); // C# does not generate the + expressions for ushort 

            InitBinary128<short>(Fast.Subtract, Sse2.Subtract);
            InitBinary128<ushort>(Fast.Subtract, Sse2.Subtract);

            InitBinary128<short>(Fast.Multiply, Sse2.MultiplyLow);
            InitBinary128<ushort>(ExpressionType.Multiply, Sse2.MultiplyLow);

            InitBinary128<short>(ExpressionType.ExclusiveOr, Sse2.Xor);
            InitBinary128<ushort>(ExpressionType.ExclusiveOr, Sse2.Xor);

            InitBinary128<short>(ExpressionType.And, Sse2.And);
            InitBinary128<ushort>(ExpressionType.And, Sse2.And);

            InitBinary128<short>(ExpressionType.Or, Sse2.Or);
            InitBinary128<ushort>(ExpressionType.Or, Sse2.Or);

            InitBinary128<short, ushort>(ExpressionType.Equal, Sse2.CompareEqual);
            InitBinary128<ushort>(ExpressionType.Equal, Sse2.CompareEqual);

            InitBinary128Forced<short, byte, int>(ExpressionType.RightShift, Sse2.ShiftRightArithmetic);
            InitBinary128Forced<ushort, byte, int>(ExpressionType.RightShift, Sse2.ShiftRightLogical);
            InitBinary128Forced<short, byte, int>(ExpressionType.LeftShift, Sse2.ShiftLeftLogical);
            InitBinary128Forced<ushort, byte, int>(ExpressionType.LeftShift, Sse2.ShiftLeftLogical);
        }
        protected override void InitSsse3()
        {
            InitUnary128<short>(Fast.Negate, Ssse3.Negate); // native short negation isn't supported by C#
        }
        protected override void InitSse41()
        {
            InitConditional128<short, ushort>(Sse41.BlendVariable);
            InitConditional128<ushort, ushort>(Sse41.BlendVariable);
        }

        protected override void InitAvx()
        {
            InitType256<int>();
            InitType256<uint>();
            InitType256<float>();

            InitLift<int>(Vector256.Create);
            InitLift<uint>(Vector256.Create);
            InitLift<float>(Vector256.Create);

            InitStore256<int>(Avx.Store);
            InitStore256<uint>(Avx.Store);
            InitStore256<float>(Avx.Store);

            InitLoadAndConvert<int>(Avx.LoadVector256);
            InitLoadAndConvert<uint>(Avx.LoadVector256);
            InitLoadAndConvert<float>(Avx.LoadVector256);
            InitLoadAndConvert<int, float>(Avx.ConvertToVector256Single);
            InitConvert256<int, float>(Avx.ConvertToVector256Single);
            InitConvert256<float, int>(Avx.ConvertToVector256Int32);


            InitBinary256<float, uint>(ExpressionType.Equal, Avx.CompareEqual);
            InitBinary256<float>(ExpressionType.Add, Avx.Add);
            InitBinary256<float>(ExpressionType.Subtract, Avx.Subtract);
            InitBinary256<float>(ExpressionType.Multiply, Avx.Multiply);
            InitBinary256<float>(ExpressionType.Divide, Avx.Divide);


            InitConditional256<float, uint>(Avx.BlendVariable);
            InitConditional256<int, uint>(Avx.BlendVariable);
            InitConditional256<uint, uint>(Avx.BlendVariable);

            InitUnary256<float>(ExpressionType.Negate, Avx.Negate);
        }

        protected override void InitAvx2()
        {
            InitLoadAndConvert<byte, int>(Avx2.ConvertToVector256Int32);
            InitLoadAndConvert<sbyte, int>(Avx2.ConvertToVector256Int32);
            InitLoadAndConvert<short, int>(Avx2.ConvertToVector256Int32);
            InitLoadAndConvert<ushort, int>(Avx2.ConvertToVector256Int32);



            InitBinary256<int, uint>(ExpressionType.Equal, Avx2.CompareEqual);
            InitBinary256<uint>(ExpressionType.Equal, Avx2.CompareEqual);

            InitBinary256<int>(ExpressionType.Add, Avx2.Add);
            InitBinary256<uint>(ExpressionType.Add, Avx2.Add);

            InitBinary256<int>(ExpressionType.Subtract, Avx2.Subtract);
            InitBinary256<uint>(ExpressionType.Subtract, Avx2.Subtract);

            InitBinary256<int>(ExpressionType.Multiply, Avx2.MultiplyLow);
            InitBinary256<uint>(ExpressionType.Multiply, Avx2.MultiplyLow);

            InitUnary256<int>(ExpressionType.Negate, Avx2.Negate);

            InitConvert256to128<uint, ushort>(Avx2.ConvertToVector128Int16);

            InitBinary256Forced<int, byte, int>(ExpressionType.RightShift, Avx2.ShiftRightArithmetic);
            InitBinary256Forced<uint, byte, int>(ExpressionType.RightShift, Avx2.ShiftRightLogical);

            InitBinary256Forced<int, byte, int>(ExpressionType.LeftShift, Avx2.ShiftLeftLogical);
            InitBinary256Forced<uint, byte, int>(ExpressionType.LeftShift, Avx2.ShiftLeftLogical);
        }

    }
}

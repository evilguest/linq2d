﻿using System;

using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Linq2d.CodeGen.Fake;

namespace Linq2d.CodeGen
{
    using Intrinsics;
    public unsafe class Vector4Info: VectorInfo
    {
        protected override void InitSse()
        {
            InitType128<float>();
            InitLift<float>(Vector128.Create);

            InitStore<float>(Sse.Store);
            InitLoadAndConvert<float>(Sse.LoadVector128);

            InitLift<float>(Vector128.Create);

            InitBinary128<float>(ExpressionType.Add, Sse.Add);
            InitBinary128<float>(ExpressionType.Subtract, Sse.Subtract);
            InitBinary128<float>(ExpressionType.Multiply, Sse.Multiply);
            InitBinary128<float>(ExpressionType.Divide, Sse.Divide);
            InitBinary128<float>(ExpressionType.Equal, Sse.CompareEqual);
        }

        protected override void InitSse2()
        {
            InitType128<int>();
            InitType128<uint>();

            InitLift<int>(Vector128.Create);
            InitLift<uint>(Vector128.Create);

            InitStore<int>(Sse2.Store);
            InitStore<uint>(Sse2.Store);

            InitLoadAndConvert<int>(Sse2.LoadVector128);
            InitLoadAndConvert<uint>(Sse2.LoadVector128);

            InitConvert128<int, float>(Sse2.ConvertToVector128Single);
            InitConvert128<float, int>(Sse2.ConvertToVector128Int32);


            //InitUnary128<int, uint>(Math.Abs, Avx2.Abs)

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

            InitBinary128<int, byte>(ExpressionType.Equal, Sse2.CompareEqual);
            InitBinary128<uint, byte>(ExpressionType.Equal, Sse2.CompareEqual);

            InitBinary128Forced<int, byte, int>(ExpressionType.RightShift, Sse2.ShiftRightArithmetic);
            InitBinary128Forced<uint, byte, int>(ExpressionType.RightShift, Sse2.ShiftRightLogical);

            InitBinary128Forced<int, byte, int>(ExpressionType.LeftShift, Sse2.ShiftLeftLogical);
            InitBinary128Forced<uint, byte, int>(ExpressionType.LeftShift, Sse2.ShiftLeftLogical);
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

            InitConditional128<int>(Sse41.BlendVariable);
            InitConditional128<uint>(Sse41.BlendVariable);
            InitConditional128<float>(Sse41.BlendVariable);
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
            InitLoadAndConvert<byte, double>(ConvertToVector256Double);
            InitLoadAndConvert<sbyte, double>(ConvertToVector256Double);
            InitLoadAndConvert<short, double>(ConvertToVector256Double);
            InitLoadAndConvert<ushort, double>(ConvertToVector256Double);
            InitLoadAndConvert<int, double>(ConvertToVector256Double);

            InitConvert<double, float>(Avx.ConvertToVector128Single);
            InitConvert<float, double>(Avx.ConvertToVector256Double);
            InitConvert<int, double>(Avx.ConvertToVector256Double);
            InitConvert<double, int>(Avx.ConvertToVector128Int32);
            //InitConvert256<long, double>(ConvertToVector256Double);
            // conversion from long to double requires the AVX-512 support
            // https://www.felixcloutier.com/x86/vcvtqq2pd
            InitUnary256<double>(Math.Sqrt, Avx.Sqrt);

            InitBinary256<double>(ExpressionType.Add, Avx.Add);
            InitBinary256<double>(ExpressionType.Subtract, Avx.Subtract);
            InitBinary256<double>(ExpressionType.Multiply, Avx.Multiply);
            InitBinary256<double>(ExpressionType.Divide, Avx.Divide);

            InitBinary256<double, byte>(ExpressionType.LessThan, LessThan);
            InitBinary256<double, byte>(ExpressionType.LessThanOrEqual, LessThanOrEqual);
            InitBinary256<double, byte>(ExpressionType.GreaterThan, GreaterThan);
            InitBinary256<double, byte>(ExpressionType.GreaterThanOrEqual, GreaterThanOrEqual);

            InitBinary256<double>(Math.Max, Avx.Max);
            InitBinary256<double>(Math.Min, Avx.Min);

            InitConditional256<double>(Avx.BlendVariable);
            InitConditional<Vector256<byte>, Vector32<byte>>(Vector32.DoubleConditional);

            InitUnary256<double>(ExpressionType.Negate, Negate);
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

            InitBinary256Forced<long, byte, int>(ExpressionType.RightShift, Avx2.ShiftRightArithmetic); 
            InitBinary256Forced<ulong, byte, int>(ExpressionType.RightShift, Avx2.ShiftRightLogical);

            InitBinary256Forced<long, byte, int>(ExpressionType.LeftShift, Avx2.ShiftLeftLogical);
            InitBinary256Forced<ulong, byte, int>(ExpressionType.LeftShift, Avx2.ShiftLeftLogical);

            InitConditional256<long>(Avx2.BlendVariable);
            InitConditional256<ulong>(Avx2.BlendVariable);
        }

        public Vector4Info()
        {
            InitType32<byte>();
            InitLift<byte>(Vector32.Create);

            InitStore<byte>(Vector32.Store);
            InitLoadAndConvert<byte>(Vector32.Load);
            Available = true;
        }
        public static Vector256<byte> LessThan(Vector256<double> l, Vector256<double> r)
            => Avx.Compare(l, r, FloatComparisonMode.OrderedLessThanSignaling).AsByte();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<byte> GreaterThan(Vector256<double> l, Vector256<double> r)
            => Avx.Compare(l, r, FloatComparisonMode.OrderedGreaterThanSignaling).AsByte();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<byte> LessThanOrEqual(Vector256<double> l, Vector256<double> r)
            => Avx.Compare(l, r, FloatComparisonMode.OrderedLessThanOrEqualSignaling).AsByte();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<byte> GreaterThanOrEqual(Vector256<double> l, Vector256<double> r)
            => Avx.Compare(l, r, FloatComparisonMode.OrderedGreaterThanOrEqualSignaling).AsByte();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> ConvertToVector256Double(byte* address)
            => Avx.ConvertToVector256Double(Sse41.ConvertToVector128Int32(address));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> ConvertToVector256Double(Vector256<long> data)
            => Vector256.Create((double)data.GetElement(0), data.GetElement(1), data.GetElement(2), data.GetElement(3));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> ConvertToVector256Double(sbyte* address)
            => Avx.ConvertToVector256Double(Sse41.ConvertToVector128Int32(address));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> ConvertToVector256Double(short* address)
            => Avx.ConvertToVector256Double(Sse41.ConvertToVector128Int32(address));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> ConvertToVector256Double(ushort* address)
            => Avx.ConvertToVector256Double(Sse41.ConvertToVector128Int32(address));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> ConvertToVector256Double(int* address)
            => Avx.ConvertToVector256Double(Sse2.LoadVector128(address));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> Negate(Vector256<double> a) => Avx.Multiply(a, Vector256.Create(-1.0));

    }
}
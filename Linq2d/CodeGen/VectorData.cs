using System;
using System.Collections.Generic;
using System.Reflection;

using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Linq2d.CodeGen.Fake;
using System.Diagnostics.CodeAnalysis;

namespace Linq2d.CodeGen
{
    public interface IVectorInfo
    {
        IReadOnlyDictionary<Type, MethodInfo> StoreOperations { get; }
        IReadOnlyDictionary<(Type sourceType, Type targetType), MethodInfo> LoadAndConvertOperations { get; }
        IReadOnlyDictionary<Type, MethodInfo> LiftOperations { get; }
        IReadOnlyDictionary<(Type sourceType, Type targetType), MethodInfo> ConvertOperations { get; }
        IReadOnlyDictionary<(ExpressionType ex, Type l, Type r), MethodInfo> BinaryOperations { get; }
        IReadOnlyDictionary<(ExpressionType ex, Type o), MethodInfo> UnaryOperations { get; }
        IReadOnlyDictionary<MethodInfo, MethodInfo> MethodTable { get; }
        IReadOnlyDictionary<Type, Type> Vector { get; }
        IReadOnlyDictionary<(Type, Type), MethodInfo> ConditionalOperations { get; }
    }

    public abstract class VectorInfo : IVectorInfo
    {
        #region fields
        private readonly Dictionary<(Type sourceType, Type targetType), MethodInfo> _loadAndConvertOperations = new Dictionary<(Type sourceType, Type targetType), MethodInfo>();
        private readonly Dictionary<(Type sourceType, Type targetType), MethodInfo> _convertOperations = new Dictionary<(Type sourceType, Type targetType), MethodInfo>();
        private readonly Dictionary<(ExpressionType ex, Type l, Type r), MethodInfo> _binaryOperations = new Dictionary<(ExpressionType ex, Type l, Type r), MethodInfo>();
        private readonly Dictionary<(ExpressionType ex, Type o), MethodInfo> _unaryOperations = new Dictionary<(ExpressionType ex, Type o), MethodInfo>();
        private readonly Dictionary<Type, MethodInfo> _storeOperations = new Dictionary<Type, MethodInfo>();
        private readonly Dictionary<Type, MethodInfo> _liftOperations = new Dictionary<Type, MethodInfo>();
        private readonly Dictionary<MethodInfo, MethodInfo> _methodTable = new Dictionary<MethodInfo, MethodInfo>();
        private readonly Dictionary<Type, Type> _vectorTable = new Dictionary<Type, Type>();
        private readonly Dictionary<(Type, Type), MethodInfo> _conditionalTable = new Dictionary<(Type, Type), MethodInfo>();
        #endregion

        #region IVectorInfo
        public IReadOnlyDictionary<Type, MethodInfo> StoreOperations => _storeOperations;
        public IReadOnlyDictionary<(Type sourceType, Type targetType), MethodInfo> LoadAndConvertOperations => _loadAndConvertOperations;
        public IReadOnlyDictionary<(Type sourceType, Type targetType), MethodInfo> ConvertOperations => _convertOperations;
        public IReadOnlyDictionary<Type, MethodInfo> LiftOperations => _liftOperations;
        public IReadOnlyDictionary<(ExpressionType ex, Type l, Type r), MethodInfo> BinaryOperations => _binaryOperations;
        public IReadOnlyDictionary<(ExpressionType ex, Type o), MethodInfo> UnaryOperations => _unaryOperations;
        public IReadOnlyDictionary<MethodInfo, MethodInfo> MethodTable => _methodTable;
        public IReadOnlyDictionary<Type, Type> Vector => _vectorTable;
        public IReadOnlyDictionary<(Type, Type), MethodInfo> ConditionalOperations => _conditionalTable;
        #endregion

        #region Init
        #region Types
        public void InitType32<T>()
            where T : struct
            => _vectorTable[typeof(T)] = typeof(Vector32<T>);
        public void InitType128<T>()
            where T: struct
            => _vectorTable[typeof(T)] = typeof(Vector128<T>);
        public void InitType256<T>()
            where T : struct
            => _vectorTable[typeof(T)] = typeof(Vector256<T>);
        #endregion
        #region Store
        unsafe public delegate void Store<T, D>(T* address, D data) where T : unmanaged;

        public void InitStore<T>(Store<T, Vector32<T>> method)
            where T : unmanaged
            => InitStore<T, Vector32<T>>(method);
        public void InitStore<T>(Store<T, Vector128<T>> method)
            where T : unmanaged
            => InitStore<T, Vector128<T>>(method);
        public void InitStore256<T>(Store<T, Vector256<T>> method)
            where T : unmanaged
            => InitStore(method);
        public void InitStore<T, D>(Store<T, D> method)
            where T : unmanaged
            => _storeOperations[typeof(T)] = method.Method;
        #endregion
        #region LoadAndConvert
        unsafe public delegate D Load<T, D>(T* address) where T : unmanaged;
        public void InitLoadAndConvert<T, R>(Load<T, Vector32<R>> method)
            where T : unmanaged
            where R : unmanaged
            => _loadAndConvertOperations[(typeof(T), typeof(R))] = method.Method;
        public void InitLoadAndConvert<T, R>(Load<T, Vector128<R>> method)
            where T : unmanaged
            where R : unmanaged
            => _loadAndConvertOperations[(typeof(T), typeof(R))] = method.Method;

        public void InitLoadAndConvert<T>(Load<T, Vector32<T>> method)
            where T : unmanaged
            => InitLoadAndConvert<T, T>(method);
        public void InitLoadAndConvert<T>(Load<T, Vector128<T>> method)
            where T : unmanaged
            => InitLoadAndConvert<T, T>(method);

        public void InitLoadAndConvert<T, R>(Load<T, Vector256<R>> method)
            where T : unmanaged
            where R : unmanaged
            => _loadAndConvertOperations[(typeof(T), typeof(R))] = method.Method;

        public void InitLoadAndConvert<T>(Load<T, Vector256<T>> method)
            where T : unmanaged
            => InitLoadAndConvert<T, T>(method);
        #endregion

        #region Convert
        public void InitConvert<T, R>(Func<T, R> method)
            => _convertOperations[(typeof(T), typeof(R))] = method.Method;

        public void InitConvert<T, R>(Func<Vector128<T>, Vector256<R>> method)
            where T : struct
            where R : struct
            => InitConvert<Vector128<T>, Vector256<R>>(method);
        public void InitConvert<T, R>(Func<Vector256<T>, Vector128<R>> method)
            where T : struct
            where R : struct
            => InitConvert<Vector256<T>, Vector128<R>>(method);
        //public void InitConvert128<T, R>(Func<Vector128<T>, Vector128<R>> method)
        //    where T : struct
        //    where R : struct
        //    => InitConvert(method);
        public void InitConvert256<T, R>(Func<Vector256<T>, Vector256<R>> method)
            where T : struct
            where R : struct
            => InitConvert(method);
        public void InitLift<T>(Func<T, Vector32<T>> method)
            where T : struct
            => _liftOperations[typeof(T)] = method.Method;
        public void InitLift<T>(Func<T, Vector128<T>> method)
            where T : struct
            => _liftOperations[typeof(T)] = method.Method;
        public void InitLift<T>(Func<T, Vector256<T>> method)
            where T : struct
            => _liftOperations[typeof(T)] = method.Method;
        #endregion

        #region Unary
        public void InitUnary<T, R>(ExpressionType ex, Func<T, R> method)
            => _unaryOperations[(ex, typeof(T))] = method.Method;
        public void InitUnary128<T, R>(ExpressionType ex, Func<Vector128<T>, Vector128<R>> method)
            where T : unmanaged
            where R : unmanaged
            => InitUnary(ex, method);
        public void InitUnary128<T>(ExpressionType ex, Func<Vector128<T>, Vector128<T>> method)
            where T : unmanaged
            => InitUnary(ex, method);
        public void InitUnary256<T, R>(ExpressionType ex, Func<Vector256<T>, Vector256<R>> method)
            where T : unmanaged
            where R : unmanaged
            => InitUnary(ex, method);
        public void InitUnary256<T>(ExpressionType ex, Func<Vector256<T>, Vector256<T>> method)
            where T : unmanaged
            => InitUnary(ex, method);
        #endregion
        #region Binary

        public void InitBinary<T1, T2, R>(ExpressionType ex, Func<T1, T2, R> method)
            => _binaryOperations[(ex, typeof(T1), typeof(T2))] = method.Method;
        public void InitBinary128<T1, T2, R>(ExpressionType ex, Func<Vector128<T1>, Vector128<T2>, Vector128<R>> method)
            where T1 : unmanaged
            where T2 : unmanaged
            where R : unmanaged
            => InitBinary(ex, method);
        public void InitBinary128<T>(ExpressionType ex, Func<Vector128<T>, Vector128<T>, Vector128<T>> method)
            where T : unmanaged
            => InitBinary(ex, method);
        public void InitBinary128<T1, T2>(ExpressionType ex, Func<Vector128<T1>, T2, Vector128<T1>> method)
            where T1 : unmanaged
            => InitBinary(ex, method);
        public void InitBinary128Forced<T1, T2N, T2A>(ExpressionType ex, Func<Vector128<T1>, T2N, Vector128<T1>> method)
            where T1 : unmanaged
            => _binaryOperations[(ex, typeof(Vector128<T1>), typeof(T2A))] = method.Method;

        public void InitBinary256<T1, T2, R>(ExpressionType ex, Func<Vector256<T1>, Vector256<T2>, Vector256<R>> method)
            where T1 : unmanaged
            where T2 : unmanaged
            where R : unmanaged
            => InitBinary(ex, method);
        public void InitBinary256<T>(ExpressionType ex, Func<Vector256<T>, Vector256<T>, Vector256<T>> method)
            where T : unmanaged
            => InitBinary(ex, method);
        public void InitBinary256Forced<T1, T2N, T2A>(ExpressionType ex, Func<Vector256<T1>, T2N, Vector256<T1>> method)
            where T1 : unmanaged
            => _binaryOperations[(ex, typeof(Vector256<T1>), typeof(T2A))] = method.Method;
        #endregion
        #region Custom
        public void InitUnary128<T>(Func<T, T> scalar, Func<Vector128<T>, Vector128<T>> vector)
            where T: struct
            => _methodTable[scalar.Method] = vector.Method;
        public void InitBinary128<T>(Func<T, T, T> scalar, Func<Vector128<T>, Vector128<T>, Vector128<T>> vector)
            where T : struct
            => _methodTable[scalar.Method] = vector.Method;
        public void InitUnary256<T>(Func<T, T> scalar, Func<Vector256<T>, Vector256<T>> vector)
            where T : struct
            => _methodTable[scalar.Method] = vector.Method;
        public void InitBinary256<T>(Func<T, T, T> scalar, Func<Vector256<T>, Vector256<T>, Vector256<T>> vector)
            where T : struct
            => _methodTable[scalar.Method] = vector.Method;

        #endregion
        #region Conditional
        public void InitConditional<T, A>(Func<T, A, A, A> conditional) 
            => _conditionalTable[(typeof(T), typeof(A))] = conditional.Method;
        public void InitConditional128<T>(Func<Vector128<T>, Vector128<T>, Vector128<T>, Vector128<T>> conditional)
            where T : unmanaged
            => InitConditional(conditional);
        public void InitConditional256<T>(Func<Vector256<T>, Vector256<T>, Vector256<T>, Vector256<T>> conditional)
            where T : unmanaged
            => InitConditional(conditional);

        #endregion 
        #endregion
    }

    public unsafe class Vector2Info : VectorInfo
    {
        public Vector2Info()
        {
            if (Sse2.IsSupported)
            {
                InitType128<long>();
                InitType128<ulong>();
                InitType128<double>();

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

                InitBinary128Forced<long, byte, int>(ExpressionType.RightShift, Sse2.ShiftRightLogical); // ?? should be arithmetic
                InitBinary128Forced<ulong, byte, int>(ExpressionType.RightShift, Sse2.ShiftRightLogical);

                InitBinary128Forced<long, byte, int>(ExpressionType.LeftShift, Sse2.ShiftLeftLogical);
                InitBinary128Forced<ulong, byte, int>(ExpressionType.LeftShift, Sse2.ShiftLeftLogical);
            }
            if (Sse41.IsSupported)
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
    //public class Sse2Ex
    //{
    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public static Vector128<long> Negate(Vector128<long> arg)
    //        =>Avx2.MultiplyLow(arg, )
    //}
    public unsafe class Vector4Info: VectorInfo
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Info()
        {
            InitType32<byte>();
            InitStore<byte>(Vector32.Store);
            InitLoadAndConvert<byte>(Vector32.Load);
            InitLift<byte>(Vector32.Create);

            if (Sse.IsSupported)
            {
                InitType128<float>();

                InitStore<float>(Sse.Store);
                InitLoadAndConvert<float>(Sse.LoadVector128);

                InitLift<float>(Vector128.Create);

                InitBinary128<float>(ExpressionType.Add, Sse.Add);
                InitBinary128<float>(ExpressionType.Subtract, Sse.Subtract);
                InitBinary128<float>(ExpressionType.Multiply, Sse.Multiply);
                InitBinary128<float>(ExpressionType.Divide, Sse.Divide);
            }
            if (Sse2.IsSupported)
            {
                InitType128<int>();
                InitType128<uint>();

                InitStore<int>(Sse2.Store);
                InitStore<uint>(Sse2.Store);

                InitLoadAndConvert<int>(Sse2.LoadVector128);
                InitLoadAndConvert<uint>(Sse2.LoadVector128);

                InitLift<int>(Vector128.Create);
                InitLift<uint>(Vector128.Create);

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

                InitBinary128Forced<int, byte, int>(ExpressionType.RightShift, Sse2.ShiftRightArithmetic);
                InitBinary128Forced<uint, byte, int>(ExpressionType.RightShift, Sse2.ShiftRightLogical);

                InitBinary128Forced<int, byte, int>(ExpressionType.LeftShift, Sse2.ShiftLeftLogical);
                InitBinary128Forced<uint, byte, int>(ExpressionType.LeftShift, Sse2.ShiftLeftLogical);

            }

            if (Sse41.IsSupported)
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

            if (Avx.IsSupported)
            {
                InitType256<long>();
                InitType256<ulong>();
                InitType256<double>();

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

                InitLift<long>(Vector256.Create);
                InitLift<ulong>(Vector256.Create);
                InitLift<double>(Vector256.Create);

                InitConvert<double, float>(Avx.ConvertToVector128Single);
                InitConvert<float, double>(Avx.ConvertToVector256Double);
                InitConvert<int, double>(Avx.ConvertToVector256Double);
                InitConvert<double, int>(Avx.ConvertToVector128Int32);
                InitConvert<Vector256<long>, Vector256<double>>(ConvertToVector256Double);
                // conversion from long to double requires the AVX-512 support
                // https://www.felixcloutier.com/x86/vcvtqq2pd
                InitUnary256<double>(Math.Sqrt, Avx.Sqrt);

                InitBinary256<double>(ExpressionType.Add, Avx.Add);
                InitBinary256<double>(ExpressionType.Subtract, Avx.Subtract);
                InitBinary256<double>(ExpressionType.Multiply, Avx.Multiply);
                InitBinary256<double>(ExpressionType.Divide, Avx.Divide);

                InitBinary256<double>(ExpressionType.LessThan, LessThan);
                InitBinary256<double>(ExpressionType.LessThanOrEqual, LessThanOrEqual);
                InitBinary256<double>(ExpressionType.GreaterThan, GreaterThan);
                InitBinary256<double>(ExpressionType.GreaterThanOrEqual, GreaterThanOrEqual);

                InitBinary256<double>(Math.Max, Avx.Max);
                InitBinary256<double>(Math.Min, Avx.Min);

                InitConditional256<double>(Avx.BlendVariable);
                InitConditional<Vector256<double>, Vector32<byte>>(Vector32.DoubleConditional);

                InitUnary256<double>(ExpressionType.Negate, Negate);
            }

            if (Avx2.IsSupported)
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

                InitBinary256Forced<long, byte, int>(ExpressionType.RightShift, Avx2.ShiftRightLogical); // ?? Should be arithmetic? Puzzled.
                InitBinary256Forced<ulong, byte, int>(ExpressionType.RightShift, Avx2.ShiftRightLogical);

                InitBinary256Forced<long, byte, int>(ExpressionType.LeftShift, Avx2.ShiftLeftLogical);
                InitBinary256Forced<ulong, byte, int>(ExpressionType.LeftShift, Avx2.ShiftLeftLogical);

                InitConditional256<long>(Avx2.BlendVariable);
                InitConditional256<ulong>(Avx2.BlendVariable);
            }
        }
        //public static Vector256<double> CreateVector256Double(byte value)
        //    => Vector256.Create((double)value);
        public static Vector256<double> LessThan(Vector256<double> l, Vector256<double> r)
            => Avx.Compare(l, r, FloatComparisonMode.OrderedLessThanSignaling);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> GreaterThan(Vector256<double> l, Vector256<double> r)
            => Avx.Compare(l, r, FloatComparisonMode.OrderedGreaterThanSignaling);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> LessThanOrEqual(Vector256<double> l, Vector256<double> r)
            => Avx.Compare(l, r, FloatComparisonMode.OrderedLessThanOrEqualSignaling);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> GreaterThanOrEqual(Vector256<double> l, Vector256<double> r)
            => Avx.Compare(l, r, FloatComparisonMode.OrderedGreaterThanOrEqualSignaling);
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
    public unsafe class Vector8Info : VectorInfo
    {
        public Vector8Info()
        {
            if (Sse2.IsSupported)
            {
                InitType128<short>();
                InitType128<ushort>();

                InitStore<short>(Sse2.Store);
                InitStore<ushort>(Sse2.Store);

                InitLoadAndConvert<short>(Sse2.LoadVector128);
                InitLoadAndConvert<ushort>(Sse2.LoadVector128);

                InitBinary128<short>(ExpressionType.Add, Sse2.Add);
                InitBinary128<ushort>(ExpressionType.Add, Sse2.Add);

                InitBinary128<short>(ExpressionType.Subtract, Sse2.Subtract);
                InitBinary128<ushort>(ExpressionType.Subtract, Sse2.Subtract);

                InitBinary128<short>(ExpressionType.Multiply, Sse2.MultiplyLow);
                InitBinary128<ushort>(ExpressionType.Multiply, Sse2.MultiplyLow);

                InitBinary128<short>(ExpressionType.ExclusiveOr, Sse2.Xor);
                InitBinary128<ushort>(ExpressionType.ExclusiveOr, Sse2.Xor);

                InitBinary128<short>(ExpressionType.And, Sse2.And);
                InitBinary128<ushort>(ExpressionType.And, Sse2.And);

                InitBinary128<short>(ExpressionType.Or, Sse2.Or);
                InitBinary128<ushort>(ExpressionType.Or, Sse2.Or);
                InitBinary128Forced<short, byte, int>(ExpressionType.RightShift, Sse2.ShiftRightArithmetic);
                InitBinary128Forced<ushort, byte, int>(ExpressionType.RightShift, Sse2.ShiftRightLogical);
                InitBinary128Forced<short, byte, int>(ExpressionType.LeftShift, Sse2.ShiftLeftLogical);
                InitBinary128Forced<ushort, byte, int>(ExpressionType.LeftShift, Sse2.ShiftLeftLogical);
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
                InitConvert256<int, float>(Avx.ConvertToVector256Single);
                InitConvert256<float, int>(Avx.ConvertToVector256Int32);

                InitBinary256<float>(ExpressionType.Add, Avx.Add);
                InitBinary256<float>(ExpressionType.Subtract, Avx.Subtract);
                InitBinary256<float>(ExpressionType.Multiply, Avx.Multiply);
                InitBinary256<float>(ExpressionType.Divide, Avx.Divide);

                InitBinary256Forced<int, byte, int>(ExpressionType.RightShift, Avx2.ShiftRightArithmetic);
                InitBinary256Forced<uint, byte, int>(ExpressionType.RightShift, Avx2.ShiftRightLogical);

                InitBinary256Forced<int, byte, int>(ExpressionType.LeftShift, Avx2.ShiftLeftLogical);
                InitBinary256Forced<uint, byte, int>(ExpressionType.LeftShift, Avx2.ShiftLeftLogical);

                InitUnary256<float>(ExpressionType.Negate, Negate);
            }
            if (Avx2.IsSupported)
            {
                InitLoadAndConvert<byte, int>(Avx2.ConvertToVector256Int32);
                InitLoadAndConvert<sbyte, int>(Avx2.ConvertToVector256Int32);
                InitLoadAndConvert<short, int>(Avx2.ConvertToVector256Int32);
                InitLoadAndConvert<ushort, int>(Avx2.ConvertToVector256Int32);



                InitBinary256<int>(ExpressionType.Add, Avx2.Add);
                InitBinary256<uint>(ExpressionType.Add, Avx2.Add);

                InitBinary256<int>(ExpressionType.Subtract, Avx2.Subtract);
                InitBinary256<uint>(ExpressionType.Subtract, Avx2.Subtract);

                InitBinary256<int>(ExpressionType.Multiply, Avx2.MultiplyLow);
                InitBinary256<uint>(ExpressionType.Multiply, Avx2.MultiplyLow);

                InitUnary256<int>(ExpressionType.Negate, Negate);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<int> Negate(Vector256<int> a) => Avx2.MultiplyLow(a, Vector256.Create(-1));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<float> Negate(Vector256<float> a) => Avx.Multiply(a, Vector256.Create(-1.0f));
    }
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
    public unsafe class Vector32Info : VectorInfo
    {
        public Vector32Info()
        {
            if (Sse.IsSupported)
            {
            }
            if (Sse2.IsSupported)
            {
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
                InitType256<byte>();
                InitType256<sbyte>();

                InitStore<byte, Vector256<byte>>(Avx.Store);
                InitStore<sbyte, Vector256<sbyte>>(Avx.Store);

                InitLoadAndConvert<byte>(Avx.LoadVector256);
                InitLoadAndConvert<sbyte>(Avx.LoadVector256);
            }
            if (Avx2.IsSupported)
            {
            }
        }
    }

    public class VectorData
    {
        private static Dictionary<int, IVectorInfo> _vectorInfo = new Dictionary<int, IVectorInfo>()
        {
            [2] = new Vector2Info(),
            [4] = new Vector4Info(),
            [8] = new Vector8Info(),
            [16] = new Vector16Info(),
            [32] = new Vector32Info()
        };

        public static IReadOnlyDictionary<int, IVectorInfo> VectorInfo { get => _vectorInfo; }

        public static IReadOnlyDictionary<MethodInfo, MethodInfo> MethodTable { get; } = new Dictionary<MethodInfo, MethodInfo>();



        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector256<long> Negate(Vector256<long> a) => Avx2.MultiplyLow(a, Vector256.Create(-1L));



        [ExcludeFromCodeCoverage]
        public static R Load<T, R>(T[,] array, int i, int j, int size)
            => throw new NotImplementedException();

    }

    namespace Fake
    {
        public struct Vector32<T> where T : struct
        {
            //[FieldOffset(0)]
            //private byte _b0;
            //[FieldOffset(1)]
            //private byte _b1;
            //[FieldOffset(2)]
            //private byte _b2;
            //[FieldOffset(3)]
            //private byte _b3;
            //[FieldOffset(0)]
            //private short _s0;
            //[FieldOffset(2)]
            //private short _s1;
            //[FieldOffset(0)]
            internal int _i;
            internal Vector32(byte b0, byte b1, byte b2, byte b3) =>
                _i = (b3 << 24) | (b2 << 16) | (b1 << 8) | b0;
            internal void Init(short s0, short s1) =>
                _i = ((ushort)s1 << 16) | (ushort)s0;
            internal void Init(short s)
                => Init(s, s);
            internal void InitScalar(short s)
                => _i = s;
            internal Vector32(int i) => _i = i;
        }
        public static class Vector32
        {
            public static Vector32<byte> Create(byte b) 
                => new Vector32<byte>(b, b, b, b);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe void Store(byte* address, Vector32<byte> data)
                => *(int*)address = data._i;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe Vector32<byte> Load(byte* address)
            {
                var r = new Vector32<byte>();
                r._i = *(int*)address;
                return r;
            }

            public static Vector32<byte> ConvertToVector32Byte(Vector128<int> data)
            {
                var t = data.AsByte();
                return new Vector32<byte>(t.GetElement(0), t.GetElement(4), t.GetElement(8), t.GetElement(12));
            }
            public static Vector32<byte> ConvertToVector32Byte(Vector256<long> data)
            {
                var t = data.AsByte();
                return new Vector32<byte>(t.GetElement(0), t.GetElement(8), t.GetElement(16), t.GetElement(24));
            }

            public static Vector32<byte> DoubleConditional(Vector256<double> boolean, Vector32<byte> ifTrue, Vector32<byte> ifFalse)
            {
                int s = ConvertToVector32Byte(boolean.AsInt64())._i;
                return new Vector32<byte>((s & ifTrue._i) | (~s & ifFalse._i));
            }
        }
    }
}

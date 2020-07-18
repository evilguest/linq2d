using System;
using System.Collections.Generic;
using System.Reflection;

using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Numerics;

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
        public void InitType128<T>()
            where T: struct
            => _vectorTable[typeof(T)] = typeof(Vector128<T>);
        public void InitType256<T>()
            where T : struct
            => _vectorTable[typeof(T)] = typeof(Vector256<T>);
        #endregion
        #region Store
        unsafe public delegate void Store<T, D>(T* address, D data) where T : unmanaged;

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
        public void InitLoadAndConvert<T, R>(Load<T, Vector128<R>> method)
            where T : unmanaged
            where R : unmanaged
            => _loadAndConvertOperations[(typeof(T), typeof(R))] = method.Method;
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
        public void InitLift<T>(Func<T, Vector128<T>> method)
            where T : struct
            => _liftOperations[typeof(T)] = method.Method;
        public void InitLift<T>(Func<T, Vector256<T>> method)
            where T : struct
            => _liftOperations[typeof(T)] = method.Method;

        public void InitConvert<T, R>(Func<Vector128<T>, Vector256<R>> method)
            where T : struct
            where R : struct
            => InitConvert<Vector128<T>, Vector256<R>>(method);
        public void InitConvert<T, R>(Func<Vector256<T>, Vector128<R>> method)
            where T : struct
            where R : struct
            => InitConvert<Vector256<T>, Vector128<R>>(method);
        public void InitConvert128<T, R>(Func<Vector128<T>, Vector128<R>> method)
            where T : struct
            where R : struct
            => InitConvert<Vector128<T>, Vector128<R>>(method);
        public void InitConvert256<T, R>(Func<Vector256<T>, Vector256<R>> method)
            where T : struct
            where R : struct
            => InitConvert<Vector256<T>, Vector256<R>>(method);
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
        public void InitBinary128Forced<T1, T2>(ExpressionType ex, Func<Vector128<T1>, T2, Vector128<T1>> method)
            where T1 : unmanaged
            => _binaryOperations[(ex, typeof(Vector128<T1>), typeof(T2))] = method.Method;

        public void InitBinary256<T1, T2, R>(ExpressionType ex, Func<Vector256<T1>, Vector256<T2>, Vector256<R>> method)
            where T1 : unmanaged
            where T2 : unmanaged
            where R : unmanaged
            => InitBinary(ex, method);
        public void InitBinary256<T>(ExpressionType ex, Func<Vector256<T>, Vector256<T>, Vector256<T>> method)
            where T : unmanaged
            => InitBinary(ex, method);
        public void InitBinary256Forced<T1, T2>(ExpressionType ex, Func<Vector256<T1>, T2, Vector256<T1>> method)
            where T1 : unmanaged
            => _binaryOperations[(ex, typeof(Vector256<T1>), typeof(T2))] = method.Method;
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

                InitBinary128Forced<long, int>(ExpressionType.RightShift, (t, s)=>Sse2.ShiftRightLogical(t, (byte)s));
                InitBinary128Forced<ulong, int>(ExpressionType.RightShift, (t, s) => Sse2.ShiftRightLogical(t, (byte)s));

                InitBinary128Forced<long, int>(ExpressionType.LeftShift, (t, s) => Sse2.ShiftRightLogical(t, (byte)s));
                InitBinary128Forced<ulong, int>(ExpressionType.LeftShift, (t, s) => Sse2.ShiftRightLogical(t, (byte)s));
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
            if (Avx2.IsSupported)
            {
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
        public Vector4Info()
        {
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

                InitBinary128Forced<int, int>(ExpressionType.RightShift, ShiftRightLogical);
                InitBinary128Forced<uint, int>(ExpressionType.RightShift, ShiftRightLogical);

                InitBinary128Forced<int, int>(ExpressionType.LeftShift, ShiftLeftLogical);
                InitBinary128Forced<uint, int>(ExpressionType.LeftShift, ShiftLeftLogical);

            }
            if (Sse3.IsSupported)
            {
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
            if (Sse42.IsSupported)
            {

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
                InitLoadAndConvert((byte* address) => Avx.ConvertToVector256Double(Sse41.ConvertToVector128Int32(address)));
                InitLoadAndConvert((sbyte* address) => Avx.ConvertToVector256Double(Sse41.ConvertToVector128Int32(address)));
                InitLoadAndConvert((short* address) => Avx.ConvertToVector256Double(Sse41.ConvertToVector128Int32(address)));
                InitLoadAndConvert((ushort* address) => Avx.ConvertToVector256Double(Sse41.ConvertToVector128Int32(address)));

                InitLift<long>(Vector256.Create);
                InitLift<ulong>(Vector256.Create);
                InitLift<double>(Vector256.Create);

                InitConvert<double, float>(Avx.ConvertToVector128Single);
                InitConvert<float, double>(Avx.ConvertToVector256Double);
                InitConvert<int, double>(Avx.ConvertToVector256Double);
                InitConvert<double, int>(Avx.ConvertToVector128Int32);
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

                InitBinary256Forced<long, int>(ExpressionType.RightShift, ShiftRightLogical);
                InitBinary256Forced<ulong, int>(ExpressionType.RightShift, ShiftRightLogical);

                InitBinary256Forced<long, int>(ExpressionType.LeftShift, ShiftLeftLogical);
                InitBinary256Forced<ulong, int>(ExpressionType.LeftShift, ShiftLeftLogical);

                InitConditional256<long>(Avx2.BlendVariable);
                InitConditional256<ulong>(Avx2.BlendVariable);
            }

            static Vector256<double> LessThan(Vector256<double> l, Vector256<double> r)
                => Avx.Compare(l, r, FloatComparisonMode.OrderedLessThanSignaling);
            static Vector256<double> GreaterThan(Vector256<double> l, Vector256<double> r)
                => Avx.Compare(l, r, FloatComparisonMode.OrderedGreaterThanSignaling);
            static Vector256<double> LessThanOrEqual(Vector256<double> l, Vector256<double> r)
                => Avx.Compare(l, r, FloatComparisonMode.OrderedLessThanOrEqualSignaling);
            static Vector256<double> GreaterThanOrEqual(Vector256<double> l, Vector256<double> r)
                => Avx.Compare(l, r, FloatComparisonMode.OrderedGreaterThanOrEqualSignaling);

        }

        private static Vector256<long> ShiftRightLogical(Vector256<long> t, int s) => Avx2.ShiftRightLogical(t, (byte)s);
        private static Vector256<ulong> ShiftRightLogical(Vector256<ulong> t, int s) => Avx2.ShiftRightLogical(t, (byte)s);
        private static Vector256<int> ShiftRightLogical(Vector256<int> t, int s) => Avx2.ShiftRightLogical(t, (byte)s);
        private static Vector256<uint> ShiftRightLogical(Vector256<uint> t, int s) => Avx2.ShiftRightLogical(t, (byte)s);
        private static Vector256<short> ShiftRightLogical(Vector256<short> t, int s) => Avx2.ShiftRightLogical(t, (byte)s);
        private static Vector256<ushort> ShiftRightLogical(Vector256<ushort> t, int s) => Avx2.ShiftRightLogical(t, (byte)s);

        private static Vector128<long> ShiftRightLogical(Vector128<long> t, int s) => Avx2.ShiftRightLogical(t, (byte)s);
        private static Vector128<ulong> ShiftRightLogical(Vector128<ulong> t, int s) => Avx2.ShiftRightLogical(t, (byte)s);
        private static Vector128<int> ShiftRightLogical(Vector128<int> t, int s) => Avx2.ShiftRightLogical(t, (byte)s);
        private static Vector128<uint> ShiftRightLogical(Vector128<uint> t, int s) => Avx2.ShiftRightLogical(t, (byte)s);
        private static Vector128<short> ShiftRightLogical(Vector128<short> t, int s) => Avx2.ShiftRightLogical(t, (byte)s);
        private static Vector128<ushort> ShiftRightLogical(Vector128<ushort> t, int s) => Avx2.ShiftRightLogical(t, (byte)s);
        private static Vector256<long> ShiftLeftLogical(Vector256<long> t, int s) => Avx2.ShiftLeftLogical(t, (byte)s);
        private static Vector256<ulong> ShiftLeftLogical(Vector256<ulong> t, int s) => Avx2.ShiftLeftLogical(t, (byte)s);
        private static Vector256<int> ShiftLeftLogical(Vector256<int> t, int s) => Avx2.ShiftLeftLogical(t, (byte)s);
        private static Vector256<uint> ShiftLeftLogical(Vector256<uint> t, int s) => Avx2.ShiftLeftLogical(t, (byte)s);
        private static Vector256<short> ShiftLeftLogical(Vector256<short> t, int s) => Avx2.ShiftLeftLogical(t, (byte)s);
        private static Vector256<ushort> ShiftLeftLogical(Vector256<ushort> t, int s) => Avx2.ShiftLeftLogical(t, (byte)s);

        private static Vector128<long> ShiftLeftLogical(Vector128<long> t, int s) => Avx2.ShiftLeftLogical(t, (byte)s);
        private static Vector128<ulong> ShiftLeftLogical(Vector128<ulong> t, int s) => Avx2.ShiftLeftLogical(t, (byte)s);
        private static Vector128<int> ShiftLeftLogical(Vector128<int> t, int s) => Avx2.ShiftLeftLogical(t, (byte)s);
        private static Vector128<uint> ShiftLeftLogical(Vector128<uint> t, int s) => Avx2.ShiftLeftLogical(t, (byte)s);
        private static Vector128<short> ShiftLeftLogical(Vector128<short> t, int s) => Avx2.ShiftLeftLogical(t, (byte)s);
        private static Vector128<ushort> ShiftLeftLogical(Vector128<ushort> t, int s) => Avx2.ShiftLeftLogical(t, (byte)s);
    }
    public unsafe class Vector8Info : VectorInfo
    {
        public Vector8Info()
        {
            if (Sse.IsSupported)
            {
            }
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
            }
        }
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
            }
        }
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
        public static MethodInfo GetLoadAndConvertOperation256(Type sourceType, Type targetType)
        {
            MethodInfo result = null;
            _lac256.TryGetValue((sourceType, targetType), out result);
            return result;
        }
        public MethodInfo GetLoadAndConvertOperation128(Type sourceType)
        {
            throw new NotSupportedException();
        }


        private static readonly Dictionary<(Type sourceType, Type targetType), MethodInfo> _lac256 = new Dictionary<(Type sourceType, Type targetType), MethodInfo>();
        public static IReadOnlyDictionary<(Type sourceType, Type targetType), MethodInfo> LoadAndConvertTable { get => _lac256; }


        private static void InitLoadAndConvertCall256<T, R>()
            where R : unmanaged
            where T : unmanaged
        {
            Type typeT = typeof(T);
            Type typeR = typeof(R);
            string name;
            Type type;
            if(typeT == typeR)
            {
                type = typeof(Avx);
                name = "LoadVector256";
            }
            else
            {
                type = typeof(Avx2);
                name = $"ConvertToVector256{typeR.Name}";
            }
            _lac256[(typeT, typeR)] = type.GetMethod(name, new[] { typeT.MakePointerType() });
        }

        private static readonly Dictionary<(Type sourceType, Type targetType), MethodInfo> _c256 = new Dictionary<(Type sourceType, Type targetType), MethodInfo>();
        public static IReadOnlyDictionary<(Type sourceType, Type targetType), MethodInfo> ConvertTable { get => _c256; }

        private static void InitConvertCall<T, R>(Func<Vector256<T>, Vector256<R>> conversion)
            where T: unmanaged
            where R: unmanaged
        {
            _c256[(typeof(T), typeof(R))] = conversion.Method;
        }
        private static void InitConvertCall256<T, R>()
            where R : unmanaged
            where T : unmanaged
        {
            Type typeT = typeof(T);
            Type typeR = typeof(R);
            var m = typeof(Avx2).GetMethod($"ConvertToVector256{typeR.Name}", new[] { typeof(Vector128<T>) });
            if (m == null)
                m = typeof(Avx).GetMethod($"ConvertToVector256{typeR.Name}", new[] { typeof(Vector256<T>) });
            _c256[(typeT, typeR)] = m;

        }

        private static readonly Dictionary<MethodInfo, MethodInfo> _methodTable = new Dictionary<MethodInfo, MethodInfo>();
        public static IReadOnlyDictionary<MethodInfo, MethodInfo> MethodTable { get => _methodTable; }
        private static void InitMethodCall<T>(Func<T, T> scalar, Func<Vector256<T>, Vector256<T>> vector)
            where T : unmanaged
            => _methodTable[scalar.Method] = vector.Method;

        private static void InitMethodCall<T>(Func<T, T, T> scalar, Func<Vector256<T>, Vector256<T>, Vector256<T>> vector)
            where T : unmanaged
            => _methodTable[scalar.Method] = vector.Method;

        private static void InitMethodCall<T, R>(Func<T, R> scalar, Func<Vector256<T>, Vector256<R>> vector)
            where T : unmanaged
            where R : unmanaged 
            => _methodTable[scalar.Method] = vector.Method;

        private static void InitMethodCall<T1, T2, R>(Func<T1, T2, R> scalar, Func<Vector256<T1>, Vector256<T2>, Vector256<R>> vector)
            where T1 : unmanaged
            where T2 : unmanaged
            where R : unmanaged
            => _methodTable[scalar.Method] = vector.Method;
        

        private static readonly Dictionary<Type, MethodInfo> _l256 = new Dictionary<Type, MethodInfo>();
        public static IReadOnlyDictionary<Type, MethodInfo> LoadTable { get => _l256; }

        private static readonly Dictionary<(ExpressionType ex, Type l, Type r), MethodInfo> _binaryOperations = new Dictionary<(ExpressionType, Type, Type), MethodInfo>();
        public static IReadOnlyDictionary<(ExpressionType ex, Type l, Type r), MethodInfo> BinaryOperations { get => _binaryOperations; }
        public static IReadOnlyDictionary<(ExpressionType ex, Type o), MethodInfo> UnaryOperations { get => _unaryOperations; }

        private static readonly Dictionary<Type, MethodInfo> _s256 = new Dictionary<Type, MethodInfo>();
        private static readonly Dictionary<(ExpressionType ex, Type o), MethodInfo> _unaryOperations = new Dictionary<(ExpressionType ex, Type o), MethodInfo>();

        public static IReadOnlyDictionary<Type, MethodInfo> StoreTable { get => _s256; }

        private static void InitOperationTable<T1, T2, R>(ExpressionType et, Expression<Func<T1, T2, R>> method)
        {
            Expression e = method.Body;
            if (e is BlockExpression be)
                e = be.Expressions[0];
            if (e is MethodCallExpression mce)
                _binaryOperations[(et, typeof(T1), typeof(T2))] = mce.Method;
            else
                throw new ArgumentException($"Invalid expression type passed to {nameof(InitOperationTable)}", nameof(method));
        }

        private static void InitOperationTable<T, R>(ExpressionType et, Expression<Func<T, R>> method)
        {
            Expression e = method.Body;
            if (e is BlockExpression be)
                e = be.Expressions[0];
            if (e is MethodCallExpression mce)
                _unaryOperations[(et, typeof(T))] = mce.Method;
            else
                throw new ArgumentException($"Invalid expression type passed to {nameof(InitOperationTable)}", nameof(method));
        }


        private static void InitOperationTable256<T>(ExpressionType et, string methodName = null)
        {
            methodName = methodName ?? et.ToString();
            var typeT = typeof(T);
            var vectorT = typeof(Vector256<>).MakeGenericType(typeT);
            MethodInfo m = null;
            if (Avx.IsSupported)
                m = typeof(Avx).GetMethod(methodName, new[] { vectorT, vectorT });
            if (m == null && Avx2.IsSupported)
                m = typeof(Avx2).GetMethod(methodName, new[] { vectorT, vectorT });

            if (m != null)
                _binaryOperations[(et, vectorT, vectorT)] = m;
        }

        private static void InitLoadCall256<T>()
        {
            Type typeT = typeof(T);
            if(Avx.IsSupported)
                _l256[typeT] = typeof(Avx).GetMethod("LoadVector256", new[] { typeT.MakePointerType() });

        }

        private static void InitStoreCall256<T>()
        {
            Type typeT = typeof(T);
            if (Avx.IsSupported)
                _s256[typeT] = typeof(Avx).GetMethod("Store", new[] { typeT.MakePointerType(), typeof(Vector256<>).MakeGenericType(typeT)});
        }

        static VectorData()
        {
            InitLoadAndConvertCall256<byte, byte>();
            InitLoadAndConvertCall256<byte, short>();
            InitLoadAndConvertCall256<byte, int>();
            InitLoadAndConvertCall256<byte, long>();

            InitLoadAndConvertCall256<sbyte, sbyte>();
            InitLoadAndConvertCall256<sbyte, short>();
            InitLoadAndConvertCall256<sbyte, int>();
            InitLoadAndConvertCall256<sbyte, long>();

            InitLoadAndConvertCall256<ushort, ushort>();
            InitLoadAndConvertCall256<ushort, int>();
            InitLoadAndConvertCall256<ushort, long>();

            InitLoadAndConvertCall256<short, short>();
            InitLoadAndConvertCall256<short, int>();
            InitLoadAndConvertCall256<short, long>();

            InitLoadAndConvertCall256<uint, uint>();
            InitLoadAndConvertCall256<uint, long>();

            InitLoadAndConvertCall256<int, int>();
            InitLoadAndConvertCall256<int, long>();

            InitLoadAndConvertCall256<ulong, ulong>();
            InitLoadAndConvertCall256<long, long>();

            InitLoadAndConvertCall256<float, float>();
            InitLoadAndConvertCall256<double, double>();


            //InitConvertCall<ushort, int>(Avx2.ConvertToVector256Int64);

            InitConvertCall256<ushort, int>();
            InitConvertCall256<short, int>();
            InitConvertCall256<byte, int>();
            InitConvertCall256<sbyte, int>();
            InitConvertCall256<ushort, int>();

            InitConvertCall256<ushort, long>();
            InitConvertCall256<short, long>();
            InitConvertCall256<byte, long>();
            InitConvertCall256<sbyte, long>();
            InitConvertCall256<ushort, long>();

            InitLoadCall256<byte>();
            InitLoadCall256<sbyte>();
            InitLoadCall256<ushort>();
            InitLoadCall256<short>();
            InitLoadCall256<uint>();
            InitLoadCall256<int>();
            InitLoadCall256<ulong>();
            InitLoadCall256<long>();
            InitLoadCall256<float>();
            InitLoadCall256<double>();

            InitStoreCall256<byte>();
            InitStoreCall256<sbyte>();
            InitStoreCall256<ushort>();
            InitStoreCall256<short>();
            InitStoreCall256<uint>();
            InitStoreCall256<int>();
            InitStoreCall256<ulong>();
            InitStoreCall256<long>();
            InitStoreCall256<float>();
            InitStoreCall256<double>();

            InitOperationTable256<byte>(ExpressionType.Add);
            InitOperationTable256<sbyte>(ExpressionType.Add);
            InitOperationTable256<ushort>(ExpressionType.Add);
            InitOperationTable256<short>(ExpressionType.Add);
            InitOperationTable256<uint>(ExpressionType.Add);
            InitOperationTable256<int>(ExpressionType.Add);
            InitOperationTable256<ulong>(ExpressionType.Add);
            InitOperationTable256<long>(ExpressionType.Add);
            InitOperationTable256<float>(ExpressionType.Add);
            InitOperationTable256<double>(ExpressionType.Add);

            InitOperationTable256<byte>(ExpressionType.Subtract);
            InitOperationTable256<sbyte>(ExpressionType.Subtract);
            InitOperationTable256<ushort>(ExpressionType.Subtract);
            InitOperationTable256<short>(ExpressionType.Subtract);
            InitOperationTable256<uint>(ExpressionType.Subtract);
            InitOperationTable256<int>(ExpressionType.Subtract);
            InitOperationTable256<ulong>(ExpressionType.Subtract);
            InitOperationTable256<long>(ExpressionType.Subtract);
            InitOperationTable256<float>(ExpressionType.Subtract);
            InitOperationTable256<double>(ExpressionType.Subtract);

            InitOperationTable256<byte>(ExpressionType.Multiply, "MultiplyLow");
            InitOperationTable256<sbyte>(ExpressionType.Multiply, "MultiplyLow");
            InitOperationTable256<ushort>(ExpressionType.Multiply, "MultiplyLow");
            InitOperationTable256<short>(ExpressionType.Multiply, "MultiplyLow");
            InitOperationTable256<uint>(ExpressionType.Multiply, "MultiplyLow");
            InitOperationTable256<int>(ExpressionType.Multiply, "MultiplyLow");
            InitOperationTable256<ulong>(ExpressionType.Multiply, "MultiplyLow");
            InitOperationTable256<long>(ExpressionType.Multiply, "MultiplyLow");
            InitOperationTable256<float>(ExpressionType.Multiply, "MultiplyLow");
            InitOperationTable256<double>(ExpressionType.Multiply, "MultiplyLow");

            InitOperationTable256<float>(ExpressionType.Divide);
            InitOperationTable256<double>(ExpressionType.Divide);

            InitOperationTable(ExpressionType.RightShift, (Vector256<short> a, int b)=>Avx2.ShiftRightArithmetic(a, (byte)b));
            InitOperationTable(ExpressionType.RightShift, (Vector256<ushort> a, int b) => Avx2.ShiftRightLogical(a, (byte)b));
            InitOperationTable(ExpressionType.RightShift, (Vector256<int> a, int b) => Avx2.ShiftRightArithmetic(a, (byte)b));
            InitOperationTable(ExpressionType.RightShift, (Vector256<uint> a, int b) => Avx2.ShiftRightLogical(a, (byte)b));
            InitOperationTable(ExpressionType.RightShift, (Vector256<long> a, int b) => Avx2.ShiftRightLogical(a, (byte)b));
            InitOperationTable(ExpressionType.RightShift, (Vector256<ulong> a, int b) => Avx2.ShiftRightLogical(a, (byte)b));

            InitOperationTable(ExpressionType.LeftShift, (Vector256<short> a, int b) => Avx2.ShiftLeftLogical(a, (byte)b));
            InitOperationTable(ExpressionType.LeftShift, (Vector256<ushort> a, int b) => Avx2.ShiftLeftLogical(a, (byte)b));
            InitOperationTable(ExpressionType.LeftShift, (Vector256<int> a, int b) => Avx2.ShiftLeftLogical(a, (byte)b));
            InitOperationTable(ExpressionType.LeftShift, (Vector256<uint> a, int b) => Avx2.ShiftLeftLogical(a, (byte)b));
            InitOperationTable(ExpressionType.LeftShift, (Vector256<long> a, int b) => Avx2.ShiftLeftLogical(a, (byte)b));
            InitOperationTable(ExpressionType.LeftShift, (Vector256<ulong> a, int b) => Avx2.ShiftLeftLogical(a, (byte)b));

            InitOperationTable(ExpressionType.Negate, (Vector256<short> a) => Negate(a));
            InitOperationTable(ExpressionType.Negate, (Vector256<int> a) => Negate(a));
            InitOperationTable(ExpressionType.Negate, (Vector256<float> a) => Negate(a));
            InitOperationTable(ExpressionType.Negate, (Vector256<double> a) => Negate(a));

            InitMethodCall<double>(Math.Sqrt, Avx.Sqrt);

            InitMethodCall<byte>(Math.Min, Avx2.Min);
            InitMethodCall<sbyte>(Math.Min, Avx2.Min);
            InitMethodCall<short>(Math.Min, Avx2.Min);
            InitMethodCall<ushort>(Math.Min, Avx2.Min);
            InitMethodCall<int>(Math.Min, Avx2.Min);
            InitMethodCall<uint>(Math.Min, Avx2.Min);
            InitMethodCall<float>(Math.Min, Avx.Min);
            InitMethodCall<double>(Math.Min, Avx.Min);

            InitMethodCall<byte>(Math.Max, Avx2.Max);
            InitMethodCall<sbyte>(Math.Max, Avx2.Max);
            InitMethodCall<short>(Math.Max, Avx2.Max);
            InitMethodCall<ushort>(Math.Max, Avx2.Max);
            InitMethodCall<int>(Math.Max, Avx2.Max);
            InitMethodCall<uint>(Math.Max, Avx2.Max);
            InitMethodCall<float>(Math.Max, Avx.Max);
            InitMethodCall<double>(Math.Max, Avx.Max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<short> Negate(Vector256<short> a) => Avx2.MultiplyLow(a, Vector256.Create((short)-1));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<int> Negate(Vector256<int> a) => Avx2.MultiplyLow(a, Vector256.Create(-1));

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector256<long> Negate(Vector256<long> a) => Avx2.MultiplyLow(a, Vector256.Create(-1L));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<float> Negate(Vector256<float> a) => Avx.Multiply(a, Vector256.Create(-1.0f));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> Negate(Vector256<double> a) => Avx.Multiply(a, Vector256.Create(-1.0));
        public static R Load<T, R>(T[,] array, int i, int j)
            => throw new NotImplementedException();

    }
}

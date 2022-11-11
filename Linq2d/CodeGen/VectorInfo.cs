using System;
using System.Collections.Generic;
using System.Reflection;

using System.Runtime.Intrinsics;

using System.Linq.Expressions;
using Linq2d.CodeGen.Fake;
using Linq2d.CodeGen.Intrinsics;

namespace Linq2d.CodeGen
{
    public abstract class VectorInfo : IVectorInfo
    {
        public VectorInfo()
        {
            if (Sse.IsSupported)
            {
                InitSse();
                Available = true;
            }
            if (Sse2.IsSupported)
            {
                InitSse2();
                Available = true;
            }
            if (Sse41.IsSupported)
            {
                InitSse41();
                Available = true;
            }
            if (Avx.IsSupported)
            {
                InitAvx();
                Available = true;
            }
            if (Avx2.IsSupported)
            {
                InitAvx2();
                Available = true;
            }
        }
        public bool Available { get;protected set; }
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
            where T : struct
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
        private void InitConvert<T, R>(Func<T, R> method)
            => _convertOperations[(typeof(T), typeof(R))] = method.Method;

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
            => InitConvert(method);
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
            where T : struct
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

        #region Overridables
        protected virtual void InitSse() { }
        protected virtual void InitSse2() { }
        protected virtual void InitSse41() { }
        protected virtual void InitAvx2() { }
        protected virtual void InitAvx() { }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;


using System.Linq.Expressions;

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
}

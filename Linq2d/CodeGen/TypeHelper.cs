using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Linq2d.CodeGen
{
    public static class TypeHelper
    {
        internal static bool TryGetConditionalMethod(this IReadOnlyDictionary<Type, MethodInfo> conditionals, Type resultType, out MethodInfo method, out Type testType)
        {
            var found = conditionals.TryGetValue(resultType, out method);
            testType = found ? method.GetParameters()[2].ParameterType : default;
            return found;
        } 
        private static ConcurrentDictionary<Type, bool> cachedTypes = new ConcurrentDictionary<Type, bool>();
        public static bool IsUnmanaged(this Type t)
        {
            var result = false;
            if (cachedTypes.ContainsKey(t))
                return cachedTypes[t];

            if (t.IsPrimitive || t.IsPointer || t.IsEnum)
                result = true;
            else if (t.IsGenericTypeDefinition || !t.IsValueType)
                result = false;
            else
                result = t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .All(f => f.FieldType.IsUnmanaged());
            cachedTypes[t] = result;
            return result;
        }
        public static bool IsSigned(this Type t)
        {
            return Type.GetTypeCode(t) switch
            {
                TypeCode.SByte => true,
                TypeCode.Int16 => true,
                TypeCode.Int32 => true,
                TypeCode.Int64 => true,
                TypeCode.Double => true,
                TypeCode.Single => true,
                _ => t.GetMethod("operator -", BindingFlags.Static, new Type[] { t }) != null
            };
        }

        public static bool IsNumeric(this Type type)
        {
            //if (type == null)
            //    return false;

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return true;
                case TypeCode.Object:
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                        return Nullable.GetUnderlyingType(type).IsNumeric();
                    return false;
            }
            return false;
        }
        public static bool IsInstanceOfSameGeneric(this Type type, Type genericType)
        {
            genericType = genericType.IsGenericType ? genericType.GetGenericTypeDefinition() : genericType;
            return type.IsGenericType && type.GetGenericTypeDefinition() == genericType;
        }
    }
}

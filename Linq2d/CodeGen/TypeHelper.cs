﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Linq2d.CodeGen
{
    static class TypeHelper
    {
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

        public static bool IsNumeric(this Type type)
        {
            if (type == null)
                return false;

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace Linq2d.CodeGen
{
    class KernelCompilerScalar : KernelCompiler
    {
        public KernelCompilerScalar(ILGenerator generator, ParameterExpression width) : base(generator, width)
        {
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if(node.Method.IsConstructedGenericMethod && node.Method.GetGenericMethodDefinition() == typeof(VectorData).GetMethod(nameof(VectorData.Load)))
            {
                var pArray = Visit(node.Arguments[0]);
                var itemType = node.Arguments[0].Type.GetElementType();

                Load2dPointerOffset(itemType, node.Arguments[1], node.Arguments[2]);
                
                int vectorSize = (int)(node.Arguments[3] as ConstantExpression).Value;
                var m = VectorData.VectorInfo[vectorSize].LoadAndConvertOperations[(itemType, node.Type.GetGenericArguments()[0])];

                Generator.Call(m);
                return node;
            }
            else
            {
                var r = base.VisitMethodCall(node); // prepare arguments
                Generator.Call(node.Method);
                return r;
            }
        }
        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Type == typeof(int) ||
                node.Type == typeof(uint) ||
                node.Type == typeof(short) ||
                node.Type == typeof(sbyte) ||
                node.Type == typeof(byte) ||
                node.Type == typeof(ushort))
            {
                var value = Convert.ToInt32(node.Value);
                Generator.Ldc(value);
                return node;
            }
            if (node.Type == typeof(long))
            {
                Generator.Emit(OpCodes.Ldc_I8, (long)node.Value);
                return node;
            }
            if (node.Type == typeof(ulong))
            {
                Generator.Emit(OpCodes.Ldc_I8, (long)(ulong)node.Value);
                return node;
            }

            if (node.Type == typeof(float))
            {
                Generator.Emit(OpCodes.Ldc_R4, (float)node.Value);
                return node;
            }
            if (node.Type == typeof(double))
            {
                Generator.Emit(OpCodes.Ldc_R8, (double)node.Value);
                return node;
            }
            if (node.Type == typeof(string))
            {
                Generator.Emit(OpCodes.Ldstr, (string)node.Value);
                return node;
            }

            throw new InvalidOperationException($"Can't handle a constant of type {node.Type}");
        }
        private IReadOnlyDictionary<Type, OpCode> _convertTable = new Dictionary<Type, OpCode>()
        {
            {typeof(byte),   OpCodes.Conv_U1},
            {typeof(sbyte),  OpCodes.Conv_I1},
            {typeof(int),    OpCodes.Conv_I4},
            {typeof(uint),   OpCodes.Conv_U4},
            {typeof(short),  OpCodes.Conv_I2},
            {typeof(ushort), OpCodes.Conv_U2},
            {typeof(long),   OpCodes.Conv_I8},
            {typeof(ulong),  OpCodes.Conv_U8},
            {typeof(float),  OpCodes.Conv_R4},
            {typeof(double), OpCodes.Conv_R8},
        };

        protected override Expression VisitUnary(UnaryExpression node)
        {
            var expr = base.VisitUnary(node);
            if(node.Method != null)
            {
                Generator.Call(node.Method);
            }
            else switch (expr.NodeType)
            {
                case ExpressionType.Convert:
                    try
                    {
                        Generator.Emit(_convertTable[expr.Type]);
                    }
                    catch (KeyNotFoundException knfe)
                    {
                        throw new NotSupportedException($"Unknown conversion target type {expr.Type.Name}", knfe);
                    }
                    break;
                case ExpressionType.Negate:
                    Generator.Emit(OpCodes.Neg); break;
                case ExpressionType.Not:
                    Generator.Emit(OpCodes.Not); break;
                case ExpressionType.Throw:
                    Generator.Emit(OpCodes.Throw); break;
                default:
                    throw new InvalidOperationException($"Unknown unary node type {expr.NodeType}");
            }
            return expr;
        }
        protected override Expression VisitNew(NewExpression node)
        {
            var expr = base.VisitNew(node);
            Generator.Emit(OpCodes.Newobj, node.Constructor);
            return expr;
        }
        public static IReadOnlyDictionary<ExpressionType, OpCode> BinaryOpTable { get; } = new Dictionary<ExpressionType, OpCode>()
        {
            {ExpressionType.Add, OpCodes.Add },
            {ExpressionType.AddChecked, OpCodes.Add_Ovf },
            {ExpressionType.Multiply, OpCodes.Mul },
            {ExpressionType.MultiplyChecked, OpCodes.Mul_Ovf},
            {ExpressionType.Subtract, OpCodes.Sub },
            {ExpressionType.SubtractChecked, OpCodes.Sub_Ovf },
            {ExpressionType.Divide, OpCodes.Div },
            {ExpressionType.Modulo, OpCodes.Rem },
            {ExpressionType.LessThan, OpCodes.Clt },
            {ExpressionType.GreaterThan, OpCodes.Cgt },
            {ExpressionType.Equal, OpCodes.Ceq },
            {ExpressionType.And, OpCodes.And },
            {ExpressionType.Or, OpCodes.Or },
            {ExpressionType.RightShift, OpCodes.Shr },
            {ExpressionType.GreaterThanOrEqual, OpCodes.Bge },
            {ExpressionType.LessThanOrEqual, OpCodes.Ble },
            {ExpressionType.AndAlso, OpCodes.Brfalse },
            {ExpressionType.OrElse, OpCodes.Brtrue },
            {ExpressionType.ArrayIndex, OpCodes.Ldelem},
        };
        public static IReadOnlyDictionary<Type, OpCode> LoadTable { get; } = new Dictionary<Type, OpCode>()
        {
            { typeof(sbyte), OpCodes.Ldind_I1 },
            { typeof(byte), OpCodes.Ldind_U1 },
            { typeof(short), OpCodes.Ldind_I2 },
            { typeof(ushort), OpCodes.Ldind_U2 },
            { typeof(int), OpCodes.Ldind_I4 },
            { typeof(uint), OpCodes.Ldind_U4 },
            { typeof(long), OpCodes.Ldind_I8 },
            { typeof(float), OpCodes.Ldind_R4 },
            { typeof(double), OpCodes.Ldind_R8 },
        };

        protected override Expression VisitIndex(IndexExpression node)
        {
            LoadItemAddress(node);
            if (LoadTable.ContainsKey(node.Type))
                Generator.Emit(LoadTable[node.Type]);
            else
                Generator.Emit(OpCodes.Ldobj, node.Type);
            return node;
        }
        protected override Expression VisitMember(MemberExpression node)
        {
            base.VisitMember(node); // loading the expression to stack
            if (node.Member is FieldInfo fi)
                Generator.Ldfld(fi);
            if (node.Member is PropertyInfo pi)
                Generator.Emit(OpCodes.Call, pi.GetGetMethod());
            return node;
        }
        protected override Expression VisitBinary(BinaryExpression node)
        {
            Expression ret; // will handle the operands
            if(node.Method != null)
            {
                Visit(node.Left);
                Visit(node.Right);
                Generator.Call(node.Method);
                return node;
            }
            switch (node.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.Multiply:
                case ExpressionType.Subtract:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.LessThan:
                case ExpressionType.GreaterThan:
                case ExpressionType.Equal:
                case ExpressionType.And:
                case ExpressionType.Or:
                case ExpressionType.RightShift:
                    ret = base.VisitBinary(node);
                    Generator.Emit(BinaryOpTable[node.NodeType]);
                    break;
                case ExpressionType.ArrayIndex:
                    ret = base.VisitBinary(node);
		    if(node.Type == typeof(byte) || node.Type == typeof(sbyte))
                        Generator.Emit(OpCodes.Ldelem_I1); 
                    else if (node.Type == typeof(short) || node.Type == typeof(ushort))
                        Generator.Emit(OpCodes.Ldelem_I2); 
                    else if (node.Type == typeof(int) || node.Type == typeof(uint))
                        Generator.Emit(OpCodes.Ldelem_I4); 
                    else if (node.Type == typeof(long) || node.Type == typeof(ulong))
                        Generator.Emit(OpCodes.Ldelem_I8); 
                    else if (node.Type == typeof(float))
                        Generator.Emit(OpCodes.Ldelem_R4); 
                    else if (node.Type == typeof(double))
                        Generator.Emit(OpCodes.Ldelem_R8); 
                    else 
                        Generator.Emit(OpCodes.Ldelem_Ref); 
                    break;

                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThanOrEqual:
                    var trueLabel = Generator.DefineLabel();
                    var endLabel = Generator.DefineLabel();
                    ret = base.VisitBinary(node);
                    Generator.Emit(BinaryOpTable[node.NodeType], trueLabel);
                    Generator.Ldc(0);
                    Generator.Br(endLabel);
                    Generator.MarkLabel(trueLabel);
                    Generator.Ldc(1);
                    Generator.MarkLabel(endLabel);
                    break;
                case ExpressionType.AndAlso:
                case ExpressionType.OrElse:
                    var l = Visit(node.Left);
                    var skipLabel = Generator.DefineLabel();
                    Generator.Emit(OpCodes.Dup);
                    Generator.Emit(BinaryOpTable[node.NodeType], skipLabel);
                    Generator.Emit(OpCodes.Pop);
                    Visit(node.Right);
                    Generator.MarkLabel(skipLabel);

                    ret = node;
                    break;

                default: throw new InvalidOperationException($"Cannot compile the binary expression of type {node.NodeType}");
            }
            return ret;
        }
    }
}

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
            var r = base.VisitMethodCall(node); // prepare arguments
            Generator.Call(node.Method);
            return r;
        }
        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Type == typeof(int) ||
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
            switch (expr.NodeType)
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
            { ExpressionType.MultiplyChecked, OpCodes.Mul_Ovf},
            {ExpressionType.Subtract, OpCodes.Sub },
            {ExpressionType.SubtractChecked, OpCodes.Sub_Ovf },
            {ExpressionType.Divide, OpCodes.Div },
            {ExpressionType.Modulo, OpCodes.Rem },
            {ExpressionType.LessThan, OpCodes.Clt },
            {ExpressionType.GreaterThan, OpCodes.Cgt },
            {ExpressionType.Equal, OpCodes.Ceq },
            {ExpressionType.And, OpCodes.And },
            {ExpressionType.Or, OpCodes.Or },
            {ExpressionType.RightShift, OpCodes.Shr }
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
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThanOrEqual:
                    var trueLabel = Generator.DefineLabel();
                    var endLabel = Generator.DefineLabel();
                    ret = base.VisitBinary(node);
                    Generator.Emit(node.NodeType == ExpressionType.GreaterThanOrEqual ? OpCodes.Bge : OpCodes.Ble, trueLabel);
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
                    Generator.Emit(node.NodeType == ExpressionType.AndAlso ? OpCodes.Brfalse : OpCodes.Brtrue, skipLabel);
                    Generator.Emit(OpCodes.Pop);
                    var r = Visit(node.Right);
                    Generator.MarkLabel(skipLabel);

                    ret = node;
                    break;



                default: throw new InvalidOperationException($"Cannot compile the binary expression of type {node.NodeType}");
            }
            return ret;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace Linq2d.CodeGen
{
    class KernelCompilerVector : KernelCompilerScalar
    {
        private HashSet<Expression> _vectorNodes;
        public KernelCompilerVector(ILGenerator generator,
                                    ParameterExpression width,
                                    HashSet<Expression> vectorNodes) : base(generator, width)
            => _vectorNodes = vectorNodes ?? throw new ArgumentNullException(nameof(vectorNodes));

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (IsVector(node))
            {
                switch (node.NodeType)
                {
                    case ExpressionType.Convert:
                        {
                            if (node.Operand is IndexExpression ie && VectorData.GetLoadAndConvertOperation256(ie.Type, node.Type) != null)
                            {
                                LoadItemAddress(ie);
                                Generator.Call(VectorData.GetLoadAndConvertOperation256(ie.Type, node.Type));
                                return Expression.Parameter(MakeVector256(node.Type));// Expression.MakeUnary(node.NodeType, node.Operand, );
                            }
                            var operand = Visit(node.Operand);
                            try
                            {
                                var mi = VectorData.ConvertTable[(node.Operand.Type, node.Type)];
                                Generator.Call(mi);
                                return Expression.Convert(node, mi.ReturnType, mi);
                            }
                            catch (KeyNotFoundException knfe)
                            {
                                throw new NotSupportedException($"No vector conversion found from {node.Operand.Type.Name} to {node.Type.Name}", knfe);
                            }
                        }
                    default:
                        {
                            var operand = base.Visit(node.Operand); // push the value to the stack
                            System.Reflection.MethodInfo mi = VectorData.UnaryOperations[(node.NodeType, node.Operand.Type)];
                            Generator.Call(mi);
                            return Expression.MakeUnary(node.NodeType, operand, null, mi);
                        }
                }
            }
            return base.VisitUnary(node);
        }
        private static bool IsVector256(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Vector256<>);
        private static Type MakeVector256(Type t) => typeof(Vector256<>).MakeGenericType(t);
        private bool IsVector(Expression node) => _vectorNodes.Contains(node);
        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.Multiply:
                case ExpressionType.Subtract:
                case ExpressionType.Divide:
                case ExpressionType.LessThan:
                case ExpressionType.GreaterThan:
                case ExpressionType.Equal:
                case ExpressionType.And:
                case ExpressionType.Or:
                    if(IsVector(node))
                    {
                        var left = Visit(node.Left); // loaded the left arg to the stack
                        if (!IsVector(node.Left)) // left is scalar - extend it to a vector
                            left = ConvertToVector(left);

                        var right = Visit(node.Right);

                        if (!IsVector(node.Right))// right is a scalar - extend it to the vector
                            right = ConvertToVector(right);

                        var mi = VectorData.BinaryOperations[(node.NodeType, left.Type, right.Type)];
                        Generator.Call(mi);
                        return Expression.MakeBinary(node.NodeType, left, right, false, mi);
                    }
                    goto default;
                case ExpressionType.LeftShift:
                case ExpressionType.RightShift:
                    if (IsVector(node))
                    {
                        var left = Visit(node.Left);
                        if (!IsVector(node.Left))
                            left = ConvertToVector(left);
                        var right = Visit(node.Right);
                        var mi = VectorData.BinaryOperations[(node.NodeType, left.Type, right.Type)];
                        Generator.Call(mi);

                        if (right.Type != mi.GetParameters()[1].ParameterType) // scalar case
                            right = Expression.Convert(right, mi.GetParameters()[1].ParameterType);

                        return Expression.MakeBinary(node.NodeType, left, right, false, mi);

                    }
                    goto default;

                default:
                    return base.VisitBinary(node); // handle scalar op
            }
        }

        private Expression ConvertToVector(Expression left)
        {
            var ci = typeof(Vector256).GetMethod("Create", new[] { left.Type });
            Generator.Call(ci);
            left = Expression.Convert(left, ci.ReturnType, ci);
            return left;
        }

        protected override Expression VisitIndex(IndexExpression node)
        {
            if (IsVector(node))
            {
                LoadItemAddress(node);
                var mi = VectorData.LoadTable[node.Type];
                Generator.Call(mi);
                return Expression.Parameter(mi.ReturnType);
            }
            return base.VisitIndex(node);
        }
    }
}

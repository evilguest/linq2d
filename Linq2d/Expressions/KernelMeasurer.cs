using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;

namespace Linq2d.Expressions
{
    class KernelMeasurer : ExpressionVisitor
    {
        private Dictionary<ParameterExpression, (int minX, int maxX, int minY, int maxY)> _accesses = new Dictionary<ParameterExpression, (int minX, int maxX, int minY, int maxY)>();
        protected override Expression VisitUnary(UnaryExpression node)
        {
            //if (node.NodeType == ExpressionType.Convert && node.Operand.Type.IsGenericType && node.Operand.Type.GetGenericTypeDefinition() == typeof(Cell<>))
            // visit [0,0]
            return base.VisitUnary(node);
        }

        public IReadOnlyDictionary<ParameterExpression, (int minX, int maxX, int minY, int maxY)> Accesses { get => _accesses; }
        public (int minX, int maxX, int minY, int maxY) MergedAccesses
        {
            get
            {
                (int minX, int maxX, int minY, int maxY) access = (0, 0, 0, 0);

                foreach (var acc in Accesses.Values)
                    access = Merge(access, acc);
                return access;
            }
        }
        private static (int minX, int maxX, int minY, int maxY) Merge((int minX, int maxX, int minY, int maxY) left, (int minX, int maxX, int minY, int maxY) right)
        {
            return (
                left.minX < right.minX ? left.minX : right.minX,
                left.maxX > right.maxX ? left.maxX : right.maxX,
                left.minY < right.minY ? left.minY : right.minY,
                left.maxY > right.maxY ? left.maxY : right.maxY
                );
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Object is ParameterExpression pe)
            { 
                var dt = node.Method.DeclaringType;
                if (dt.IsGenericType && dt.GetGenericTypeDefinition() == typeof(RelativeCell<>)) // bingo!
                {
                    int x = 0, y = 0;
                    switch (node.Arguments[0])
                    {
                        case ConstantExpression ceX:
                            x = (int)ceX.Value; break;
                        case ConditionalExpression condX when condX.Test is BinaryExpression be:
                            var posRanges = Ranges.No;
                            var negRanges = Ranges.No;
                            switch (be.NodeType)
                            {
                                case ExpressionType.LessThanOrEqual:
                                    posRanges.Add(be.Right, be.Left, null).Add(be.Left, null, be.Right);
                                    negRanges.Add(be.Left, Add(be.Right, Constant(1)), null).Add(be.Right, null, Subtract(be.Left, Constant(1)));
                                    break;
                                case ExpressionType.LessThan:
                                    posRanges.Add(be.Right, Add(be.Left, Constant(1)), null).Add(be.Left, null, Subtract(be.Right, Constant(1)));
                                    negRanges.Add(be.Left, be.Right, null).Add(be.Right, null, be.Left);
                                    break;
                                case ExpressionType.GreaterThanOrEqual:
                                    posRanges.Add(be.Left, be.Right, null).Add(be.Right, null, be.Left);
                                    negRanges.Add(be.Right, Add(be.Left, Constant(1)), null).Add(be.Left, null, Subtract(be.Right, Constant(1)));
                                    break;
                                case ExpressionType.GreaterThan:
                                    posRanges.Add(be.Left, Add(be.Right, Constant(1)), null).Add(be.Right, null, Subtract(be.Left, Constant(1)));
                                    negRanges.Add(be.Right, be.Left, null).Add(be.Left, null, be.Right);
                                    break;
                                default: throw new IndexOutOfRangeException($"cannot prove that access to {pe.Name} is within the array bounds");
                            }
                            var t = Arithmetic.Simplify(condX.IfTrue, posRanges);
                            if (t is ConstantExpression ceTrue)
                                RegisterAccessX(pe, (int)ceTrue.Value);
                            //else
                            //    break;// goto default;

                            var f = Arithmetic.Simplify(condX.IfFalse, negRanges);
                            if (f is ConstantExpression ceFalse)
                                RegisterAccessX(pe, (int)ceFalse.Value);

                            break;
                        default: throw new IndexOutOfRangeException($"cannot prove that access to {pe.Name} is within the array bounds");
                    }

                    switch (node.Arguments[1])
                    {
                        case ConstantExpression ceY:
                            y = (int)ceY.Value; break;
                        case ConditionalExpression condY when condY.Test is BinaryExpression be:
                            var posRanges = Ranges.No;
                            var negRanges = Ranges.No;
                            switch (be.NodeType)
                            {
                                case ExpressionType.LessThanOrEqual:
                                    posRanges.Add(be.Right, be.Left, null).Add(be.Left, null, be.Right);
                                    negRanges.Add(be.Left, Add(be.Right, Constant(1)), null).Add(be.Right, null, Subtract(be.Left, Constant(1)));
                                    break;
                                case ExpressionType.LessThan:
                                    posRanges.Add(be.Right, Add(be.Left, Constant(1)), null).Add(be.Left, null, Subtract(be.Right, Constant(1)));
                                    negRanges.Add(be.Left, be.Right, null).Add(be.Right, null, be.Left);
                                    break;
                                case ExpressionType.GreaterThanOrEqual:
                                    posRanges.Add(be.Left, be.Right, null).Add(be.Right, null, be.Left);
                                    negRanges.Add(be.Right, Add(be.Left, Constant(1)), null).Add(be.Left, null, Subtract(be.Right, Constant(1)));
                                    break;
                                case ExpressionType.GreaterThan:
                                    posRanges.Add(be.Left, Add(be.Right, Constant(1)), null).Add(be.Right, null, Subtract(be.Left, Constant(1)));
                                    negRanges.Add(be.Right, be.Left, null).Add(be.Left, null, be.Right);
                                    break;
                                default: throw new IndexOutOfRangeException($"cannot prove that access to {pe.Name} is within the array bounds");
                            }
                            var t = Arithmetic.Simplify(condY.IfTrue, posRanges);
                            if (t is ConstantExpression ceTrue)
                                RegisterAccessY(pe, (int)ceTrue.Value);
                            //else
                            //    break;// goto default;

                            var f = Arithmetic.Simplify(condY.IfFalse, negRanges);
                            if (f is ConstantExpression ceFalse)
                                RegisterAccessY(pe, (int)ceFalse.Value);
                            break;// goto default;
                        default: throw new IndexOutOfRangeException($"cannot prove that access to {pe.Name} is within the array bounds");
                    }
                    RegisterAccess(pe, x, y);
                }
            }
            return base.VisitMethodCall(node);
        }

        private void RegisterAccess(ParameterExpression pe, int x, int y)
        {
            var a = (minX: x, maxX: x, minY: y, maxY: y);
            if (_accesses.ContainsKey(pe))
            {
                a = _accesses[pe];
                a.minX = Math.Min(a.minX, x);
                a.maxX = Math.Max(a.maxX, x);
                a.minY = Math.Min(a.minY, y);
                a.maxY = Math.Max(a.maxY, y);
            }
            _accesses[pe] = a;
        }
        private void RegisterAccessX(ParameterExpression pe, int x)
        {
            var a = (minX: x, maxX: x, minY: 0, maxY: 0);
            if (_accesses.ContainsKey(pe))
            {
                a = _accesses[pe];
                a.minX = Math.Min(a.minX, x);
                a.maxX = Math.Max(a.maxX, x);
            }
            _accesses[pe] = a;
        }
        private void RegisterAccessY(ParameterExpression pe, int y)
        {
            var a = (minX: 0, maxX: 0, minY: y, maxY: y);
            if (_accesses.ContainsKey(pe))
            {
                a = _accesses[pe];
                a.minY = Math.Min(a.minY, y);
                a.maxY = Math.Max(a.maxY, y);
            }
            _accesses[pe] = a;
        }

        private static (T min, T max) GetMinMax<T>(Expression expression)
            where T : IComparable<T>
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));
            if (expression.Type != typeof(T))
                throw new ArgumentException($"Type mismatch: Expression type {expression.Type} doesn't match the requested type {typeof(T)}");
            T r = default;
            return (r, r);
        }
    }
}

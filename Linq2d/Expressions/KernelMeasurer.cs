using Linq2d.CodeGen;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
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
            if (node.Object is ParameterExpression pe && node.Method.DeclaringType.IsInstanceOfSameGeneric(typeof(RelativeCell<>))) // bingo!
            {
                if (node.Arguments[0] is ConstantExpression ceX && node.Arguments[1] is ConstantExpression ceY)
                    RegisterAccess(pe, (int)ceX.Value, (int)ceY.Value);
                else
                    throw new IndexOutOfRangeException($"cannot prove that access to {pe.Name} is within the array bounds");
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
    }
}

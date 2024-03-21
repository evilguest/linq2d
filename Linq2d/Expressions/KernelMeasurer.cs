using Linq2d.CodeGen;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Linq2d.Expressions
{
    class KernelMeasurer : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, AccessRange> _accesses = [];
        public IReadOnlyDictionary<ParameterExpression, AccessRange> Accesses { get => _accesses; }
/*        protected override Expression VisitUnary(UnaryExpression node)
        {
            //if (node.NodeType == ExpressionType.Convert && node.Operand.Type.IsGenericType && node.Operand.Type.GetGenericTypeDefinition() == typeof(Cell<>))
            // visit [0,0]
            return base.VisitUnary(node);
        }
*/
        public AccessRange MergedAccesses
        {
            get
            {
                AccessRange range = new (0, 0);

                foreach (var acc in Accesses.Values)
                    range &= acc;

                return range;
            }
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
            AccessRange range = new(x, y);
            _accesses[pe] = _accesses.TryGetValue(pe, out var existingRange) 
                ? range & existingRange 
                : range;
        }
    }

    internal record struct AccessRange(int MinX, int MaxX, int MinY, int MaxY)
    {
        public AccessRange(int x, int y):this(x, x, y, y) {}
        public static AccessRange operator &(AccessRange left, AccessRange right) 
            => new(
                Math.Min(left.MinX, right.MinX),
                Math.Max(left.MaxX, right.MaxX),
                Math.Min(left.MinY, right.MinY),
                Math.Max(left.MaxY, right.MaxY)
            );
    }
}

using System.Collections.Generic;
using System.Linq.Expressions;

namespace Linq2d.Expressions
{
    class ExpressionCounter : ExpressionVisitor
    {
        public static Dictionary<Expression, (int usageCount, int cost)> Evaluate(Expression expression)
        {
            var c = new ExpressionCounter();
            c.Visit(expression);
            return c.Expressions;
        }

        private ExpressionCounter() : base()
        {

        }
        private Dictionary<Expression, (int usageCount, int cost)> Expressions { get; } = new Dictionary<Expression, (int usageCount, int cost)>(new CodeComparer());
        private int _cost = 0;
        public override Expression Visit(Expression node)
        {
            if (node == null)
                return node;

            var cost = _cost;
            base.Visit(node);

            if (!Expressions.ContainsKey(node))
                Expressions[node] = (1, _cost - cost); // cost delta covers the cost of all children
            else
            {
                var (usages, oldCost) = Expressions[node];
                Expressions[node] = (usages + 1, oldCost);
            }
            _cost += 1;
            return node;
        }
    }
}

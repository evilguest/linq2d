using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Linq2d.Expressions
{
    class DependencyChecker(IEnumerable<Expression> parameters) : ExpressionVisitor
    {
        public static Expression FindInvariant(Expression expression, IEnumerable<Expression> parameters)
        {
            var dc = new DependencyChecker(parameters);
            dc.Visit(expression);
            return dc._invariants.Count switch
            {
                0 => null,
                1 => dc._invariants.Single().expr,
                _ => dc._invariants.OrderByDescending(_ => _.cost).First().expr,
            };
        }

        private readonly HashSet<(int cost, Expression expr)> _invariants = [];
        private readonly HashSet<Expression> _parameters = new(parameters ?? throw new System.ArgumentNullException(nameof(parameters)));
        //private static CodeComparer _comparer = new CodeComparer();
        private bool _depends = false;
        private int _cost = 0;

        public override Expression Visit(Expression node)
        {
            if (node == null)
                return node;

            if (_parameters.Contains(node)) // we're one of the parameters
            {
                _depends = true;
                return node;
            }
            _cost++;
            var cost = _cost;
            var depends = _depends;
            _depends = false;
            base.Visit(node);
            if(!_depends && (_cost-cost)>0)
                _invariants.Add((_cost-cost, node));
            _depends |= depends;
            return node;
        }
        protected override Expression VisitBinary(BinaryExpression node)
        {
            base.VisitBinary(node);
            if (_depends && node.NodeType == ExpressionType.Assign)
                _parameters.Add(node.Left);
            return node;
        }
    }
}

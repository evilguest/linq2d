using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Linq2d.Expressions
{
    class DependencyChecker: ExpressionVisitor
    {
        public static Expression FindInvariant(Expression expression, IEnumerable<Expression> parameters)
        {
            var dc = new DependencyChecker(parameters);
            dc.Visit(expression);
            switch (dc._invariants.Count)
            {
                case 0: return null;
                case 1: return dc._invariants.Single().expr;
                default: return dc._invariants.OrderByDescending(_ => _.cost).First().expr;
            }
        }

        private HashSet<(int cost, Expression expr)> _invariants = new HashSet<(int cost, Expression expr)>();
        private HashSet<Expression> _parameters;
        private static CodeComparer _comparer = new CodeComparer();
        private bool _depends = false;
        private int _cost = 0;
        public DependencyChecker(IEnumerable<Expression> parameters)
            => _parameters = new HashSet<Expression>(parameters ?? throw new System.ArgumentNullException(nameof(parameters)));

        public override Expression Visit(Expression node)
        {
            if (node == null)
                return node;

            if (_parameters.Contains(node)) // we're one of the parameters
            {
                _depends = true;
                return node;
            }

            var cost = _cost;
            var depends = _depends;
            _depends = false;
            base.Visit(node);
            if(!_depends && (_cost-cost)>0)
                _invariants.Add((_cost-cost, node));
            _depends = depends;
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

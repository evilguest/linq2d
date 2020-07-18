using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Linq2d.Expressions
{

    internal class ExpressionReplacer : ExpressionVisitor
    {
        public static Expression Replace(Expression expression, Expression from, Expression to, IEqualityComparer<Expression> comparer=null)
        {
            return new ExpressionReplacer(from, to, comparer).Visit(expression);
        }

        private readonly Expression _from;
        private readonly Expression _to;
        private readonly IEqualityComparer<Expression> _comparer;

        private ExpressionReplacer(Expression from, Expression to, IEqualityComparer<Expression> comparer)
        {
            _from = from ?? throw new ArgumentNullException(nameof(from));
            _to = to ?? throw new ArgumentNullException(nameof(to));
            _comparer = comparer ?? EqualityComparer<Expression>.Default;
        }
        public override Expression Visit(Expression node)
        {
            if (_comparer.Equals(_from, node))
                return _to;
            else
                return base.Visit(node);
        }
    }


}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Linq2d.Expressions
{
    internal class VariablePool
    {
        public ParameterExpression GetVariable(Type variableType)
        {
            var e = (from v in _pool where v.Type == variableType select v).FirstOrDefault();

            if (e != null)
                _pool.Remove(e);
            else
                e = Expression.Parameter(variableType);

            _used.Add(e);

            return e;
        }

        public VariablePool(IEnumerable<ParameterExpression> variables)
        {
            if (variables != null)
                _pool.AddRange(variables);
        }
        public IEnumerable<ParameterExpression> Variables { get => _pool.Concat(_used); }
        private List<ParameterExpression> _pool = new List<ParameterExpression>();
        private List<ParameterExpression> _used = new List<ParameterExpression>();
    }
}

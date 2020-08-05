using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection.Metadata;

namespace Linq2d.Expressions
{

    class CommonSubexpressions
    {
        public static Expression Eliminate(Expression expression, ref IEnumerable<ParameterExpression> variablePool)
        {
            var pool = new VariablePool(variablePool);
            do
            {
                var q = from e in ExpressionCounter.Evaluate(expression)
                        where e.Value.usageCount > 1 && e.Value.cost > 0
                        orderby e.Value.usageCount descending, e.Value.cost descending
                        select e.Key;
                var mostExpensive = q.FirstOrDefault();
                if (mostExpensive == null)
                    break;
                // introduce a variable for mostExpensive
                var t = pool.GetVariable(mostExpensive.Type);
                // TODO: fix the order of the variables. Assignments should go after all depends
                expression = MergeBlocks(t, mostExpensive, ExpressionReplacer.Replace(expression, mostExpensive, t, new CodeComparer()));
            } while (true);
            variablePool = pool.Variables;
            return expression;
        }

        private static Expression MergeBlocks(ParameterExpression parameter, Expression paramInit, Expression expression)
        {
            if (expression is BlockExpression tail)
            {
                var variables = tail.Variables.Append(parameter);
                var i = 0;
                for (; i < tail.Expressions.Count; i++)
                    if (tail.Expressions[i].References(parameter))
                        break;
                var expressions = tail.Expressions.Take(i).Append(Expression.Assign(parameter, paramInit)).Concat(tail.Expressions.Skip(i));
                return Expression.Block(variables, expressions);
            }
            else
                return Expression.Block(new[] { parameter }, Expression.Assign(parameter, paramInit), expression);
        }

        private class ExpressionCounter : ExpressionVisitor
        {
            public static Dictionary<Expression, (int usageCount, int cost)> Evaluate(Expression expression)
            {
                var c = new ExpressionCounter();
                c.Visit(expression);
                return c.Expressions;
            }

            private ExpressionCounter():base()
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
                    Expressions[node] = (1, _cost-cost); // cost delta covers the cost of all children
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
}

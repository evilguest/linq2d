using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Linq.Expressions.Deconstruct;
using System.Runtime.InteropServices;
using System;

namespace Linq2d.Expressions
{
    static class CommonSubexpressions
    {
        public static Expression EliminateCommonSubexpressions(this Expression expression, ref IEnumerable<ParameterExpression> variablePool)
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
        public static bool DependsOn(this Expression expression, IEnumerable<ParameterExpression> variables)
        {
            return DependencyChecker.FindInvariant(expression, variables) == null;
        }
        public static BlockExpression GetInvariants(this IList<Expression> expressions, params ParameterExpression[] variables)
        {
            var invariants = new List<Expression>();
            var invariantVars = new List<ParameterExpression>();
            var vars = new HashSet<ParameterExpression>(variables);
            var movedVariables = new List<ParameterExpression>();
            for (var i = 0; i < expressions.Count; i++)
            {
                var ei = expressions[i];

                if (ei is BlockExpression be)
                {
                    foreach (var ee in be.Expressions)
                    {
                        if (ee is BinaryExpression ae && ae.NodeType == ExpressionType.Assign)
                        {
                            var left = (ParameterExpression)ae.Left;
                            var eei = DependencyChecker.FindInvariant(ae.Right, vars);
                            if (eei == ae.Right)
                            {
                                invariants.Add(ae);
                                invariantVars.Add(left);
                                ei = ExpressionReplacer.Replace(ei, ee, Expression.Empty());
                            }
                            else 
                            {
                                var ee1 = ee;
                                vars.Add(left);
                                while (eei != null)
                                {
                                    var t = Expression.Parameter(eei.Type);
                                    invariants.Add(Expression.Assign(t, eei));
                                    invariantVars.Add(t);
                                    var ee2 = ExpressionReplacer.Replace(ee1, eei, t);
                                    ei = ExpressionReplacer.Replace(ei, ee1, ee2);
                                    ee1 = ee2;
                                    eei = DependencyChecker.FindInvariant(ee1, vars);
                                }
                            }
                        }
                        else
                        {
                            var ee1 = ee;
                            var eei = DependencyChecker.FindInvariant(ee1, vars);
                            while (eei != null)
                            {
                                var t = Expression.Parameter(eei.Type);
                                invariants.Add(Expression.Assign(t, eei));
                                invariantVars.Add(t);
                                var ee2 = ExpressionReplacer.Replace(ee1, eei, t);
                                ei = ExpressionReplacer.Replace(ei, ee1, ee2);
                                ee1 = ee2;
                                eei = DependencyChecker.FindInvariant(ee1, vars);
                            }
                        }
                    }
                }
                else
                {
                    var eii = DependencyChecker.FindInvariant(ei, variables);
                    while (eii != null)
                    {
                        var t = Expression.Parameter(eii.Type);
                        invariants.Add(Expression.Assign(t, eii));
                        invariantVars.Add(t);
                        ei = ExpressionReplacer.Replace(ei, eii, t);
                        eii = DependencyChecker.FindInvariant(ei, variables);
                    }
                }
                expressions[i] = ei;
            }
            /*                var ei = DependencyChecker.FindInvariant(expressions[i], variables);
                            while (ei != null)
                            {
                                if (ei is BinaryExpression ae && ae.NodeType == ExpressionType.Assign)
                                {
                                    invariants.Add(ae);
                                    invariantVars.Add((ParameterExpression)ae.Left);
                                    expressions[i] = ExpressionReplacer.Replace(expressions[i], ae, Expression.Empty());
                                }
                                else
                                {
                                    var t = Expression.Parameter(ei.Type);
                                    invariants.Add(Expression.Assign(t, ei));
                                    invariantVars.Add(t);
                                    expressions[i] = ExpressionReplacer.Replace(expressions[i], ei, t, new CodeComparer());
                                }
                            }
                        */


            return Expression.Block(invariantVars, invariants);
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
    }
}

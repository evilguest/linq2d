using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Linq2d.Expressions
{
    using static Expression;
    using static Enumerable;

    public static class ExpressionHelper
    {
        public static NewExpression New<T>(Type t, T argument)
        {
            var c = t.GetConstructor(new[] { typeof(T) });
            return Expression.New(c, Constant(argument));
        }
        public static NewExpression New<T>(Expression<Func<T>> construct)
        {
            switch (construct.Body)
            {
                case NewExpression ne: return ne;
                case BlockExpression b when b.Expressions[0] is NewExpression ne: return ne;
                default: throw new ArgumentException($"Cannot find construction in the expression passed");
            }
        }
        public static MethodCallExpression Call<D>(Expression<D> call, params Expression[] arguments)
            where D: Delegate
        {
            switch (call.Body)
            {
                case MethodCallExpression mce: return mce.Update(mce.Object, arguments);
                case BlockExpression b when b.Expressions[0] is MethodCallExpression mce: return mce.Update(mce.Object, arguments);
                default: throw new ArgumentException($"Cannot find a method call in the expression passed");
            }
        }

        public static bool References(this Expression expression, Expression reference)
        {
            return ExpressionFinder.Find(expression, e => e == reference);
        }

        private class ExpressionFinder: ExpressionVisitor
        {
            public static bool Find(Expression expression, Predicate<Expression> predicate)
            {
                var f = new ExpressionFinder(predicate);
                f.Visit(expression);
                return f.Found;
            }
            private readonly Predicate<Expression> _predicate;

            public bool Found { get; private set; } = false;

            public ExpressionFinder(Predicate<Expression> predicate) 
                => _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));

            public override Expression Visit(Expression node)
            {
                Found |= _predicate(node);
                if (Found)
                    return node;

                return base.Visit(node);
            }
        }
        public static NewExpression New<T, E>(Expression<Func<T,E>> construct, Expression argument )
        {
            switch (construct.Body)
            {
                case NewExpression ne:
                    return ne.Update(new[] { argument });
                case BlockExpression b when b.Expressions[0] is NewExpression ne: 
                    return ne.Update(new[] { argument });
                default: throw new ArgumentException($"Cannot find construction in the expression passed");
            }
        }
        public static bool IsConstant<T>(this Expression e, T constant)
            where T : IEquatable<T> => e is ConstantExpression ce && ce.Type == typeof(T) && (constant != null && constant.Equals((T)ce.Value) || constant == null && ce.Value == null);
        public static BlockExpression MergeBlocks(Expression expr1, Expression expr2)
        {
            IEnumerable<ParameterExpression> variables = new ParameterExpression[0];
            IEnumerable<Expression> body = new Expression[0];

            if (expr1 is BlockExpression block1)
            {
                body = body.Concat(block1.Expressions);
                variables = variables.Concat(block1.Variables);
            }
            else
                body = body.Append(expr1);

            if (expr2 is BlockExpression block2)
            {
                body = body.Concat(block2.Expressions);
                variables = variables.Concat(block2.Variables);
            }
            else
                body = body.Append(expr2);

            return Block(variables.Distinct(), body);
        }

        public static Expression For(ParameterExpression loopVar, Expression initValue, Expression condition, Expression incrementValue, Expression loopContent)
        {
            var exitLabel = Label("LoopExit");

            return Block(new[] { loopVar },
                Assign(loopVar, initValue),
                Loop(
                    IfThenElse(
                        condition,
                        MergeBlocks(
                            loopContent,
                            AddAssign(loopVar, incrementValue)
                        ),
                        Break(exitLabel)
                    ),
                exitLabel)
            );
        }
    }
}

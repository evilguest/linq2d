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
        //public static NewExpression New<T>(Type t, T argument)
        //{
        //    var c = t.GetConstructor(new[] { typeof(T) });
        //    return Expression.New(c, Constant(argument));
        //}
        //public static NewExpression New<T>(Expression<Func<T>> construct)
        //{
        //    switch (construct.Body)
        //    {
        //        case NewExpression ne: return ne;
        //        case BlockExpression b when b.Expressions[0] is NewExpression ne: return ne;
        //        default: throw new ArgumentException($"Cannot find construction in the expression passed");
        //    }
        //}
        public static MethodCallExpression Call<T, R>(this Expression<Func<T, R>> call, params Expression[] arguments)
            =>
            call.Body switch
            {
                MethodCallExpression mce => mce.Update(mce.Object, arguments),
                BlockExpression b when b.Expressions[0] is MethodCallExpression mce => mce.Update(mce.Object, arguments),
                _ => throw new ArgumentException($"Cannot find a method call in the expression passed")
            };



        public static bool References(this Expression expression, Expression reference)
            => ExpressionFinder.Find(expression, e => e == reference);

        private class ExpressionFinder : ExpressionVisitor
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
        public static NewExpression New<T, E>(Expression<Func<T, E>> construct, Expression argument)
            => construct.Body switch
            {
                NewExpression ne => ne.Update(new[] { argument }),
                BlockExpression b when b.Expressions[0] is NewExpression ne => ne.Update(new[] { argument }),
                _ => throw new ArgumentException($"Cannot find construction in the expression passed")
            };

        //public static bool IsConstant<T>(this Expression e, T constant)
        //    where T : IEquatable<T> => e is ConstantExpression ce && ce.Type == typeof(T) && (constant != null && constant.Equals((T)ce.Value) || constant == null && ce.Value == null);
        public static BlockExpression AppendToBlock(BlockExpression block, BinaryExpression expr2)
        {
            IEnumerable<ParameterExpression> variables = block.Variables;
            IEnumerable<Expression> body = block.Expressions;

            body = body.Append(expr2);

            return Block(variables.Distinct(), body);
        }

        public static BlockExpression For(ParameterExpression loopVar, Expression initValue, Expression condition, Expression incrementValue, BlockExpression loopContent)
        {
            var exitLabel = Label("LoopExit");

            return Block(new[] { loopVar },
                Assign(loopVar, initValue),
                Loop(
                    IfThenElse(
                        condition,
                        AppendToBlock(
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

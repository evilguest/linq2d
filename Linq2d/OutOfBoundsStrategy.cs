using System;
using System.Linq.Expressions;
using System.Reflection;

using static System.Linq.Expressions.Expression;
using static Linq2d.Expressions.ExpressionHelper;

namespace Linq2d
{
    public class OutOfBoundsPolicy
    {
        public Expression Value { get; }
        public GetCoordinate Coordinate { get; } 
        public OutOfBoundsPolicy(Expression value, GetCoordinate coordinate)
        {
            Value = value;
            Coordinate = coordinate ?? throw new ArgumentNullException(nameof(coordinate));
        }

    }
    public abstract class OutOfBoundsStrategy
    {
        protected OutOfBoundsStrategy(OutOfBoundsPolicy all) : this(all, all) { }
        protected OutOfBoundsStrategy(OutOfBoundsPolicy below, OutOfBoundsPolicy above) : this(below, below, above, above) { }
        protected OutOfBoundsStrategy(OutOfBoundsPolicy xBelow, OutOfBoundsPolicy yBelow, OutOfBoundsPolicy xAbove, OutOfBoundsPolicy yAbove)
        {
            XBelow = xBelow ?? throw new ArgumentNullException(nameof(xBelow));
            XAbove = xAbove ?? throw new ArgumentNullException(nameof(xAbove));
            YBelow = yBelow ?? throw new ArgumentNullException(nameof(yBelow));
            YAbove = yAbove ?? throw new ArgumentNullException(nameof(yAbove));
        }

        public OutOfBoundsPolicy XBelow { get; }
        public OutOfBoundsPolicy XAbove { get; }
        public OutOfBoundsPolicy YBelow{ get; }
        public OutOfBoundsPolicy YAbove{ get; }

        public static T Throw<T>(Exception e) => throw e;
        public static Expression Throws(Type t) => Call(typeof(OutOfBoundsStrategy).GetMethod(nameof(Throw), BindingFlags.Public | BindingFlags.Static).MakeGenericMethod(t), New((string s) => new IndexOutOfRangeException(s), Constant("Index was out of range")));
        public static GetCoordinate Keep { get; } = (coordinate, upperBoundExclusive) => coordinate;
        public static GetCoordinate LimitHigh { get; } = (coordinate, upperBoundExclusive) => Subtract(upperBoundExclusive, Constant(1));
        public static GetCoordinate LimitLow { get; } = (coordinate, upperBoundExclusive) => Constant(0);
        public static GetCoordinate Const(int value) => (coordinate, upperBoundExclusive) => Constant(value);

        public static OutOfBoundsStrategyUntyped Substitute(object value)
            => new OutOfBoundsStrategyUntyped(new OutOfBoundsPolicy(Constant(value), Keep));

        public static OutOfBoundsStrategy<T> Substitute<T>(T value)
            => new OutOfBoundsStrategy<T>(new OutOfBoundsPolicy(Constant(value), Keep));
        public static OutOfBoundsStrategy<T> Default<T>()
            => new OutOfBoundsStrategy<T>(new OutOfBoundsPolicy(Throws(typeof(T)), Keep));
        public static OutOfBoundsStrategyUntyped NearestNeighbour { get; } 
            = new OutOfBoundsStrategyUntyped(
                new OutOfBoundsPolicy(null, LimitLow), 
                new OutOfBoundsPolicy(null, LimitHigh));
        public static OutOfBoundsStrategy<T> Integral<T>(T initValue)
            => new OutOfBoundsStrategy<T>(new OutOfBoundsPolicy(Constant(initValue), Const(-1)),
                new OutOfBoundsPolicy(null, LimitHigh));
    }
    public class OutOfBoundsStrategy<T> : OutOfBoundsStrategy {
        internal OutOfBoundsStrategy(OutOfBoundsPolicy all) : base(all) { }
        internal OutOfBoundsStrategy(OutOfBoundsPolicy below, OutOfBoundsPolicy above) : base(below, above) { }
        //protected OutOfBoundsStrategy(OutOfBoundsPolicy xBelow, OutOfBoundsPolicy xAbove, OutOfBoundsPolicy yBelow, OutOfBoundsPolicy yAbove) 
        //    : base(xBelow, xAbove, yBelow, yAbove) { }
    }
    public class OutOfBoundsStrategyUntyped: OutOfBoundsStrategy
    {
        internal OutOfBoundsStrategyUntyped(OutOfBoundsPolicy all) : base(all) { }
        internal OutOfBoundsStrategyUntyped(OutOfBoundsPolicy below, OutOfBoundsPolicy above) : base(below, above) { }
    }
    public delegate Expression GetCoordinate(Expression coordinate, Expression upperLimitExclusive);

    //public delegate Expression GetSubstExpression(Expression i, Expression j, Expression h, Expression w);
}

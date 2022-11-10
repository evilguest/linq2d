using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using Linq2d.Expressions;
using static System.Linq.Expressions.Expression;
using static Linq2d.Expressions.ExpressionHelper;
namespace Linq2d
{
    using GetCoordinate = Func<Expression, Expression, Expression>;
    public struct OutOfBoundsPolicy
    {
        public Expression Value { get; }
        public GetCoordinate Coordinate { get; }
        private OutOfBoundsPolicy(Expression value, GetCoordinate coordinate)
        {
            Value = value;
            Coordinate = coordinate;
        }
        private OutOfBoundsPolicy(Expression value) : this(value, DoKeep) { }
        public OutOfBoundsPolicy(GetCoordinate coordinate) : this(null, coordinate) { }
        public static OutOfBoundsPolicy ReplaceBy(object value) => new(Constant(value));
        public static OutOfBoundsPolicy ReplaceByAndMoveTo(object value, int coordinate) => new(Constant(value), ConstantCoordinate(coordinate));
        public static OutOfBoundsPolicy Throw<T>() => new(Throws<T>());

        public static OutOfBoundsPolicy LimitLow { get => new(DoLimitLow); }
        public static OutOfBoundsPolicy LimitHigh { get => new(DoLimitHigh); }

        public static Expression Throws<T>() => ExpressionHelper.Call((Exception e) => Throw<T>(e), New((string s) => new IndexOutOfRangeException(s), Constant("Index was out of range")));
        private static T Throw<T>(Exception e) => throw e;
        public static GetCoordinate DoKeep { get; } = (coordinate, upperBoundExclusive) => coordinate;
        public static GetCoordinate DoLimitHigh { get; } = (coordinate, upperBoundExclusive) => Subtract(upperBoundExclusive, Constant(1));
        public static GetCoordinate DoLimitLow { get; } = (coordinate, upperBoundExclusive) => Constant(0);
        public static GetCoordinate ConstantCoordinate(int value) => (coordinate, upperBoundExclusive) => Constant(value);

    }
    public abstract class OutOfBoundsStrategy
    {
        protected OutOfBoundsStrategy(OutOfBoundsPolicy all) : this(all, all) { }
        protected OutOfBoundsStrategy(OutOfBoundsPolicy below, OutOfBoundsPolicy above) : this(below, below, above, above) { }
        protected OutOfBoundsStrategy(OutOfBoundsPolicy xBelow, OutOfBoundsPolicy yBelow, OutOfBoundsPolicy xAbove, OutOfBoundsPolicy yAbove)
        {
            XBelow = xBelow;
            XAbove = xAbove;
            YBelow = yBelow;
            YAbove = yAbove;
        }

        public OutOfBoundsPolicy XBelow { get; init; }
        public OutOfBoundsPolicy XAbove { get; init; }
        public OutOfBoundsPolicy YBelow{ get; init; }
        public OutOfBoundsPolicy YAbove{ get; init; }


        public static OutOfBoundsStrategyUntyped Substitute(object value)
            => new OutOfBoundsStrategyUntyped(OutOfBoundsPolicy.ReplaceBy(value));

        public static OutOfBoundsStrategy<T> Substitute<T>(T value)
            => new OutOfBoundsStrategy<T>(OutOfBoundsPolicy.ReplaceBy(value));
        public static OutOfBoundsStrategy<T> Default<T>()
            => new OutOfBoundsStrategy<T>(OutOfBoundsPolicy.Throw<T>());
        public static OutOfBoundsStrategyUntyped NearestNeighbour { get; } 
            = new OutOfBoundsStrategyUntyped(
                OutOfBoundsPolicy.LimitLow, 
                OutOfBoundsPolicy.LimitHigh);
        public static OutOfBoundsStrategy<T> Integral<T>(T initValue)
            => new OutOfBoundsStrategy<T>(OutOfBoundsPolicy.ReplaceByAndMoveTo(initValue, -1),
                OutOfBoundsPolicy.LimitHigh);
    }
    public class OutOfBoundsStrategy<T> : OutOfBoundsStrategy {
        internal OutOfBoundsStrategy(OutOfBoundsPolicy all) : base(all) { }
        internal OutOfBoundsStrategy(OutOfBoundsPolicy below, OutOfBoundsPolicy above) : base(below, above) { }
    }
    public class OutOfBoundsStrategyUntyped: OutOfBoundsStrategy
    {
        internal OutOfBoundsStrategyUntyped(OutOfBoundsPolicy all) : base(all) { }
        internal OutOfBoundsStrategyUntyped(OutOfBoundsPolicy below, OutOfBoundsPolicy above) : base(below, above) { }
    }
}

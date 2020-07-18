using System.Linq.Expressions;

namespace System.Linq.Processing2d
{
    using System.Collections.Generic;
    using static Expression;
    using static Enumerable;

    class ExpressionHelper
    {
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

    internal abstract unsafe class StripeHandler<T>
        where T: unmanaged
    {
        private readonly T* _sourceptr;
        private readonly int _bottom;
        private readonly int _top;
        private readonly int _left;
        private readonly int _right;
        private readonly long _delta;
        private readonly uint _stride;

        public StripeHandler(T* psourcetmp, int top, int bottom, int left, int right, long delta, uint stride)
        {
            _sourceptr = psourcetmp;
            _bottom = bottom;
            _top = top;
            _left = left;
            _right = right;
            _delta = delta;
            _stride = stride;
        }

        public abstract T RunKernel(T* sourceptr, int i, int j);

        public void Run()
        {
            var length = _bottom;
            var delta = _delta;
            var sourceptr = _sourceptr;
            for (int i = _top; i < _bottom; i++)
            {
                for (int j = _left; j < _right; j++)
                {
                    sourceptr[delta] = RunKernel(sourceptr, i, j);
                    sourceptr++;
                }
                sourceptr += 2;
            }
        }
    }
}

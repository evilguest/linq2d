using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Linq.Expressions.Expression;

namespace System.Linq.Processing2d
{
    class ExpressionHelper
    {
        public static Expression For(ParameterExpression loopVar, Expression initValue, Expression condition, Expression increment, Expression loopContent)
        {
            var breakLabel = Label("LoopBreak");

            return Block(new[] { loopVar },
                Assign(loopVar, initValue),
                Loop(
                    IfThenElse(
                        condition,
                        Block(
                            loopContent,
                            increment
                        ),
                        Break(breakLabel)
                    ),
                breakLabel)
            );
        }
    }
}

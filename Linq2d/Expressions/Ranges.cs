using System.Collections.Generic;
using System.Linq.Expressions;

namespace Linq2d.Expressions
{
    public static class Ranges
    {
        public static IReadOnlyDictionary<Expression, (Expression minVal, Expression maxVal)> Add(this IReadOnlyDictionary<Expression, (Expression minVal, Expression maxVal)> dictionary,
            Expression variable, Expression minValue, Expression maxValue)
        {
            var d = new Dictionary<Expression, (Expression minVal, Expression maxVal)>(dictionary);
            d[variable] = (minValue, maxValue);
            return d;
        }
        public static IReadOnlyDictionary<Expression, (Expression minVal, Expression maxVal)> Add(this IReadOnlyDictionary<Expression, (Expression minVal, Expression maxVal)> dictionary,
            Expression variable, Expression minValue)
        {
            var d = new Dictionary<Expression, (Expression minVal, Expression maxVal)>(dictionary);
            d[variable] = (minValue, null);
            return d;
        }
        public static IReadOnlyDictionary<Expression, (Expression minVal, Expression maxVal)> No => new Dictionary<Expression, (Expression minVal, Expression maxVal)>();
    }
}

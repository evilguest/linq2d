using Linq.Expressions.Deconstruct;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace Linq2d.Expressions
{
    internal class CodeComparer : IEqualityComparer<Expr>, IEqualityComparer<Expression>
    {
        public bool Equals([AllowNull] Expr x, [AllowNull] Expr y) => x.ToString() == y.ToString();

        public bool Equals([AllowNull] Expression x, [AllowNull] Expression y)
        {
            if (x == y)
                return true;
            if (x == null)
                return y == null;
            if (y == null)
                return x == null;

            if (x.NodeType != y.NodeType)
                return false;

            if (x.Type != y.Type)
                return false;

            return (x, y) switch
            {
                (ConstantExpression c1, ConstantExpression c2) => Equals(c1.Value, c2.Value),
                (UnaryExpression u1, UnaryExpression u2) => Equals(u1.Operand, u2.Operand),
                (BinaryExpression b1, BinaryExpression b2) => Equals(b1.Left, b2.Left) && Equals(b1.Right, b2.Right),
                (MethodCallExpression m1, MethodCallExpression m2) => Equals(m1.Object, m2.Object) && m1.Method == m2.Method,
                (IndexExpression i1, IndexExpression i2) => Equals(i1.Object, i2.Object) && Enumerable.SequenceEqual(i1.Arguments, i2.Arguments, this),
                (MemberExpression m1, MemberExpression m2) => Equals(m1.Expression, m2.Expression) && m1.Member == m2.Member,
                (BlockExpression b1, BlockExpression b2) => Enumerable.SequenceEqual(b1.Expressions, b2.Expressions, this) && Enumerable.SequenceEqual(b1.Variables, b2.Variables),

                _ => false
            };
        }

        public int GetHashCode([DisallowNull] Expr obj) => obj.ToString().GetHashCode();

        public int GetHashCode([DisallowNull] Expression obj) => obj.ToString().GetHashCode();
    }
}
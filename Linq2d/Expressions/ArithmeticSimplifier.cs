using Linq.Expressions.Deconstruct;
using Linq2d.CodeGen;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

using static Linq.Expressions.Deconstruct.Expr;
using static System.Linq.Expressions.Expression;

namespace Linq2d.Expressions
{
    using System;
    using System.Linq;

    public static class Arithmetic
    {
        public static Expression Simplify(Expression expression, IReadOnlyDictionary<Expression, (Expression minVal, Expression maxVal)> variableRanges)
        {
            return Simplify(expression, variableRanges, new Dictionary<Expr, Expr>(new CodeComparer()));
        }

        //private static readonly IReadOnlyDictionary<ExpressionType, string> _operatorNameTable = new Dictionary<ExpressionType, string>()
        //{
        //    //            [ExpressionType.Convert] = "op_Implicit",
        //    //            [ExpressionType.Convert] = "op_Explicit",
        //    [ExpressionType.Add] = "op_Addition",
        //    [ExpressionType.Subtract] = "op_Subtraction",
        //    [ExpressionType.Multiply] = "op_Multiply",
        //    [ExpressionType.Divide] = "op_Division",
        //    [ExpressionType.Modulo] = "op_Modulus",
        //    [ExpressionType.ExclusiveOr] = "op_ExclusiveOr",
        //    [ExpressionType.And] = "op_BitwiseAnd",
        //    [ExpressionType.Or] = "op_BitwiseOr",
        //    [ExpressionType.AndAlso] = "op_LogicalAnd",
        //    [ExpressionType.OrElse] = "op_LogicalOr",
        //    [ExpressionType.Assign] = "op_Assign",
        //    [ExpressionType.LeftShift] = "op_LeftShift",
        //    [ExpressionType.RightShift] = "op_RightShift",
        //    //op_SignedRightShift
        //    //op_UnsignedRightShift
        //    [ExpressionType.Equal] = "op_Equality",
        //    [ExpressionType.GreaterThan] = "op_GreaterThan",
        //    [ExpressionType.LessThan] = "op_LessThan",
        //    [ExpressionType.NotEqual] = "op_Inequality",
        //    [ExpressionType.GreaterThanOrEqual] = "op_GreaterThanOrEqual",
        //    [ExpressionType.LessThanOrEqual] = "op_LessThanOrEqual",
        //    [ExpressionType.MultiplyAssign] = "op_MultiplicationAssignment",
        //    [ExpressionType.SubtractAssign] = "op_SubtractionAssignment",
        //    [ExpressionType.ExclusiveOrAssign] = "op_ExclusiveOrAssignment",
        //    [ExpressionType.LeftShiftAssign] = "op_LeftShiftAssignment",
        //    [ExpressionType.ModuloAssign] = "op_ModulusAssignment",
        //    [ExpressionType.AddAssign] = "op_AdditionAssignment",
        //    [ExpressionType.AndAssign] = "op_BitwiseAndAssignment",
        //    [ExpressionType.OrAssign] = "op_BitwiseOrAssignment",
        //    //op_Comma
        //    [ExpressionType.DivideAssign] = "op_DivisionAssignment",
        //    [ExpressionType.Decrement] = "op_Decrement",
        //    [ExpressionType.Increment] = "op_Increment",
        //    [ExpressionType.Negate] = "op_UnaryNegation",
        //    [ExpressionType.UnaryPlus] = "op_UnaryPlus",
        //    [ExpressionType.OnesComplement] = "op_OnesComplement",
        //};
        private static Expression Simplify(Expression expression, IReadOnlyDictionary<Expression, (Expression minVal, Expression maxVal)> variableRanges, Dictionary<Expr, Expr> cache)
        {
            if (!cache.ContainsKey(expression))
            {
                var e = expression;
                
                var simplified = e.TransformEx(Simplify);
                while (!simplified.Equals(e))
                {
                    e = simplified;
                    simplified = e.TransformEx(Simplify);
                };
                cache[expression] = simplified.TransformEx(Reconvert);
            }
            return cache[expression];


            Expr Simplify(Expr expr) => expr switch
            {
                #region Universal arithmetics
                Expr.Convert(var t, Expr.Convert(var e)) when t == ((Expression)e).Type => e,
                Negate(Negate(var e)) => e,                                     // - -e => e
                Not(Not(var e)) => e,                                           // !!e => e
                Expr.Convert(var to, Constant(var v, var iv))
                        => Constant(Convert.ChangeType(v, to), to),
                Negate(Subtract(var e1, var e2)) => Subtract(e2, e1),           // -(a-b)=>b-a;

                LessThan(var e1, var e2) when e1.Equals(e2) => Constant(false),
                LessThan(Add(var e1, var e2), Add(var e3, var e4)) when e1.Equals(e3) => LessThan(e2, e4),
                LessThan(Add(var e1, var e2), Add(var e3, var e4)) when e1.Equals(e4) => LessThan(e2, e3),
                LessThan(Add(var e1, var e2), Add(var e3, var e4)) when e2.Equals(e3) => LessThan(e1, e4),
                LessThan(Add(var e1, var e2), Add(var e3, var e4)) when e2.Equals(e4) => LessThan(e2, e3),
                LessThanOrEqual(var e1, var e2) when e1.Equals(e2) => Constant(true),

                GreaterThanOrEqual(var e1, var e2) => LessThanOrEqual(e2, e1),
                GreaterThan(var e1, var e2) => LessThan(e2, e1),

                Equal(var e1, var e2) when e1.Equals(e2) => Constant(true),
                Subtract(var e1, var e2) when e1.Equals(e2) => Constant(Convert.ChangeType(0, ((Expression)expr).Type)),
                //Subtract(var e1, var e2) => Add(e1, Negate(e2)),

                Negate(Add(var e1, var e2)) => Add(Negate(e1), Negate(e2)),
                Add(var e1, Negate(var e2)) when e1.Equals(e2) => Constant(0),
                Add(Add(var e1, Negate(var e2)), var e3) when e2.Equals(e3) => e1,
                Add(Add(var e1, var e2), Negate(var e3)) when e2.Equals(e3) => e1,
                Add(Negate(var e1), var e2) when e1.Equals(e2) => Constant(0),
                Add(Multiply(var e1, var e2), var e3) when e2.Equals(e3) => Multiply(Add(e1, Constant(1)), e2),
                Add(Add(var e1, Multiply(var e2, var e3)), var e4) when e3.Equals(e4) => Add(Multiply(Add(e2, Constant(1)), e3), e1),

                Member(Constant(var obj)) m when m.Expr.Member is PropertyInfo pi => Constant(pi.GetValue(obj)),
                Member(null) m when m.Expr.Member is PropertyInfo pi => Constant(pi.GetValue(null)),
                Member(Constant(var obj)) m when m.Expr.Member is FieldInfo fi => Constant(fi.GetValue(obj)),
                Member(null) m when m.Expr.Member is FieldInfo fi => Constant(fi.GetValue(null)),
                #endregion

                #region arithmetics
                Multiply(Constant(var v, var iv) zero, _) when iv == 0 => zero,          // 0 * e => 0
                Multiply(Constant(var v, var iv), var e) when iv == 1 => e,              // 1 * e => e
                Multiply(Constant(var v, var iv), var e) when iv == -1 => Negate(e),     // -1 * e => -e
                Divide(Constant(var v, var iv) zero, _) when iv == 0 => zero,            // 0 / e => 0
                Divide(var e, Constant(var v, var iv)) when iv == 1 => e,                // e / 1 => e
                Divide(Constant(var v, var iv), var e) when iv == -1 => Negate(e),       // e / -1 => -e
                Add(Constant(var v, var iv), var e) when iv == 0 => e,                   // 0 + e => e

                #region Divisions by 2^N
                Divide(var e, Constant(1 << 1)) => RightShift(e, Constant(1)),
                Divide(var e, Constant(1 << 2)) => RightShift(e, Constant(2)),
                Divide(var e, Constant(1 << 3)) => RightShift(e, Constant(3)),
                Divide(var e, Constant(1 << 4)) => RightShift(e, Constant(4)),
                Divide(var e, Constant(1 << 5)) => RightShift(e, Constant(5)),
                Divide(var e, Constant(1 << 6)) => RightShift(e, Constant(6)),
                Divide(var e, Constant(1 << 7)) => RightShift(e, Constant(7)),
                Divide(var e, Constant(1 << 8)) => RightShift(e, Constant(8)),
                Divide(var e, Constant(1 << 9)) => RightShift(e, Constant(9)),
                Divide(var e, Constant(1 << 10)) => RightShift(e, Constant(10)),
                Divide(var e, Constant(1 << 11)) => RightShift(e, Constant(11)),
                Divide(var e, Constant(1 << 12)) => RightShift(e, Constant(12)),
                Divide(var e, Constant(1 << 13)) => RightShift(e, Constant(13)),
                Divide(var e, Constant(1 << 14)) => RightShift(e, Constant(14)),
                Divide(var e, Constant(1 << 15)) => RightShift(e, Constant(15)),
                Divide(var e, Constant(1 << 16)) => RightShift(e, Constant(16)),
                Divide(var e, Constant(1 << 17)) => RightShift(e, Constant(17)),
                Divide(var e, Constant(1 << 18)) => RightShift(e, Constant(18)),
                Divide(var e, Constant(1 << 19)) => RightShift(e, Constant(19)),
                Divide(var e, Constant(1 << 20)) => RightShift(e, Constant(20)),
                Divide(var e, Constant(1 << 21)) => RightShift(e, Constant(21)),
                Divide(var e, Constant(1 << 22)) => RightShift(e, Constant(22)),
                Divide(var e, Constant(1 << 23)) => RightShift(e, Constant(23)),
                Divide(var e, Constant(1 << 24)) => RightShift(e, Constant(24)),
                #endregion

                #region const expressions

                Negate(Constant(var x, var iv, var t)) => Constant(Convert.ChangeType(-Convert.ToDouble(x), t)),
                Not(Constant(bool x)) => Constant(!x),
                ArrayIndex(Constant(Array a), Constant(int i)) => Constant(a.GetValue(i)),

                #region Logic
                AndAlso(Constant(true), var e) => e,
                AndAlso(Constant(false), _) => Constant(false),
                OrElse(Constant(true), _) => Constant(true),
                OrElse(Constant(false), var e) => e,
                #endregion

                #region Conditionals
                Conditional(Constant(true), var eTrue, _) => eTrue,
                Conditional(Constant(false), _, var eFalse) => eFalse,
                #endregion

                #region int
                Multiply(Constant(int x), Constant(int y)) => Constant(x * y),  // x * y => c
                Divide(Constant(int x), Constant(int y)) => Constant(x / y),    // x / y => c
                Add(Constant(int x), Constant(int y)) => Constant(x + y),       // x + y => c
                                                                                //Subtract(Constant(int x), Constant(int y)) => Constant(x - y),  // x - y => c
                LessThan(Constant(int x), Constant(int y)) => Constant(x < y),
                LessThanOrEqual(Constant(int x), Constant(int y)) => Constant(x <= y),
                Equal(Constant(int x), Constant(int y)) => Constant(x == y),
                NotEqual(Constant(int x), Constant(int y)) => Constant(x != y),
                #endregion

                #region long
                Multiply(Constant(long x), Constant(long y)) => Constant(x * y),  // x * y => c
                Divide(Constant(long x), Constant(long y)) => Constant(x / y),    // x / y => c
                Add(Constant(long x), Constant(long y)) => Constant(x + y),       // x + y => c
                LessThan(Constant(long x), Constant(long y)) => Constant(x < y),
                LessThanOrEqual(Constant(long x), Constant(long y)) => Constant(x <= y),
                Equal(Constant(long x), Constant(long y)) => Constant(x == y),
                NotEqual(Constant(long x), Constant(long y)) => Constant(x != y),
                #endregion

                #region byte
                Multiply(Constant(byte x), Constant(byte y)) => Constant(x * y),  // x * y => c
                Divide(Constant(byte x), Constant(byte y)) => Constant(x / y),    // x / y => c
                Add(Constant(byte x), Constant(byte y)) => Constant(x + y),       // x + y => c
                                                                                  //Subtract(Constant(byte x), Constant(byte y)) => Constant(x - y),  // x - y => c
                LessThan(Constant(byte x), Constant(byte y)) => Constant(x < y),
                LessThanOrEqual(Constant(byte x), Constant(byte y)) => Constant(x <= y),
                Equal(Constant(byte x), Constant(byte y)) => Constant(x == y),
                NotEqual(Constant(byte x), Constant(byte y)) => Constant(x != y),
                #endregion

                #region float
                Multiply(Constant(float x), Constant(float y)) => Constant(x * y),  // x * y => c
                Divide(Constant(float x), Constant(float y)) => Constant(x / y),    // x / y => c
                Add(Constant(float x), Constant(float y)) => Constant(x + y),       // x + y => c

                LessThan(Constant(float x), Constant(float y)) => Constant(x < y),
                LessThanOrEqual(Constant(float x), Constant(float y)) => Constant(x <= y),
                Equal(Constant(float x), Constant(float y)) => Constant(x == y),
                NotEqual(Constant(float x), Constant(float y)) => Constant(x != y),
                #endregion

                #region double
                Multiply(Constant(double x), Constant(double y)) => Constant(x * y),  // x * y => c
                Divide(Constant(double x), Constant(double y)) => Constant(x / y),    // x / y => c
                Add(Constant(double x), Constant(double y)) => Constant(x + y),       // x + y => c
                LessThan(Constant(double x), Constant(double y)) => Constant(x < y),
                LessThanOrEqual(Constant(double x), Constant(double y)) => Constant(x <= y),
                Equal(Constant(double x), Constant(double y)) => Constant(x == y),
                NotEqual(Constant(double x), Constant(double y)) => Constant(x != y),
                #endregion

                #region Reassociation
                Multiply(var e, Constant(_) c) a when a.Expr.Method == null => Multiply(c, e),
                Add(var e, Constant(_) c) a when a.Expr.Method == null => Add(c, e),
                AndAlso(var e, Constant(_) c) => AndAlso(c, e),
                OrElse(var e, Constant(_) c) => OrElse(c, e),

                Add(var e1, Add(var e2, var e3)) => Add(Add(e1, e2), e3),
                Multiply(var e1, Multiply(var e2, var e3)) => Multiply(Multiply(e1, e2), e3),
                AndAlso(var e1, AndAlso(var e2, var e3)) => AndAlso(AndAlso(e1, e2), e3),
                OrElse(var e1, OrElse(var e2, var e3)) => OrElse(OrElse(e1, e2), e3),



                LessThan(Constant(_) x, Add(Constant(_) y, var e)) => Not(LessThanOrEqual(e, Add(x, Negate(y)))),
                LessThanOrEqual(Constant(_) x, Add(Constant(_) y, var e)) => Not(LessThan(e, Add(x, Negate(y)))),

                LessThan(Constant(_) x, Parameter p) => Not(LessThanOrEqual(p, x)),
                LessThanOrEqual(Constant(_) x, Parameter p) => Not(LessThan(p, x)),

                LessThan(Add(Constant(_) c, var e2), var e3) => LessThan(e2, Add(Negate(c), e3)),
                LessThanOrEqual(Add(Constant(_) c, var e2), var e3) => LessThanOrEqual(e2, Add(Negate(c), e3)),

                LessThan(Expr e1, Add(var e2, var e3)) when e1.Equals(e3) => LessThan(Constant(Convert.ChangeType(0, ((Expression)e1).Type)), e2),
                LessThanOrEqual(Expr e1, Add(var e2, var e3)) when e1.Equals(e3) => LessThanOrEqual(Constant(Convert.ChangeType(0, ((Expression)e1).Type)), e2),

                #endregion

                #region Range Checks
                LessThan(Parameter pe, var e) when variableRanges.ContainsKey(pe) => CheckRange(expr, pe, e),
                LessThanOrEqual(Parameter pe, var e) when variableRanges.ContainsKey(pe) => CheckRange(expr, pe, e),
                #endregion

                #endregion
                #endregion

                #region Const Array Access
                Constant(Array value) when value.Rank == 1 => NewArrayInit(value.GetType().GetElementType(), from object v in value select Constant(v)),
                #endregion

                _ => expr
            }; ;
            Expr Reconvert(Expr expr) =>
                expr switch
                {
                    Add(Negate(var e1), var e2) => Subtract(e2, e1),
                    Add(var e1, Negate(var e2)) => Subtract(e1, e2),
                    _ => expr
                };
            Expression CheckRange(Expression node, ParameterExpression left, Expression right)
            {
                var range = variableRanges[left];
                if (range.maxVal != null && Arithmetic.Simplify(MakeBinary(node.NodeType, range.maxVal, right), variableRanges, cache) is ConstantExpression rangeMatch1 && (bool)rangeMatch1.Value == true)
                    return rangeMatch1;
                if (range.minVal != null && Arithmetic.Simplify(MakeBinary(node.NodeType, range.minVal, right), variableRanges, cache) is ConstantExpression rangeMatch2 && (bool)rangeMatch2.Value == false)
                    return rangeMatch2;
                return node;
            }
        }
    }
}

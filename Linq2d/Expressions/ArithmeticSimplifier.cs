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

    public static partial class Arithmetic
    {
        public static Expression Simplify(Expression expression, IReadOnlyDictionary<Expression, (Expression minVal, Expression maxVal)> variableRanges)
        {
            return Simplify(expression, variableRanges, new Dictionary<Expr, Expr>(new CodeComparer()));
        }

        private static readonly IReadOnlyDictionary<ExpressionType, string> _operatorNameTable = new Dictionary<ExpressionType, string>()
        {
            //            [ExpressionType.Convert] = "op_Implicit",
            //            [ExpressionType.Convert] = "op_Explicit",
            [ExpressionType.Add] = "op_Addition",
            [ExpressionType.Subtract] = "op_Subtraction",
            [ExpressionType.Multiply] = "op_Multiply",
            [ExpressionType.Divide] = "op_Division",
            [ExpressionType.Modulo] = "op_Modulus",
            [ExpressionType.ExclusiveOr] = "op_ExclusiveOr",
            [ExpressionType.And] = "op_BitwiseAnd",
            [ExpressionType.Or] = "op_BitwiseOr",
            [ExpressionType.AndAlso] = "op_LogicalAnd",
            [ExpressionType.OrElse] = "op_LogicalOr",
            [ExpressionType.Assign] = "op_Assign",
            [ExpressionType.LeftShift] = "op_LeftShift",
            [ExpressionType.RightShift] = "op_RightShift",
            //op_SignedRightShift
            //op_UnsignedRightShift
            [ExpressionType.Equal] = "op_Equality",
            [ExpressionType.GreaterThan] = "op_GreaterThan",
            [ExpressionType.LessThan] = "op_LessThan",
            [ExpressionType.NotEqual] = "op_Inequality",
            [ExpressionType.GreaterThanOrEqual] = "op_GreaterThanOrEqual",
            [ExpressionType.LessThanOrEqual] = "op_LessThanOrEqual",
            [ExpressionType.MultiplyAssign] = "op_MultiplicationAssignment",
            [ExpressionType.SubtractAssign] = "op_SubtractionAssignment",
            [ExpressionType.ExclusiveOrAssign] = "op_ExclusiveOrAssignment",
            [ExpressionType.LeftShiftAssign] = "op_LeftShiftAssignment",
            [ExpressionType.ModuloAssign] = "op_ModulusAssignment",
            [ExpressionType.AddAssign] = "op_AdditionAssignment",
            [ExpressionType.AndAssign] = "op_BitwiseAndAssignment",
            [ExpressionType.OrAssign] = "op_BitwiseOrAssignment",
            //op_Comma
            [ExpressionType.DivideAssign] = "op_DivisionAssignment",
            [ExpressionType.Decrement] = "op_Decrement",
            [ExpressionType.Increment] = "op_Increment",
            [ExpressionType.Negate] = "op_UnaryNegation",
            [ExpressionType.UnaryPlus] = "op_UnaryPlus",
            [ExpressionType.OnesComplement] = "op_OnesComplement",
        };
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


            Expr Simplify(Expr expr) 
            {
                Expr ret = expr switch
                {
                    #region Universal arithmetics
                    Negate(Negate(var e)) => e,                                     // - -e => e
                    Not(Not(var e)) => e,                                           // !!e => e
                    Expr.Convert(Type to, Constant(Type from, object v))
                            => Constant(Convert.ChangeType(v, to), to),
                    Negate(Subtract(var e1, var e2)) => Subtract(e2, e1),           // -(a-b)=>b-a;

                    LessThan(var e1, var e2) when e1.Equals(e2) => Constant(false),
                    LessThanOrEqual(var e1, var e2) when e1.Equals(e2) => Constant(true),

                    GreaterThanOrEqual(var e1, var e2) => LessThanOrEqual(e2, e1),
                    GreaterThan(var e1, var e2) => LessThan(e2, e1),

                    Equal(var e1, var e2) when e1.Equals(e2) => Constant(true),
                    Subtract(var e1, var e2) when e1.Equals(e2) => Constant(Convert.ChangeType(0, ((Expression)expr).Type)),
                    Subtract(var e1, var e2) => Add(e1, Negate(e2)),

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
                    Multiply(Constant(Type t, var o) zero, _) when t.IsNumeric() && o.Equals(Convert.ChangeType(0, t)) => zero,          // 0 * e => 0
                    Multiply(Constant(Type t, var o), var e) when t.IsNumeric() && o.Equals(Convert.ChangeType(1, t)) => e,              // 1 * e => e
                    Multiply(Constant(Type t, var o), var e) when t.IsNumeric() && o.Equals(Convert.ChangeType(-1, t)) => Negate(e),     // -1 * e => -e
                    Divide(Constant(Type t, var o) zero, _) when t.IsNumeric() && o.Equals(Convert.ChangeType(0, t)) => zero,            // 0 / e => 0
                    Divide(var e, Constant(Type t, var o)) when t.IsNumeric() && o.Equals(Convert.ChangeType(1, t)) => e,                // e / 1 => e
                    Divide(Constant(Type t, var o), var e) when t.IsNumeric() && o.Equals(Convert.ChangeType(-1, t)) => Negate(e),       // e / -1 => -e
                    Add(Constant(var t, var o), var e) when t.IsNumeric() && o.Equals(Convert.ChangeType(0, t)) => e,                    // 0 + e => e

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

                    Negate(Constant(Type t, var x)) => Constant(Convert.ChangeType(-Convert.ToDouble(x), t)),
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
                    Add(var e, Constant(_) c) a when a.Expr.Method == null  => Add(c, e), 
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


                    _ => expr
                };
                return ret;
            };
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

    /*
    class ArithmeticSimplifier : ExpressionVisitor
    {
        private readonly Dictionary<Expression, (Expression minVal, Expression maxVal)> _variableRanges;

        public ArithmeticSimplifier(IDictionary<Expression, (Expression minVal, Expression maxVal)> variableRanges)
        {
            _variableRanges = new Dictionary<Expression, (Expression minVal, Expression maxVal)>(variableRanges);
        }
        protected override Expression VisitUnary(UnaryExpression node)
        {
            switch (Visit(node.Operand))
            {
                case ConstantExpression ce:
                    switch (node.NodeType)
                    {
                        case ExpressionType.Negate: return Constant(Convert.ChangeType(-Convert.ToDouble(ce.Value), node.Type));
                        case ExpressionType.Convert: return Constant(Convert.ChangeType(ce.Value, node.Type));
                    }
                    break;
                case UnaryExpression ue:
                    switch (node.NodeType)
                    {
                        case ExpressionType.Not:
                            if (ue.NodeType == ExpressionType.Not)
                                return Visit(ue.Operand);
                            else break;
                        case ExpressionType.Negate:
                            if (ue.NodeType == ExpressionType.Negate)
                                return Visit(ue.Operand);
                            else break;


                    }
                    break;
                case BinaryExpression be when be.NodeType == ExpressionType.Subtract:
                    if (node.NodeType == ExpressionType.Negate)
                        return Subtract(Visit(be.Right), Visit(be.Left));
                    break;
            }

            return base.VisitUnary(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var left = Visit(node.Left);
            var right = Visit(node.Right);

            if (left is ConstantExpression ceLeft && right is ConstantExpression ceRight)
            {
                switch (node.NodeType)
                {
                    case ExpressionType.Add:
                    case ExpressionType.Multiply:
                    case ExpressionType.Subtract:
                    case ExpressionType.Divide:
                    case ExpressionType.LessThan:
                    case ExpressionType.LessThanOrEqual:
                    case ExpressionType.GreaterThan:
                    case ExpressionType.GreaterThanOrEqual:
                        {
                            var leftVal = Convert.ToDouble(ceLeft.Value);
                            var rightVal = Convert.ToDouble(ceRight.Value);
                            object value = null;
                            switch (node.NodeType)
                            {
                                case ExpressionType.Add: value = leftVal + rightVal; break;
                                case ExpressionType.Multiply: value = leftVal * rightVal; break;
                                case ExpressionType.Subtract: value = leftVal - rightVal; break;
                                case ExpressionType.Divide: value = leftVal / rightVal; break;
                                case ExpressionType.LessThan: value = leftVal < rightVal; break;
                                case ExpressionType.LessThanOrEqual: value = leftVal <= rightVal; break;
                                case ExpressionType.GreaterThan: value = leftVal > rightVal; break;
                                case ExpressionType.GreaterThanOrEqual: value = leftVal >= rightVal; break;
                            }
                            return Constant(Convert.ChangeType(value, node.Type));
                        }
                    case ExpressionType.ArrayIndex:
                        return Constant(((Array)ceLeft.Value).GetValue((int)ceRight.Value));
                }
            }

            if (left is ConstantExpression && right is BinaryExpression beRight)
            {
                if (beRight.Left is ConstantExpression)
                {
                    switch (node.NodeType)
                    {
                        case ExpressionType.LessThan:
                        case ExpressionType.LessThanOrEqual:
                        case ExpressionType.GreaterThan:
                        case ExpressionType.GreaterThanOrEqual:
                            switch (beRight.NodeType)
                            {
                                case ExpressionType.Add: return Visit(MakeBinary(node.NodeType, Subtract(left, beRight.Left), beRight.Right));
                                case ExpressionType.Subtract: return Visit(MakeBinary(node.NodeType, Subtract(left, beRight.Left), Negate(beRight.Right)));
                            }
                            break;
                        case ExpressionType.Add:
                            switch (beRight.NodeType)
                            {
                                case ExpressionType.Add: return Visit(Add(Add(left, beRight.Left), beRight.Right));
                                case ExpressionType.Subtract: return Visit(Subtract(Add(left, beRight.Left), beRight.Right));
                            }
                            break;
                        case ExpressionType.Subtract:
                            switch (beRight.NodeType)
                            {
                                case ExpressionType.Add: return Visit(Subtract(Subtract(left, beRight.Left), beRight.Right));
                                case ExpressionType.Subtract: return Visit(Add(Subtract(left, beRight.Left), beRight.Right));
                            }
                            break;
                        case ExpressionType.Multiply:
                            if (beRight.NodeType == ExpressionType.Multiply)
                                return Visit(Multiply(Multiply(left, beRight.Left), beRight.Right));
                            break;
                    }
                }
                if (beRight.Right is ConstantExpression)
                {
                    switch (node.NodeType)
                    {
                        case ExpressionType.LessThan:
                        case ExpressionType.LessThanOrEqual:
                        case ExpressionType.GreaterThan:
                        case ExpressionType.GreaterThanOrEqual:
                            switch (beRight.NodeType)
                            {
                                case ExpressionType.Add: return MakeBinary(node.NodeType, Visit(Subtract(left, beRight.Right)), beRight.Left);
                                case ExpressionType.Subtract: return MakeBinary(node.NodeType, Visit(Add(left, beRight.Right)), beRight.Left);
                            }
                            break;
                        case ExpressionType.Add:
                            switch (beRight.NodeType)
                            {
                                case ExpressionType.Add: return Visit(Add(Add(left, beRight.Right), beRight.Left));
                                case ExpressionType.Subtract: return Visit(Add(Subtract(left, beRight.Right), beRight.Left));
                            }
                            break;
                        case ExpressionType.Subtract:
                            switch (beRight.NodeType)
                            {
                                case ExpressionType.Add: return Visit(Subtract(Subtract(left, beRight.Right), beRight.Left));
                                case ExpressionType.Subtract: return Visit(Subtract(Add(left, beRight.Right), beRight.Left));
                            }
                            break;
                        case ExpressionType.Multiply:
                            if (beRight.NodeType == ExpressionType.Multiply)
                                return Visit(Multiply(Multiply(left, beRight.Right), beRight.Left));
                            break;
                    }
                }
            }

            if (left is BinaryExpression beLeft && right is ConstantExpression)
            {
                if (beLeft.Left is ConstantExpression)
                {
                    switch (node.NodeType)
                    {
                        case ExpressionType.LessThan:
                        case ExpressionType.LessThanOrEqual:
                        case ExpressionType.GreaterThan:
                        case ExpressionType.GreaterThanOrEqual:
                            switch (beLeft.NodeType)
                            {
                                case ExpressionType.Add: return Visit(MakeBinary(node.NodeType, beLeft.Right, Subtract(right, beLeft.Left)));
                                case ExpressionType.Subtract: return Visit(MakeBinary(node.NodeType, Negate(beLeft.Right), Subtract(right, beLeft.Left)));
                            }
                            break;
                        case ExpressionType.Add:
                            switch (beLeft.NodeType)
                            {
                                case ExpressionType.Add: return Visit(Add(beLeft.Right, Add(beLeft.Left, right)));
                                case ExpressionType.Subtract: return Visit(Subtract(Add(beLeft.Left, right), beLeft.Right));
                            }
                            break;
                        case ExpressionType.Subtract:
                            switch (beLeft.NodeType)
                            {
                                case ExpressionType.Add: return Visit(Add(Subtract(beLeft.Left, right), beLeft.Right));
                                case ExpressionType.Subtract: return Visit(Subtract(Subtract(beLeft.Left, right), beLeft.Right));
                            }
                            break;
                        case ExpressionType.Multiply:
                            if (beLeft.NodeType == ExpressionType.Multiply)
                                return Visit(Multiply(Multiply(beLeft.Left, right), beLeft.Right));
                            break;
                    }
                }
                if (beLeft.Right is ConstantExpression)
                {
                    switch (node.NodeType)
                    {
                        case ExpressionType.LessThan:
                        case ExpressionType.LessThanOrEqual:
                        case ExpressionType.GreaterThan:
                        case ExpressionType.GreaterThanOrEqual:
                            switch (beLeft.NodeType)
                            {
                                case ExpressionType.Add: return Visit(MakeBinary(node.NodeType, beLeft.Left, Subtract(right, beLeft.Right)));
                                case ExpressionType.Subtract: return Visit(MakeBinary(node.NodeType, beLeft.Left, Add(beLeft.Right, right)));
                            }
                            break;
                        case ExpressionType.Add:
                            switch (beLeft.NodeType)
                            {
                                case ExpressionType.Add: return Visit(Add(beLeft.Left, Add(beLeft.Right, right)));
                                case ExpressionType.Subtract: return Visit(Add(beLeft.Left, Subtract(right, beLeft.Right)));
                            }
                            break;
                        case ExpressionType.Subtract:
                            switch (beLeft.NodeType)
                            {
                                case ExpressionType.Add: return Visit(Add(beLeft.Left, Subtract(beLeft.Right, right)));
                                case ExpressionType.Subtract: return Visit(Subtract(beLeft.Left, Add(beLeft.Right, right)));
                            }
                            break;
                        case ExpressionType.Multiply:
                            if (beLeft.NodeType == ExpressionType.Multiply)
                                return Visit(Multiply(beLeft.Left, Multiply(beLeft.Right, right)));
                            break;
                    }
                }

            }

            switch (node.NodeType)
            {
                case ExpressionType.Add:
                    if (left is ConstantExpression ce1)
                    {
                        if (ce1.Type.IsNumeric())
                        {
                            var v = Convert.ToDouble(ce1.Value);
                            if (v == 0.0)
                                return right;
                            if (v < 0.0)
                                return Visit(Subtract(right, Constant(Convert.ChangeType(-v, node.Type))));
                        }
                        if (ce1.Type == typeof(string) && (string)ce1.Value == "")
                            return right;
                    }
                    if (right is ConstantExpression ce2)
                    {
                        if (ce2.Type.IsNumeric())
                        {
                            var v = Convert.ToDouble(ce2.Value);
                            if (v == 0.0)
                                return left;
                            if (v < 0.0)
                                return Visit(Subtract(left, Constant(Convert.ChangeType(-v, node.Type))));
                        }
                        if (ce2.Type == typeof(string) && (string)ce2.Value == "")
                            return left;
                    }
                    break;
                case ExpressionType.Subtract:
                    if (left is ConstantExpression ce3)
                    {
                        var v = Convert.ToDouble(ce3.Value);
                        if (v == 0.0)
                            return Visit(Negate(right));
                    }
                    if (right is ConstantExpression ce4)
                    {
                        var v = Convert.ToDouble(ce4.Value);
                        if (v == 0.0)
                            return left;
                        if (v < 0.0)
                            return Visit(Add(left, Constant(Convert.ChangeType(-v, node.Type))));
                    }
                    break;
                case ExpressionType.Multiply:
                    if (left is ConstantExpression ce5 && ce5.Value is int v5)
                    {
                        if (v5 == 0)
                            return Constant(0);
                        if (v5 == 1)
                            return right;
                        if (v5 == -1)
                            return Visit(Negate(right));
                    }
                    if (right is ConstantExpression ce6 && ce6.Value is int v6)
                    {
                        if (v6 == 0)
                            return Constant(0);
                        if (v6 == 1)
                            return left;
                        if (v6 == -1)
                            return Visit(Negate(left));
                    }
                    break;

                case ExpressionType.AndAlso:
                    if (left is ConstantExpression ce7)
                        return (bool)ce7.Value ? right : Constant(false);
                    if (right is ConstantExpression ce8)
                        return (bool)ce8.Value ? left : Constant(false);
                    break;
                case ExpressionType.OrElse:
                    if (left is ConstantExpression ce9)
                        return (bool)ce9.Value ? Constant(true) : right;
                    if (right is ConstantExpression ce10)
                        return (bool)ce10.Value ? Constant(true) : left;
                    break;
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                    if (left is BinaryExpression beLeft1)
                    {
                        if (beLeft1.NodeType == ExpressionType.Add && beLeft1.Left == right)
                            return Visit(MakeBinary(node.NodeType, beLeft1.Right, Constant(0)));
                        if (beLeft1.NodeType == ExpressionType.Add && beLeft1.Right == right)
                            return Visit(MakeBinary(node.NodeType, beLeft1.Left, Constant(0)));
                        if (beLeft1.NodeType == ExpressionType.Subtract && beLeft1.Left == right)
                            return Visit(MakeBinary(node.NodeType, Negate(beLeft1.Right), Constant(0)));

                        if (right is BinaryExpression beRight1)
                        {
                            if (beLeft1.Left == beRight1.Left)
                            {
                                if (beLeft1.NodeType == ExpressionType.Add && beRight1.NodeType == ExpressionType.Add)
                                    return Visit(MakeBinary(node.NodeType, beLeft1.Right, beRight1.Right));
                                if (beLeft1.NodeType == ExpressionType.Subtract && beRight1.NodeType == ExpressionType.Subtract)
                                    return Visit(MakeBinary(node.NodeType, Negate(beLeft1.Right), Negate(beRight1.Right)));
                                if (beLeft1.NodeType == ExpressionType.Add && beRight1.NodeType == ExpressionType.Subtract)
                                    return Visit(MakeBinary(node.NodeType, beLeft1.Right, Negate(beRight1.Right)));
                                if (beLeft1.NodeType == ExpressionType.Subtract && beRight1.NodeType == ExpressionType.Add)
                                    return Visit(MakeBinary(node.NodeType, Negate(beLeft1.Right), beRight1.Right));
                            }
                            if (beLeft1.Right == beRight1.Right)
                            {
                                if (beLeft1.NodeType == ExpressionType.Add && beRight1.NodeType == ExpressionType.Add
                                    || beLeft1.NodeType == ExpressionType.Subtract && beRight1.NodeType == ExpressionType.Subtract)
                                    return Visit(MakeBinary(node.NodeType, beLeft1.Left, beRight1.Left));
                            }
                            if (beLeft1.Left == beRight1.Right)
                            {
                                if (beLeft1.NodeType == ExpressionType.Add && beRight1.NodeType == ExpressionType.Add)
                                    return Visit(MakeBinary(node.NodeType, beLeft1.Right, beRight1.Left));
                                if (beLeft1.NodeType == ExpressionType.Subtract && beRight1.NodeType == ExpressionType.Add)
                                    return Visit(MakeBinary(node.NodeType, Negate(beLeft1.Right), beRight1.Left));
                            }
                            if (beLeft1.Right == beRight1.Left)
                            {
                                if (beLeft1.NodeType == ExpressionType.Add && beRight1.NodeType == ExpressionType.Add)
                                    return Visit(MakeBinary(node.NodeType, beLeft1.Left, beRight1.Right));
                                if (beLeft1.NodeType == ExpressionType.Add && beRight1.NodeType == ExpressionType.Subtract)
                                    return Visit(MakeBinary(node.NodeType, beLeft1.Left, Negate(beRight1.Right)));
                            }
                        }
                    }
                    if (right is BinaryExpression beRight2)
                    {
                        if (beRight2.NodeType == ExpressionType.Add && beRight2.Left == left)
                            return Visit(MakeBinary(node.NodeType, Constant(0), beRight2.Right));
                        if (beRight2.NodeType == ExpressionType.Add && beRight2.Right == left)
                            return Visit(MakeBinary(node.NodeType, Constant(0), beRight2.Left));
                        if (beRight2.NodeType == ExpressionType.Subtract && beRight2.Left == left)
                            return Visit(MakeBinary(node.NodeType, Constant(0), Negate(beRight2.Right)));
                    }
                    if (left is ParameterExpression peLeft && _variableRanges.ContainsKey(peLeft))
                    {
                        var range = _variableRanges[peLeft];
                        switch (node.NodeType)
                        {
                            case ExpressionType.LessThan:
                            case ExpressionType.LessThanOrEqual:
                                if (range.maxVal != null && Visit(MakeBinary(node.NodeType, range.maxVal, right)) is ConstantExpression rangeMatch1 && (bool)rangeMatch1.Value == true)
                                    return Constant(true);
                                if (range.minVal != null && Visit(MakeBinary(node.NodeType, range.minVal, right)) is ConstantExpression rangeMatch2 && (bool)rangeMatch2.Value == false)
                                    return Constant(false);
                                break;

                            case ExpressionType.GreaterThan:
                            case ExpressionType.GreaterThanOrEqual:
                                if (range.minVal != null && Visit(MakeBinary(node.NodeType, range.minVal, right)) is ConstantExpression rangeMatch3 && (bool)rangeMatch3.Value == true)
                                    return Constant(true);
                                if (range.maxVal != null && Visit(MakeBinary(node.NodeType, range.maxVal, right)) is ConstantExpression rangeMatch4 && (bool)rangeMatch4.Value == false)
                                    return Constant(false);
                                break;
                        }
                        break;
                    }

                    if (right is ParameterExpression peRight && _variableRanges.ContainsKey(peRight))
                    {
                        var range = _variableRanges[peRight];
                        switch (node.NodeType)
                        {
                            case ExpressionType.LessThan:
                            case ExpressionType.LessThanOrEqual:
                                if (range.minVal != null && Visit(MakeBinary(node.NodeType, left, range.minVal)) is ConstantExpression rangeMatch1 && (bool)rangeMatch1.Value == true)
                                    return Constant(true);
                                if (range.maxVal != null && Visit(MakeBinary(node.NodeType, left, range.maxVal)) is ConstantExpression rangeMatch2 && (bool)rangeMatch2.Value == false)
                                    return Constant(false);
                                break;
                            case ExpressionType.GreaterThan:
                            case ExpressionType.GreaterThanOrEqual:
                                if (range.minVal != null && Visit(MakeBinary(node.NodeType, left, range.minVal)) is ConstantExpression rangeMatch3 && (bool)rangeMatch3.Value == false)
                                    return Constant(false);
                                if (range.maxVal != null && Visit(MakeBinary(node.NodeType, left, range.maxVal)) is ConstantExpression rangeMatch4 && (bool)rangeMatch4.Value == true)
                                    return Constant(true);
                                break;

                        }
                    }
                    break;
            }

            return MakeBinary(node.NodeType, left, right, node.IsLiftedToNull, node.Method);
        }
        protected override Expression VisitConditional(ConditionalExpression node)
        {
            var test = Visit(node.Test);

            if (test is ConstantExpression ce)
                if ((bool)ce.Value)
                    return Visit(node.IfTrue);
                else
                    return Visit(node.IfFalse);

            return Condition(test, Visit(node.IfTrue), Visit(node.IfFalse));
        }
        protected override Expression VisitSwitch(SwitchExpression node)
        {
            var sw = Visit(node.SwitchValue);
            if (sw is ConstantExpression ce)
            {
                foreach (var theCase in node.Cases)
                    if (theCase.TestValues.Contains(ce))
                        return Visit(theCase.Body);
                return Visit(node.DefaultBody);
            }
            return base.VisitSwitch(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var expression = Visit(node.Expression);
            if (expression is ConstantExpression ce)
            {
                if (node.Member is PropertyInfo pi)
                    return Constant(pi.GetValue(ce.Value));
                if (node.Member is FieldInfo fi)
                    return Constant(fi.GetValue(ce.Value));
            }
            return MakeMemberAccess(expression, node.Member);
        }
        public static Dictionary<MethodInfo, LambdaExpression> _methodReplacements = new Dictionary<MethodInfo, LambdaExpression>();
        static ArithmeticSimplifier()
        {
            InitMethodReplacement((byte a, byte b) => Math.Max(a, b), (byte a, byte b) => a > b ? a : b);
            InitMethodReplacement((byte a, byte b) => Math.Min(a, b), (byte a, byte b) => a < b ? a : b);
            InitMethodReplacement((sbyte a, sbyte b) => Math.Max(a, b), (sbyte a, sbyte b) => a > b ? a : b);
            InitMethodReplacement((sbyte a, sbyte b) => Math.Min(a, b), (sbyte a, sbyte b) => a < b ? a : b);
            InitMethodReplacement((ushort a, ushort b) => Math.Max(a, b), (ushort a, ushort b) => a > b ? a : b);
            InitMethodReplacement((ushort a, ushort b) => Math.Min(a, b), (ushort a, ushort b) => a < b ? a : b);
            InitMethodReplacement((short a, short b) => Math.Max(a, b), (short a, short b) => a > b ? a : b);
            InitMethodReplacement((short a, short b) => Math.Min(a, b), (short a, short b) => a < b ? a : b);
            InitMethodReplacement((uint a, uint b) => Math.Max(a, b), (uint a, uint b) => a > b ? a : b);
            InitMethodReplacement((uint a, uint b) => Math.Min(a, b), (uint a, uint b) => a < b ? a : b);
            InitMethodReplacement((int a, int b) => Math.Max(a, b), (int a, int b) => a > b ? a : b);
            InitMethodReplacement((int a, int b) => Math.Min(a, b), (int a, int b) => a < b ? a : b);
            InitMethodReplacement((ulong a, ulong b) => Math.Max(a, b), (ulong a, ulong b) => a > b ? a : b);
            InitMethodReplacement((ulong a, ulong b) => Math.Min(a, b), (ulong a, ulong b) => a < b ? a : b);
            InitMethodReplacement((long a, long b) => Math.Max(a, b), (long a, long b) => a > b ? a : b);
            InitMethodReplacement((long a, long b) => Math.Min(a, b), (long a, long b) => a < b ? a : b);
        }
        private static void InitMethodReplacement<T1, T2, R>(Expression<Func<T1, T2, R>> method, Expression<Func<T1, T2, R>> replacement)
        {
            var mi = method.Body as MethodCallExpression;
            _methodReplacements.Add(mi.Method, replacement);
        }
        private static bool ShouldReplace(MethodInfo mi) => _methodReplacements.ContainsKey(mi);
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var instance = Visit(node.Object);
            var args = (from a in node.Arguments select Visit(a)).ToArray();
            if (instance is ConstantExpression ceObj && args.All(a => a is ConstantExpression))
                return Constant(node.Method.Invoke(ceObj.Value, (from a in args.Cast<ConstantExpression>() select a.Value).ToArray()));

            if (ShouldReplace(node.Method))
                return Visit(ReplaceMethod(node));

            return Call(instance, node.Method, args);
        }

        private Expression ReplaceMethod(MethodCallExpression node)
        {
            var rep = _methodReplacements[node.Method];
            var body = rep.Body;

            for(int i = 0; i<node.Arguments.Count;i++)
                body = new ExpressionReplacer(rep.Parameters[i], node.Arguments[i]).Visit(body);
            return body;
        }

        protected override Expression VisitIndex(IndexExpression node)
        {
            var instance = Visit(node.Object);
            var args = from a in node.Arguments select Visit(a);
            if (instance is ConstantExpression ceObj && args.All(a => a is ConstantExpression))
                return Constant(node.Indexer.GetValue(ceObj.Value, (from a in args.Cast<ConstantExpression>() select a.Value).ToArray()));

            return MakeIndex(instance, node.Indexer, args.ToArray());
        }
    }
    */
}

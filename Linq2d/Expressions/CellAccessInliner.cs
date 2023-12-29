using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Linq2d.Expressions
{
    using static Expression;

    public class CellAccessInliner : ExpressionVisitor
    {
        private readonly Expression _i;
        private readonly Expression _j;
        private readonly Expression _h;
        private readonly Expression _w;
        private readonly IDictionary<Expression, (Expression from, Expression to, OutOfBoundsStrategy strategy)> _replacements;

        public CellAccessInliner(Expression i, Expression j, Expression h, Expression w, params (Expression from, Expression to, OutOfBoundsStrategy strategy)[] replacements)
        {
            _replacements = (replacements ?? throw new ArgumentNullException(nameof(replacements))).ToDictionary(r => r.from);
            _i = i ?? throw new ArgumentNullException(nameof(i));
            _j = j ?? throw new ArgumentNullException(nameof(j));
            _h = h ?? throw new ArgumentNullException(nameof(h));
            _w = w ?? throw new ArgumentNullException(nameof(w));
        }

        private bool ShouldReplace(Expression param)
        {
            if (param == null)
                return false;
            return param.Type.IsGenericType && (param.Type.GetGenericTypeDefinition() == typeof(Cell<>) || param.Type.GetGenericTypeDefinition() == typeof(RelativeCell<>));
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (ShouldReplace(node))
                return MakeIndex(_replacements[node].to, ArrayItem(node.Type.GetGenericArguments()[0]), new[] { _i, _j });
            ////throw new InvalidOperationException("Cannot pass the Cell<T> to an external method - inlining is impossible!");
            //// TODO: spawn a new Cell<T> instance instead.
            //else
            return base.VisitParameter(node);
        }
        protected override Expression VisitUnary(UnaryExpression node)
        {
            var operand = Visit(node.Operand);

            if (node.NodeType==ExpressionType.Convert && operand is MethodCallExpression mce && mce.Method == CellOffset(mce.Type.GetGenericArguments()[0]))
            {
                var dx = mce.Arguments[0];
                var dy = mce.Arguments[1];
                var (_, to, oobStrategy) = _replacements[mce.Object];
                operand = CheckBounds(node.Type, dx, dy, to, oobStrategy);
            }


            if (node.NodeType == ExpressionType.Convert && operand.Type == node.Type)
                return operand; // conversion to self
            else
                return node.Update(operand);
            ///
            ///
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var left = Visit(node.Left);
            var right = Visit(node.Right);
            if (node.Method != null) // let's try to perform the method fixup
            {
                if (node.Method.GetParameters()[0].ParameterType != left.Type)
                    left = Convert(left, node.Method.GetParameters()[0].ParameterType);
                if (node.Method.GetParameters()[1].ParameterType != right.Type)
                    right = Convert(right, node.Method.GetParameters()[0].ParameterType);
            }

            try
            {
                return node.Update(left, node.Conversion, right);
            }
            catch (InvalidOperationException)
            {
                return MakeBinary(node.NodeType, left, right);
            }
        }
        private static MethodInfo CellItemGet(Type t) => typeof(RelativeCell<>).MakeGenericType(t).GetProperty("Item").GetGetMethod();
        private static PropertyInfo CellValue(Type t) => typeof(Cell<>).MakeGenericType(t).GetProperty("Value");
        private static MethodInfo CellOffset(Type t) => typeof(RelativeCell<>).MakeGenericType(t).GetMethod(nameof(RelativeCell<int>.Offset));

        private static readonly PropertyInfo CellX = typeof(Cell).GetProperty(nameof(Cell.X));
        private static readonly PropertyInfo CellY = typeof(Cell).GetProperty(nameof(Cell.Y));
        private static readonly PropertyInfo CellH = typeof(Cell).GetProperty(nameof(Cell.H));
        private static readonly PropertyInfo CellW = typeof(Cell).GetProperty(nameof(Cell.W));

        //private static readonly MethodInfo Window = typeof(Array2d).GetMethod(nameof(Array2d.Window));
        //private static readonly MethodInfo Area = typeof(Array2d).GetMethod(nameof(Array2d.Area));

        private static PropertyInfo ArrayItem(Type t) => t.MakeArrayType(2).GetProperty("Item");
        private static MethodInfo ArrayLength(Type t) => t.MakeArrayType(2).GetMethod(nameof(Array.GetLength));

        private static ConstructorInfo WindowType(Type cellT) 
            => typeof(ValueTuple<,,,>).MakeGenericType(cellT, cellT, cellT, cellT).GetConstructor(new[] { cellT, cellT, cellT, cellT });
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            //if(node.Object == null)
            //{
            //    if(node.Method.GetGenericMethodDefinition() == Window)
            //    {
            //        var cellExpr = node.Arguments[0];
            //        var cellType = node.Arguments[0].Type.GetGenericArguments()[0];
            //        var sizeExpr = node.Arguments[1];
            //        var halfWinExpr = Divide(sizeExpr, Constant(2));
            //        return New(WindowType(node.Arguments[0].Type), 
            //            Call(cellExpr, CellOffset(cellType), Subtract(Negate(halfWinExpr), Constant(1)), Subtract(Negate(halfWinExpr), Constant(1))
            //    }
            //    if(node.Method == Area)
            //    {

            //    }
            //}
            if (ShouldReplace(node.Object))
            {
                var arrayType = node.Object.Type.GetGenericArguments()[0];
                try
                {
                    var o = node.Object;
                    Expression dx = node.Arguments[0];
                    Expression dy = node.Arguments[1];
                    if (node.Object is MethodCallExpression mce && mce.Method == CellOffset(node.Type))
                    {
                        dx = Add(dx, mce.Arguments[0]);
                        dy = Add(dy, mce.Arguments[1]);
                        o = mce.Object;
                    }

                    dx = Visit(dx);
                    dy = Visit(dy);
                    var (_, to, oobStrategy) = _replacements[o];
                    if (node.Method == CellItemGet(node.Type))
                    {
                        return CheckBounds(arrayType, dx, dy, to, oobStrategy);
                    }
                    if (node.Method == CellOffset(arrayType))
                    {
                        // todo: substitute! substitute!
                        return node.Update(o, new[] { dx, dy });
                    }

                    throw new InvalidOperationException($"Unsupported method Cell<{arrayType.Name}>.{node.Method.Name}");
                }
                catch (KeyNotFoundException knfe)
                {
                    throw new InvalidOperationException($"Couldn't find a replacement for the Cell<{arrayType.Name}> access", knfe);
                }
            }

            return base.VisitMethodCall(node);
        }

        private Expression CheckBounds(Type arrayType, Expression dx, Expression dy, Expression to, OutOfBoundsStrategy oobStrategy)
        {
            Expression x = Add(_i, dx);
            Expression y = Add(_j, dy);

            //Expression checkI = Between(_i, Negate(dx), Subtract(_h, dx));
            //Expression checkJ = Between(_j, Negate(dy), Subtract(_w, dy));

            BinaryExpression xBelowZero = LessThan(_i, Negate(dx));
            BinaryExpression xBelowUpperBound = LessThan(_i, Subtract(_h, dx));
            BinaryExpression yBelowZero = LessThan(_j, Negate(dy));
            BinaryExpression yBelowUpperBound = LessThan(_j, Subtract(_w, dy));
            Expression lowX = oobStrategy.XBelow.Coordinate(x, _h);
            Expression highX = oobStrategy.XAbove.Coordinate(x, _h);
            return
                Condition(
                    xBelowZero, 
                    oobStrategy.XBelow.Value ?? CheckYRange(lowX, dy, oobStrategy, to, arrayType),
                    Condition(
                        xBelowUpperBound, CheckYRange(x, dy, oobStrategy, to, arrayType),
                        oobStrategy.XAbove.Value ?? CheckYRange(highX, dy, oobStrategy, to, arrayType)));
        }

        private Expression CheckYRange(Expression x, Expression dy, OutOfBoundsStrategy oobStrategy, Expression to, Type arrayType)
        {
            BinaryExpression yBelowZero = LessThan(_j, Negate(dy));
            BinaryExpression yBelowUpperBound = LessThan(_j, Subtract(_w, dy));
            Expression y = Add(_j, dy);
            Expression lowY = oobStrategy.YBelow.Coordinate(y, _w);
            Expression highY = oobStrategy.YAbove.Coordinate(y, _w);
            return Condition(
                        yBelowZero,
                        oobStrategy.YBelow.Value ?? MakeIndex(to, ArrayItem(arrayType), new[] { x, lowY }),
                        Condition(
                            yBelowUpperBound,
                            MakeIndex(to, ArrayItem(arrayType), new[] { x, y }),
                            oobStrategy.YAbove.Value ?? MakeIndex(to, ArrayItem(arrayType), new[] { x, highY })));
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member == CellH)
                return _h; //Call(to, ArrayLength(arrayType), Constant(0));

            if (node.Member == CellW)
                return _w; //Call(to, ArrayLength(arrayType), Constant(1));

            //if (node.Member == CellValue(node.Type))
            //    return Visit(node.Expression);

            var expression = Visit(node.Expression);
            if (expression is MethodCallExpression mce && mce.Method == CellOffset(mce.Type.GetGenericArguments()[0]))
            {
                var dx = mce.Arguments[0];
                var dy = mce.Arguments[1];
                var x = Add(_i, dx);
                var y = Add(_j, dy);
                var (_, to, oobStrategy) = _replacements[mce.Object];
                if (node.Member == CellX)
                    return Condition(LessThan(_i, Negate(dx)), oobStrategy.XBelow.Coordinate(x, _h),
                                Condition(Not(LessThan(_i, Subtract(_h, dx))), oobStrategy.XAbove.Coordinate(x, _h),
                                    x));


                if (node.Member == CellY)
                    return Condition(LessThan(_j, Negate(dy)), oobStrategy.YBelow.Coordinate (y, _w),
                                Condition(Not(LessThan(_j, Subtract(_w, dy))), oobStrategy.YAbove.Coordinate(y, _w),
                                    y));
                if (node.Member == CellValue(node.Type))
                {
                    return CheckBounds(node.Type, dx, dy, to, oobStrategy);
                }

            }

            if(ShouldReplace(expression))
            {
                var (_, to, _) = _replacements[expression];
                if (node.Member == CellX)
                    return _i;

                if (node.Member == CellY)
                    return _j;
                if (node.Member == CellValue(node.Type))
                    return MakeIndex(to, ArrayItem(node.Type), new[] { _i, _j });
            }
            return node;//MakeMemberAccess(expression, node.Member);
        }
    }
}

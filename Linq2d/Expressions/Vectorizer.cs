using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Intrinsics;
using Linq2d.CodeGen;

namespace Linq2d.Expressions
{
    class Vectorizer: ExpressionVisitor
    {
        
        public static VectorInfo Vectorize(int vectorSize, Expression expression)
        {
            var v = new Vectorizer(vectorSize);
            var vectorizedExpression = v.Visit(expression);
            return new VectorInfo(v._success, vectorizedExpression, v._blockedBy);
        }

        private Vectorizer(int vectorSize)
            => _vectorSize = vectorSize;
        public override Expression Visit(Expression node) 
        {
            if (!_success) 
                return node; // shortcut if fail - no need to scan the whole tree


            //if (result != null && IsVector(result.Type))
            //    _stepSize = Math.Min(_stepSize, GetCount(result.Type));

            return base.Visit(node);
        }

        protected override Expression VisitConditional(ConditionalExpression node)
        {
            var test = Visit(node.Test);
            if (!_success)
                return node;
            var ifTrue = Visit(node.IfTrue);
            if (!_success)
                return node;
            var ifFalse = Visit(node.IfFalse);
            if (!_success)
                return node;

            if (IsVector(test.Type))
            {
                if (!IsVector(ifTrue.Type))
                {
                    if (VectorInfo.LiftOperations.ContainsKey(ifTrue.Type))
                        ifTrue = ConvertToVector(ifTrue);
                    else
                        return Fail(node);
                }
                if (!IsVector(ifFalse.Type))
                {
                    if (VectorInfo.LiftOperations.ContainsKey(ifFalse.Type))
                        ifFalse = ConvertToVector(ifFalse);
                    else
                        return Fail(node);
                }
                if (VectorInfo.ConditionalOperations.ContainsKey((test.Type, ifTrue.Type)))
                    return Expression.Call(VectorInfo.ConditionalOperations[(test.Type, ifTrue.Type)], test, ifTrue, ifFalse);
                else
                    return Fail(node);
            }
            else
                return node.Update(test, ifTrue, ifFalse);
        }
        private IVectorInfo VectorInfo { get => VectorData.VectorInfo[_vectorSize]; }
        protected override Expression VisitIndex(IndexExpression node)
        {
            if (node.Indexer == ArrayItem(node.Type))
            {
                if (VectorInfo.LoadAndConvertOperations.ContainsKey((node.Type, node.Type)))
                    return Expression.Call(GetLoadSubstitute(node.Type, node.Type), node.Object, node.Arguments[0], node.Arguments[1]);
                else
                {
                    return Fail(node);
                }    
            }
            else return node;
        }
        private Expression Fail(Expression node)
        {
            _blockedBy = node;
            _success = false;
            return node;
        }

        private static bool IsVector(Type type) 
            => type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Vector256<>) || type.GetGenericTypeDefinition() == typeof(Vector128<>));
        protected override Expression VisitUnary(UnaryExpression node)
        {
            // first things first: if the node operand is an index and we're trying to convert...
            if(node.Operand is IndexExpression ieo && ieo.Indexer == ArrayItem(ieo.Type))
            {
                if (VectorInfo.LoadAndConvertOperations.ContainsKey((ieo.Type, node.Type)))
                    return Expression.Call(GetLoadSubstitute(ieo.Type, node.Type), ieo.Object, ieo.Arguments[0], ieo.Arguments[1]);
                else
                    return Fail(node);
            }
            var operand = Visit(node.Operand);

            if (!_success)
                return node;

            if (IsVector(operand.Type))
            {
                var operandElementType = operand.Type.GetGenericArguments()[0];
                if (node.NodeType == ExpressionType.Convert)
                { 
                    if (VectorInfo.Vector.ContainsKey(node.Type) && VectorInfo.ConvertOperations.ContainsKey((operand.Type, VectorInfo.Vector[node.Type])))
                        return Expression.Call(VectorInfo.ConvertOperations[(operand.Type, VectorInfo.Vector[node.Type])], operand);
                    else
                        return Fail(node);
                }
                else
                {
                    if (VectorInfo.UnaryOperations.ContainsKey((node.NodeType, operandElementType)))
                        return Expression.Call(VectorInfo.UnaryOperations[(node.NodeType, operandElementType)], operand);
                    else
                        return Fail(node);
                }
            }
            else return node.Update(operand);
        }

        private Expression ConvertToVector(Expression scalar)
        {
            var convertMethod = VectorInfo.LiftOperations[scalar.Type];
            return Expression.Convert(scalar, convertMethod.ReturnType, convertMethod);
        }

        private Dictionary<ParameterExpression, ParameterExpression> _variableReplacements = new Dictionary<ParameterExpression, ParameterExpression>();

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_variableReplacements.ContainsKey(node))
                return _variableReplacements[node];
            else
                return base.VisitParameter(node);
        }
        protected override Expression VisitBinary(BinaryExpression node)
        {
            var left = Visit(node.Left);
            var right = Visit(node.Right);
            if (!_success)
                return node;

            if (!IsVector(left.Type) && !IsVector(right.Type))
                return node.Update(left, node.Conversion, right);

            // try the operation without promotion:
            if (VectorInfo.BinaryOperations.ContainsKey((node.NodeType, left.Type, right.Type)))
                return Expression.MakeBinary(node.NodeType, left, right, false, VectorInfo.BinaryOperations[(node.NodeType, left.Type, right.Type)]);

            // promote both operands as necessary
            if (!IsVector(left.Type))
            {
                if (node.NodeType == ExpressionType.Assign)
                {
                    var lParam = left as ParameterExpression;
                    var newLParam = Expression.Variable(VectorInfo.Vector[lParam.Type], lParam.Name);
                    _variableReplacements[lParam] = newLParam;
                    return node.Update(newLParam, null, right);
                }
                else
                    left = ConvertToVector(left);
            }
            if (!IsVector(right.Type))
                right = ConvertToVector(right);

            if (VectorInfo.BinaryOperations.ContainsKey((node.NodeType, left.Type, right.Type)))
                return Expression.MakeBinary(node.NodeType, left, right, false, VectorInfo.BinaryOperations[(node.NodeType, left.Type, right.Type)]);

            return Fail(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var obj = Visit(node.Object);
            var arguments = Visit(node.Arguments);
            if(arguments.Any(a=>IsVector(a.Type)))
            {
                if (VectorInfo.MethodTable.ContainsKey(node.Method))
                    return Expression.Call(obj, VectorInfo.MethodTable[node.Method], arguments);
                else
                    return Fail(node);
            }
            return base.VisitMethodCall(node);
        }
        private MethodInfo GetLoadSubstitute(Type t, Type r) 
            => typeof(VectorData).GetMethod(nameof(VectorData.Load)).MakeGenericMethod(t, VectorInfo.Vector[r]);

        private bool _success = true;
        private Expression _blockedBy;
        private readonly int _vectorSize;

        //private VectorInfo VectorInfo { get; private set; }
        private static PropertyInfo ArrayItem(Type t) => t.MakeArrayType(2).GetProperty("Item");
    }
    public class VectorInfo
    {
        public bool Success { get; }
        public Expression Expression { get; }
        public Expression BlockedBy { get; }

        public VectorInfo(bool success, Expression expression, Expression blockedBy)
        {
            Success = success;
            Expression = expression;
            BlockedBy = blockedBy;
        }
    }
}

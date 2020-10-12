using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Intrinsics;
using Linq2d.CodeGen;
using Linq2d.CodeGen.Fake;

namespace Linq2d.Expressions
{
    class Vectorizer: ExpressionVisitor
    {
        
        public static VectorizationResult Vectorize(int vectorSize, Expression expression, ParameterExpression[] resultVars, ParameterExpression[] sourceArgs)
        {
            var v = new Vectorizer(vectorSize, resultVars, sourceArgs);
            if (expression is ConstantExpression ce)
            {
                if (VectorData.VectorInfo[vectorSize].LiftOperations.ContainsKey(ce.Type))
                    return new VectorizationResult(true, Expression.Call(VectorData.VectorInfo[vectorSize].LiftOperations[ce.Type], ce), null, null);
                else
                    return new VectorizationResult(false, expression, expression, $"Failed to lift the constant {ce.Value} to {ce.Type} vector of size {vectorSize}");

            }
            var vectorizedExpression = v.Visit(expression);
            return new VectorizationResult(v._success, vectorizedExpression, v._blockedBy, v._reason);
        }

        private Vectorizer(int vectorSize, ParameterExpression[] resultVars, ParameterExpression[] sourceArgs)
        {
            _vectorSize = vectorSize;
            _resultVars = new HashSet<ParameterExpression>(resultVars ?? throw new ArgumentNullException(nameof(resultVars)));
            _sourceArgs = new HashSet<ParameterExpression>(sourceArgs ?? throw new ArgumentNullException(nameof(sourceArgs)));
        }

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
                        return Fail(ifTrue, $"Failed to lift the {ifTrue.Type} expression to vector of size {_vectorSize}");
                }
                if (!IsVector(ifFalse.Type))
                {
                    if (VectorInfo.LiftOperations.ContainsKey(ifFalse.Type))
                        ifFalse = ConvertToVector(ifFalse);
                    else
                        return Fail(ifFalse, $"Failed to lift the {ifFalse.Type} expression to vector of size {_vectorSize}");
                }
                if (VectorInfo.ConditionalOperations.ContainsKey((test.Type, ifTrue.Type)))
                    return Expression.Call(VectorInfo.ConditionalOperations[(test.Type, ifTrue.Type)], test, ifTrue, ifFalse);
                else
                    return Fail(node, $"Failed to find a conditional operation over {test.Type} vector of size {_vectorSize}");
            }
            else
                return node.Update(test, ifTrue, ifFalse);
        }
        private IVectorInfo VectorInfo { get => VectorData.VectorInfo[_vectorSize]; }

        private int? RecursiveOverlap(Expression e)
            => (e is IndexExpression ie
                && ie.Object is ParameterExpression pe
                && _resultVars.Contains(pe)
                && ie.Arguments[0] is ConstantExpression dxConst
                && (int)dxConst.Value == 0
                && ie.Arguments[1] is ConstantExpression dyConst
                && (int)dyConst.Value >= -_vectorSize) ? -(int?)dyConst.Value : null;


        
        protected override Expression VisitIndex(IndexExpression node)
        {
            if (node.Object is ParameterExpression pe && (_resultVars.Contains(pe) || _sourceArgs.Contains(pe)))
            {
                // we need to check if the array access is the recursive one, and the first argument is zero, then the second
                // argument should be below -_vectorSize; otherwise vectorization is dangerous
                if (_resultVars.Contains(pe))
                {
                    if (node.Arguments[0] is ConstantExpression dxConst) // x access
                    {
                        if ((int)dxConst.Value == 0)                     // result[0, ...]
                        {
                            if (!(Arithmetic.Simplify(Expression.LessThan(node.Arguments[1], Expression.Constant(-_vectorSize)), Ranges.No) is ConstantExpression le) || (bool)le.Value != true)
                                return Fail(node, $"Cannot prove that the same-row access to the {pe.Name} is vectorization-safe for step {_vectorSize}");
                        }
                    }
                    else return Fail(node, $"We can't prove that the recursive access to {pe.Name} is vectorization-safe");
                }

                if (VectorInfo.LoadAndConvertOperations.ContainsKey((node.Type, node.Type)))
                    return Expression.Call(GetLoadSubstitute(node.Type, node.Type), node.Object, node.Arguments[0], node.Arguments[1], Expression.Constant(_vectorSize));
                else
                    return Fail(node, $"Failed to find a suitable load operation for the {node.Type} vector of size {_vectorSize}");
                    
            }
            else return node;
        }
        private Expression Fail(Expression node, string reason)
        {
            _blockedBy = node;
            _success = false;
            _reason = reason;
            return node;
        }

        private static bool IsVector(Type type)
        {
            if (!type.IsGenericType)
                return false;
            var g = type.GetGenericTypeDefinition();
            return g == typeof(Vector256<>) || g == typeof(Vector128<>) || g == typeof(Vector32<>);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            // first things first: if the node operand is an index and we're trying to convert...
            if(node.Operand is IndexExpression ieo && ieo.Indexer == ArrayItem(ieo.Type))
            {
                if (VectorInfo.LoadAndConvertOperations.ContainsKey((ieo.Type, node.Type)))
                    return Expression.Call(GetLoadSubstitute(ieo.Type, node.Type), ieo.Object, ieo.Arguments[0], ieo.Arguments[1], Expression.Constant(_vectorSize));
                else
                    return Fail(node, $"Failed to find a load-and-conver operation from type {ieo.Type} to {node.Type} vector of size {_vectorSize}");
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
                        return Fail(node, $"Failed to find a suitable convert operation from {operand.Type} to {node.Type} for vector of size {_vectorSize}");
                }
                else
                {
                    if (VectorInfo.UnaryOperations.ContainsKey((node.NodeType, operandElementType)))
                        return Expression.Call(VectorInfo.UnaryOperations[(node.NodeType, operandElementType)], operand);
                    else
                        return Fail(node, $"Failed to find a {node.NodeType} operation for {operandElementType} vector of size {_vectorSize}");
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
                else if(node.NodeType == ExpressionType.ArrayIndex)
                {
                    return node;
                }
                else
                    left = ConvertToVector(left);
            }
            if (!IsVector(right.Type))
                right = ConvertToVector(right);

            // check if any of the nodes accesses the sameRow recursive
            var lro = RecursiveOverlap(left);
            if (lro.HasValue) // A-ha! 
            {
                // do the smart stuff with the left node.
                left = left;
            }

            var rro = RecursiveOverlap(left);
            if (rro.HasValue) // A-ha! 
            {
                // do the smart stuff with the right node.
                right = right;
            }

            if (VectorInfo.BinaryOperations.ContainsKey((node.NodeType, left.Type, right.Type)))
                return Expression.MakeBinary(node.NodeType, left, right, false, VectorInfo.BinaryOperations[(node.NodeType, left.Type, right.Type)]);

            return Fail(node, $"Failed to find a vector {node.NodeType} operation over {left.Type} and {right.Type}");
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
                    return Fail(node, $"Failed to find a vector analog of {node.Method} operation");
            }
            return base.VisitMethodCall(node);
        }
        private MethodInfo GetLoadSubstitute(Type t, Type r) 
            => typeof(VectorData).GetMethod(nameof(VectorData.Load)).MakeGenericMethod(t, VectorInfo.Vector[r]);

        private bool _success = true;
        private string _reason;
        private Expression _blockedBy;
        private readonly int _vectorSize;
        private readonly HashSet<ParameterExpression> _resultVars;
        private readonly HashSet<ParameterExpression> _sourceArgs;


        //private VectorInfo VectorInfo { get; private set; }
        private static PropertyInfo ArrayItem(Type t) => t.MakeArrayType(2).GetProperty("Item");
    }
    public class VectorizationResult
    {
        public bool Success { get; }
        public Expression Expression { get; }
        public Expression BlockedBy { get; }
        public string Reason { get; }
        public VectorizationResult(bool success, Expression expression, Expression blockedBy, string reason)
        {
            Success = success;
            Expression = expression;
            BlockedBy = blockedBy;
            Reason = reason;
        }
    }
}

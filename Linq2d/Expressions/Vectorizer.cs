using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        
        public static VectorizationResult Vectorize(int vectorSize, Expression expression, IReadOnlyList<ParameterExpression> resultVars, IReadOnlyList<ParameterExpression> sourceArgs)
        {
            var v = new Vectorizer(vectorSize, resultVars, sourceArgs);
            var vectorizedExpression = expression is ConstantExpression ce
                ? v.HandleConstant(ce)
                : v.Visit(expression);
            return new VectorizationResult(v._success, vectorSize, vectorizedExpression, v._blockedBy, v._reason);
        }

        protected Expression HandleConstant(ConstantExpression node) 
            => VectorData.VectorInfo[_vectorSize].LiftOperations.TryGetValue(node.Type, out var method)
                ? Expression.Call(method, node)
                : Fail(node, $"Failed to lift the constant {node.Value} to {node.Type} vector of size {_vectorSize}");

        private Vectorizer(int vectorSize, IReadOnlyList<ParameterExpression> resultVars, IReadOnlyList<ParameterExpression> sourceArgs)
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
                return VectorInfo.ConditionalOperations.TryGetConditionalMethod(ifTrue.Type, out var conditionalMethod, out var testType)
                    ? (testType == test.Type)                                       // we found an exact match
                        ? Expression.Call(conditionalMethod, ifFalse, ifTrue, test) // so we just call the comparison
                        : VectorInfo.ConvertOperations.TryGetValue((test.Type, testType), out var conversionMethod) // else we try finding a conversion
                            ? Expression.Call(conditionalMethod, ifFalse, ifTrue, Expression.Call(conversionMethod, test))
                            : Fail(node, $"Failed to find a conversion from the conditional operation test result {test.Type} to the expected conditional input type {testType} for the vector of size {_vectorSize}")
                    : Fail(node, $"Failed to find a conditional operation over {test.Type} vector of size {_vectorSize}");
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
            return g == typeof(Vector256<>) || g == typeof(Vector128<>) || g == typeof(Vector64<>) || g == typeof(Vector32<>);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (!_success)
                return node;
            switch (node.NodeType)
            {
                case ExpressionType.Convert:
                    // first things first: if the node operand is an index and we're trying to convert...
                    if (node.Operand is IndexExpression ieo && ieo.Indexer == ArrayItem(ieo.Type))
                    {
                        if (VectorInfo.LoadAndConvertOperations.ContainsKey((ieo.Type, node.Type)))
                            return Expression.Call(GetLoadSubstitute(ieo.Type, node.Type), ieo.Object, ieo.Arguments[0], ieo.Arguments[1], Expression.Constant(_vectorSize));
                            //: Fail(node, $"Failed to find a load-and-convert operation from type {ieo.Type} to {node.Type} vector of size {_vectorSize}");
                    }
                    //if (node.Operand is BinaryExpression be
                    //    && be.Left is UnaryExpression lue 
                    //    && lue.NodeType == ExpressionType.Convert 
                    //    && lue.Operand.Type == node.Type
                    //    && be.Right is UnaryExpression rue
                    //    && rue.NodeType == ExpressionType.Convert
                    //    && rue.Operand.Type == node.Type
                    //    && VectorInfo
                    //    )
                        goto default;
                default:
                    // check if we have an unconverted operation available
                    if (node.Operand is UnaryExpression oue && oue.NodeType == ExpressionType.Convert)
                    {
                        var innerOperand = Visit(oue.Operand);
                        if (VectorInfo.UnaryOperations.TryGetValue((node.NodeType, innerOperand.Type), out var unaryMethod))
                            return Expression.Call(unaryMethod, innerOperand);
                    }
                    var operand = Visit(node.Operand);
                    if (IsVector(operand.Type))
                    {
                        return node.NodeType == ExpressionType.Convert
                            ? ConvertToNodeType(operand, node)
                            : VectorInfo.UnaryOperations.TryGetValue((node.NodeType, operand.Type), out var unaryMethod)
                                ? Expression.Call(unaryMethod, operand)
                                : Fail(node, $"Failed to find a {node.NodeType} operation for {operand.Type.GetGenericArguments()[0]} vector of size {_vectorSize}");
                    }
                    return node.Update(operand);
            }
        }

        private Expression ConvertToNodeType(Expression operand, Expression targetNode)
        {
            if (VectorInfo.Vector.ContainsKey(targetNode.Type))
            {
                if (operand.Type == VectorInfo.Vector[targetNode.Type]) //already converted
                    return operand;
                if (VectorInfo.ConvertOperations.TryGetValue((operand.Type, VectorInfo.Vector[targetNode.Type]), out var convertMethod))
                    return Expression.Call(convertMethod, operand);
            }
            return Fail(targetNode, $"Failed to find a suitable convert operation from {operand.Type} to {targetNode.Type} for vector of size {_vectorSize}");
        }

        private Expression ConvertToVector(Expression scalar)
        {
            var convertMethod = VectorInfo.LiftOperations[scalar.Type];
            return Expression.Convert(scalar, convertMethod.ReturnType, convertMethod);
        }

        private Dictionary<ParameterExpression, ParameterExpression> _variableReplacements = new();

        protected override Expression VisitParameter(ParameterExpression node) 
            => _variableReplacements.TryGetValue(node, out var replacement) 
                ? replacement 
                : base.VisitParameter(node);

        private static Expression ConvertTo(Expression e, Type targetType)
        {
            if (e.Type == targetType)
                return e;
            if (e is UnaryExpression ue && ue.NodeType == ExpressionType.Convert && ue.Operand.Type == targetType)
                return ue.Operand;
            return Expression.Convert(e, targetType);
        }
        protected override Expression VisitBinary(BinaryExpression node)
        {
            // check if we can perform the operation without conversions
            if (node.Left is UnaryExpression lue && lue.NodeType == ExpressionType.Convert
                && node.Right is UnaryExpression rue && rue.NodeType == ExpressionType.Convert)
            {
                var lo = Visit(lue.Operand);
                var ro = Visit(rue.Operand);
                if (VectorInfo.BinaryOperations.ContainsKey((node.NodeType, lo.Type, ro.Type)))
                {
                    var vectorOp = VectorInfo.BinaryOperations[(node.NodeType, lo.Type, ro.Type)];
                    var rightType = vectorOp.GetParameters()[1].ParameterType;
                    ro = ConvertTo(ro, rightType);

                    return Expression.MakeBinary(node.NodeType, lo, ro, false, vectorOp);
                }
            }
            var left = Visit(node.Left);
            var right = Visit(node.Right);
            if (!_success)
                return node;

            if (!IsVector(left.Type) && !IsVector(right.Type))
                return node.Update(left, node.Conversion, right);

            // try the operation without promotion:
            if (VectorInfo.BinaryOperations.ContainsKey((node.NodeType, left.Type, right.Type)))
            {
                var vectorOp = VectorInfo.BinaryOperations[(node.NodeType, left.Type, right.Type)];
                right = ConvertTo(right, vectorOp.GetParameters()[1].ParameterType);
                return Expression.MakeBinary(node.NodeType, left, right, false, vectorOp);
            }

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
                    // todo: try looking for a SIMD gather operation... 
                    return Fail(node, $"Failed to find a vector operation for array access {left}[{right}]");
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

            if (VectorInfo.BinaryOperations.TryGetValue((node.NodeType, left.Type, right.Type), out var operation))
                return Expression.MakeBinary(node.NodeType, left, right, false, operation);

            return Fail(node, $"Failed to find a vector {node.NodeType} operation over {left.Type} and {right.Type}");
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var obj = Visit(node.Object);
            var arguments = Visit(node.Arguments);
            if(arguments.Any(a=>IsVector(a.Type)))
            {
                if (VectorInfo.MethodTable.ContainsKey(node.Method))
                    return Expression.Call(obj, VectorInfo.MethodTable[node.Method], VectorizeParameters(VectorInfo.MethodTable[node.Method], arguments));
                else
                    return Fail(node, $"Failed to find a vector analog of {node.Method} operation");
            }
            return base.VisitMethodCall(node);
        }

        private ReadOnlyCollection<Expression> VectorizeParameters(MethodInfo methodInfo, ReadOnlyCollection<Expression> arguments)
        {
            var parameters = methodInfo.GetParameters();
            var expressions = new Expression[parameters.Length];
            for (var i=0; i<parameters.Length;i++)
                expressions[i] = IsVector(parameters[i].ParameterType) && !IsVector(arguments[i].Type) ? ConvertToVector(arguments[i]) : arguments[i];
            return new ReadOnlyCollection<Expression>(expressions);
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
    public record class VectorizationResult(bool Success, int Step, Expression Expression, Expression BlockedBy, string Reason);
}

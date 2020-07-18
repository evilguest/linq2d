using Linq.Expressions.Deconstruct;
using Linq2d.CodeGen;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Intrinsics;
using System.Text;

namespace Linq2d.Expressions
{
    class VectorVerify: ExpressionVisitor
    {
        private readonly ISet<Expression> _vectorNodes;

        public static bool CanBeVectorized(Expression expr, ISet<Expression> vectorNodes)
        {
            var vv = new VectorVerify(vectorNodes);
            vv.Visit(expr);
            return vv.Result;
        }

        public bool Result { get; private set; } = true;

        private VectorVerify(ISet<Expression> vectorNodes)
        {
            _vectorNodes = vectorNodes ?? throw new ArgumentNullException(nameof(vectorNodes));
        }

        protected override Expression VisitIndex(IndexExpression node)
        {
            if(VectorData.LoadTable.ContainsKey(node.Type))
            {
                _vectorNodes.Add(node);
            }
            else
            {
                Result = false;
            }

            return base.VisitIndex(node);
        }

        private bool IsVector(Expression node) => _vectorNodes.Contains(node);
        protected override Expression VisitUnary(UnaryExpression node)
        {
            var result = base.VisitUnary(node);
            if (IsVector(node.Operand))
            {
                _vectorNodes.Add(node);
                if(node.NodeType == ExpressionType.Convert)
                    Result &= VectorData.ConvertTable.ContainsKey((node.Operand.Type, node.Type));
                else                 
                    Result &= VectorData.UnaryOperations.ContainsKey((node.NodeType, GetVector(node.Type)));
            }
            return result;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var result = base.VisitBinary(node);
            if (_vectorNodes.Contains(node.Left) || _vectorNodes.Contains(node.Right))
            {
                _vectorNodes.Add(node);
                Result &= (VectorData.BinaryOperations.ContainsKey((node.NodeType, GetVector(node.Left.Type), GetVector(node.Right.Type))) || VectorData.BinaryOperations.ContainsKey((node.NodeType, GetVector(node.Left.Type), node.Right.Type)));
            }
            return result;
        }
        private static Type GetVector(Type elementType) => typeof(Vector256<>).MakeGenericType(elementType);
    }
}

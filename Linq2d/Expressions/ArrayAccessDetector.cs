using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Linq2d.Expressions
{
    class ArrayAccessDetector : ExpressionVisitor
    {

        public static HashSet<Expression> GetArrayAccessNodes(Expression expression)
        {
            var aad = new ArrayAccessDetector();
            aad.Visit(expression);
            return aad.AccessNodes;
        }
        private HashSet<Expression> AccessNodes { get; } = new HashSet<Expression>();
        //bool _containsArrayAccess = false;
        protected override Expression VisitIndex(IndexExpression node)
        {
            if (node.Type.IsPointer)
                AccessNodes.Add(node);

            return base.VisitIndex(node);
        }
        protected override Expression VisitUnary(UnaryExpression node)
        {
            var ret= base.VisitUnary(node);
            if (AccessNodes.Contains(node.Operand))
                AccessNodes.Add(node);
            return ret;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var ret = base.VisitBinary(node);
            if (AccessNodes.Contains(node.Left) || AccessNodes.Contains(node.Right))
                AccessNodes.Add(node);
                return ret;
        }
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var ret = base.VisitMethodCall(node);
            if (AccessNodes.Contains(node.Object) || node.Arguments.Any(e => AccessNodes.Contains(e)))
                AccessNodes.Add(node);
            return ret;
        }

        //public override Expression Visit(Expression node)
        //{
        //    if (node != null)
        //    {
        //        var containsArrayAccess = _containsArrayAccess;

        //        _containsArrayAccess = false;

        //        base.Visit(node);

        //        if (_containsArrayAccess)
        //            AccessNodes.Add(node);

        //        _containsArrayAccess |= containsArrayAccess;
        //    }
        //    return node;
        //}
    }
}

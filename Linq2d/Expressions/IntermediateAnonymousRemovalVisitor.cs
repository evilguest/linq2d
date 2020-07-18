using System.Linq.Expressions;

namespace Linq2d.Expressions
{
    internal class IntermediateAnonymousRemovalVisitor : ExpressionVisitor
    {
        protected override Expression VisitMember(MemberExpression node)
        {
            Expression r = base.VisitMember(node);
            if (r is MemberExpression me && me.Expression is NewExpression ne)
                r = SubstPropAccess(me, ne);
            return r;
        }

        private Expression SubstPropAccess(MemberExpression me, NewExpression ne) => base.Visit(ne.Arguments[ne.Members.IndexOf(me.Member)]);
    }
}
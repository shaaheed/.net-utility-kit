using System.Linq.Expressions;

namespace Msi.UtilityKit.Search
{
    public class GreaterThanComparisonExpression : IComparisonExpressionProvider
    {
        public Expression GetExpression(Expression left, Expression right)
        {
            return Expression.GreaterThan(left, right);
        }
    }
}

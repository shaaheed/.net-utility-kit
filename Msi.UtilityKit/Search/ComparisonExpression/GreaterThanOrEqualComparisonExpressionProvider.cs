using System.Linq.Expressions;

namespace Msi.UtilityKit.Search
{
    public class GreaterThanOrEqualComparisonExpressionProvider : IComparisonExpressionProvider
    {
        public Expression GetExpression(Expression left, Expression right)
        {
            return Expression.GreaterThanOrEqual(left, right);
        }
    }
}

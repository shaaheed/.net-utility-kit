using System.Linq.Expressions;

namespace Msi.UtilityKit.Search
{
    internal class LessThanOrEqualComparisonExpressionProvider : IComparisonExpressionProvider
    {
        public Expression GetExpression(Expression left, Expression right)
        {
            return Expression.LessThanOrEqual(left, right);
        }
    }
}

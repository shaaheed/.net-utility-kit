using System.Linq.Expressions;

namespace Msi.UtilityKit.Search
{
    public class LessThanComparisonExpressionProvider : IComparisonExpressionProvider
    {
        public Expression GetExpression(Expression left, Expression right)
        {
            return Expression.LessThan(left, right);
        }
    }
}

using System.Linq.Expressions;

namespace Msi.UtilityKit.Search
{
    public class EqualComparisonExpressionProvider : IComparisonExpressionProvider
    {
        public Expression GetExpression(Expression left, Expression right)
        {
            return Expression.Equal(left, right);
        }
    }
}

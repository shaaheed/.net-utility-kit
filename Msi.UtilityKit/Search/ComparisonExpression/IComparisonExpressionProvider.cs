using System.Linq.Expressions;

namespace Msi.UtilityKit.Search
{
    public interface IComparisonExpressionProvider
    {
        Expression GetExpression(Expression left, Expression right);
    }
}

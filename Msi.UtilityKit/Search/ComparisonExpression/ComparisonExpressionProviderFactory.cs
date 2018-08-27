using System;
using System.Collections.Generic;
namespace Msi.UtilityKit.Search
{
    public class ComparisonExpressionProviderFactory : IComparisonExpressionProviderFactory
    {

        private Dictionary<string, IComparisonExpressionProvider> _providers = new Dictionary<string, IComparisonExpressionProvider>
        {
            { "eq", new EqualComparisonExpressionProvider() },
            { "gt", new GreaterThanComparisonExpression() },
            { "gte", new GreaterThanOrEqualComparisonExpressionProvider() },
            { "lt", new LessThanComparisonExpressionProvider() },
            { "lte", new LessThanOrEqualComparisonExpressionProvider() },
        };

        public void AddProvider(string @operator, IComparisonExpressionProvider expression)
        {
            _providers.Add(@operator, expression);
        }

        public IComparisonExpressionProvider CreateProvider(string @operator)
        {
            if (_providers.ContainsKey(@operator))
            {
                return _providers[@operator];
            }
            throw new ArgumentException($"Operator '{@operator}' is not supported.");
        }

    }
}

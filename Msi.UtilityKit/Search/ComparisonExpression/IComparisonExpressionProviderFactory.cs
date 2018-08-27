namespace Msi.UtilityKit.Search
{
    public interface IComparisonExpressionProviderFactory
    {

        void AddProvider(string @operator, IComparisonExpressionProvider expression);

        IComparisonExpressionProvider CreateProvider(string @operator);

    }
}

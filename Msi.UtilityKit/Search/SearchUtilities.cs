using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Msi.UtilityKit.Search
{
    public static class SearchUtilities
    {

        private static SearchUtilitiesOptions _utilitiesOptions;
        private static IComparisonExpressionProviderFactory _comparisonExpressionProviderFactory;

        public static IQueryable<T> ApplySearch<T>(this IQueryable<T> query, ISearchOptions options)
        {

            if (options?.Search?.Length > 0)
            {

                if(_utilitiesOptions == null)
                {
                    _utilitiesOptions = SearchUtilitiesOptions.DefaultOptions;
                    _comparisonExpressionProviderFactory = _utilitiesOptions.ComparisonExpressionProviderFactory;
                }

                var _searchQuery = options.Search;
                var searchQueryLength = _searchQuery.Length;

                // get given type's all properties
                var properties = typeof(T).GetProperties();
                var propertiesLength = properties.Length;

                // iterate over all terms
                for (int i = 0; i < searchQueryLength; i++)
                {

                    if (string.IsNullOrEmpty(_searchQuery[i])) continue;

                    // expression -> name eq shahid
                    var tokens = _searchQuery[i].Split(' ');
                    if(tokens.Length > 3)
                    {
                        // remove null or empty items
                        tokens = tokens.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    }
                    if (tokens.Length == 3)
                    {
                        string @operator = tokens[1];
                        string value = tokens[2];

                        for (int j = 0; j < propertiesLength; j++)
                        {
                            // if property has sortable attribute and sort term equals to property name
                            var searchableAttribute = properties[j].GetCustomAttributes<SearchableAttribute>().FirstOrDefault();
                            bool isSearchable = searchableAttribute != null && properties[j].Name.Equals(tokens[0], StringComparison.OrdinalIgnoreCase);

                            if (isSearchable)
                            {
                                // build up the LINQ expression backwards:
                                // query = query.Where(x => x.Property == "Value");

                                var parameter = ExpressionUtilities.Parameter<T>();

                                // x.Property
                                var left = parameter.GetPropertyExpression(properties[j]);

                                // "Value"
                                var constantValue = Convert.ChangeType(tokens[2], properties[j].PropertyType);
                                var right = Expression.Constant(constantValue);

                                // x.Property == "Value"
                                var comparisonExpressionProvider = _comparisonExpressionProviderFactory.CreateProvider(tokens[1].ToLower());
                                var comparisonExpression = comparisonExpressionProvider.GetExpression(left, right);

                                // x => x.Property == "Value"
                                var lambda = ExpressionUtilities
                                    .GetLambda<T, bool>(parameter, comparisonExpression);

                                // query = query.Where...
                                query = query.DynamicWhere(lambda);

                            }
                        }
                    }
                }
            }
            return query;
        }

        public static void Configure(Action<SearchUtilitiesOptions> options)
        {
            _utilitiesOptions = new SearchUtilitiesOptions();
            options.Invoke(_utilitiesOptions);
        }

    }
}

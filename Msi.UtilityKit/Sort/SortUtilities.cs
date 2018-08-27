using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Msi.UtilityKit.Sort
{
    public static class SortUtilities
    {

        private static string decendingString = "desc";

        public static IQueryable<T> ApplySort<T>(this IQueryable<T> query, ISortOptions options)
        {
            if (options?.OrderBy?.Length > 0)
            {
                var _orderBy = options.OrderBy;
                var orderByLength = _orderBy.Length;

                // get given type's properties
                var properties = typeof(T).GetProperties();
                var propertiesLength = properties.Length;

                bool useThenBy = false;

                // iterate over all terms
                for (int i = 0; i < orderByLength; i++)
                {
                    // if invalid term then skip it
                    if (string.IsNullOrEmpty(_orderBy[i])) continue;

                    var tokens = _orderBy[i].Split(' ');
                    if (tokens.Length != 0)
                    {
                        for (int j = 0; j < propertiesLength; j++)
                        {
                            // if property has sortable attribute and sort term equals to property name
                            bool hasSortableAttribute = properties[j].GetCustomAttributes<SortableAttribute>().Count() > 0 && properties[j].Name.Equals(tokens[0], StringComparison.OrdinalIgnoreCase);

                            if (hasSortableAttribute)
                            {
                                bool decending = tokens.Length > 1 && tokens[1].Equals(decendingString, StringComparison.OrdinalIgnoreCase);

                                // build expression query
                                var property = ExpressionUtilities.GetPropertyInfo<T>(properties[j].Name);
                                var parameter = ExpressionUtilities.Parameter<T>();
                                var key = ExpressionUtilities.GetPropertyExpression(parameter, property);
                                var lambda = ExpressionUtilities.GetLambda(typeof(T), property.PropertyType, parameter, key);

                                // query.OrderBy/ThenBy[Decending](x => x.Property)
                                query = ExpressionUtilities.CallOrderByOrThenBy(query, useThenBy, decending, property.PropertyType, lambda);
                                useThenBy = true;
                                break;
                            }
                        }
                    }
                }
            }
            return query;
        }

    }
}

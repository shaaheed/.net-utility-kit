using System;
using System.Linq;

namespace Msi.UtilityKit.Pagination
{
    public static class PaginationUtilities
    {

        private static PaginationUtilitiesOptions _utilitiesOptions;

        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, IPagingOptions options)
        {
            if (options != null)
            {

                if (_utilitiesOptions == null)
                {
                    _utilitiesOptions = PaginationUtilitiesOptions.DefaultOptions;
                }

                options.Offset = options.Offset ?? _utilitiesOptions.PagingOptions.Offset;
                options.Limit = options.Limit ?? _utilitiesOptions.PagingOptions.Limit;
                query = query.Skip(options.Offset.Value).Take(options.Limit.Value);
            }
            return query;
        }

        public static void Configure(Action<PaginationUtilitiesOptions> options)
        {
            _utilitiesOptions = new PaginationUtilitiesOptions();
            options.Invoke(_utilitiesOptions);
        }


    }
}

namespace Msi.UtilityKit.Pagination
{
    public class PaginationUtilitiesOptions
    {

        public PagingOptions PagingOptions { get; set; }

        public static PaginationUtilitiesOptions DefaultOptions
        {
            get
            {
                return new PaginationUtilitiesOptions
                {
                    PagingOptions = new PagingOptions
                    {
                        Limit = 0,
                        Offset = 20
                    }
                };
            }
        }

    }
}

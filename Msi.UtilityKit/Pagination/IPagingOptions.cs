namespace Msi.UtilityKit.Pagination
{
    public interface IPagingOptions
    {
        int? Offset { get; set; }
        int? Limit { get; set; }
    }
}

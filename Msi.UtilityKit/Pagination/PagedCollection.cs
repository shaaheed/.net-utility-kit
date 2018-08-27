using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Msi.UtilityKit.Pagination
{
    public class PagedCollection<T> : Collection<T>
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Offset { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; set; }
        public int Size { get; set; }
        public T First { get; set; }
        public T Last { get; set; }
        public T Next { get; set; }
        public T Previous { get; set; }
    }
}

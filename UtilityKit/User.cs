using Msi.UtilityKit.Search;
using System;

namespace UtilityKit
{
    public class User
    {
        [Searchable]
        public int Id { get; set; }
        [Searchable]
        public string Name { get; set; }
        public Address Address { get; set; }

    }
}

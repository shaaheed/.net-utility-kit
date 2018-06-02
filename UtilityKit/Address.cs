namespace UtilityKit
{
    public class Address
    {
        public string Street { get; set; }
        public int Post { get; set; }

        public string GetAddress()
        {
            return $"Street: {Street}, Post: {Post}";
        }

    }
}

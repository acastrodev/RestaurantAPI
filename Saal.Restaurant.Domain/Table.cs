using System.Text.Json.Serialization;

namespace Saal.Restaurant.Domain
{
    public class Table
    {
        public int Id { get; set; }
        public string? Description { get; set; }

        [JsonIgnore]
        public List<Order> Orders { get; set; } = [];

        [JsonIgnore]
        public Bill? Bill { get; set; }
    }
}
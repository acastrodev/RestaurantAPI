using System.Text.Json.Serialization;

namespace Saal.Restaurant.Domain
{
    public class OrderMenuItem
    {
        public int OrderId { get; set; } 
        [JsonIgnore]
        public Order? Order { get; set; }
        public int MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }
        public int Quantity { get; set; }
    }
}
using System.Text.Json.Serialization;

namespace Saal.Restaurant.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public int TableId { get; set; }

        public int BillId { get; set; }

        [JsonIgnore]
        public Table? Table { get; set; }
        public List<OrderMenuItem> OrderMenuItems { get; set; } = [];
    }
}
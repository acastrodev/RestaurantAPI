namespace Saal.Restaurant.Application.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public List<OrderMenuItemDto> OrderMenuItems { get; set; } = [];
    }

}

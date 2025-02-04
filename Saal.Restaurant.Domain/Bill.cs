namespace Saal.Restaurant.Domain
{
    public class Bill
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public BillStatus Status { get; set; } = BillStatus.Open;
        public decimal Total { get; set; }
    }
}
using Saal.Restaurant.Domain;

namespace Saal.Restaurant.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersAsync();

        Task<Order> CreateOrderAsync(Order order);
    }
}
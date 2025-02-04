using Microsoft.EntityFrameworkCore;

using Saal.Restaurant.Application.Interfaces;
using Saal.Restaurant.Domain;
using Saal.Restaurant.Infrastructure;

namespace Saal.Restaurant.Application
{
    public class OrderService(RestaurantDbContext context) : IOrderService
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderMenuItems)
                .ThenInclude(omi => omi.MenuItem)
                .ToListAsync();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            foreach (var orderMenuItem in order.OrderMenuItems)
            {
                orderMenuItem.MenuItem = await _context.MenuItems.FindAsync(orderMenuItem.MenuItemId);
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
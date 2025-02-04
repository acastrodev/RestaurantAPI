using Microsoft.EntityFrameworkCore;

using Saal.Restaurant.Application;
using Saal.Restaurant.Domain;
using Saal.Restaurant.Infrastructure;

namespace Saal.Restaurant.Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private OrderService _orderService;
        private RestaurantDbContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RestaurantDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new RestaurantDbContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _orderService = new OrderService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task GetOrdersAsync_ShouldReturnOrders()
        {
            var orders = await _orderService.GetOrdersAsync();
            Assert.That(orders, Is.Not.Null);
            Assert.That(orders, Is.Empty);
        }

        [Test]
        public async Task CreateOrderAsync_ShouldAddOrder()
        {
            var order = new Order
            {
                TableId = 1,
                OrderMenuItems =
                [
                    new() { MenuItemId = 1, Quantity = 2 }
                ]
            };

            var createdOrder = await _orderService.CreateOrderAsync(order);

            Assert.That(createdOrder, Is.Not.Null);
            Assert.That(createdOrder.OrderMenuItems, Has.Count.EqualTo(1));
        }
    }
}

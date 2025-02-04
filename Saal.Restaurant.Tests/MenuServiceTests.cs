using Microsoft.EntityFrameworkCore;

using Saal.Restaurant.Application;
using Saal.Restaurant.Domain;
using Saal.Restaurant.Infrastructure;

namespace Saal.Restaurant.Tests
{
    [TestFixture]
    public class MenuServiceTests
    {
        private MenuService _menuService;
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
            _menuService = new MenuService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task GetMenuAsync_ShouldReturnMenuItems()
        {
            var menu = await _menuService.GetMenuAsync();
            Assert.That(menu, Is.Not.Null);
            Assert.That(menu, Is.Not.Empty);
        }

        [Test]
        public async Task AddMenuItemAsync_ShouldAddNewItem()
        {
            var newItem = new MenuItem { Id = 5, Description = "Salad", Price = 4.99m };
            var addedItem = await _menuService.AddMenuItemAsync(newItem);

            Assert.That(addedItem, Is.Not.Null);
            Assert.That(addedItem.Description, Is.EqualTo("Salad"));
        }
    }
}

using Microsoft.EntityFrameworkCore;

using Saal.Restaurant.Application;
using Saal.Restaurant.Infrastructure;

namespace Saal.Restaurant.Tests
{
    [TestFixture]
    public class TableServiceTests
    {
        private TableService _tableService;
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
            _tableService = new TableService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task GetTablesAsync_ShouldReturnTables()
        {
            var tables = await _tableService.GetTablesAsync();
            Assert.That(tables, Is.Not.Null);
            Assert.That(tables, Is.Not.Empty);
        }

        [Test]
        public async Task GetTableByIdAsync_ShouldReturnTable()
        {
            var table = await _tableService.GetTableByIdAsync(1);
            Assert.That(table, Is.Not.Null);
            Assert.That(table.Id, Is.EqualTo(1));
        }
    }
}

using Microsoft.EntityFrameworkCore;

using Saal.Restaurant.Application;
using Saal.Restaurant.Domain;
using Saal.Restaurant.Infrastructure;

namespace Saal.Restaurant.Tests
{
    [TestFixture]
    public class BillServiceTests
    {
        private BillService _billService;
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
            _billService = new BillService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task GenerateBillAsync_ShouldCreateBill()
        {
            var bill = await _billService.CreateBillAsync(1);

            Assert.That(bill, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(bill.TableId, Is.EqualTo(1));
                Assert.That(bill.Status, Is.EqualTo(BillStatus.Open));
            });
        }

        [Test]
        public async Task UpdateBillTotalAsync_ShouldUpdateTotal()
        {
            var bill = new Bill { Id = 1, TableId = 1, Status = BillStatus.Open, Total = 0 };
            var order = new Order { Id = 1, TableId = 1, BillId = 1, OrderMenuItems = [new() { MenuItemId = 1, Quantity = 2 }] };

            _context.Bills.Add(bill);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var updatedBill = await _billService.UpdateBillTotalAsync(1);

            Assert.That(updatedBill, Is.Not.Null);
            Assert.That(updatedBill.Total, Is.EqualTo(11.98m));
        }

        [Test]
        public async Task UpdateBillStatusAsync_ShouldChangeStatus()
        {
            var bill = new Bill { Id = 1, TableId = 1, Status = BillStatus.Open };
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();

            var updatedBill = await _billService.UpdateBillStatusAsync(1, BillStatus.Closed);

            Assert.That(updatedBill, Is.Not.Null);
            Assert.That(updatedBill.Status, Is.EqualTo(BillStatus.Closed));
        }
    }
}

using System.Linq;

using Microsoft.EntityFrameworkCore;

using Saal.Restaurant.Application.Interfaces;
using Saal.Restaurant.Domain;
using Saal.Restaurant.Infrastructure;

namespace Saal.Restaurant.Application
{
    public class BillService(RestaurantDbContext context) : IBillService
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<IEnumerable<Bill>> GetBillsAsync()
        {
            return await _context.Bills.ToListAsync();
        }

        public async Task<Bill?> GetBillByStatusAsync(int tableId, BillStatus status)
        {
            return await _context.Bills.FirstOrDefaultAsync(b => b.TableId == tableId && b.Status == status);
        }

        public async Task<Bill?> GetBillByIdAsync(int id)
        {
            return await _context.Bills.FindAsync(id);
        }

        public async Task<Bill?> CreateBillAsync(int tableId)
        {
            var table = await _context.Tables
                .Include(t => t.Orders)
                .ThenInclude(o => o.OrderMenuItems)
                .ThenInclude(omi => omi.MenuItem)
                .FirstOrDefaultAsync(t => t.Id == tableId);

            if (table == null)
            {
                return null;
            }

            var bill = new Bill
            {
                TableId = tableId,
                Status = BillStatus.Open,
                Total = 0
            };

            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();
            return bill;
        }

        public async Task<Bill?> UpdateBillTotalAsync(int tableId)
        {
            var bill = await _context.Bills.FirstOrDefaultAsync(b => b.TableId == tableId && b.Status == BillStatus.Open);

            if (bill == null)
            {
                return null;
            }

            var table = await _context.Tables
                .Include(t => t.Orders)
                .ThenInclude(o => o.OrderMenuItems)
                .ThenInclude(omi => omi.MenuItem)
                .FirstOrDefaultAsync(t => t.Id == tableId);

            if (table == null)
            {
                return null;
            }

            bill.Total = table.Orders
                .Where(order => order.BillId == bill.Id)
                .SelectMany(order => order.OrderMenuItems)
                .Sum(omi => omi.MenuItem!.Price * omi.Quantity);

            await _context.SaveChangesAsync();
            return bill;
        }


        public async Task<Bill?> UpdateBillStatusAsync(int id, BillStatus status)
        {
            var bill = await _context.Bills.FindAsync(id);

            if (bill == null)
            {
                return null;
            }

            bill.Status = status;
            await _context.SaveChangesAsync();
            return bill;
        }
    }
}
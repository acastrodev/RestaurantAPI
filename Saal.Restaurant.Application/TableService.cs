using Microsoft.EntityFrameworkCore;

using Saal.Restaurant.Application.Interfaces;
using Saal.Restaurant.Domain;
using Saal.Restaurant.Infrastructure;

namespace Saal.Restaurant.Application
{
    public class TableService(RestaurantDbContext context) : ITableService
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<IEnumerable<Table>> GetTablesAsync()
        {
            return await _context.Tables.ToListAsync();
        }

        public async Task<Table?> GetTableByIdAsync(int id)
        {
            return await _context.Tables.FindAsync(id);
        }
    }
}
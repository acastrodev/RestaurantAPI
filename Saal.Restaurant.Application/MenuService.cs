using Microsoft.EntityFrameworkCore;

using Saal.Restaurant.Application.Interfaces;
using Saal.Restaurant.Domain;
using Saal.Restaurant.Infrastructure;

namespace Saal.Restaurant.Application
{
    public class MenuService(RestaurantDbContext context) : IMenuService
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<IEnumerable<MenuItem>> GetMenuAsync() => await _context.MenuItems.ToListAsync();

        public async Task<MenuItem> AddMenuItemAsync(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }
    }
}
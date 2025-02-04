using Saal.Restaurant.Domain;

namespace Saal.Restaurant.Application.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuItem>> GetMenuAsync();

        Task<MenuItem> AddMenuItemAsync(MenuItem menuItem);
    }
}
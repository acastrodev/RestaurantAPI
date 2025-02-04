using Saal.Restaurant.Domain;

namespace Saal.Restaurant.Application.Interfaces
{
    public interface ITableService
    {
        Task<IEnumerable<Table>> GetTablesAsync();

        Task<Table?> GetTableByIdAsync(int id);
    }
}
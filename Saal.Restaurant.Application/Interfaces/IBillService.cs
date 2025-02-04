using Saal.Restaurant.Domain;

namespace Saal.Restaurant.Application.Interfaces
{
    public interface IBillService
    {
        Task<IEnumerable<Bill>> GetBillsAsync();

        Task<Bill?> GetBillByIdAsync(int id);

        Task<Bill?> GetBillByStatusAsync(int tableId, BillStatus status);

        Task<Bill?> CreateBillAsync(int tableId);

        Task<Bill?> UpdateBillTotalAsync(int tableId);

        Task<Bill?> UpdateBillStatusAsync(int billId, BillStatus status);
    }
}
using Domain.DatabaseEntity;
using Domain.ViewEntity;

namespace Service.Interfaces
{
    public interface IOperationWithMoneyService
    {
        Task<bool> CreateAsync(OperationWithMoney operation);
        Task<bool> DeleteAsync(Guid id);
        Task<List<OperationWithMoneyView>> GetRangeAsync(bool isConsumption, int amount, int skip = 0);
        Task<List<OperationCategoryView>> GetWeeklyGroupByDayAsync(bool isConsumption, DateTime finalDate = default);
        Task<List<OperationCategoryView>> GetMonthlyGroupByDayAsync(bool isConsumption, DateTime finalDate = default);
    }
}

using Domain.ViewEntity;

namespace Service.Interfaces
{
    public interface IOperationWithMoneyService
    {
        Task<bool> CreateAsync(OperationWithMoneyView operationView);
        Task<bool> UpdateAsync(OperationWithMoneyView operationView);
        Task<bool> DeleteAsync(Guid id);
        Task<List<OperationWithMoneyView>> GetRangeWithCategoriesAsync(bool isConsumption, int pageNumber, int pageElementCount);
        Task<List<BarChartView>> GetWeeklyForBarCharAsync(bool isConsumption, DateTime finalDate);
        Task<List<BarChartView>> GetMonthlyForBarCharAsync(bool isConsumption, DateTime finalDate);
        Task<List<DoughnutView>> GetWeeklyForDoughnutAsync(bool isConsumption, DateTime finalDate);
        Task<List<DoughnutView>> GetMonthlyForDoughnutAsync(bool isConsumption, DateTime finalDate);
    }
}

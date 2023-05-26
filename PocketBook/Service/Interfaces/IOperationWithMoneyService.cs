using Domain.View;

namespace Service.Interfaces
{
    public interface IOperationWithMoneyService
    {
        Task<List<OperationWithMoneyForTableView>> GetFiveLatesConsumption();
        Task<List<OperationWithMoneyForTableView>> GetWeeklyConsumption();
        Task<List<OperationWithMoneyForTableView>> GetWeeklyConsumptionGroupByDay();
        Task<bool> Create(OperationWithMoneyForTableView operationWithMoneyForTableView);
        Task<bool> Delete(Guid id);
    }
}

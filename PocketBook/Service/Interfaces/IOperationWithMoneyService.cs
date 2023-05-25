using Domain.Entity;
using Domain.View;

namespace Service.Interfaces
{
    public interface IOperationWithMoneyService
    {
        Task<List<OperationWithMoneyForTableView>> GetFiveLatesConsumption();
        Task<List<OperationWithMoneyForTableView>> GetWeeklyConsumption();
        Task<bool> Create(OperationWithMoney operationWithMoney);
        Task<bool> Delete(Guid id);
    }
}

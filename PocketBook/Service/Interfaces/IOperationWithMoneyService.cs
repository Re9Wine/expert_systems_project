using Domain.DatabaseEntity;
using Domain.ViewEntity;

namespace Service.Interfaces
{
    public interface IOperationWithMoneyService
    {
        Task<List<OperationWithMoneyView>> GetFiveLates(bool isConsumption);
        Task<List<OperationCategoryView>> GetWeekly(bool isConsumption);
        Task<List<OperationCategoryView>> GetWeeklyGroupByDay(bool isConsumption);
        Task<bool> Create(OperationWithMoney operationWithMoney);
    }
}

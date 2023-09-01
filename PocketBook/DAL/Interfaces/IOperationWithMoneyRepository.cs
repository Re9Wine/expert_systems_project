using Domain.DatabaseEntity;
using Domain.ViewEntity;

namespace DAL.Interfaces
{
    public interface IOperationWithMoneyRepository
    {
        Task<OperationWithMoney?> GetById(Guid id);
        Task<bool> Create(OperationWithMoney entity);
        Task<bool> Delete(OperationWithMoney entity);
        Task<List<OperationWithMoneyView>> GetFiveLatest(bool isConsumption);
        Task<List<OperationWithMoneyView>> GetForPeriod(bool isConsumption, DateTime date);
    }
}

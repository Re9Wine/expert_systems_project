using Domain.Entity;

namespace Service.Interfaces
{
    public interface IOperationWithMoneyService
    {
        Task<OperationWithMoney?> GetById(Guid id);
        Task<List<OperationWithMoney>> GetAll();
        Task<bool> Create(OperationWithMoney operationWithMoney);
        Task<bool> Update(OperationWithMoney operationWithMoney);
        Task<bool> Delete(Guid id);
        Task<List<OperationWithMoney>> GetFiveLatestConsumption();
    }
}

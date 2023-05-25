using Domain.Entity;

namespace DAL.Interfaces
{
    public interface IOperationWithMoneyRepository
    {
        Task<OperationWithMoney?> GetById(Guid id);
        Task<bool> Create(OperationWithMoney entity);
        Task<bool> Delete(OperationWithMoney entity);
    }
}

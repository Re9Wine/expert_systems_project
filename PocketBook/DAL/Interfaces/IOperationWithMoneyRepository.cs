using Domain.Entity;

namespace DAL.Interfaces
{
    public interface IOperationWithMoneyRepository : IBaseRepository<OperationWithMoney>
    {
        Task<List<OperationWithMoney>> GetFiveLatestConsumption();
    }
}

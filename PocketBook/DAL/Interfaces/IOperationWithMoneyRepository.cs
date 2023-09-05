using Domain.DatabaseEntity;

namespace DAL.Interfaces
{
    public interface IOperationWithMoneyRepository
    {
        Task<OperationWithMoney?> GetByIdAsync(Guid id);
        Task<List<OperationWithMoney>> GetRangeWihtCategoriesAsync(bool isConsumption, int amount, int skip);
        Task<List<OperationWithMoney>> GetPerPeriodAsync(bool isConusption, DateTime periodBeginnig, DateTime periodEnd);
        Task<bool> CreateAsync(OperationWithMoney entity);
        Task<bool> DeleteAsync(OperationWithMoney entity);
    }
}

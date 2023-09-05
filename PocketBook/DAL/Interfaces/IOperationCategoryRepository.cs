using Domain.DatabaseEntity;

namespace DAL.Interfaces
{
    public interface IOperationCategoryRepository
    {
        Task<OperationCategory?> GetByIdAsync(Guid id);
        Task<List<OperationCategory>> GetAllAsync(); // TODO мб заменить на получение части
        Task<bool> CreateAsync(OperationCategory entity);
        Task<bool> UpdateAsync(OperationCategory entity);
        Task<List<OperationCategory>> GetPerPeriodWithOperationsAsync
            (bool isConusption, DateTime periodBeginnig, DateTime periodEnd);
    }
}

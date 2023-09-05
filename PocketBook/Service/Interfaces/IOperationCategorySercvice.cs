using Domain.DatabaseEntity;
using Domain.ViewEntity;

namespace Service.Interfaces
{
    public interface IOperationCategorySercvice
    {
        Task<OperationCategory?> GetByIdAsync(Guid id);
        Task<List<OperationCategory>> GetAllAsync(); // TODO мб заменить на получение части
        Task<bool> CreateAsync(OperationCategory entity);
        Task<bool> UpdateAsync(OperationCategory entity);
        Task<List<OperationCategoryView>> GetWeeklyAsync(bool isConusption, DateTime finalDate = default);
        Task<List<OperationCategoryView>> GetMonthlyAsync(bool isConusption, DateTime finalDate = default);
    }
}

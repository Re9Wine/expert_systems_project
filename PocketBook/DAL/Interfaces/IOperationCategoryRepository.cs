using Domain.DatabaseEntity;

namespace DAL.Interfaces;

public interface IOperationCategoryRepository : IBaseRepository<OperationCategory>
{
    Task<OperationCategory?> GetByNameAsync(string name);
    Task<List<OperationCategory>> GetRangeAsync(bool isConsumption,int count, int skip);
    Task<List<OperationCategory>> GetAllConsumption();
    Task<bool> UpdateRangeAsync(List<OperationCategory> categories);
}
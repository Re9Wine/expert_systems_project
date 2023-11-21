using Domain.ViewEntity;

namespace Service.Interfaces;

public interface IOperationCategoryService
{
    Task<bool> CreateAsync(OperationCategoryView categoryView);
    Task<bool> UpdateAsync(OperationCategoryView categoryView);
    Task<bool> DeleteAsync(string name);
    Task<List<OperationCategoryView>> GetRangeAsync(int pageNumber, int pageElementCount);
}
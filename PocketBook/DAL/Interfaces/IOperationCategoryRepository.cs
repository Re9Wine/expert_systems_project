using Domain.Entity;

namespace DAL.Interfaces
{
    public interface IOperationCategoryRepository
    {
        Task<OperationCategory?> GetByName(string name);
        Task<OperationCategory?> GetById(Guid id);
        Task<List<OperationCategory>> GetAll();
        Task<bool> Create(OperationCategory entity);
        Task<bool> Update(OperationCategory entity);
        Task<bool> Delete(OperationCategory entity);
        Task<List<OperationCategory>> GetByType(string type);
    }
}

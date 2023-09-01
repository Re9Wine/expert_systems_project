using Domain.DatabaseEntity;

namespace DAL.Interfaces
{
    public interface IOperationCategoryRepository
    {
        Task<OperationCategory?> GetById(Guid id);
        Task<bool> Create(OperationCategory entity);
        Task<bool> Update(OperationCategory entity);
        Task<List<OperationCategory>> GetByType(bool isConsumption);
    }
}

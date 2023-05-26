using Domain.Entity;

namespace Service.Interfaces
{
    public interface IOperationCategorySercvice
    {
        Task<OperationCategory?> GetByName(string name);
        Task<List<OperationCategory>> GetAll();
        Task<List<OperationCategory>> GetByType(string type);
        Task<bool> Create(OperationCategory operationCategory);
        Task<bool> Update(OperationCategory operationCategory);
        Task<bool> Delete(Guid id);
    }
}

using Domain.Entity;

namespace Service.Interfaces
{
    public interface IOperationCategorySercvice
    {
        Task<OperationCategory?> GetById(Guid id);
        Task<List<OperationCategory>> GetAll();
        Task<bool> Create(OperationCategory operationCategory);
        Task<bool> Update(OperationCategory operationCategory);
        Task<bool> Delete(Guid id);
    }
}

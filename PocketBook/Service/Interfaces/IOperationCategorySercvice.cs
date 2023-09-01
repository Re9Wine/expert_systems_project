using Domain.DatabaseEntity;

namespace Service.Interfaces
{
    public interface IOperationCategorySercvice
    {
        Task<List<OperationCategory>> GetByType(bool isConsumption);
        Task<bool> Create(OperationCategory operationCategory);
        Task<bool> Update(OperationCategory operationCategory);
    }
}

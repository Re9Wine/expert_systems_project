using Domain.Entity;

namespace DAL.Interfaces
{
    public interface IOperationCategoryRepository : IBaseRepository<OperationCategory>
    {
        Task<List<OperationCategory>> GetByType(string type);
    }
}

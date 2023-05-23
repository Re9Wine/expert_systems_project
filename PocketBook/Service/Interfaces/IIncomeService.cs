using Domain.Entity;

namespace Service.Interfaces
{
    public interface IIncomeService
    {
        Task<bool> Create(Income entity);
        Task<bool> Update(Income entity);
        Task<bool> DeleteById(Guid id);
        Task<Income?> GetById(Guid id);
        Task<List<Income>> GetAll();
    }
}

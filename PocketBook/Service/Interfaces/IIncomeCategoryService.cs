using Domain.Entity;

namespace Service.Interfaces
{
    public interface IIncomeCategoryService
    {
        Task<bool> Create(IncomeCategory entity);
        Task<bool> Update(IncomeCategory entity);
        Task<bool> DeleteById(Guid id);
        Task<IncomeCategory?> GetById(Guid id);
        Task<List<IncomeCategory>> GetAll();
    }
}

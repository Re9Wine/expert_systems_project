using Domain.Entity;

namespace Service.Interfaces
{
    public interface IConsumptionCategoryService
    {
        Task<bool> Create(ConsumptionCategory entity);
        Task<bool> Update(ConsumptionCategory entity);
        Task<bool> DeleteById(Guid id);
        Task<ConsumptionCategory?> GetById(Guid id);
        Task<List<ConsumptionCategory>> GetAll();
    }
}

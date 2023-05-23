using Domain.Entity;

namespace Service.Interfaces
{
    public interface IConsumptionService
    {
        Task<bool> Create(Consumption entity);
        Task<bool> Update(Consumption entity);
        Task<bool> DeleteById(Guid id);
        Task<Consumption?> GetById(Guid id);
        Task<List<Consumption>> GetAll();
    }
}

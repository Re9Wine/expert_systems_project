using DAL.Interfaces;
using Domain.Entity;
using Service.Interfaces;

namespace Service.Implementations
{
    public class ConsumptionService : IConsumptionService
    {
        private readonly IConsumptionRepository _repository;

        public ConsumptionService(IConsumptionRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Create(Consumption entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            return _repository.Create(entity);
        }

        public async Task<bool> DeleteById(Guid id)
        {
            var entity = await _repository.GetById(id);

            if (entity == null)
            {
                return false;
            }

            return await _repository.Delete(entity);
        }

        public Task<List<Consumption>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<Consumption?> GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public Task<bool> Update(Consumption entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            return _repository.Update(entity);
        }
    }
}

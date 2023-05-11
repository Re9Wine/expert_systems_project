using DAL.Interfaces;
using Domain.Entity;
using Service.Interfaces;

namespace Service.Implementations
{
    public class ConsumptionCategoryService : IConsumptionCategoryService
    {
        private readonly IConsumptionCategoryRepository _repository;

        public ConsumptionCategoryService(IConsumptionCategoryRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Create(ConsumptionCategory entity)
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

        public Task<List<ConsumptionCategory>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<ConsumptionCategory?> GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public Task<bool> Update(ConsumptionCategory entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            return _repository.Update(entity);
        }
    }
}

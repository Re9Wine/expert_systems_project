using DAL.Interfaces;
using Domain.Entity;
using Service.Interfaces;

namespace Service.Implementations
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _repository;

        public IncomeService(IIncomeRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Create(Income entity)
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

        public Task<List<Income>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<Income?> GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public Task<bool> Update(Income entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            return _repository.Update(entity);
        }
    }
}

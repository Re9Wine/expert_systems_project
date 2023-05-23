using DAL.Interfaces;
using Domain.Entity;
using Service.Interfaces;

namespace Service.Implementations
{
    public class IncomeCategoryService : IIncomeCategoryService
    {
        private readonly IIncomeCategoryRepository _repository;

        public IncomeCategoryService(IIncomeCategoryRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Create(IncomeCategory entity)
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

        public Task<List<IncomeCategory>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<IncomeCategory?> GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public Task<bool> Update(IncomeCategory entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            return _repository.Update(entity);
        }
    }
}

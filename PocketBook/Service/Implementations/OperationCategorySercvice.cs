using DAL.Interfaces;
using Domain.Entity;
using Service.Interfaces;

namespace Service.Implementations
{
    public class OperationCategorySercvice : IOperationCategorySercvice
    {
        private readonly IOperationCategoryRepository _repository;

        public OperationCategorySercvice(IOperationCategoryRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Create(OperationCategory operationCategory)
        {
            if (operationCategory == null)
            {
                return Task.FromResult(false);
            }

            return _repository.Create(operationCategory);
        }

        public async Task<bool> Delete(Guid id)
        {
            var operationCategory = await _repository.GetById(id);

            if (operationCategory == null)
            {
                return false;
            }

            return await _repository.Delete(operationCategory);
        }

        public Task<List<OperationCategory>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<OperationCategory?> GetByName(string name)
        {
            return _repository.GetByName(name);
        }

        public Task<List<OperationCategory>> GetByType(string type)
        {
            return _repository.GetByType(type);
        }

        public async Task<bool> Update(OperationCategory operationCategory)
        {
            if (operationCategory == null)
            {
                return false;
            }

            var updatedCategory = await _repository.GetByName(operationCategory.Name);

            if (updatedCategory == null)
            {
                return false;
            }

            updatedCategory.Limit = operationCategory.Limit;

            return await _repository.Update(updatedCategory);
        }
    }
}

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

        public Task<OperationCategory?> GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public Task<bool> Update(OperationCategory operationCategory)
        {
            if (operationCategory == null)
            {
                return Task.FromResult(false);
            }

            return _repository.Update(operationCategory);
        }
    }
}

using DAL.Interfaces;
using Domain.DatabaseEntity;
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

        public async Task<bool> Create(OperationCategory operationCategory)
        {
            if (operationCategory == null)
            {
                return false;
            }

            return await _repository.Create(operationCategory);
        }

        public async Task<List<OperationCategory>> GetByType(bool isConsumption)
        {
            return await _repository.GetByType(isConsumption);
        }

        public async Task<bool> Update(OperationCategory operationCategory)
        {
            if (operationCategory == null)
            {
                return false;
            }

            var updatedCategory = await _repository.GetById(operationCategory.Id);

            if (updatedCategory == null)
            {
                return false;
            }

            return await _repository.Update(updatedCategory);
        }
    }
}

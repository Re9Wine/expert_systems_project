using DAL.Interfaces;
using Domain.Entity;
using Service.Interfaces;

namespace Service.Implementations
{
    public class OperationWithMoneyService : IOperationWithMoneyService
    {
        private readonly IOperationWithMoneyRepository _repository;

        public OperationWithMoneyService(IOperationWithMoneyRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Create(OperationWithMoney operationWithMoney)
        {
            if (operationWithMoney == null || operationWithMoney.Value == 0)
            {
                return Task.FromResult(false);
            }

            return _repository.Create(operationWithMoney);
        }

        public async Task<bool> Delete(Guid id)
        {
            var operationWithMoney = await _repository.GetById(id);

            if (operationWithMoney == null)
            {
                return false;
            }

            return await _repository.Delete(operationWithMoney);
        }

        public Task<List<OperationWithMoney>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<OperationWithMoney?> GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public Task<List<OperationWithMoney>> GetFiveLatestConsumption()
        {
            return _repository.GetFiveLatestConsumption();
        }

        public Task<bool> Update(OperationWithMoney operationWithMoney)
        {
            if (operationWithMoney == null || operationWithMoney.Value == 0)
            {
                return Task.FromResult(false);
            }

            return _repository.Update(operationWithMoney);
        }
    }
}

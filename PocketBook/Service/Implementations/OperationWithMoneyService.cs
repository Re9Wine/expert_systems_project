using DAL.Interfaces;
using Domain.Entity;
using Domain.View;
using Service.Interfaces;

namespace Service.Implementations
{
    public class OperationWithMoneyService : IOperationWithMoneyService
    {
        private readonly IOperationWithMoneyRepository _operationWithMoneyRepository;
        private readonly IOperationWithMoneyForTableViewRepository _operationWithMoneyForTableViewRepository;

        public OperationWithMoneyService(IOperationWithMoneyRepository operationWithMoneyRepository, IOperationWithMoneyForTableViewRepository operationWithMoneyForTableViewRepository)
        {
            _operationWithMoneyRepository = operationWithMoneyRepository;
            _operationWithMoneyForTableViewRepository = operationWithMoneyForTableViewRepository;
        }

        public Task<bool> Create(OperationWithMoney operationWithMoney)
        {
            if (operationWithMoney == null || operationWithMoney.Value == 0)
            {
                return Task.FromResult(false);
            }

            return _operationWithMoneyRepository.Create(operationWithMoney);
        }

        public async Task<bool> Delete(Guid id)
        {
            var operationWithMoney = await _operationWithMoneyRepository.GetById(id);

            if (operationWithMoney == null)
            {
                return false;
            }

            return await _operationWithMoneyRepository.Delete(operationWithMoney);
        }

        public Task<List<OperationWithMoneyForTableView>> GetFiveLatesConsumption()
        {
            return _operationWithMoneyForTableViewRepository.GetFiveLatestConsumption();
        }

        public async Task<List<OperationWithMoneyForTableView>> GetWeeklyConsumption()
        {
            DateTime dateNow = DateTime.Now;
            DateTime dateWeekAgo = dateNow.AddDays(-7);

            var operationsPerWeek = await _operationWithMoneyForTableViewRepository.GetWeeklyConsumption(dateWeekAgo);
            var operationsPerWeekByCategory = new List<OperationWithMoneyForTableView>();

            for (int i = 0; i < operationsPerWeek.Count(); i++)
            {
                if (operationsPerWeekByCategory.Any(x => x.Category == operationsPerWeek[i].Category))
                {
                    var buffer = operationsPerWeekByCategory.FirstOrDefault(x => x.Category == operationsPerWeek[i].Category);

                    if(buffer != null)
                    {
                        buffer.Value += operationsPerWeek[i].Value;
                    }
                }
                else
                {
                    operationsPerWeekByCategory.Add(new OperationWithMoneyForTableView()
                    {
                        Category = operationsPerWeek[i].Category,
                        Value = operationsPerWeek[i].Value,
                    });

                }
            }

            return operationsPerWeekByCategory;
        }
    }
}

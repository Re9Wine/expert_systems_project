using DAL.Interfaces;
using Domain.DatabaseEntity;
using Domain.ViewEntity;
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

        public async Task<bool> Create(OperationWithMoney operationWithMoney)
        {
            if(operationWithMoney == null)
            {
                return false;
            }

            return await _repository.Create(operationWithMoney);
        }

        public async Task<List<OperationWithMoneyView>> GetFiveLates(bool isConsumption)
        {
            return await _repository.GetFiveLatest(isConsumption);
        }

        public async Task<List<OperationCategoryView>> GetWeekly(bool isConsumption)
        {
            DateTime dateNow = DateTime.Now;
            DateTime dateWeek = dateNow.AddDays(-1 * (int)dateNow.DayOfWeek);

            var operationsPerWeek = await _repository.GetForPeriod(isConsumption, dateWeek);
            var operationsPerWeekView = new List<OperationCategoryView>();

            operationsPerWeek.ForEach(operation =>
            {
                if(operationsPerWeekView.FirstOrDefault(x => x.Name == operation.CategoryName)
                    is not { } operationPerWeekView)
                {
                    operationsPerWeekView.Add(new OperationCategoryView(operation.CategoryName, operation.Value));
                }
                else
                {
                    operationPerWeekView.Outlay += operation.Value;
                }
            });

            return operationsPerWeekView;
        }

        public async Task<List<OperationCategoryView>> GetWeeklyGroupByDay(bool isConsumption)
        {
            DateTime dateNow = DateTime.Now;
            DateTime dateWeek = dateNow.AddDays(-1 * (int)dateNow.DayOfWeek);

            var operationsPerWeek = await _repository.GetForPeriod(isConsumption, dateWeek);
            var operationsPerWeekView = new List<OperationCategoryView>();

            operationsPerWeek.ForEach(operation =>
            {
                if (operationsPerWeekView.FirstOrDefault(x => x.Date.DayOfWeek == operation.Date.DayOfWeek)
                    is not { } operationPerWeekView)
                {
                    operationsPerWeekView.Add(new OperationCategoryView(operation.Date, operation.Value));
                }
                else
                {
                    operationPerWeekView.Outlay += operation.Value;
                }
            });

            return operationsPerWeekView.OrderByDescending(x => x.Date.DayOfWeek).ToList();
        }
    }
}

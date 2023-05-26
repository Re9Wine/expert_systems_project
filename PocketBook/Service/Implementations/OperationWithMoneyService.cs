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
        private readonly IOperationCategoryRepository _operationCategoryRepository;

        public OperationWithMoneyService(
            IOperationWithMoneyRepository operationWithMoneyRepository,
            IOperationWithMoneyForTableViewRepository operationWithMoneyForTableViewRepository,
            IOperationCategoryRepository operationCategoryRepository)
        {
            _operationWithMoneyRepository = operationWithMoneyRepository;
            _operationWithMoneyForTableViewRepository = operationWithMoneyForTableViewRepository;
            _operationCategoryRepository = operationCategoryRepository;
        }

        public async Task<bool> Create(OperationWithMoneyForTableView operationWithMoneyForTableView)
        {
            if (operationWithMoneyForTableView == null || operationWithMoneyForTableView.Value == 0)
            {
                return false;
            }

            var operationCategory = await _operationCategoryRepository.GetByName(operationWithMoneyForTableView.Category);

            if(operationCategory == null)
            {
                return false;
            }

            var operationWithMoney = new OperationWithMoney()
            {
                OperationId = operationCategory.Id,
                Value = operationWithMoneyForTableView.Value,
                Description = operationWithMoneyForTableView.Description,
            };

            return await _operationWithMoneyRepository.Create(operationWithMoney);
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

            int dayOfWeek = (int)dateNow.DayOfWeek;

            if (dayOfWeek == 0)
            {
                dayOfWeek = 7;
            }

            DateTime dateWeek = dateNow.AddDays(-1 * dayOfWeek);

            var operationsPerWeek = await _operationWithMoneyForTableViewRepository.GetConsumptionForPeriod(dateWeek);
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

        public async Task<List<OperationWithMoneyForTableView>> GetWeeklyConsumptionGroupByDay()
        {
            DateTime dateNow = DateTime.Now;

            int dayOfWeek = (int)dateNow.DayOfWeek;

            if(dayOfWeek == 0)
            {
                dayOfWeek = 7;
            }

            DateTime dateWeek = dateNow.AddDays(-1 * dayOfWeek);

            var operationsPerWeek = await _operationWithMoneyForTableViewRepository.GetConsumptionForPeriod(dateWeek);
            var operationsPerWeekGropByDay = new List<OperationWithMoneyForTableView>();

            for (int i = 0; i < operationsPerWeek.Count; i++)
            {
                if (operationsPerWeekGropByDay.Any(x => x.Date.Day == operationsPerWeek[i].Date.Day))
                {
                    var buffer = operationsPerWeekGropByDay.FirstOrDefault(x => x.Date.Day == operationsPerWeek[i].Date.Day);

                    if (buffer != null)
                    {
                        buffer.Value += operationsPerWeek[i].Value;
                    }
                }
                else
                {
                    operationsPerWeekGropByDay.Add(new OperationWithMoneyForTableView()
                    {
                        Date = operationsPerWeek[i].Date,
                        Value = operationsPerWeek[i].Value,
                    });
                }
            }

            return operationsPerWeekGropByDay.OrderByDescending(x => x.Date).ToList();
        }
    }
}

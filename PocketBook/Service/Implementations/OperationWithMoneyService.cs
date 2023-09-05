using DAL.Interfaces;
using Domain.DatabaseEntity;
using Domain.ViewEntity;
using Service.Interfaces;
using System.Data;

namespace Service.Implementations
{
    public class OperationWithMoneyService : IOperationWithMoneyService
    {
        private readonly IOperationWithMoneyRepository _repository;

        public OperationWithMoneyService(IOperationWithMoneyRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateAsync(OperationWithMoney operation)
        {
            return await _repository.CreateAsync(operation);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var operation = await _repository.GetByIdAsync(id);

            if (operation == null)
            {
                return false;
            }

            return await _repository.DeleteAsync(operation);
        }

        public async Task<List<OperationCategoryView>> GetMonthlyGroupByDayAsync
            (bool isConsumption, DateTime finalDate = default)
        {
            if (finalDate == default)
            {
                finalDate = DateTime.Now;
            }

            finalDate = finalDate.Date;
            DateTime monthBeginning = finalDate.AddDays(-1 * finalDate.Day);

            return await GetPerPeriodGroupByDayAsync(isConsumption, monthBeginning, finalDate);
        }

        public async Task<List<OperationWithMoneyView>> GetRangeAsync
            (bool isConsumption, int amount, int skip = 0)
        {
            var operations = await _repository.GetRangeWihtCategoriesAsync(isConsumption, amount, skip);
            var operationsView = new List<OperationWithMoneyView>();

            operations.ForEach(operation =>
            {
                operationsView.Add(new OperationWithMoneyView()
                {
                    CategoryName = operation.OperationCategoryNavigation.Name,
                    Date = operation.Date,
                    Value = operation.Value,
                    Description = operation.Description,
                });
            });

            return operationsView;
        }

        public async Task<List<OperationCategoryView>> GetWeeklyGroupByDayAsync
            (bool isConsumption, DateTime finalDate = default)
        {
            if (finalDate == default)
            {
                finalDate = DateTime.Now;
            }

            finalDate = finalDate.Date;
            DateTime weekBeginning = finalDate.AddDays(-1 * (int)finalDate.DayOfWeek);

            return await GetPerPeriodGroupByDayAsync(isConsumption, weekBeginning, finalDate);
        }

        private async Task<List<OperationCategoryView>> GetPerPeriodGroupByDayAsync
            (bool isConsumption, DateTime periodBeginnig, DateTime periodEnd)
        {
            var operationsPerPeriod = await _repository.GetPerPeriodAsync(isConsumption, periodBeginnig, periodEnd);

            return operationsPerPeriod
                .GroupBy(x => x.Date.Date)
                .Select(x => new OperationCategoryView()
                {
                    Date = x.Key,
                    Sum = x.Sum(y => y.Value)
                })
                .OrderByDescending(x => x.Date)
                .ToList();
        }
    }
}

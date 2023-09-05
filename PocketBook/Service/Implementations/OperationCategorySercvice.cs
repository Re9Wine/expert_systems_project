using DAL.Interfaces;
using Domain.DatabaseEntity;
using Domain.ViewEntity;
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

        public async Task<bool> CreateAsync(OperationCategory entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public async Task<List<OperationCategory>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<OperationCategory?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<OperationCategoryView>> GetMonthlyAsync(bool isConusption, DateTime finalDate = default)
        {
            if (finalDate == default)
            {
                finalDate = DateTime.Now;
            }

            finalDate = finalDate.Date;
            DateTime monthBeginning = finalDate.AddDays(-1 * finalDate.Day);

            return await GetPerPeriodAsync(isConusption, monthBeginning, finalDate);
        }

        public async Task<List<OperationCategoryView>> GetWeeklyAsync(bool isConusption, DateTime finalDate = default)
        {
            if (finalDate == default)
            {
                finalDate = DateTime.Now;
            }

            finalDate = finalDate.Date;
            DateTime weekBeginning = finalDate.AddDays(-1 * (int)finalDate.DayOfWeek);

            return await GetPerPeriodAsync(isConusption, weekBeginning, finalDate);
        }

        public async Task<bool> UpdateAsync(OperationCategory entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        private async Task<List<OperationCategoryView>> GetPerPeriodAsync
            (bool isConusption, DateTime periodBeginnig, DateTime periodEnd)
        {
            var categoryOperationsPerPeriod = await _repository
                .GetPerPeriodWithOperationsAsync(isConusption, periodBeginnig, periodEnd);

            return categoryOperationsPerPeriod
                .Select(categoryOperations => new OperationCategoryView()
                {
                    Category = categoryOperations.Name,
                    Sum = categoryOperations.OperationWithMoneys.Select(x => x.Value).Sum()
                })
                .ToList();
        }
    }
}

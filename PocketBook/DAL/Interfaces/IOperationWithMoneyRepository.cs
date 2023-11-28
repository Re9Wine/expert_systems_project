using Domain.DatabaseEntity;

namespace DAL.Interfaces;

public interface IOperationWithMoneyRepository : IBaseRepository<OperationWithMoney>
{
    Task<List<OperationWithMoney>> GetAll();
    Task<List<OperationWithMoney>> GetRangeWithCategoriesAsync(bool isConsumption, int count, int skip);

    Task<List<OperationWithMoney>>
        GetPerPeriodWithCategoriesAsync(bool isConsumption, DateTime periodStart, DateTime periodEnd);

    decimal GetMonthlyIncome();
}
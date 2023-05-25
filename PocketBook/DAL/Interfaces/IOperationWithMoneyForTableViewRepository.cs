using Domain.View;

namespace DAL.Interfaces
{
    public interface IOperationWithMoneyForTableViewRepository
    {
        Task<List<OperationWithMoneyForTableView>> GetFiveLatestConsumption();
        Task<List<OperationWithMoneyForTableView>> GetWeeklyConsumption(DateTime date);
    }
}

using PocketBook.Domain.DTOs;
using PocketBook.Domain.Requests.MoneyTransactionRequests;

namespace PocketBook.BLL.Services.MoneyTransactionServices;

public interface IMoneyTransactionService
{
    Task<MoneyTransactionDTO?> CreateAsync(CreateMoneyTransactionRequest createTransactionRequest);
    Task<bool> UpdateAsync(UpdateMoneyTransactionRequest updateTransactionRequest);
    Task<bool> DeleteAsync(Guid id);
    Task<List<MoneyTransactionDTO>> GetRangeAsync(int skip, int take, bool isConsumption = true);
    Task<List<MoneyTransactionDTO>> GetConsumptionPerPeriodAsync(DateTime periodStart, DateTime periodEnd);
    Task<List<BarCharDTO>> GetConsumptionForBarCharAsync(DateTime periodEnd);
    Task<List<DoughnutDTO>> GetConsumptionForDoughnutAsync(DateTime periodEnd);
    Task<List<ConsumptionTableDTO>> GetMonthlyConsumptionAsync(DateTime periodEnd);
    Task<List<string>> GetMonthlyRecommendationsAsync(DateTime periodEnd);
}
using PocketBook.Domain.DTOs;
using PocketBook.Domain.Pages;
using PocketBook.Domain.Requests.MoneyTransactionRequests;

namespace PocketBook.BLL.Services.MoneyTransactionServices;

public interface IMoneyTransactionService
{
    Task<MoneyTransactionDTO?> CreateAsync(CreateMoneyTransactionRequest createTransactionRequest);
    Task<bool> UpdateAsync(UpdateMoneyTransactionRequest updateTransactionRequest);
    Task<bool> DeleteAsync(Guid id);
    Task<MoneyTransactionPage> GetRangeAsync(int pageNumber, int pageSize, bool isConsumption = true);
    Task<List<MoneyTransactionDTO>> GetFiveLastedConsumption();
    Task<List<BarCharDTO>> GetConsumptionForBarCharAsync(DateTime periodEnd);
    Task<List<DoughnutDTO>> GetConsumptionForDoughnutAsync(DateTime periodEnd);
    Task<Dictionary<int, List<RecommendationTableDTO>>> GetMonthlyRecommendationsAsync(DateTime periodEnd);
    Task<SpendingTrendsDTO> GetSpendingTrendsAsync();
}
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PocketBook.BLL.Exceptions;
using PocketBook.BLL.Resources;
using PocketBook.BLL.Services.Statics;
using PocketBook.DAL.Repositories.MoneyTransactionRepositories;
using PocketBook.DAL.Repositories.TransactionCategoryRepositories;
using PocketBook.Domain;
using PocketBook.Domain.DTOs;
using PocketBook.Domain.Entities;
using PocketBook.Domain.Enums;
using PocketBook.Domain.Pages;
using PocketBook.Domain.Requests.MoneyTransactionRequests;

namespace PocketBook.BLL.Services.MoneyTransactionServices;

public class MoneyTransactionService : IMoneyTransactionService
{
    private readonly IMapper _mapper;
    private readonly IMoneyTransactionRepository _repository;
    private readonly ITransactionCategoryRepository _categoryRepository;

    public MoneyTransactionService(IMapper mapper, IMoneyTransactionRepository repository, 
        ITransactionCategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _categoryRepository = categoryRepository;
    }

    public async Task<MoneyTransactionDTO?> CreateAsync(CreateMoneyTransactionRequest createTransactionRequest)
    {
        var existingCategory = await _categoryRepository.GetByNameAsync(createTransactionRequest.Category);

        if (existingCategory is null)
        {
            throw new ValidationExceptionResult(TransactionCategoryExceptionMessages.CategoryIsNotAvailable);
        }
        
        var moneyTransaction = _mapper.Map<MoneyTransaction>(createTransactionRequest);

        moneyTransaction.TransactionCategoryId = existingCategory.Id;

        var moneyTransactionEntity = await _repository.AddAsync(moneyTransaction);

        if (!moneyTransactionEntity.TransactionCategory.IsConsumption)
        {
            await UpdateCategoryPriorities(moneyTransactionEntity.Date);
        }

        return _mapper.Map<MoneyTransactionDTO>(moneyTransactionEntity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var transaction = await _repository.GetByIdAsync(id);

        if (transaction is null)
        {
            return false;
        }

        var isConsumption = transaction.TransactionCategory.IsConsumption;
        var isDeleted = await _repository.DeleteAsync(transaction);
        
        if (!isConsumption)
        {
            await UpdateCategoryPriorities(DateTime.UtcNow);
        }

        return isDeleted;
    }

    public async Task<MoneyTransactionPage> GetRangeAsync(int pageNumber, int pageSize, bool isConsumption = true)
    {
        var transactions = await _repository.GetAsync(
            transaction => transaction.TransactionCategory.IsConsumption == isConsumption,
            query => query.OrderByDescending(transaction => transaction.Date),
            (pageNumber - 1) * pageSize, pageSize);
        var allTransactionsNumber = _repository.GetTransactionNumber(isConsumption);
        var moneyTransactionPage = new MoneyTransactionPage
        {
            Transactions = _mapper.Map<IEnumerable<MoneyTransactionDTO>>(transactions),
            PageInfo = new PageInfo
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = allTransactionsNumber
            }
        };

        return moneyTransactionPage;
    }

    public async Task<List<MoneyTransactionDTO>> GetFiveLastedConsumption()
    {
        var transactions = await _repository.GetAsync(
            transaction => transaction.TransactionCategory.IsConsumption,
            query => query.OrderByDescending(transaction => transaction.Date), 0, 5);

        return _mapper.Map<List<MoneyTransactionDTO>>(transactions);
    }

    public async Task<List<BarCharDTO>> GetConsumptionForBarCharAsync(DateTime periodEnd)
    {
        var periodStart = periodEnd.AddDays(-1 * (int)periodEnd.DayOfWeek);
        
        var transactions = await _repository.GetAsync(transaction =>
            transaction.Date.Date >= periodStart && transaction.Date.Date <= periodEnd &&
            transaction.TransactionCategory.IsConsumption);
        var transactionsGroupByDate = transactions.GroupBy(transaction => transaction.Date.Date);

        return _mapper.Map<List<BarCharDTO>>(transactionsGroupByDate);
    }

    public async Task<List<DoughnutDTO>> GetConsumptionForDoughnutAsync(DateTime periodEnd)
    {
        var periodStart = periodEnd.AddDays(-1 * (int)periodEnd.DayOfWeek);
        
        var transactions = await _repository.GetAsync(transaction =>
            transaction.Date.Date >= periodStart && transaction.Date.Date <= periodEnd &&
            transaction.TransactionCategory.IsConsumption);
        var transactionsGroupByCategory = transactions.GroupBy(transaction => transaction.TransactionCategory.Name);

        return _mapper.Map<List<DoughnutDTO>>(transactionsGroupByCategory);
    }

    public async Task<Dictionary<int, List<RecommendationTableDTO>>> GetMonthlyRecommendationsAsync(DateTime periodEnd)
    {
        var monthStart = periodEnd.AddDays(-1 * periodEnd.Day + 1);
        
        var consumption = await _repository.GetAsync(transaction => 
                transaction.Date.Date >= monthStart && transaction.Date.Date <= periodEnd && 
                transaction.TransactionCategory.IsConsumption,
            query => query.OrderByDescending(transaction => transaction.Date));

        if (!consumption.Any()) return new Dictionary<int, List<RecommendationTableDTO>>();
        
        var consumptionGroupByCategory = consumption.GroupBy(transaction => transaction.TransactionCategory.Name);
        var income = await _repository.GetAsync(transaction => 
                transaction.Date.Date >= monthStart && transaction.Date.Date <= periodEnd && 
                !transaction.TransactionCategory.IsConsumption,
            query => query.OrderByDescending(transaction => transaction.Date));
        var totalIncome = income.Sum(transaction => transaction.Value);
        var totalConsumption = consumption.Sum(transaction => transaction.Value);
        RecommendationTableDTO totalMoneyRecommendation;
        
        if (totalIncome == 0)
        {
            totalMoneyRecommendation = new RecommendationTableDTO
            {
                Status = RecommendationStatus.LimitExceeded,
                Recommendation = string.Format(
                    RecommendationsMessages.TotalMoneyWithZeroIncomeRecommendationMessageFormat,
                    Math.Round(totalConsumption, 2), RecommendationsMessages.FindWayToEarnMoney)
            };
        }
        else
        {
            totalMoneyRecommendation = (totalConsumption / totalIncome) switch
            {
                >= 1.0m => new RecommendationTableDTO
                {
                    Status = RecommendationStatus.LimitExceeded,
                    Recommendation = string.Format(RecommendationsMessages.TotalMoneyRecommendationMessageFormat,
                        Math.Round(totalIncome - totalConsumption, 2), Math.Round(totalIncome),
                        RecommendationsMessages.FindWayToEarnMoney)
                },
                _ => new RecommendationTableDTO
                {
                    Status = RecommendationStatus.LimitNotExceeded,
                    Recommendation = string.Format(RecommendationsMessages.TotalMoneyRecommendationMessageFormat,
                        Math.Round(totalIncome - totalConsumption, 2), Math.Round(totalIncome),
                        RecommendationsMessages.AllGood),
                }
            };
        }

        var recommendations = new List<RecommendationTableDTO>
        {
            totalMoneyRecommendation
        };

        var categoryRecommendations = consumptionGroupByCategory.Where(transactionGroup => transactionGroup.Any())
            .Select(transactionGroup =>
            {
                var categoryConsumption = transactionGroup.Sum(transaction => transaction.Value);
                var limit = transactionGroup.First().TransactionCategory.Limit;
                decimal howMuchIsLimitExceeded;

                if (limit != 0)
                {
                    howMuchIsLimitExceeded = categoryConsumption / limit;
                }
                else
                {
                    howMuchIsLimitExceeded = categoryConsumption;
                    limit = 0;
                }

                return howMuchIsLimitExceeded switch
                {
                    >= 1.3m => new RecommendationTableDTO
                    {
                        Status = RecommendationStatus.LimitExceeded,
                        Recommendation = string.Format(RecommendationsMessages.CategoryRecommendationMessageFormat,
                            Math.Round(categoryConsumption, 2), Math.Round(limit, 2), transactionGroup.Key,
                            RecommendationsMessages.LimitExceeded)
                    },
                    >= 1.0m => new RecommendationTableDTO
                    {
                        Status = RecommendationStatus.LimitIsSlightlyExceeded,
                        Recommendation = string.Format(RecommendationsMessages.CategoryRecommendationMessageFormat,
                            Math.Round(categoryConsumption, 2), Math.Round(limit, 2), transactionGroup.Key,
                            RecommendationsMessages.LimitIsSlightlyExceeded)
                    },
                    _ => new RecommendationTableDTO
                    {
                        Status = RecommendationStatus.LimitNotExceeded,
                        Recommendation = string.Format(RecommendationsMessages.CategoryRecommendationMessageFormat,
                            Math.Round(categoryConsumption, 2), Math.Round(limit, 2), transactionGroup.Key,
                            RecommendationsMessages.LimitNotExceeded)
                    }
                };
            }).ToList();
        
        recommendations.AddRange(categoryRecommendations);

        return recommendations.GroupBy(item => item.Status)
            .OrderBy(item => item.Key)
            .ToDictionary(item => (int)item.Key, item => item.ToList());
    }

    public async Task<SpendingTrendsDTO> GetSpendingTrendsAsync()
    {
        var currentDate = DateTime.UtcNow.Date;
        var previousMonthEnd = currentDate.AddDays(currentDate.Day * -1);
        var previousPreviousMonthStart = currentDate.AddDays(1 + currentDate.Day * -1).AddMonths(-2);
        var spending = await _repository.GetAsync(transaction => 
            transaction.Date >= previousPreviousMonthStart && transaction.Date <= previousMonthEnd
            && transaction.TransactionCategory.IsConsumption);

        var categories = (await _categoryRepository.GetAsync(category => category.IsConsumption))
            .Select(category => category.Name)
            .OrderBy(s => s).ToList();
        List<decimal> previousMonthSpending;
        List<decimal> previousPreviousMonthSpending;
        
        var spendingByMonth = spending.GroupBy(x => x.Date.Month)
            .OrderByDescending(x => x.Key).ToList();
        
        if (spendingByMonth.First().Key != previousMonthEnd.Month)
        {
            previousMonthSpending = new decimal[categories.Count].ToList();
        }
        else
        {
            var previousMonthSpendingByCategory = spendingByMonth.First()
                .GroupBy(transaction => transaction.TransactionCategory.Name).OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, y => y.Sum(z => z.Value));

            var totalPreviousMonthSpendingByCategory = categories.ToDictionary(category => category, _ => 0M);
        
            foreach (var category in categories)
            {
                if (previousMonthSpendingByCategory.TryGetValue(category, out var sum))
                {
                    totalPreviousMonthSpendingByCategory[category] += sum;
                }
            }

            previousMonthSpending = totalPreviousMonthSpendingByCategory.Values.ToList();
        }

        if (spendingByMonth.Last().Key != previousPreviousMonthStart.Month)
        {
            previousPreviousMonthSpending = new decimal[categories.Count].ToList();
        }
        else
        {
            var previousPreviousMonthSpendingByCategory = spendingByMonth.Last()
                .GroupBy(transaction => transaction.TransactionCategory.Name).OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, y => y.Sum(z => z.Value));

            var totalPreviousPreviousMonthSpendingByCategory = categories.ToDictionary(category => category, _ => 0M);
        
            foreach (var category in categories)
            {
                if (previousPreviousMonthSpendingByCategory.TryGetValue(category, out var sum))
                {
                    totalPreviousPreviousMonthSpendingByCategory[category] += sum;
                }
            }

            previousPreviousMonthSpending = totalPreviousPreviousMonthSpendingByCategory.Values.ToList();
        }
        
        return new SpendingTrendsDTO
        {
            Categories = categories,
            PreviousMonth = new MonthlyExpenses
            {
                Name = previousMonthEnd.ToString("MMMM"),
                Values = previousMonthSpending
            },
            PreviousPreviousMonth = new MonthlyExpenses
            {
                Name = previousPreviousMonthStart.ToString("MMMM"),
                Values = previousPreviousMonthSpending
            }
        };
    }

    public async Task<List<ForecastByCategoryDTO>> GetForecastByCategoriesAsync()
    {
        var currentDate = DateTime.UtcNow.Date;
        var previousMonthEnd = currentDate.AddDays(currentDate.Day * -1);
        var previousTwelfthMonthStart = currentDate.AddDays(1 + currentDate.Day * -1).AddYears(-1);
        
        var consumptions = await _repository.GetAsync(transaction => 
            transaction.Date >= previousTwelfthMonthStart && transaction.Date <= previousMonthEnd &&
            transaction.TransactionCategory.IsConsumption);

        var spendingByCategory = consumptions.GroupBy(x => x.TransactionCategory.Name)
            .OrderByDescending(x => x.Key).ToList();

        var forecast = new List<ForecastByCategoryDTO>();
        
        foreach (var spending in spendingByCategory)
        {
            var spendingByMonth = spending.GroupBy(s => (s.Date.Year, s.Date.Month)).ToList();
            
            if(spendingByMonth.Count <= 3) continue;

            var spendingSumByMonth = spendingByMonth.OrderByDescending(x => x.Key)
                .Select(x => x.Sum(transaction => transaction.Value)).ToList();
            var timeSymbols = spendingByMonth.Select(x => x.Key).ToList();
            
            forecast.Add(new ForecastByCategoryDTO
            {
                Category = spending.Key,
                Forecast = LeastSquares.ForecastForOneValue(spendingSumByMonth, timeSymbols)
            });
        }
        
        return forecast;
    }

    public async Task<bool> UpdateAsync(UpdateMoneyTransactionRequest updateTransactionRequest)
    {
        var existingCategory = await _categoryRepository.GetByNameAsync(updateTransactionRequest.Category);

        if (existingCategory is null)
        {
            throw new ValidationExceptionResult(TransactionCategoryExceptionMessages.CategoryIsNotAvailable);
        }

        var moneyTransaction = _mapper.Map<MoneyTransaction>(updateTransactionRequest);

        moneyTransaction.TransactionCategoryId = existingCategory.Id;
        
        await _repository.UpdateAsync(moneyTransaction);

        if (!moneyTransaction.TransactionCategory.IsConsumption)
        {
            await UpdateCategoryPriorities(moneyTransaction.Date);
        }
        
        return true;
    }

    private async Task UpdateCategoryPriorities(DateTime periodEnd)
    {
        var monthStart = periodEnd.AddDays(-1 * periodEnd.Day + 1);

        var consumptionCategories = await _categoryRepository.GetAsync(category => category.IsConsumption);
        var incomeCategories = await _repository.GetAsync(transaction =>
            transaction.Date >= monthStart && transaction.Date <= periodEnd &&
            !transaction.TransactionCategory.IsConsumption);

        var totalIncome = incomeCategories.Sum(income => income.Value);
        var totalPriority = consumptionCategories.Sum(category => category.Priority);

        foreach (var category in consumptionCategories)
        {
            category.Limit = totalIncome * category.Priority / totalPriority;
        }
        
        await _categoryRepository.UpdateRangeAsync(consumptionCategories);
    }
}
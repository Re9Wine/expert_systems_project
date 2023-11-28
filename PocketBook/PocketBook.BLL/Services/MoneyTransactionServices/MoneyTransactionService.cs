using AutoMapper;
using PocketBook.BLL.Exceptions;
using PocketBook.BLL.Resources;
using PocketBook.DAL.Repositories.MoneyTransactionRepositories;
using PocketBook.DAL.Repositories.TransactionCategoryRepositories;
using PocketBook.Domain.DTOs;
using PocketBook.Domain.Entities;
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

        moneyTransaction.CategoryId = existingCategory.Id;

        var moneyTransactionEntity = await _repository.AddAsync(moneyTransaction);

        if (!moneyTransactionEntity.TransactionCategory.IsConsumption)
        {
            await UpdateCategoryPriorities(moneyTransactionEntity.Date.Date);
        }

        return _mapper.Map<MoneyTransactionDTO>(moneyTransactionEntity);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        return _repository.DeleteAsync(new MoneyTransaction { Id = id });
    }

    public async Task<List<MoneyTransactionDTO>> GetRangeAsync(int skip, int take, bool isConsumption = true)
    {
        var transactions = await _repository.GetAsync(
            transaction => transaction.TransactionCategory.IsConsumption == isConsumption,
            query => query.OrderByDescending(transaction => transaction.Date),
            skip, take);

        return _mapper.Map<List<MoneyTransactionDTO>>(transactions);
    }

    public async Task<List<MoneyTransactionDTO>> GetConsumptionPerPeriodAsync(DateTime periodStart, DateTime periodEnd)
    {
        var transactions = await _repository.GetAsync(transaction => 
                transaction.Date >= periodStart && transaction.Date <= periodEnd && 
                transaction.TransactionCategory.IsConsumption,
            query => query.OrderByDescending(transaction => transaction.Date));

        return _mapper.Map<List<MoneyTransactionDTO>>(transactions);
    }

    public async Task<List<BarCharDTO>> GetConsumptionForBarCharAsync(DateTime periodEnd)
    {
        var periodStart = periodEnd.AddDays(-1 * (int)periodEnd.DayOfWeek);
        
        var transactions = await _repository.GetAsync(transaction =>
            transaction.Date >= periodStart && transaction.Date <= periodEnd &&
            transaction.TransactionCategory.IsConsumption);
        var transactionsGroupByDate = transactions.GroupBy(transaction => transaction.Date);

        return _mapper.Map<List<BarCharDTO>>(transactionsGroupByDate);
    }

    public async Task<List<DoughnutDTO>> GetConsumptionForDoughnutAsync(DateTime periodEnd)
    {
        var periodStart = periodEnd.AddDays(-1 * (int)periodEnd.DayOfWeek);
        
        var transactions = await _repository.GetAsync(transaction =>
            transaction.Date >= periodStart && transaction.Date <= periodEnd &&
            transaction.TransactionCategory.IsConsumption);
        var transactionsGroupByCategory = transactions.GroupBy(transaction => transaction.TransactionCategory.Name);

        return _mapper.Map<List<DoughnutDTO>>(transactionsGroupByCategory);
    }

    public async Task<List<ConsumptionTableDTO>> GetMonthlyConsumptionAsync(DateTime periodEnd)
    {
        var monthStart = periodEnd.AddDays(-1 * periodEnd.Day + 1);
        
        var transactions = await _repository.GetAsync(transaction => 
                transaction.Date >= monthStart && transaction.Date <= periodEnd && 
                transaction.TransactionCategory.IsConsumption,
            query => query.OrderByDescending(transaction => transaction.Date));
        var transactionsGroupByCategory = transactions.GroupBy(transaction => transaction.TransactionCategory.Name);

        return _mapper.Map<List<ConsumptionTableDTO>>(transactionsGroupByCategory);
    }

    public async Task<List<string>> GetMonthlyRecommendationsAsync(DateTime periodEnd)
    {
        var monthStart = periodEnd.AddDays(-1 * periodEnd.Day + 1);
        
        var consumption = await _repository.GetAsync(transaction => 
                transaction.Date >= monthStart && transaction.Date <= periodEnd && 
                transaction.TransactionCategory.IsConsumption,
            query => query.OrderByDescending(transaction => transaction.Date));
        var consumptionGroupByCategory = consumption.GroupBy(transaction => transaction.TransactionCategory.Name);

        var recommendations = consumptionGroupByCategory.Where(transactionGroup => transactionGroup.Any())
            .Select(transactionGroup =>
            {
                var categoryConsumption = transactionGroup.Sum(transaction => transaction.Value);
                var limit = transactionGroup.First().TransactionCategory.Limit;
                var howMuchIsLimitExceeded = categoryConsumption / limit;

                return howMuchIsLimitExceeded switch
                {
                    >= 1.3m => string.Format(RecommendationsMessages.CategoryRecommendationMessageFormat,
                        Math.Round(categoryConsumption, 2), Math.Round(limit, 2), transactionGroup.Key,
                        RecommendationsMessages.LimitExceeded),
                    >= 1.0m => string.Format(RecommendationsMessages.CategoryRecommendationMessageFormat,
                        Math.Round(categoryConsumption, 2), Math.Round(limit, 2), transactionGroup.Key,
                        RecommendationsMessages.LimitIsSlightlyExceeded),
                    _ => string.Format(RecommendationsMessages.CategoryRecommendationMessageFormat,
                        Math.Round(categoryConsumption, 2), Math.Round(limit, 2), transactionGroup.Key,
                        RecommendationsMessages.LimitNotExceeded)
                };
            }).ToList();
        
        var income = await _repository.GetAsync(transaction => 
                transaction.Date >= monthStart && transaction.Date <= periodEnd && 
                !transaction.TransactionCategory.IsConsumption,
            query => query.OrderByDescending(transaction => transaction.Date));
        var totalIncome = income.Sum(transaction => transaction.Value);
        var totalConsumption = consumption.Sum(transaction => transaction.Value);

        var totalMoneyRecommendation = (totalConsumption / totalIncome) switch
        {
            >= 1.0m => string.Format(RecommendationsMessages.TotalMoneyRecommendationMessageFormat,
                Math.Round(totalIncome - totalConsumption, 2), Math.Round(totalIncome),
                RecommendationsMessages.FindWayToEarnMoney),
            _ => string.Format(RecommendationsMessages.TotalMoneyRecommendationMessageFormat,
                Math.Round(totalIncome - totalConsumption, 2), Math.Round(totalIncome),
                RecommendationsMessages.AllGood),
        };
        
        recommendations.Add(totalMoneyRecommendation);
        
        return recommendations;
    }

    public async Task<bool> UpdateAsync(UpdateMoneyTransactionRequest updateTransactionRequest)
    {
        var existingCategory = await _categoryRepository.GetByNameAsync(updateTransactionRequest.Category);

        if (existingCategory is null)
        {
            throw new ValidationExceptionResult(TransactionCategoryExceptionMessages.CategoryIsNotAvailable);
        }

        var moneyTransaction = _mapper.Map<MoneyTransaction>(updateTransactionRequest);

        moneyTransaction.CategoryId = existingCategory.Id;
        
        await _repository.UpdateAsync(moneyTransaction);

        if (!moneyTransaction.TransactionCategory.IsConsumption)
        {
            await UpdateCategoryPriorities(moneyTransaction.Date.Date);
        }
        
        return true;
    }

    private async Task UpdateCategoryPriorities(DateTime periodEnd)
    {
        var monthStart = periodEnd.AddDays(-1 * periodEnd.Day + 1);
        
        var consumptionCategories = await _categoryRepository.GetAsync(category => category.IsConsumption);
        var monthIncome = await _repository.GetAsync(transaction => 
            transaction.Date >= monthStart && transaction.Date <= periodEnd && 
            !transaction.TransactionCategory.IsConsumption);
        
        var totalIncome = monthIncome.Sum(income => income.Value);
        var totalPriority = consumptionCategories.Sum(category => category.Priority);

        foreach (var category in consumptionCategories)
        {
            category.Limit = totalIncome * category.Priority / totalPriority;
        }

        await _categoryRepository.UpdateRangeAsync(consumptionCategories);
    }
}
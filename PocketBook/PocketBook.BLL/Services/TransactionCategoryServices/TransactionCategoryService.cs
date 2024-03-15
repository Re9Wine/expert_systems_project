using AutoMapper;
using PocketBook.BLL.Exceptions;
using PocketBook.BLL.Resources;
using PocketBook.DAL.Repositories.TransactionCategoryRepositories;
using PocketBook.Domain.DTOs;
using PocketBook.Domain.Entities;
using PocketBook.Domain.Requests.TransactionCategoryRequests;

namespace PocketBook.BLL.Services.TransactionCategoryServices;

public class TransactionCategoryService : ITransactionCategoryService
{
    private readonly IMapper _mapper;
    private readonly ITransactionCategoryRepository _repository;

    public TransactionCategoryService(IMapper mapper, ITransactionCategoryRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<TransactionCategoryDTO?> CreateAsync(CreateTransactionCategoryRequest createCategoryRequest)
    {
        var existingCategory = await _repository.GetByNameAsync(createCategoryRequest.Name);

        if (existingCategory is not null)
        {
            throw new ValidationExceptionResult(TransactionCategoryExceptionMessages.CategoryExists);
        }
        
        var category = _mapper.Map<TransactionCategory>(createCategoryRequest);
        var categoryEntity = await _repository.AddAsync(category);

        return _mapper.Map<TransactionCategoryDTO>(categoryEntity);
    }

    public async Task<bool> UpdateAsync(UpdateTransactionCategoryRequest updateCategoryRequest)
    {
        var category = await _repository.GetByNameAsync(updateCategoryRequest.Name);

        if (category is null)
        {
            throw new ValidationExceptionResult(TransactionCategoryExceptionMessages.CategoryIsNotAvailable);
        }
        
        if (!category.IsChangeable)
        {
            throw new ValidationExceptionResult(TransactionCategoryExceptionMessages.CategoryIsImmutable);
        }

        category.Limit = updateCategoryRequest.Limit;
        
        await _repository.UpdateAsync(category);
        
        return true;
    }

    public async Task<bool> DeleteAsync(string name)
    {
        var category = await _repository.GetByNameAsync(name);

        if (category is null)
        {
            return false;
        }
        
        return await _repository.DeleteAsync(category);
    }

    public async Task<List<TransactionCategoryDTO>> GetByTypeAsync(bool isConsumption)
    {
        var categories = await _repository.GetAsync(category => category.IsConsumption == isConsumption);

        return _mapper.Map<List<TransactionCategoryDTO>>(categories);
    }

    public async Task<List<TransactionCategoryDTO>> GetChangeableAsync(bool isConsumption)
    {
        var categories = await _repository.GetAsync(category => 
            category.IsConsumption == isConsumption && category.IsChangeable);

        return _mapper.Map<List<TransactionCategoryDTO>>(categories);
    }

    public async Task<List<ConsumptionTableDTO>> GetMonthlyConsumptionAsync(DateTime periodEnd)
    {
        var monthStart = periodEnd.Date.AddDays(-1 * periodEnd.Day + 1);

        var categories = await _repository.GetAsync(
            filter: category => category.IsConsumption,
            orderBy: query => query.OrderByDescending(category => category.Name));
        
        var monthlyConsumption = categories.Select(category =>
        {
            var sum = 0M;
            
            if (category.MoneyTransactions.Any())
            {
                sum = category.MoneyTransactions
                    .Where(transaction => transaction.Date.Date >= monthStart && transaction.Date <= periodEnd)
                    .Sum(transaction => transaction.Value);
            }
            
            return new ConsumptionTableDTO
            {
                Category = category.Name,
                Sum = sum,
                Limit = category.Limit
            };
        }).ToList();

        return monthlyConsumption;
    }
}
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
        var existingCategoryName = await _repository.GetByNameAsync(updateCategoryRequest.Name);

        if (existingCategoryName is null)
        {
            throw new ValidationExceptionResult(TransactionCategoryExceptionMessages.CategoryIsNotAvailable);
        }
        
        if (!existingCategoryName.IsChangeable)
        {
            throw new ValidationExceptionResult(TransactionCategoryExceptionMessages.CategoryIsImmutable);
        }

        var category = _mapper.Map<TransactionCategory>(updateCategoryRequest);

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
        var monthStart = periodEnd.AddDays(-1 * periodEnd.Day + 1);
        
        var categories = await _repository.GetAsync(category => 
            (!category.MoneyTransactions.Any() || category.MoneyTransactions.Any(transaction => 
                transaction.Date >= monthStart && transaction.Date <= periodEnd)) &&
            category.IsConsumption);
        var monthlyConsumption = categories.Select(category =>
        {
            if (!category.MoneyTransactions.Any())
            {
                return new ConsumptionTableDTO
                {
                    Category = category.Name,
                    Sum = category.MoneyTransactions.Sum(transaction => transaction.Value),
                    Limit = category.Limit
                };
            }

            return new ConsumptionTableDTO
            {
                Category = category.Name,
                Sum = 0,
                Limit = category.Limit
            };
        }).ToList();

        return monthlyConsumption;
    }
}
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
        var existingCategoryId = await _repository.GetByIdAsync(updateCategoryRequest.Id);

        if (existingCategoryId is null)
        {
            throw new ValidationExceptionResult(TransactionCategoryExceptionMessages.CategoryIsNotAvailable);
        }
        
        if (!existingCategoryId.IsChangeable)
        {
            throw new ValidationExceptionResult(TransactionCategoryExceptionMessages.CategoryIsImmutable);
        }

        var existingCategoryName = await _repository.GetByNameAsync(updateCategoryRequest.Name);

        if (existingCategoryName is not null)
        {
            throw new ValidationExceptionResult(TransactionCategoryExceptionMessages.CategoryExists);
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
}
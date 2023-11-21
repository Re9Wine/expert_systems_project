using AutoMapper;
using DAL.Interfaces;
using Domain.DatabaseEntity;
using Domain.ViewEntity;
using Service.Interfaces;

namespace Service.Implementations;

public class OperationCategoryService : IOperationCategoryService
{
    private readonly IOperationCategoryRepository _repository;
    private readonly MapperConfiguration _configCategoryAndView;

    public OperationCategoryService(IOperationCategoryRepository repository)
    {
        _repository = repository;
        _configCategoryAndView = new MapperConfiguration(config =>
            config.CreateMap<OperationCategory, OperationCategoryView>().ReverseMap());
    }
    public async Task<bool> CreateAsync(OperationCategoryView categoryView)
    {
        if (await _repository.GetByNameAsync(categoryView.Name) is not null)
        {
            return false;
        }
                
        var mapper = new Mapper(_configCategoryAndView);
        var category = mapper.Map<OperationCategory>(categoryView);
        
        category.IsChangeable = true;
            
        return await _repository.CreateAsync(category);
    }

    public async Task<bool> UpdateAsync(OperationCategoryView categoryView)
    {
        if (await _repository.GetByNameAsync(categoryView.Name) is not {} oldCategory)
        {
            return false;
        }

        if (oldCategory.Name == "Зарплата")
        {
            return await UpdateSalaryAsync(categoryView.Limit);
        }
                
        var mapper = new Mapper(_configCategoryAndView);
        var category = mapper.Map<OperationCategory>(categoryView);

        if (!oldCategory.IsChangeable)
        {
            category.IsConsumption = oldCategory.IsConsumption;
        }
            
        category.Id = oldCategory.Id;
        category.Priority = oldCategory.Priority;

        return await _repository.UpdateAsync(category);
    }

    public async Task<bool> DeleteAsync(string name)
    {
        if (await _repository.GetByNameAsync(name) is not { } category)
        {
            return false;
        }

        return await _repository.DeleteAsync(category);
    }

    public async Task<List<OperationCategoryView>> GetRangeAsync(int pageNumber, int pageElementCount)
    {
        var mapper = new Mapper(_configCategoryAndView);
        var categories = _repository.GetRangeAsync(pageElementCount, pageNumber * pageElementCount);

        return mapper.Map<List<OperationCategoryView>>(await categories);
    }

    private async Task<bool> UpdateSalaryAsync(decimal limit)
    {
        var categories = await _repository.GetAllConsumption();
        var totalPriority = categories.Sum(_ => _.Priority);

        foreach (var category in categories)
        {
            category.Limit = (decimal)(category.Priority * limit / totalPriority);
        }
        
        return await _repository.UpdateRangeAsync(categories);
    }
}

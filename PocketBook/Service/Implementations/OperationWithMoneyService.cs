using AutoMapper;
using DAL.Interfaces;
using Domain;
using Domain.DatabaseEntity;
using Domain.ViewEntity;
using Service.Interfaces;

namespace Service.Implementations;

public class OperationWithMoneyService : IOperationWithMoneyService
{
    private readonly IOperationWithMoneyRepository _repository;
    private readonly MapperConfiguration _configOperationAndView;

    public OperationWithMoneyService(IOperationWithMoneyRepository repository)
    {
        _repository = repository;
        _configOperationAndView = new MapperConfiguration(config =>
            config.CreateMap<OperationWithMoney, OperationWithMoneyView>().ReverseMap());
    }
        
    public async Task<bool> CreateAsync(OperationWithMoneyView operationView)
    {
        if (await _repository.GetByIdAsync(operationView.Id) is not null)
        {
            return false;
        }
        
        var mapper = new Mapper(_configOperationAndView);
        var operation = mapper.Map<OperationWithMoney>(operationView);

        operationView.Date = DateTime.Now;

        return await _repository.CreateAsync(operation);
    }

    public async Task<bool> UpdateAsync(OperationWithMoneyView operationView)
    {
        if (await _repository.GetByIdAsync(operationView.Id) is not { } oldOperation)
        {
            return false;
        }

        var mapper = new Mapper(_configOperationAndView);
        var operation = mapper.Map<OperationWithMoney>(operationView);

        operation.Date = oldOperation.Date;
        
        return await _repository.UpdateAsync(operation);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        if (await _repository.GetByIdAsync(id) is not { } operation)
        {
            return false;
        }

        return await _repository.DeleteAsync(operation);
    }

    public async Task<List<OperationWithMoneyView>> GetRangeWithCategoriesAsync(bool isConsumption, int pageNumber,
        int pageElementCount)
    {
        var mapper = new Mapper(_configOperationAndView);
        var operations =
            _repository.GetRangeWithCategoriesAsync(isConsumption, pageElementCount, pageNumber * pageElementCount);

        return mapper.Map<List<OperationWithMoneyView>>(await operations);
    }

    public async Task<List<BarChartView>> GetWeeklyForBarCharAsync(bool isConsumption, DateTime finalDate)
    {
        finalDate = finalDate.Date;

        var weekStart = finalDate.AddDays(-1 * (int)finalDate.DayOfWeek);
        var operations = await _repository.GetPerPeriodWithCategoriesAsync(isConsumption, weekStart, finalDate);
        
        return ConvertOperationWithMoneyToBarChartView(operations);
    }

    public async Task<List<BarChartView>> GetMonthlyForBarCharAsync(bool isConsumption, DateTime finalDate)
    {
        finalDate = finalDate.Date;

        var weekStart = finalDate.AddDays(-1 * (int)finalDate.DayOfWeek);
        var operations = await _repository.GetPerPeriodWithCategoriesAsync(isConsumption, weekStart, finalDate);
        
        return ConvertOperationWithMoneyToBarChartView(operations);
    }

    public async Task<List<DoughnutView>> GetWeeklyForDoughnutAsync(bool isConsumption, DateTime finalDate)
    {
        finalDate = finalDate.Date;

        var weekStart = finalDate.AddDays(-1 * (int)finalDate.DayOfWeek);
        var operations = await _repository.GetPerPeriodWithCategoriesAsync(isConsumption, weekStart, finalDate);

        return ConvertOperationWithMoneyToDoughnutView(operations);
    }

    public async Task<List<DoughnutView>> GetMonthlyForDoughnutAsync(bool isConsumption, DateTime finalDate)
    {
        finalDate = finalDate.Date;

        var monthStart = finalDate.AddDays(-1 * finalDate.Day);
        var operations = await _repository.GetPerPeriodWithCategoriesAsync(isConsumption, monthStart, finalDate);
        
        return ConvertOperationWithMoneyToDoughnutView(operations);
    }

    private List<DoughnutView> ConvertOperationWithMoneyToDoughnutView(
        ICollection<OperationWithMoney> operations) => operations
        .GroupBy(e => e.OperationCategoryNavigation.Name).Select(e => new DoughnutView
        {
            Category = e.Key,
            Value = e.Sum(o => o.Value),
            ErrorMessages = GetErrorMessages(operations.ToList())
        }).ToList();

    private List<BarChartView> ConvertOperationWithMoneyToBarChartView(
        ICollection<OperationWithMoney> operations) => operations
        .GroupBy(e => e.Date.Date).Select(e => new BarChartView
        {
            Date = e.Key,
            Sum = e.Sum(o => o.Value),
            ErrorMessages = GetErrorMessages(operations.ToList())
        }).ToList();

    private List<string> GetErrorMessages(List<OperationWithMoney> operations)
    {
        var errorMessages = new List<string>();
        var monthlyIncome = _repository.GetMonthlyIncome();

        if (operations.Sum(e => e.Value) > monthlyIncome)
        {
            errorMessages.Add(Resources.ExcessConsumption);
        }

        errorMessages.AddRange(from operation in operations.GroupBy(e => e.OperationCategoryNavigation.Name)
            where operation.Sum(e => e.Value) > operation.First().OperationCategoryNavigation.Limit
            select string.Format(Resources.ExcessConsumptionByCategoryFormat, operation.Key));

        return errorMessages;
    }
}

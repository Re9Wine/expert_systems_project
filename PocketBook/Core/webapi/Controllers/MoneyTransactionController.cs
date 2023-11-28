using Microsoft.AspNetCore.Mvc;
using PocketBook.BLL.Services.MoneyTransactionServices;
using PocketBook.Domain.Requests.MoneyTransactionRequests;
using webapi.Extensions;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class MoneyTransactionController : ControllerBase
{
    private readonly IMoneyTransactionService _service;

    public MoneyTransactionController(IMoneyTransactionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(object formData)
    {
        var createMoneyTransactionRequest = JsonSerializer<CreateMoneyTransactionRequest>.Deserialize(formData);
        var transaction = await _service.CreateAsync(createMoneyTransactionRequest);

        return Ok(transaction);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(Guid operationId)
    {
        var isDeleted = await _service.DeleteAsync(operationId);
        
        return isDeleted ? Ok() : BadRequest();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(object formData)
    {
        var updateMoneyTransactionRequest = JsonSerializer<UpdateMoneyTransactionRequest>.Deserialize(formData);
        var isUpdated = await _service.UpdateAsync(updateMoneyTransactionRequest);

        return isUpdated ? Ok() : BadRequest();
    }

    [HttpGet]
    [Route("GetFiveLastedConsumption")]
    public async Task<IActionResult> GetFiveLastedConsumption()
    {
        var transactions = await _service.GetRangeAsync(0, 5);

        return transactions.Any() ? Ok(transactions) : NoContent();
    }

    [HttpGet]
    [Route("GetForBarChar")]
    public async Task<IActionResult> GetForBarCharAsync()
    {
        var barCharDTOs = await _service.GetConsumptionForBarCharAsync(DateTime.Now);

        return barCharDTOs.Any() ? Ok(barCharDTOs) : NoContent();
    }

    [HttpGet]
    [Route("GetForDoughnut")]
    public async Task<IActionResult> GetForDoughnutAsync()
    {
        var doughnutDTOs = await _service.GetConsumptionForDoughnutAsync(DateTime.Now);

        return doughnutDTOs.Any() ? Ok(doughnutDTOs) : NoContent();
    }

    [HttpGet]
    [Route("GetMonthlyRecommendationsAsync")]
    public async Task<IActionResult> GetMonthlyRecommendationsAsync()
    {
        var recommendations = await _service.GetMonthlyRecommendationsAsync(DateTime.Now);

        return recommendations.Any() ? Ok(recommendations) : NoContent();
    }

    [HttpGet]
    [Route("GetMonthlyConsumptionAsync")]
    public async Task<IActionResult> GetMonthlyConsumptionAsync()
    {
        var consumptionTableDTOs = await _service.GetMonthlyConsumptionAsync(DateTime.Now);

        return consumptionTableDTOs.Any() ? Ok(consumptionTableDTOs) : NoContent();
    }
}
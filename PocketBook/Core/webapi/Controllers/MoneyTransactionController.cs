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
    public async Task<IActionResult> CreateAsync([FromBody] CreateMoneyTransactionRequest createRequest)
    {
        var transaction = await _service.CreateAsync(createRequest);

        return Ok(transaction);
    }

    [HttpDelete("{operationId:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid operationId)
    {
        var isDeleted = await _service.DeleteAsync(operationId);
        
        return isDeleted ? Ok() : BadRequest();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateMoneyTransactionRequest updateRequest)
    {
        var isUpdated = await _service.UpdateAsync(updateRequest);

        return isUpdated ? Ok() : BadRequest();
    }

    [HttpGet("FiveLastedConsumption")]
    public async Task<IActionResult> GetFiveLastedConsumption()
    {
        var transactions = await _service.GetFiveLastedConsumption();

        return transactions.Any() ? Ok(transactions) : NoContent();
    }

    [HttpGet("ForBarChar")]
    public async Task<IActionResult> GetForBarCharAsync()
    {
        var barCharDTOs = await _service.GetConsumptionForBarCharAsync(DateTime.UtcNow.Date);

        return barCharDTOs.Any() ? Ok(barCharDTOs) : NoContent();
    }

    [HttpGet("ForDoughnut")]
    public async Task<IActionResult> GetForDoughnutAsync()
    {
        var doughnutDTOs = await _service.GetConsumptionForDoughnutAsync(DateTime.UtcNow.Date);

        return doughnutDTOs.Any() ? Ok(doughnutDTOs) : NoContent();
    }

    [HttpGet("MonthlyRecommendations")]
    public async Task<IActionResult> GetMonthlyRecommendationsAsync()
    {
        var recommendations = await _service.GetMonthlyRecommendationsAsync(DateTime.UtcNow.Date);

        return recommendations.Any() ? Ok(recommendations) : NoContent();
    }

    [HttpGet("Page/{pageNumber:int}")]
    public async Task<IActionResult> GetConsumptionPage([FromRoute] int pageNumber)
    {
        const int pageSize = 15;
        var page = await _service.GetRangeAsync(pageNumber, pageSize);

        return page.Transactions.Any() ? Ok(page) : NoContent();
    }
}
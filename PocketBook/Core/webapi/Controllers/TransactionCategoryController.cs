using Microsoft.AspNetCore.Mvc;
using PocketBook.BLL.Services.TransactionCategoryServices;
using PocketBook.Domain.Requests.TransactionCategoryRequests;
using PocketBook.Domain.Resources;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionCategoryController : ControllerBase
{
    private readonly ITransactionCategoryService _service;

    public TransactionCategoryController(ITransactionCategoryService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTransactionCategoryRequest createRequest)
    {
        var category = await _service.CreateAsync(createRequest);

        return Ok(category);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateTransactionCategoryRequest updateRequest)
    {
        var isUpdated = await _service.UpdateAsync(updateRequest);
        
        return isUpdated ? Ok() : BadRequest();
    }

    [HttpDelete("{name}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest(ValidationExceptionMessages.StringIsEmpty);
        }

        var isDeleted = await _service.DeleteAsync(name);

        return isDeleted ? Ok() : BadRequest();
    }

    [HttpGet("ByType/{isConsumption:bool}")]
    public async Task<IActionResult> GetByTypeAsync([FromRoute] bool isConsumption)
    {
        var categories = await _service.GetByTypeAsync(isConsumption);

        return categories.Any() ? Ok(categories) : NoContent();
    }

    [HttpGet("Changeable")]
    public async Task<IActionResult> GetChangeableAsync()
    {
        var categories = await _service.GetChangeableAsync(true);

        return categories.Any() ? Ok(categories) : NoContent();
    }
    
    [HttpGet("MonthlyConsumption")]
    public async Task<IActionResult> GetMonthlyConsumptionAsync()
    {
        var consumptionTableDTOs = await _service.GetMonthlyConsumptionAsync(DateTime.Now);

        return consumptionTableDTOs.Any() ? Ok(consumptionTableDTOs) : NoContent();
    }
}
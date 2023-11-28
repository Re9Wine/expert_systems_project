using Microsoft.AspNetCore.Mvc;
using PocketBook.BLL.Services.TransactionCategoryServices;
using PocketBook.Domain.Requests.TransactionCategoryRequests;
using PocketBook.Domain.Resources;
using webapi.Extensions;

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
    public async Task<IActionResult> CreateAsync(object formData)
    {
        var createCategoryRequest = JsonSerializer<CreateTransactionCategoryRequest>.Deserialize(formData);
        var category = await _service.CreateAsync(createCategoryRequest);

        return Ok(category);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(object formData)
    {
        var updateCategoryRequest = JsonSerializer<UpdateTransactionCategoryRequest>.Deserialize(formData);
        var isUpdated = await _service.UpdateAsync(updateCategoryRequest);
        
        return isUpdated ? Ok() : BadRequest();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest(ValidationExceptionMessages.StringIsEmpty);
        }

        var isDeleted = await _service.DeleteAsync(name);

        return isDeleted ? Ok() : BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetByTypeAsync(bool isConsumption)
    {
        var categories = await _service.GetByTypeAsync(isConsumption);

        return Ok(categories);
    }
}
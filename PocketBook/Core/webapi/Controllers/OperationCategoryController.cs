using Domain.DatabaseEntity;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Text.Json;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperationCategoryController : ControllerBase
    {
        private readonly IOperationCategorySercvice _sercvice;

        public OperationCategoryController(IOperationCategorySercvice sercvice)
        {
            _sercvice = sercvice;
        }

        [HttpGet]
        public async Task<IActionResult> GetByType(bool isConsumption)
        {
            var result = await _sercvice.GetByType(isConsumption);

            if (result == null || result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(object formData)
        {
            var operationCategory = JsonSerializer.Deserialize<OperationCategory>((JsonElement)formData);

            if (operationCategory == null)
            {
                return BadRequest();
            }

            return await _sercvice.Create(operationCategory) ? Ok() : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(object formData)
        {
            var operationCategory = JsonSerializer.Deserialize<OperationCategory>((JsonElement)formData);

            if (operationCategory == null)
            {
                return BadRequest();
            }

            return await _sercvice.Update(operationCategory) ? Ok() : BadRequest();
        }
    }
}

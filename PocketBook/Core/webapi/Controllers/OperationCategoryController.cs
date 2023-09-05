using Domain;
using Domain.DatabaseEntity;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

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

        [HttpPost]
        public async Task<IActionResult> CreateAsync(object formData)
        {
            var operationCategory = JsonSerializer<OperationCategory>.Deserialize(formData);

            if (operationCategory == null)
            {
                return BadRequest();
            }

            return await _sercvice.CreateAsync(operationCategory) ? Ok() : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(object formData)
        {
            var operationCategory = JsonSerializer<OperationCategory>.Deserialize(formData);

            if (operationCategory == null)
            {
                return BadRequest();
            }

            return await _sercvice.UpdateAsync(operationCategory) ? Ok() : BadRequest();
        }

        [HttpGet]
        [Route("GetWeekly")]
        public async Task<IActionResult> GetWeeklyAsync(bool isConsumption, DateTime finalDate = default)
        {
            var result = await _sercvice.GetWeeklyAsync(isConsumption, finalDate);

            if (result.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("GetMonthly")]
        public async Task<IActionResult> GetMonthlyAsync(bool isConsumption, DateTime finalDate = default)
        {
            var result = await _sercvice.GetMonthlyAsync(isConsumption, finalDate);

            if (result.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _sercvice.GetAllAsync();

            if (result.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }
    }
}

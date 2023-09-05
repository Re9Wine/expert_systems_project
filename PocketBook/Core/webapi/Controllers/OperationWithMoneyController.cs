using Domain;
using Domain.DatabaseEntity;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperationWithMoneyController : ControllerBase
    {
        private readonly IOperationWithMoneyService _service;

        public OperationWithMoneyController(IOperationWithMoneyService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetWeeklyGroupByDay")]
        public async Task<IActionResult> GetWeeklyGroupByDayAsync(bool isConsumption, DateTime finalDate = default)
        {
            var result = await _service.GetWeeklyGroupByDayAsync(isConsumption, finalDate);

            if (result.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("GetMonthlyGroupByDay")]
        public async Task<IActionResult> GetMonthlyGroupByDayAsync(bool isConsumption, DateTime finalDate = default)
        {
            var result = await _service.GetMonthlyGroupByDayAsync(isConsumption, finalDate);

            if(result.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("GetRange")]
        public async Task<IActionResult> GetRangeAsync(bool isConsumption, int amount = 5, int skip = 0)
        {
            var result = await _service.GetRangeAsync(isConsumption, amount, skip);

            if(result.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(object formData)
        {
            var operationWithMoney = JsonSerializer<OperationWithMoney>.Deserialize(formData);

            if (operationWithMoney == null)
            {
                return BadRequest();
            }

            return await _service.CreateAsync(operationWithMoney) ? Ok() : BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> UpdateAsync(Guid operationWithMoneyId)
        {
            return await _service.DeleteAsync(operationWithMoneyId) ? Ok() : BadRequest();
        }
    }
}

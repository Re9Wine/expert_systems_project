using Domain.DatabaseEntity;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Text.Json;

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
        [Route("FiveLatest")]
        public async Task<IActionResult> GetFiveLatestAsync(bool isConsumption)
        {
            var result = await _service.GetFiveLates(isConsumption);

            if (result == null || result.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("Weekly")]
        public async Task<IActionResult> GetWeeklyConsumptionAsync(bool isConsumption)
        {
            var result = await _service.GetWeekly(isConsumption);

            if (result == null || result.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("WeeklyGroupByDay")]
        public async Task<IActionResult> GetWeeklyConsumptionGropByDay(bool isConsumption)
        {
            var result = await _service.GetWeeklyGroupByDay(isConsumption);

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(object formData)
        {
            var operationWithMoneyFormData = JsonSerializer.Deserialize<OperationWithMoney>((JsonElement)formData);

            if (operationWithMoneyFormData == null)
            {
                return BadRequest();
            }

            return await _service.Create(operationWithMoneyFormData) ? Ok() : BadRequest();
        }
    }
}

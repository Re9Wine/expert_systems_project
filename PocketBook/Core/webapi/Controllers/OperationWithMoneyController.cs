using Domain;
using Domain.Entity;
using Domain.View;
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
        [Route("FiveLatestConsumption")]
        public async Task<IActionResult> GetFiveLatestConsumptionAsync()
        {
            var result = await _service.GetFiveLatesConsumption();

            if(result == null || result.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("WeeklyConsumption")]
        public async Task<IActionResult> GetWeeklyConsumptionAsync()
        {
            var result = await _service.GetWeeklyConsumption();

            if(result == null || result.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("WeeklyConsumptionGroupByDay")]
        public async Task<IActionResult> GetWeeklyConsumptionGropByDay()
        {
            var result = await _service.GetWeeklyConsumptionGroupByDay();

            if(result == null)
            {
                return NoContent() ;
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(object formData)
        {
            var operationWithMoneyFormData = JsonSerializer.Deserialize<OperationWithMoneyForTableView>((JsonElement) formData);

            if(operationWithMoneyFormData == null)
            {
                return BadRequest();
            }

            return await _service.Create(operationWithMoneyFormData) ? Ok() : BadRequest();
        }
    }
}

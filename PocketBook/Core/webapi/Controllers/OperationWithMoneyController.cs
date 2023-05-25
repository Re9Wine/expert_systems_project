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
    }
}

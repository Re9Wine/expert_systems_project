using Domain;
using Domain.ViewEntity;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperationWithMoneyController : ControllerBase // TODO добавить обработку ошибок + ошибок при валидации
    {
        private readonly IOperationWithMoneyService _service;

        public OperationWithMoneyController(IOperationWithMoneyService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(object formData)
        {
            if (JsonSerializer<OperationWithMoneyView>.Deserialize(formData) is not {} operationView)
            {
                return BadRequest(); // TODO добавить список ошибок
            }

            return await _service.CreateAsync(operationView) ? Ok() : BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid operationId)
        {
            return await _service.DeleteAsync(operationId) ? Ok() : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(object formData)
        {
            if (JsonSerializer<OperationWithMoneyView>.Deserialize(formData) is not { } operationView)
            {
                return BadRequest(); // TODO добавить список ошибок
            }
            
            return await _service.UpdateAsync(operationView) ? Ok() : BadRequest();
        }

        [HttpGet]
        [Route("GetFiveLastedConsumption")]
        public async Task<IActionResult> GetFiveLastedConsumption()
        {
            var operations = await _service.GetRangeWithCategoriesAsync(true, 0, 5);
            
            return operations.Any() ? Ok(operations) : NoContent();
        }
        
        [HttpGet]
        [Route("GetRange")]
        public async Task<IActionResult> GetRangeAsync(bool isConsumption, int pageNumber, int pageElementCount)
        {
            var operations = await _service.GetRangeWithCategoriesAsync(isConsumption, pageNumber, pageElementCount);

            return operations.Any() ? Ok(operations) : NoContent();
        }

        [HttpGet]
        [Route("GetWeeklyForBarChar")]
        public async Task<IActionResult> GetWeeklyForBarCharAsync()
        {

            var finalDate = DateTime.Now;


            var barChar = await _service.GetWeeklyForBarCharAsync(true, finalDate);

            return barChar.Any() ? Ok(barChar) : NoContent();
        }
        
        [HttpGet]
        [Route("GetMonthlyForBarChar")]
        public async Task<IActionResult> GetMonthlyForBarCharAsync(bool isConsumption, DateTime finalDate)
        {
            if (finalDate.Equals(default))
            {
                finalDate = DateTime.Now;
            }
            
            var barChar = await _service.GetMonthlyForBarCharAsync(isConsumption, finalDate);

            return barChar.Any() ? Ok(barChar) : NoContent();
        }
        
        [HttpGet]
        [Route("GetWeeklyForDoughnut")]
        public async Task<IActionResult> GetWeeklyForDoughnutAsync()
        {

            var finalDate = DateTime.Now;


            var barChar = await _service.GetWeeklyForDoughnutAsync(true, finalDate);

            return barChar.Any() ? Ok(barChar) : NoContent();
        }
        
        [HttpGet]
        [Route("GetMonthlyForDoughnut")]
        public async Task<IActionResult> GetMonthlyForDoughnutAsync(bool isConsumption, DateTime finalDate)
        {
            if (finalDate.Equals(default))
            {
                finalDate = DateTime.Now;
            }
            
            var barChar = await _service.GetMonthlyForDoughnutAsync(isConsumption, finalDate);

            return barChar.Any() ? Ok(barChar) : NoContent();
        }
    }
}

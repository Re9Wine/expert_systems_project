using Domain;
using Domain.ViewEntity;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperationCategoryController : ControllerBase // TODO добавить обработку ошибок + ошибок при валидации
    {
        private readonly IOperationCategoryService _service;

        public OperationCategoryController(IOperationCategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(object formData)
        {
            if (JsonSerializer<OperationCategoryView>.Deserialize(formData) is not {} categoryView)
            {
                return BadRequest();// TODO добавить список ошибок
            }

            return await _service.CreateAsync(categoryView) ? Ok() : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(object formData)
        {
            if (JsonSerializer<OperationCategoryView>.Deserialize(formData) is not {} categoryView)
            {
                return BadRequest(); // TODO добавить список ошибок
            }

            return await _service.UpdateAsync(categoryView) ? Ok() : BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Категория пуста");
            }

            return await _service.DeleteAsync(name) ? Ok() : BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetRangeAsync(int pageNumber, int pageElementCount)
        {
            var categories = await _service.GetRangeAsync(pageNumber, pageElementCount);

            return categories.Any() ? Ok(categories) : NoContent();
        }
    }
}

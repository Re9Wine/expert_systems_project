using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        //public class FormData
        //{
        //    public string Name { get; set; }
        //    public double Limit { get; set; }
        //}

        [HttpPost]
        public IActionResult Post([FromBody] object formData)
        {
            ConsumptionCategory test = JsonSerializer.Deserialize<ConsumptionCategory>((JsonElement)formData);

            //Console.WriteLine($"Received data: Field1 = {formData.Name}, Field2 = {formData.Limit}");
            return Ok();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

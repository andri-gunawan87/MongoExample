using Microsoft.AspNetCore.Mvc;
using MongoExample.Models;
using MongoExample.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly MongoDBServices _mongoDBServices;

        public DataController(MongoDBServices mongoDBServices)
        {
            _mongoDBServices = mongoDBServices;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<List<SampleTable>> Get()
        {
            return await _mongoDBServices.GetAsync();
        }


        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SampleTable sampleTable)
        {
            await _mongoDBServices.CreateAsync(sampleTable);
            return CreatedAtAction(nameof(Get), new {id = sampleTable.Id}, sampleTable);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] string movieId)
        {
            await _mongoDBServices.AddToPlaylistAsync(id, movieId);
            return NoContent();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBServices.DeleteAsync(id);
            return NoContent();
        }
    }
}

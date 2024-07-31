using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using minimalAPIMongo.Domains;
using minimalAPIMongo.Services;
using MongoDB.Driver;

namespace minimalAPIMongo.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("Application/json")]
    public class ClientController : ControllerBase
    {

        private readonly IMongoCollection<Client> _client;


        public ClientController(MongoDbService mongoDbService)
        {
            _client = mongoDbService.GetDatabase.GetCollection<Client>("client");
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> Get()
        {
            try
            {
                var client = await _client.Find(FilterDefinition<Client>.Empty).ToListAsync();
                return Ok(client);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("id")]
        public async Task<ActionResult> GetById(string id)
        {
            try
            {
                var client = await _client.Find(c  => c.Id == id).FirstOrDefaultAsync();
                return client is not null ? Ok(client) : NotFound();
            }
            catch (Exception ex)r
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Client client)
        {
            try
            {
                await _client.InsertOneAsync(client);
                return StatusCode(201,client);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(Client client)
        {
            try
            {
                var filter = Builders<Client>.Filter.Eq(x => x.Id, client.Id);
                await _client.ReplaceOneAsync(filter, client);

                return Ok(client);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]

        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var client = _client.FindOneAndDelete(c => c.Id == id);

                if (client is not null)
                {
                    return null;
                }

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

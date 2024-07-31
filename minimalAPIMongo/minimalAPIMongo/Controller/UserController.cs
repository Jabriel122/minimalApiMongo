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
    public class UserController : ControllerBase
    {

        private readonly IMongoCollection<User> _user;

        public UserController(MongoDbService mongoDbService)
        {
            _user = mongoDbService.GetDatabase.GetCollection<User>("user");
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            try
            {
                var user = await _user.Find(FilterDefinition<User>.Empty).ToListAsync();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Id")]
        public async Task<ActionResult> GetById(string id)
        {
            try
            {
                var user = await _user.Find(u => u.Id == id).FirstOrDefaultAsync();
                return Ok(user);
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
                var user = _user.FindOneAndDelete(u => u.Id == id);

                if (user == null)
                {
                    return null;
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(User user)
        {
            try
            {
                await _user.InsertOneAsync(user);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(User user)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
                await _user.ReplaceOneAsync(filter, user);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

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
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Armazena os dados de acesso da collection
        /// </summary>
        private readonly IMongoCollection<Product> _product;

        /// <summary>
        /// Construtor que recebe como dependência o obj da classe MongoDbService
        /// </summary>
        /// <param name="mongoDbService"></param>
        public ProductController(MongoDbService mongoDbService)
        {
            _product = mongoDbService.GetDatabase.GetCollection<Product>("product");
        }

        [HttpGet]

        public async Task<ActionResult<List<Product>>> Get()
        {
            try
            {
                var product = await _product.Find(FilterDefinition<Product>.Empty).ToListAsync();
                return Ok(product);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("id")]

        public async Task<ActionResult> SearchforId(string id)
        {
            try
            {
                var product = await _product.Find(p => p.Id == id).FirstOrDefaultAsync();
                return product is not null ? Ok(product) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]

        public async Task<ActionResult> Update(Product product)
        {
            try
            {
               var filter = Builders<Product>.Filter.Eq(x => x.Id, product.Id);
                
                await _product.ReplaceOneAsync(filter, product);

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                await _product.InsertOneAsync(product);
                return StatusCode(201, product);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var produto = _product.FindOneAndDelete(p => p.Id == id);

                if (produto != null)
                {
                    return null;
                }


                return StatusCode(201);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using minimalAPIMongo.Domains;
using minimalAPIMongo.Model;
using minimalAPIMongo.Services;
using MongoDB.Driver;

namespace minimalAPIMongo.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("Application/json")]

    public class OrderController : ControllerBase
    {

        private readonly IMongoCollection<Order> _order;
        private readonly IMongoCollection<Client> _client;
        private readonly IMongoCollection<Product> _product;

        public OrderController(MongoDbService mongoDbService)
        {
            _order = mongoDbService.GetDatabase.GetCollection<Order>("order");
            _client= mongoDbService.GetDatabase.GetCollection<Client>("client");
            _product = mongoDbService.GetDatabase.GetCollection<Product>("product");
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> Get()
        {
            try
            {
                var order = await _order.Find(FilterDefinition<Order>.Empty).ToListAsync();
                return Ok(order);
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
                var order = await _order.Find( o => o.Id == id).FirstOrDefaultAsync();
                return order is not null ? Ok(order) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Cadastrar(OrderViewModal orderViewModal)
        {
            try
            {
                Order order = new Order();
                order.Id = orderViewModal.Id;
                order.Date = orderViewModal.Date;
                order.Status = orderViewModal.Status;
                order.ProductId = orderViewModal.ProductId;
                order.clientId = orderViewModal.clientId;

                var client = await _client.Find(x => x.Id == order.clientId).FirstOrDefaultAsync();

                if(client == null) 
                { 
                    return NotFound();
                }

                order.Client = client;

                await _order.InsertOneAsync(order);

                return StatusCode(201, order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(Order order)
        {
            try
            {
                var filter = Builders<Order>.Filter.Eq(o => o.Id, order.Id);
                await _order.ReplaceOneAsync(filter, order);

                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete (string id)
        {
            try
            {
                var order = await _order.FindOneAndDeleteAsync(o => o.Id == id);

                if(order is not null)
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

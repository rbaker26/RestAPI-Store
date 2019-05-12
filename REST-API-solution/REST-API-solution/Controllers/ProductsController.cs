using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using REST_lib;

namespace ProductsREST.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return SQL_Interface.Instance.GetProducts();
        }

        // GET api/values/5
        [HttpGet("{productId}")]
        public ActionResult<Product> Get(int productId)
        {
            return SQL_Interface.Instance.GetProductById(productId);
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/values/5
        //[HttpPut("{productId}")]
        //public void Put(int productId, [FromBody] int value)
        //{
        //    var productsUpdateQueue = Messenger.Instance.Channel.QueueDeclare().QueueName;
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

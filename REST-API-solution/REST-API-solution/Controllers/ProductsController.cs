using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using REST_lib;

namespace ProductsREST.Controllers
{
    /// <summary>
    /// A readonly REST API
    /// This API is only for returning things to a client / consumer.
    /// All inventory edits will be made in the reciever's thread
    /// </summary>
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
    }
}

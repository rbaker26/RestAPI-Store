using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using cartREST;
using REST_lib;

namespace cartREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<CartUpdate>> Get()
        {
            return NoContent();
        }

        // GET api/values/5
        [HttpGet("{email}")]
        public ActionResult<IEnumerable<ProductUpdate>> Get(string email)
        {
            return SQL_Interface.Instance.PurchaseCart(email);
        }

        // POST api/values
        [HttpPost]
        public ActionResult<CartUpdate> Post([FromBody] CartUpdate cartUpdate)
        {
            //Console.Out.WriteLine("********************************************");
            //Console.Out.WriteLine(cartUpdate);
            //Console.Out.WriteLine("********************************************");
            try
            {
                if (cartUpdate.Email.Equals(""))
                    throw new Exception();
                SQL_Interface.Instance.AddProductToCart(cartUpdate.Email, cartUpdate.productUpdate);
                return CreatedAtAction(nameof(Get), new { email = cartUpdate.Email }, cartUpdate);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


	}
}

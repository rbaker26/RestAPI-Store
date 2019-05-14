using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using REST_lib;
using ordersREST;

namespace ordersREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            List<ProductUpdate> list = new List<ProductUpdate>();
            list.Add(new ProductUpdate(1, 11));
            list.Add(new ProductUpdate(2, 22));
            list.Add(new ProductUpdate(3, 33));
            list.Add(new ProductUpdate(4, 44));
            list.Add(new ProductUpdate(5, 55));
            list.Add(new ProductUpdate(6, 66));

            SQL_Interface.Instance.AddNewOrder("007dsi@gmail.com", list);

            return NoContent();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(string email)
        {

            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {

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

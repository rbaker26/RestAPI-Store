using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProductsREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {

            return SQL_Interface.Instance.GetProducts();

            //catch(Exception e)
            //{
            //    Console.Out.WriteLine("\n\n**********************************************************************");
            //    Console.Out.WriteLine(e.Message);
            //    Console.Out.WriteLine(e.InnerException);
            //    Console.Out.WriteLine(e.Source);
            //    Console.Out.WriteLine("**********************************************************************\n\n");
            //    return SQL_Interface.Instance.GetProducts();
            //}

        

            // return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.IO;

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
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Product>));

            ser.WriteObject(ms, SQL_Interface.Instance.GetProducts());

            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            Console.Write("JSON form of Person object: ");
            Console.WriteLine(sr.ReadToEnd());


            List<Product> fromJ = new List<Product>();
            ms.Position = 0;
            fromJ = (List<Product>)ser.ReadObject(ms);

            Console.Out.WriteLine("***********************");
            Console.Out.WriteLine("***********************");

            foreach(Product p in fromJ)
            {
                Console.Out.WriteLine(p);
            }

            Console.Out.WriteLine("***********************");


            return SQL_Interface.Instance.GetProducts();
        }

        // GET api/values/5
        [HttpGet("{productId}")]
        public ActionResult<Product> Get(int productId)
        {
            DataContractJsonSerializer ds;
            return SQL_Interface.Instance.GetProductById(productId);
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

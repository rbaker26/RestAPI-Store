using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;



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
            //string s = JsonConverter.ToJason(SQL_Interface.Instance.GetProducts());;
            //Console.Out.WriteLine("****************************");
            //Console.Out.WriteLine(s);
            //Console.Out.WriteLine("****************************");



            //Console.Out.WriteLine("****************************");
            //Console.Out.WriteLine("****************************");
            //List<Product> lp = JsonConverter.FromJson<List<Product>>(s);
            //foreach(Product p in lp)
            //{
            //    Console.Out.WriteLine(p);
            //}
            //Console.Out.WriteLine("****************************");
            Product p = new Product(1, "desc", 2, 9.99f);
            Console.Out.WriteLine(p.GetHashCode());
            Console.Out.WriteLine(p);


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

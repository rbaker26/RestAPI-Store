using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using REST_lib;

namespace REST_lib_test
{
    [TestClass]
    public class JsonConverterTest
    {
        [TestMethod]
        public void TestProductToJson()
        {
            Product p = new Product(1, "hammer", 55, 4.50f);
            string expected = "{\"Description\":\"" + p.Description + "\",\"Price\":" + p.Price + ",\"ProductId\":" + p.ProductId + ",\"Quantity\":" + p.Quantity + "}";

            string result = JsonConverter.ToJson(p);
            Assert.AreEqual(result, expected);
        }


        [TestMethod]
        public void TestProductFromJson()
        {
            //Product p = new Product(1, "hammer", 55, 4.50f);
            //string expected = "{\"Description\":\"" + p.Description + "\",\"Price\":4.5,\"ProductId\":1,\"Quantity\":55}";

            //string result = JsonConverter.ToJason(p);

            //Console.Error.WriteLine(expected);
            //Console.Error.WriteLine(result);
            //Assert.AreEqual(result, expected);


            Product expected = new Product(1, "hammer", 55, 4.50f);
            string json = "{\"Description\":\"" + expected.Description + "\",\"Price\":" + expected.Price + ",\"ProductId\":" + expected.ProductId + ",\"Quantity\":" + expected.Quantity + "}";


            Product result = JsonConverter.FromJson<Product>(json);
            Assert.AreEqual(result, expected);

        }

    }
}
 

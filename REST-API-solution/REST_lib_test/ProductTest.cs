using System;
using System.Collections.Generic;
using System.Text;


using Microsoft.VisualStudio.TestTools.UnitTesting;
using REST_lib;

namespace REST_lib_test
{
    [TestClass]
    public class ProductTest
    {

        [TestMethod]
        public void TestProductDefaultContructor()
        {
            Product p = new Product();
            Assert.AreEqual(p.ProductId, 0);
            Assert.AreEqual(p.Description, null);
            Assert.AreEqual(p.Quantity, 0);
            Assert.AreEqual(p.Price, 0.0f);

        }

        
        [TestMethod]
        public void TestProductArgContructor()
        {
            Product p = new Product(1,"desc",2,9.99f);
            Assert.AreEqual(p.ProductId, 1);
            Assert.AreEqual(p.Description, "desc");
            Assert.AreEqual(p.Quantity, 2);
            Assert.AreEqual(p.Price, 9.99f);
        }


        [TestMethod]
        public void TestProductToString()
        {
            Product p = new Product(1, "desc", 2, 9.99f);
            string result = ("ID:\t" + p.ProductId + "\tDescription:\t" + p.Description
                + "\tQuantity:\t" + p.Quantity + "\tPrice:\t" + p.Price);

            Assert.AreEqual(p.ToString(), result);
        }
    }
}

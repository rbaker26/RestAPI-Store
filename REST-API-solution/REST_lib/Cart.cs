using System;
using System.Collections.Generic;
using System.Text;

namespace REST_lib
{
    public class Cart
    {
        public string Email { get; set; }
        public List<KeyValuePair<Product,Int32>> ProductsCart { get; set; }

    }
}

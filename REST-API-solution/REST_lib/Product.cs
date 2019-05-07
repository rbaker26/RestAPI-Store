﻿using System;
using System.Collections.Generic;
using System.Text;

namespace REST_lib
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }



        public override string ToString()
        {
            return "ID:\t" + ProductId + "\tDescription:\t" + Description
                + "\tQuantity:\t" + Quantity + "\tPrice:\t" + Price;
        }
        public Product(int ProductId, string Description, int Quantity, float Price)
        {
            this.ProductId = ProductId;
            this.Description = Description;
            this.Quantity = Quantity;
            this.Price = Price;
        }
        public Product()
        {
            this.ProductId = 0;
            this.Description = null;
            this.Quantity = 0;
            this.Price = 0;
        }
    }
}

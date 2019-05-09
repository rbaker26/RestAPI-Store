using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace REST_lib
{
    [DataContract]
    public class Product
    {

        //********************************************************************************
        // Data Properties
        //********************************************************************************
        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public float Price { get; set; }
        //********************************************************************************


        //********************************************************************************
        // Constructors
        //********************************************************************************
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
        //********************************************************************************



        //********************************************************************************
        // Override Functions
        //********************************************************************************
        public override bool Equals(object obj)
        {
            if(obj == null)
                return false;
            if (obj.GetType() != this.GetType())
                return false;

            Product temp = (Product)obj;
            return (this.ProductId == temp.ProductId && this.Description == temp.Description &&
                    this.Quantity == temp.Quantity   && this.Price == temp.Price);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17 + Quantity + ProductId;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + ProductId.GetHashCode();
                hash = hash * 23 + Description.GetHashCode();
                hash = hash * 23 + Quantity.GetHashCode();
                hash = hash * 23 + Price.GetHashCode();
                hash = hash * 23 + this.ToString().GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return "ID:\t" + ProductId + "\tDescription:\t" + Description
                + "\tQuantity:\t" + Quantity + "\tPrice:\t" + Price;
        }
        //********************************************************************************
    }
}

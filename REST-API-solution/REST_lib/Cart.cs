using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.Serialization;

namespace REST_lib
{
    [DataContract]
    public class Cart
    {

        //********************************************************************************
        // Data Properties
        //********************************************************************************
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// <ProductID, QuantityPurchased>
        /// This way the client cannot change things like the price of quantity on hand
        /// </summary>
        [DataMember]
        public List<KeyValuePair<Int32,Int32>> ShoppingCart { get; set; }
        //********************************************************************************


        //********************************************************************************
        // Constructors
        //********************************************************************************
        public Cart()
        {
            this.Email = "";
        }

        public Cart(string Email, List<KeyValuePair<Int32, Int32>> ShoppingCart)
        {
            this.Email = Email;
            this.ShoppingCart = ShoppingCart;

        }
        //********************************************************************************


        //********************************************************************************
        // Override Functions
        //********************************************************************************
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != this.GetType())
                return false;

            Cart temp = (Cart)obj;
            return (this.Email == temp.Email && this.ShoppingCart == temp.ShoppingCart);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Email.GetHashCode();
                hash = hash * 23 + ShoppingCart.GetHashCode();
                hash = hash * 23 + this.ToString().GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return "Email:\t" + Email + "\tShoppingCart:\t" + ShoppingCart;
        }
        //********************************************************************************
    }
}

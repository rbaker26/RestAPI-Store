using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.Serialization;


namespace REST_lib
{
    [DataContract]
    public class Order
    {

		//********************************************************************************
		// Data Properties
		//********************************************************************************
		[DataMember]
		public int OrderID { get; set; }
		[DataMember]
        public string Email { get; set; }

        /// <summary>
        /// In millisecords
        /// </summary>
		[DataMember]
        public long TimeStamp { get; set; }

        /// <summary>
        /// <ProductID, QuantityPurchased>
        /// This way the client cannot change things like the price of quantity on hand
        /// </summary>
		[DataMember]
        public List<ProductUpdate> ShoppingCart { get; set; }
		//********************************************************************************



		//********************************************************************************
		// Constructors
		//********************************************************************************
		public Order() : this(0, "", 0) { }

        public Order(int OrderID, string Email, long TimeStamp)
        {
            this.OrderID = OrderID;
            this.Email = Email;
            this.TimeStamp = TimeStamp;

			ShoppingCart = new List<ProductUpdate>();
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

            Order temp = (Order)obj;
            return (this.OrderID == temp.OrderID && this.Email == temp.Email &&
                    this.ShoppingCart == temp.ShoppingCart && this.TimeStamp == temp.TimeStamp);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17 + this.OrderID;
                hash = hash * 23 + OrderID.GetHashCode();
                hash = hash * 23 + Email.GetHashCode();
                hash = hash * 23 + ShoppingCart.GetHashCode();
                hash = hash * 23 + TimeStamp.GetHashCode();
                hash = hash * 23 + this.ToString().GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
			return "ID:\t" + OrderID + "\tEmail:\t" + Email + "\tTimeStamp:\t" + TimeStamp
				+ "\nShoppingCart:\n\t" + string.Join("\n\t", ShoppingCart) + "\n";
        }
        //********************************************************************************
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace REST_lib
{
    [DataContract]
    public class CartUpdate
    {

        //********************************************************************************
        // Data Properties
        //********************************************************************************
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public List<ProductUpdate> productUpdate {get; set;}
        //********************************************************************************


        //********************************************************************************
        // Constructors
        //********************************************************************************
        public CartUpdate()
        {
            this.Email = "";
            this.productUpdate = new List<ProductUpdate>();
        }
        public CartUpdate(string email, List<ProductUpdate> productUpdates)
        {
            this.Email = email;
            this.productUpdate = productUpdates;
        }
        //********************************************************************************


        //********************************************************************************
        // Override Functions
        #region override_functions_region 
        //********************************************************************************
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != this.GetType())
                return false;

            CartUpdate temp = (CartUpdate)obj;
            return (this.Email == temp.Email && 
                this.productUpdate.ProductId == temp.productUpdate.ProductId && 
                this.productUpdate.QuantityToBeRemoved == temp.productUpdate.QuantityToBeRemoved );
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17 + this.productUpdate.ProductId + this.productUpdate.QuantityToBeRemoved;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + this.productUpdate.ProductId.GetHashCode();
                hash = hash * 23 + this.productUpdate.QuantityToBeRemoved.GetHashCode();
                hash = hash * 23 + this.ToString().GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return "User ID:\t" + Email + "\tProduct ID:\t" + this.productUpdate.ProductId + "\tQuantity:\t" + this.productUpdate.QuantityToBeRemoved;
        }
        #endregion
        //********************************************************************************
    }
}

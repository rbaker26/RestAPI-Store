using System;
using System.Collections.Generic;
using System.Text;

namespace REST_lib
{
    public class RemoveCartUpdate
    {
        //********************************************************************************
        // Data Properties
        //********************************************************************************
        public string Email { get; set; }
        public int ProductId { get; set; }

        //********************************************************************************
        // Constructors
        //********************************************************************************
        public RemoveCartUpdate()
        {
            this.Email = "";
            this.ProductId = 0;
        }
        public RemoveCartUpdate(string email, int productId)
        {
            this.Email = email;
            this.ProductId = productId;
        }
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

            RemoveCartUpdate temp = (RemoveCartUpdate)obj;
            return (this.Email == temp.Email && 
                this.ProductId == temp.ProductId);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17 + this.ProductId;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + this.Email.GetHashCode();
                hash = hash * 23 + this.ToString().GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return "User ID:\t" + Email + "\tProduct ID:\t" + this.ProductId;
        }
        #endregion
        //********************************************************************************
    }
}
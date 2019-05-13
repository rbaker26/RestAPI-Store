using System;
using System.Collections.Generic;
using System.Text;

namespace REST_lib
{
    public class ProductUpdate
    {

        //********************************************************************************
        // Data Properties
        //********************************************************************************
        public int ProductId { get; set; }
        public int QuantityToBeRemoved { get; set; }
        //********************************************************************************


        //********************************************************************************
        // Constructors
        //********************************************************************************
        public ProductUpdate()
        {
            ProductId = 0;
            QuantityToBeRemoved = 0;
        }

        public ProductUpdate(int ProductId, int QuantityToBeRemoved)
        {
            this.ProductId = ProductId;
            this.QuantityToBeRemoved = QuantityToBeRemoved;
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

            ProductUpdate temp = (ProductUpdate)obj;
            return (this.ProductId == temp.ProductId && this.QuantityToBeRemoved == temp.QuantityToBeRemoved);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17 + QuantityToBeRemoved + ProductId;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + ProductId.GetHashCode();
                hash = hash * 23 + QuantityToBeRemoved.GetHashCode();
                hash = hash * 23 + this.ToString().GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return "ID:\t" + ProductId + "\tQuantity:\t" + QuantityToBeRemoved;
        }
        #endregion
        //********************************************************************************


    }
}


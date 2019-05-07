using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.Serialization;

namespace REST_lib
{
    [DataContract]
    public class Cart
    {
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// <ProductID, QuantityPurchased>
        /// This way the client cannot change things like the price of quantity on hacd
        /// </summary>
        [DataMember]
        public List<KeyValuePair<Int32,Int32>> ShoppingCart { get; set; }
        
    }
}

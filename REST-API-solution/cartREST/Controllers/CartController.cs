﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cartREST;
using REST_lib;

namespace cartREST.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<CartUpdate>> Get()
        {
            return NoContent();
        }

        // GET api/values/email
        [HttpGet("{email}")]
        public ActionResult<IEnumerable<ProductUpdate>> Get(string email)
        {
            if (email.Equals(""))
                return NotFound();
            try
            {
                // Set Items to cart items to purchased and return a cart (email, List<productUpdates>)
                Cart cart = SQL_Interface.Instance.PurchaseCart(email, false);

                return cart.ShoppingCart;
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public ActionResult<CartUpdate> Post([FromBody] CartUpdate cartUpdate)
        {
            if (cartUpdate.Email.Equals(""))
                return NotFound();
            try
            {
                SQL_Interface.Instance.AddProductToCart(cartUpdate.Email, cartUpdate.productUpdate);
                return CreatedAtAction(nameof(Get), new { email = cartUpdate.Email }, cartUpdate);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // PUT api/values/email
        [HttpPut("{email}")]
        public ActionResult Put(string email)
        {
            if(email.Equals(""))
            {
                return NotFound();
            }
            try
            {
                Cart cart = SQL_Interface.Instance.PurchaseCart(email, true);
                // Send the Cart to Orders via PubNub
                Messenger.Instance.SendMessage(cart, Messenger.MessageType.NewOrders);

                // Send Quantity Updates to Products Inventory Service
                Messenger.Instance.SendMessage(cart.ShoppingCart, Messenger.MessageType.ProductUpdates);
                return Ok();
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        public ActionResult Delete(RemoveCartUpdate rcu)
        {
            try
            {
                SQL_Interface.Instance.RemoveProduct(rcu.Email, rcu.ProductId);
                return NoContent();
            }
            catch(Exception)
            {
                return NotFound();
            }
        }


    }
}

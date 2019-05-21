using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using PubnubApi;
using REST_lib;

namespace ordersREST {
	/// <summary>
	/// This entire class is a bit of a hack. We really should be using GenericListener
	/// in the REST_lib namespace, but we were having issues when using multiple subscribers.
	/// </summary>
	class OrdersPubnubCallback : SubscribeCallback {

		public override void Message<T>(Pubnub pubnub, PNMessageResult<T> message) {
			string jsonString = message.Message.ToString();

			if(message.Channel == Messenger.MessageType.NewOrders.ToString()) {

				Cart c = pubnub.JsonPluggableLibrary.DeserializeToObject<Cart>(jsonString);

				Console.Out.WriteLine("Received cart: " + c);
				SQL_Interface.Instance.AddNewOrder(c.Email, c.ShoppingCart);
			}
			else if(message.Channel == Messenger.MessageType.ProductChanges.ToString()) {
				Product p = pubnub.JsonPluggableLibrary.DeserializeToObject<Product>(jsonString);

				Console.Out.WriteLine("Received product: " + p);
				SQL_Interface.Instance.SetProductInfo(p);
			}
			else {
				Console.Out.WriteLine("Dropping odd Pubnub message: " + jsonString);
			}
		}

		public override void Presence(Pubnub pubnub, PNPresenceEventResult presence) {
		}

		public override void Status(Pubnub pubnub, PNStatus status) {
			if(status.Category == PNStatusCategory.PNConnectedCategory) {

				//Messenger.Instance.SendMessage(new Product(10, "Send", 30, 4.99f), Messenger.MessageType.ProductUpdates);
				//Messenger.Instance.SendMessage(new Order(20, "HO", 490), Messenger.MessageType.ProductUpdates);
				Messenger.WriteDebugMessage("Orders callback: Listening on these channels: " + string.Join(",", status.AffectedChannels));
			}
			else {
				Messenger.WriteDebugMessage("Failed to subscribe. Dumping status: " + status.ToString());
			}
		}
	}
}

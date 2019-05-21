using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using PubnubApi;

namespace REST_lib {
	class GenericPubnubListener<MsgType> : SubscribeCallback {

		public string sourceChannel {
			get; set;
		}
		public Action<MsgType> callback {
			get; set;
		}

		public override void Message<T>(Pubnub pubnub, PNMessageResult<T> message) {
			if(message.Channel == sourceChannel) {
				string jsonString = message.Message.ToString();

				// This is super safe. If we got an object which doesn't match,
				// we'll spit out a neutral object, rather than doing anything
				// exceptional.
				MsgType payload = pubnub.JsonPluggableLibrary.DeserializeToObject<MsgType>(jsonString);

				callback.Invoke(payload);
			}
		}

		public override void Presence(Pubnub pubnub, PNPresenceEventResult presence) {
		}

		public override void Status(Pubnub pubnub, PNStatus status) {
			if(status.Category == PNStatusCategory.PNConnectedCategory) {

				//Messenger.Instance.SendMessage(new Product(10, "Send", 30, 4.99f), Messenger.MessageType.ProductUpdates);
				//Messenger.Instance.SendMessage(new Order(20, "HO", 490), Messenger.MessageType.ProductUpdates);
				Messenger.WriteDebugMessage(sourceChannel + ": Listening on these channels: " + string.Join(",", status.AffectedChannels) );
			}
			else {
				Messenger.WriteDebugMessage("Failed to subscribe. Dumping status: " + status.ToString());
			}
		}
	}
}

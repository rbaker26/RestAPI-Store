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
			//T payload = message.Message;
			if(message.Channel == sourceChannel) {
				string jsonString = message.Message.ToString();
				MsgType payload = pubnub.JsonPluggableLibrary.DeserializeToObject<MsgType>(jsonString);

				callback.Invoke(payload);
			}
		}

		public override void Presence(Pubnub pubnub, PNPresenceEventResult presence) {
		}

		public override void Status(Pubnub pubnub, PNStatus status) {
			if(status.Category == PNStatusCategory.PNConnectedCategory) {
				pubnub.Publish()
					.Channel(sourceChannel)
					.Message(new Product(10, "Send", 30, 4.99f))
					.Async(new PNPublishResultExt((publishResult, publishStatus) => {
						if(!publishStatus.Error) {
							Console.Out.WriteLine(string.Format("DateTime {0}, In Publish Example, Timetoken: {1}", DateTime.UtcNow, publishResult.Timetoken));
						}
						else {
							Console.Out.WriteLine(publishStatus.Error);
							Console.Out.WriteLine(publishStatus.ErrorData.Information);
						}
					}));
			}
			else {
				Console.Out.WriteLine("Failed to subscribe");
			}
		}
	}
}

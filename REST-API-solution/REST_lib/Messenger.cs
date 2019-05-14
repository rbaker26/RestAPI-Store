using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;
using PubnubApi;

namespace REST_lib {
	/// <summary>
	/// A class for handling messages to be sent between micro services.
	/// 
	/// Relies on a RabbitMQ server.
	/// </summary>
	public class Messenger : IDisposable {

		Pubnub pn;

		/// <summary>
		/// Get the instance of the messaging object. It is created if it does not
		/// already exist.
		/// </summary>
		public static Messenger Instance {
			get; private set;
		}
		

		/// <summary>
		/// Identifies message types. If anything gets added/removed here, check
		/// the ExchangeDeclare method as well.
		/// </summary>
		public enum MessageType {
			/// <summary>
			/// Used order messages. Sent when the cart service is told to place an
			/// order.
			/// </summary>
			NewOrders,

			/// <summary>
			/// Used to notify other services that the inventory has changed. Sent when
			/// a successful POST request is made to the product service.
			/// </summary>
			ProductUpdates
		}

		/// <summary>
		/// Sends a message using the given exchange.
		/// </summary>
		/// <typeparam name="T">Type of the message object.</typeparam>
		/// <param name="msg">The message to send.</param>
		/// <param name="mType">The type of message.</param>
		public void SendMessage<T>(T msg, MessageType mType) {

			//string jsonMsg = JsonConverter.ToJson(msg);

			pn.Publish()
				.Channel(mType.ToString())
				.Message(msg)
				.Async(new PNPublishResultExt((publishResult, publishStatus) => {
					if(!publishStatus.Error) {
						//Debug.WriteLine(string.Format("DateTime {0}, In Publish Example, Timetoken: {1}", DateTime.UtcNow, publishResult.Timetoken));
					}
					else {
						Console.Out.WriteLine(publishStatus.Error);
						Console.Out.WriteLine(publishStatus.ErrorData.Information);
					}
			}));
		}

		public void SetupListener<T>(Action<T> consumer, MessageType mType) {
			string channel = mType.ToString();

			GenericPubnubListener<T> listener = new GenericPubnubListener<T> { callback = consumer, sourceChannel = channel };

			pn.AddListener(listener);
			pn.Subscribe<string>()
				.Channels(new string[] { channel })
				.Execute();
		}

		#region Instance management

		public static void CreateInstance(string namePostfix) {
			Instance = new Messenger(namePostfix);
		}

		private static string GetDefaultMacAddress() {
			Dictionary<string, long> macAddresses = new Dictionary<string, long>();
			foreach(NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces()) {
				if(nic.OperationalStatus == OperationalStatus.Up)
					macAddresses[nic.GetPhysicalAddress().ToString()] = nic.GetIPStatistics().BytesSent + nic.GetIPStatistics().BytesReceived;
			}
			long maxValue = 0;
			string mac = "";
			foreach(KeyValuePair<string, long> pair in macAddresses) {
				if(pair.Value > maxValue) {
					mac = pair.Key;
					maxValue = pair.Value;
				}
			}
			return mac;
		}

		/// <summary>
		/// Set up our connection to the RabbitMQ server and open a channel.
		/// </summary>
		private Messenger(string namePostfix) {
			Console.Out.WriteLine("Setting up Pubnub.");

			// NOTE: If we ever want to make this repo publich, we should load these into a secrets file or something.
			PNConfiguration config = new PNConfiguration();
			config.SubscribeKey = "sub-c-f05db650-75d6-11e9-90ac-5a6b801e0231";
			config.PublishKey = "pub-c-13ae4878-f71b-4ce9-8b04-a79dc76d5f7f";
			//config.SecretKey = "my_secretkey";
			config.LogVerbosity = PNLogVerbosity.BODY;
			//config.Uuid = "PubNubCSharpExample";
			config.Uuid = GetDefaultMacAddress() + namePostfix;

			pn = new Pubnub(config);
		}

		/// <summary>
		/// Nuke the object, closing up the RabbitMQ connection. It will die horribly.
		/// </summary>
		/// <param name="disposing">If true, we are actually cleaning up for sure.</param>
		protected virtual void Dispose(bool disposing) {

			if(disposing) {
				Console.Out.WriteLine("Destroying up Pubnub");

				pn.Destroy();
			}
		}

		/// <summary>
		/// Nuke the object, closing up the RabbitMQ connection. It will die horribly.
		///
		/// This may need to get manually called while we close up the REST server.
		/// </summary>
		public void Dispose() {
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			GC.SuppressFinalize(this);

		}

		~Messenger() {
			Dispose(false);
		}
		#endregion


	}
}

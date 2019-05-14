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
	/// Relies on Pubnub. Must be manually initialized via CreateInstance.
	/// </summary>
	/// <example></example>
	/// /* This MUST be called before any of the examples below will work. */
	/// Messenger.CreateInstance("SomethingUniqueToThisMicroservice", makeVerbose: true);
	///
	/// /* Sets up a listener to do things upon recieving messages. Note that this is done async, see docs. */
	/// Messenger.Instance.SetupListener<Product>(
	///		(Product p) => { Console.Out.WriteLine("PRINTING: " + p.ToString()); },
	///		Messenger.MessageType.ProductUpdates
	/// );
	///
	/// /* Shoots a message to anyone listening. This is done async; see docs. */
	/// Messenger.Instance.SendMessage(
	///		new Product(10, "Send", 30, 4.99f),
	///		Messenger.MessageType.ProductUpdates
	///	);
	/// </example>
	public class Messenger : IDisposable {

		#region Instance management

		/// <summary>
		/// Get the instance of the messaging object. It is created if it does not
		/// already exist.
		/// </summary>
		/// <exception cref="NullReferenceException">Thrown if CreateInstance has not already been called.</exception>
		public static Messenger Instance {
			get {
				if(_instance == null) {
					throw new NullReferenceException("Cannot access Messenger.Instance without first calling CreateInstance");
				}

				return _instance;
			}
			private set {
				_instance = value;
			}
		}
		private static Messenger _instance;

		/// <summary>
		/// Creates the instance of the messenger.
		/// </summary>
		/// <param name="namePostfix"></param>
		public static void CreateInstance(string namePostfix, bool makeVerbose = false) {
			Instance = new Messenger(namePostfix, makeVerbose);
		}

		/// <summary>
		/// Attempts to get the mac address. I don't know if this works on other platforms but probably.
		/// </summary>
		/// <returns>The mac address which we deduced.</returns>
		private static string GetDefaultMacAddress() {
			// Implementation comes from https://stackoverflow.com/a/51821927/8195994
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
		private Messenger(string namePostfix, bool makeVerbose) {
			Verbose = makeVerbose;

			WriteDebugMessage("Setting up Pubnub...");

			// NOTE: If we ever want to make this repo publich, we should load these into a secrets file or something.
			PNConfiguration config = new PNConfiguration();
			config.SubscribeKey = "sub-c-f05db650-75d6-11e9-90ac-5a6b801e0231";
			config.PublishKey = "pub-c-13ae4878-f71b-4ce9-8b04-a79dc76d5f7f";
			//config.SecretKey = "my_secretkey";
			config.LogVerbosity = PNLogVerbosity.BODY;
			//config.Uuid = "PubNubCSharpExample";
			config.Uuid = GetDefaultMacAddress() + namePostfix;

			pn = new Pubnub(config);

			WriteDebugMessage("Done setting up Pubnub. Using uuid of " + config.Uuid); 
		}

		public static void DisposeInstance() {
			if(_instance != null) {
				Instance.Dispose();
				Instance = null;
			}
		}

		/// <summary>
		/// Nuke the object, closing up the RabbitMQ connection. It will die horribly.
		/// </summary>
		/// <param name="disposing">If true, we are actually cleaning up for sure.</param>
		protected virtual void Dispose(bool disposing) {

			if(disposing) {
				WriteDebugMessage("Destroying up Pubnub");

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

		#region Debugging
		/// <summary>
		/// If true, prints all debugging messages.
		/// </summary>
		public static bool Verbose {
			get; set;
		}

		/// <summary>
		/// Writes debug messages to standard out. May not print anything depending
		/// on the verbose flag.
		/// </summary>
		/// <param name="msg">Message to write.</param>
		public static void WriteDebugMessage(string msg) {
			if(Verbose) {
				Console.Out.WriteLine(msg);
			}
		}
		#endregion

		private Pubnub pn;



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
		/// Sends a message using the given message type.
		/// 
		/// Due to the way PubNub works, this is sent asynchronously. There is no
		/// promise that this message will be sent immediately.
		/// </summary>
		/// <typeparam name="T">Type of the message object.</typeparam>
		/// <param name="msg">The message's payload.</param>
		/// <param name="mType">The type of message we'll send.</param>
		public void SendMessage<T>(T msg, MessageType mType) {

			//string jsonMsg = JsonConverter.ToJson(msg);

			pn.Publish()
				.Channel(mType.ToString())
				.Message(msg)
				.Async(new PNPublishResultExt((publishResult, publishStatus) => {
					if(!publishStatus.Error) {
						WriteDebugMessage(string.Format("Published at DateTime {0}, Timetoken: {1}. Sent on {2}", DateTime.UtcNow, publishResult.Timetoken, mType.ToString()));
					}
					else {
						WriteDebugMessage(publishStatus.Error.ToString());
						WriteDebugMessage(publishStatus.ErrorData.Information);
					}
			}));
		}

		/// <summary>
		/// Sets up a listener. It will receive all messages of a given type.
		///
		/// Note that the recieving type is not strongly checked; if we get a
		/// payload with a type other than T, it will probably be converted into
		/// an object with all values set as default. In some cases, you may be
		/// able to deduce whether this has happened based on the object itself,
		/// but there is no strong garuntee.
		///
		/// As the listener and the subscription occurs asyncronously, there is
		/// no promise that the subscription will happen immediately. This should
		/// not matter unless you send messages right after and expect them to
		/// be caught.
		/// </summary>
		/// <typeparam name="T">Type of object we're listening for.</typeparam>
		/// <param name="consumer">What happens when we get an object.</param>
		/// <param name="mType">The type of message we'll listen for.</param>
		public void SetupListener<T>(Action<T> consumer, MessageType mType) {
			string channel = mType.ToString();

			GenericPubnubListener<T> listener = new GenericPubnubListener<T> { callback = consumer, sourceChannel = channel };

			pn.AddListener(listener);

			if(pn.GetSubscribedChannels() == null || !pn.GetSubscribedChannels().Contains(channel)) {
				pn.Subscribe<string>()
					.Channels(new string[] { channel })
					.Execute();
			}
		}

	}
}

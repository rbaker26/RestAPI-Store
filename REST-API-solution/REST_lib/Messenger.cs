using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace REST_lib {
	/// <summary>
	/// A class for handling messages to be sent between micro services.
	/// 
	/// Relies on a RabbitMQ server.
	/// </summary>
	public class Messenger : IDisposable {

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
		/// The connection object. This will stay up until the program closes.
		/// </summary>
		public IConnection Connection { get; private set; }

		/// <summary>
		/// The channel object. This will stay up until the program closes.
		/// </summary>
		public IModel Channel { get; private set; }

		/// <summary>
		/// Sends a message using the given exchange.
		/// </summary>
		/// <typeparam name="T">Type of the message object.</typeparam>
		/// <param name="msg">The message to send.</param>
		/// <param name="mType">The type of message.</param>
		public void SendMessage<T>(T msg, MessageType mType) {

			string exchangeName = ExchangeDeclare(mType);

			string jsonMsg = JsonConverter.ToJson(msg);

			/*
			Console.Out.WriteLine(jsonMsg);
			Console.Out.Flush();
			*/

			byte[] byteMsg = Encoding.UTF8.GetBytes(jsonMsg);
			Channel.BasicPublish(
				exchange: exchangeName,
				routingKey: "",
				basicProperties: null,
				body: byteMsg
			);
		}

		/// <summary>
		/// Configures the exchange in a consistent way and then returns the exchange name.
		///
		/// Please use this instead of manually declaring the exchange. All of our microservices
		/// MUST use the same exchange declarations or RabbitMQ will compplain. If you need a new
		/// exchange, please define it in MessageType and change this code.
		/// </summary>
		/// <param name="mType">The type of exchange.</param>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if recieving an unkown message type.</exception>
		/// <returns>The name of the exchange.</returns>
		public string ExchangeDeclare(MessageType mType) {
			string exchangeName = mType.ToString();

			switch(mType) {
				case MessageType.ProductUpdates:
				case MessageType.NewOrders:
					Channel.ExchangeDeclare(exchange: exchangeName, type: "fanout");
					break;
				default:
					throw new ArgumentOutOfRangeException("Cannot declare unhandled exchange type of " + exchangeName);
			}

			return exchangeName;
		}

		#region Instance management

		/// <summary>
		/// Create an instance of our singleton.
		/// </summary>
		static Messenger() {
			Instance = new Messenger();
		}

		/// <summary>
		/// Was used to force the message instance to exist (for testing purposes).
		/// It should not be used outside of testing.
		/// </summary>
		[Obsolete]
		public void ForceAwake() {
		}

		/// <summary>
		/// Set up our connection to the RabbitMQ server and open a channel.
		/// </summary>
		private Messenger() {
			Console.Out.WriteLine("Booting up RabbitMQ.");

			ConnectionFactory factory = new ConnectionFactory() { HostName = "68.5.123.182" };
			Connection = factory.CreateConnection();
			Channel = Connection.CreateModel();
		}

		/// <summary>
		/// Nuke the object, closing up the RabbitMQ connection. It will die horribly.
		/// </summary>
		/// <param name="disposing">If true, we are actually cleaning up for sure.</param>
		protected virtual void Dispose(bool disposing) {

			if(disposing) {
				Console.Out.WriteLine("Shutting down RabbitMQ.");

				Channel?.Close();
				Connection?.Close();
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

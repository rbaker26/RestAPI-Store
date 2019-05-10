using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace REST_lib {
	public class Messaging : IDisposable {

		public static Messaging Instance {
			get; private set;
		}

		public enum MessageType {
			NewOrders, ProductUpdates
		}

		public IConnection Connection { get; private set; }
		public IModel Channel { get; private set; }

		public void SendMessage<T>(T msg, MessageType mType) {
			string exchangeName = SetupExchange(mType);

			string jsonMsg = JsonConverter.ToJson(msg);

			Console.Out.WriteLine(jsonMsg);
			Console.Out.Flush();

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
		/// </summary>
		/// <param name="mType">The type of exchange.</param>
		/// <returns>The name of the exchange.</returns>
		private String SetupExchange(MessageType mType) {
			string exchangeName = mType.ToString();

			switch(mType) {
				case MessageType.ProductUpdates:
				case MessageType.NewOrders:
					Channel.ExchangeDeclare(exchange: exchangeName, type: "fanout");
					break;
			}

			return exchangeName;
		}

		#region Instance management

		static Messaging() {
			Instance = new Messaging();
		}


		[Obsolete]
		public void ForceAwake() {
			SetupExchange(MessageType.NewOrders);
		}

		private Messaging() {
			Console.Out.WriteLine("Setting up RabbitMQ.");

			ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
			Connection = factory.CreateConnection();
			Channel = Connection.CreateModel();
		}

		#region IDisposable Support

		protected virtual void Dispose(bool disposing) {

			if(disposing) {
				Console.Out.WriteLine("Blah");

				Channel?.Close();
				Connection?.Close();
			}
		}

		// This code added to correctly implement the disposable pattern.
		public void Dispose() {
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			GC.SuppressFinalize(this);

		}

		~Messaging() {
			Dispose(false);
		}
		#endregion
		#endregion


	}
}

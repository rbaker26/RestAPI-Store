using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace REST_lib {
	public class Messaging : IDisposable {

		public static Messaging Instance {
			get; private set;
		}

		public IConnection connection;
		public IModel channel;

		#region Instance management

		static Messaging() {
			Instance = new Messaging();
		}


		[Obsolete]
		public void ForceAwake() {

		}

		private Messaging() {
			Console.Out.WriteLine("Setting up RabbitMQ.");

			ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
			connection = factory.CreateConnection();
			channel = connection.CreateModel();
		}

		#region IDisposable Support

		protected virtual void Dispose(bool disposing) {
			/*
			if(!disposedValue) {

				if(disposing) {
				}

				disposedValue = true;
			}
			*/

			if(disposing) {
				Console.Out.WriteLine("Blah");

				channel?.Close();
				connection?.Close();
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

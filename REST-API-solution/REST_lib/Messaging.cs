using System;
using System.Collections.Generic;
using System.Text;

namespace REST_lib {
	public class Messaging : IDisposable {

		public static Messaging Instance {
			get; private set;
		}

		static Messaging() {
			Instance = new Messaging();
		}

		private Messaging() {
			Console.Out.WriteLine("Setting up RabbitMQ.");
		}

		[Obsolete]
		public void ForceAwake() {

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

	}
}

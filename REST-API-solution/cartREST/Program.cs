using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using REST_lib;

namespace cartREST
{
	public class Program {
		public static void Main(string[] args) {

			//Product p = new Product(20, "Description here", 23, 4.99f);
			//Messenger.Instance.SendMessage(p, Messenger.MessageType.NewOrders);

			try {
				IWebHost host = CreateWebHostBuilder(args).Build();
				host.RunAsync();
				host.WaitForShutdown();
			}
			finally {
				Messenger.Instance.Dispose();
			}
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()

	}
}

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
            Heartbeat.Instance.InitHeartbeat("Cart REST-API");
            Heartbeat.Instance.Start();


            Messenger.CreateInstance("Cart", makeVerbose: true);

			try {
				IWebHost host = CreateWebHostBuilder(args).Build();
				host.RunAsync();
				host.WaitForShutdown();
			}
			finally {
				Messenger.DisposeInstance();
                
			}
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.UseUrls("http://*:5002");
				//.UseUrls("http://localhost:5002");

	}
}

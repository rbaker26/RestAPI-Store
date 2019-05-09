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
			/*
			REST_lib.Cart cart = new REST_lib.Cart();
			cart.test = 10;
			string output = JsonConvert.SerializeObject(cart);

			REST_lib.Cart cart2 = JsonConvert.DeserializeObject<REST_lib.Cart>(output);
			*/

			Messaging.Instance.ForceAwake();

			//CreateWebHostBuilder(args).Build().Run();
			IWebHost host = CreateWebHostBuilder(args).Build();
			host.RunAsync();
			host.WaitForShutdown();

			Messaging.Instance.Dispose();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) {
            return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
		}

	}
}

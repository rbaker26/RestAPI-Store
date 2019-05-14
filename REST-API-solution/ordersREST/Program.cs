using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using REST_lib;

namespace ordersREST
{
    public class Program
    {
        public static void Main(string[] args)
        {
			Messenger.CreateInstance("Orders", makeVerbose: true);
			/*
			Messenger.Instance.SetupListener<Product>(
				(Product p) => { Console.Out.WriteLine("PRINTING: " + p.ToString()); },
				Messenger.MessageType.ProductUpdates
			);
			*/

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
                .UseStartup<Startup>();
    }
}

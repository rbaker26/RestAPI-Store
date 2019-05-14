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
			Messenger.CreateInstance("Orders");
			Messenger.Instance.SetupListener<Product>(
				(Product p) => { Console.Out.WriteLine(p.ToString()); },
				Messenger.MessageType.ProductUpdates
			);

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

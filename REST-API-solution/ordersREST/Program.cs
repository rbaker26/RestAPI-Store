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
            Heartbeat.Instance.InitHeartbeat("Orders REST-API");
            Heartbeat.Instance.Start();

            // Listen for Carts published on the newOrders channel.
            // Send this cart to the database.
            Messenger.CreateInstance("Orders", makeVerbose: true);
			/*
            Messenger.Instance.SetupListener<Cart>((Cart c) => {
                Console.Out.WriteLine(c);
                SQL_Interface.Instance.AddNewOrder(c.Email, c.ShoppingCart);
            }, Messenger.MessageType.NewOrders);
			*/

			Messenger.Instance.SetupListener<Product>((Product p) => {
				Console.Out.WriteLine("Received product: " + p);
				SQL_Interface.Instance.SetProductInfo(p);
			}, Messenger.MessageType.ProductUpdates);

            // Setup WebServer
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
                .UseUrls("http://*:5000");
    }
}

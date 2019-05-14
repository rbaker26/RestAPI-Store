﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using REST_lib;


namespace ProductsREST
{
    public class Program
    {
        public static void Main(string[] args)
        {
			Messenger.CreateInstance("Products", makeVerbose: true);
            Messenger.Instance.SetupListener<List<ProductUpdate>>((List<ProductUpdate> updates)=>{
                foreach(ProductUpdate productUpdate in updates)
                {
                    SQL_Interface.Instance.ReduceItemQuantity(productUpdate);

                }
            },
                Messenger.MessageType.ProductUpdates);

			// TODO Now that this is simpler, I highly suggest pulling it in here or just turning this into a static function.
			productsREST.Receive r = new productsREST.Receive();

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
                .UseUrls("http://localhost:5000", "http://*:5000");
    }
}


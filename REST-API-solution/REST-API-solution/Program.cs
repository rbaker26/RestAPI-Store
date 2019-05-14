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

namespace REST_API_solution
{
    public class Program
    {
        public static void Main(string[] args)
        {
			Messenger.CreateInstance("Products", makeVerbose: true);

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


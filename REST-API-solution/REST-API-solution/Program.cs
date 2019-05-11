using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace REST_API_solution
{
    public class Program
    {
        public static void Main(string[] args)
        {
			try {
				IWebHost host = CreateWebHostBuilder(args).Build();
				host.RunAsync();
				host.WaitForShutdown();
			}
			finally {
				REST_lib.Messenger.Instance.Dispose();
			}
		}

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:5000", "http://*:5000");
    }
}

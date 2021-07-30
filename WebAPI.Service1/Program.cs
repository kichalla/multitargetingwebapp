using System;
#if NETFRAMEWORK
using Microsoft.Owin.Hosting;
#else
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
#endif

namespace WebAPI.Service1
{
    public class Program
    {
        public static void Main(string[] args)
        {
#if NETFRAMEWORK
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"Listening at {baseAddress}...");
                Console.ReadLine();
            }
#else
            CreateHostBuilder(args).Build().Run();
#endif
        }

#if !NETFRAMEWORK
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
#endif
    }
}

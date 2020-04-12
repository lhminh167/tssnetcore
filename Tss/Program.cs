using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Tss.Web.Configs;

namespace Tss
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .UseSentry("https://32757548d1954aa1addbac3c657867dd@sentry.io/5189176")
                .UseSerilog(LoggingConfig.Configure)
                .UseStartup<Startup>();
    }
}
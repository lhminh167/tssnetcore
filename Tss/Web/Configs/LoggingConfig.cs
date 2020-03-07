using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.SqlServer.Destructurers;
using Serilog.Formatting.Elasticsearch;
// using Serilog.Exceptions.EntityFrameworkCore;

namespace Tss.Web.Configs
{
    /// <summary>
    /// Https start-up configuration
    /// </summary>
    public static class LoggingConfig
    {
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void Configure(
            WebHostBuilderContext context,
            LoggerConfiguration config
        )
        {
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

            config
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithHttpRequestId()
                .Enrich.WithUserName()
                .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                    .WithDefaultDestructurers()
                    .WithDestructurers(new[] {
                        new SqlExceptionDestructurer()
                        // , 
                        // new DbUpdateExceptionDestructurer()  
                    })
                );



            if (context.HostingEnvironment.IsDevelopment())
            {
                config.WriteTo.Console();
                WriteToFile(config);
            }
            else
            {
                WriteToFile(config);
            }

        }

        public static void WriteToFile(
            LoggerConfiguration config
        )
        {
            config.WriteTo.File(
                path: "./Log/.json",
                // restrictedToMinimumLevel: LogEventLevel.Debug,
                formatter: new ElasticsearchJsonFormatter(),
                flushToDiskInterval: TimeSpan.FromSeconds(2),
                // fileSizeLimitBytes: 1_000_000,
                rollOnFileSizeLimit: true,
                shared: true,
                rollingInterval: RollingInterval.Day
            // retainedFileCountLimit: 3
            );
        }
    }
}

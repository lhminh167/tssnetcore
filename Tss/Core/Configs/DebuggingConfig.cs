using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Tss.Core.Middlewares;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using Ardalis.ListStartupServices;

namespace Tss.Core.Configs
{
    /// <summary>
    /// Https start-up configuration
    /// </summary>
    public static class DebuggingConfig
    {
        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureService(IServiceCollection services)
        {
            services.Configure<ServiceConfig>(config =>
            {
                config.Services = new List<ServiceDescriptor>(services);
                config.Path = "/allservices";
            });
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env
        )
        {
            if (env.IsDevelopment())
            {
                app.UseShowAllServicesMiddleware();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();

            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}

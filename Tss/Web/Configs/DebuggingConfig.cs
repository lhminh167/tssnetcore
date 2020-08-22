using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Tss.Web.Middlewares;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Tss.Web.Configs
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
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env
        )
        {
            if (env.IsDevelopment())
            {
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

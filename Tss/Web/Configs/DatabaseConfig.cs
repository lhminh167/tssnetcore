using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Tss.Infrastructure.Contexts;
using Tss.Core.Models;

namespace Tss.Web.Configs
{
    /// <summary>
    /// HTTPS documentation components start-up configuration
    /// </summary>
    public static class DatabaseConfig
    {
        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureService(IServiceCollection services, AppSettings appSettings)
        {
            TssContext.ConfigureService(services, appSettings);
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void Configure(IApplicationBuilder app)
        {
        }
    }
}

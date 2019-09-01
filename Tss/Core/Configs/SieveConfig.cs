using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sieve;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using Sieve.Models;
using Sieve.Services;
using Microsoft.Extensions.Configuration;

namespace Tss.Core.Configs
{
    /// <summary>
    /// Https start-up configuration
    /// </summary>
    public static class SieveConfig
    {
        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddSingleton<SieveProcessor>();

            services.Configure<SieveOptions>(configuration.GetSection(nameof(Sieve)));


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

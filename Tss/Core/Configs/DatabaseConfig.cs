using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Tss.Infrastructure.Contexts;
using Tss.Core.Models;

namespace Tss.Core.Configs
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
            services.AddDbContext<TssContext>(options => options.UseMySql(appSettings.DatabaseConnectionString));
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

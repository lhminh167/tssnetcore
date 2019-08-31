using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Tss.Infrastructure.Contexts;

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
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {            
            // For development in local environment
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // For deployment to Heroku
            var isProduction = string.Equals(Environment.GetEnvironmentVariable("ENV"), "production");

            if (isProduction)
            {
                connectionString = Environment.GetEnvironmentVariable("CLEARDB_DATABASE_URL");
            }

            services.AddDbContext<TssContext>(options => options.UseMySql(connectionString));


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

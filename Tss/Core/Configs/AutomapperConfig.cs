using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Tss.Core.Configs
{
    /// <summary>
    /// Https start-up configuration
    /// </summary>
    public static class AutomapperConfig
    {
        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureService(IServiceCollection services)
        {
            // services.AddAutoMapper(Assembly.GetExecutingAssembly());

            var config = new MapperConfiguration(cfg =>
                cfg.AddMaps(Assembly.GetExecutingAssembly())
            );

            services.TryAddSingleton<IMapper>(serviceProvider =>
                config.CreateMapper()
            );

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

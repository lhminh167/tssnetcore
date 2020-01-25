using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tss.Core.Configs;
using Tss.Core.Models;

namespace Tss
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();


            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            IConfiguration appSettingsSection = Configuration.GetSection("AppSettings");
            AppSettings appSettings = appSettingsSection.Get<AppSettings>();
            services.AddOptions();
            services.Configure<AppSettings>(appSettingsSection);

            
            // For deployment to Heroku
            var isProduction = string.Equals(Environment.GetEnvironmentVariable("ENV"), "production");

            if (isProduction)
            {
                appSettings = new AppSettings();
            }



            HttpConfig.ConfigureService(services, appSettings);
            
            DIConfig.ConfigureService(services);

            DatabaseConfig.ConfigureService(services, appSettings);

            CachingConfig.ConfigureService(services);

            AutomapperConfig.ConfigureService(services);

            SwaggerConfig.ConfigureService(services);

            DebuggingConfig.ConfigureService(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            DebuggingConfig.Configure(app, env);
            
            HttpConfig.Configure(app);
            
            SwaggerConfig.Configure(app);   
        }
    }
}

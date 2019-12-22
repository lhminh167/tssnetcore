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
        public IConfiguration AppSettingsSection { get; }
        public AppSettings AppSettings { get; }

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettingsSection = Configuration.GetSection("AppSettings");
            AppSettings = AppSettingsSection.Get<AppSettings>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<AppSettings>(AppSettingsSection);

            HttpConfig.ConfigureService(services, AppSettings);
            
            DIConfig.ConfigureService(services);

            DatabaseConfig.ConfigureService(services, AppSettings);

            CachingConfig.ConfigureService(services);

            SieveConfig.ConfigureService(services, Configuration);

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

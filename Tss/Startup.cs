using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tss.Core.Configs;

namespace Tss
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DIConfig.ConfigureService(services);

            HttpConfig.ConfigureService(services);

            CachingConfig.ConfigureService(services);

            DatabaseConfig.ConfigureService(services, Configuration);

            SieveConfig.ConfigureService(services, Configuration);

            AutomapperConfig.ConfigureService(services);

            SwaggerConfig.ConfigureService(services);

            DebuggingConfig.ConfigureService(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            DebuggingConfig.Configure(app, env);

            HttpConfig.Configure(app);
            
            SwaggerConfig.Configure(app);   
        }
    }
}

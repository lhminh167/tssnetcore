using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tss.Infrastructure.Contexts;
using Swashbuckle.AspNetCore.Swagger;
using Sieve.Services;
using Sieve.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using Tss.Core.ProductFilters;
using Microsoft.EntityFrameworkCore;

namespace Tss
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
        
                    ValidIssuer = "http://localhost:5000",
                    ValidAudience = "http://localhost:5000",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                };
            });

            services.Scan(scan =>
                scan.FromCallingAssembly()
                    .AddClasses()
                    .AsMatchingInterface()
            );

            // For development in local environment
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            // For deployment to Heroku
            var isProduction = string.Equals(Environment.GetEnvironmentVariable("ENV"), "production");

            if (isProduction)
            {
                connectionString = Environment.GetEnvironmentVariable("CLEARDB_DATABASE_URL");
            }

            services.TryAddSingleton<SieveProcessor>();

            services.Configure<SieveOptions>(Configuration.GetSection("Sieve"));

            // services.Add(
            //     new ServiceDescriptor(
            //         typeof(TssContext),
            //         new TssContext(connectionString)
            //     )
            // );

            services.AddDbContext<TssContext>(options => options.UseMySql(connectionString));


            services.AddAutoMapper(typeof(ProductFiltersProductProfile));

            var config = new AutoMapperConfiguration()
                .Configure();
            //     .CreateMapper();
            // services.TryAddSingleton(mapper);

            services.TryAddSingleton<IMapper>(serviceProvider  => config.CreateMapper());



            services
            .AddMvcCore()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddCors()
            .AddJsonFormatters()
            .AddApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "T-Shirt Shop API",
                    Description = "ASP.NET Core Web API for T-Shirt Shop Project"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            );

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvcWithDefaultRoute();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}

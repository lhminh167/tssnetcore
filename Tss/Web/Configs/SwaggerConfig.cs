using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Tss.Web.Configs
{
    /// <summary>
    /// Swagger API documentation components start-up configuration
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureService(IServiceCollection services)
        {
            // Swagger API documentation
            services.AddSwaggerGen(c =>
            {
                // TODO: Need to push hardcoded strings to resource file
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "T-Shirt Shop API",
                        Version = "v1",
                        Description = "ASP.NET Core Web API for T-Shirt Shop Project",
                        TermsOfService = new Uri("TODO: Add Terms of service"),
                        Contact = new OpenApiContact
                        {
                            Name = "Le Hoang Minh",
                            Email = "lhminh167@gmail.com",
                            Url = new Uri("https://github.com/lhminh167/tssnetcore")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "MIT License",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    }
                );

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void Configure(IApplicationBuilder app)
        {
            // This will redirect default url to Swagger url
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "T-Shirt Shop API V1");
            });
        }
    }
}

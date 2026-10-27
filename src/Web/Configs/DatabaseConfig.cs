using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Tss.Infrastructure.Data;
using Tss.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
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
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<TssContext>();
                context.Database.OpenConnection();
                bool migrationLockAcquired = false;
                try
                {
                    var connection = context.Database.GetDbConnection();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT GET_LOCK('tssnetcore_ef_migrations', 60)";
                        var lockResult = command.ExecuteScalar();
                        if (lockResult == null || lockResult == DBNull.Value || Convert.ToInt32(lockResult) != 1)
                        {
                            throw new InvalidOperationException("Could not acquire the database migration lock.");
                        }

                        migrationLockAcquired = true;
                    }

                    context.Database.Migrate();
                }
                finally
                {
                    try
                    {
                        if (migrationLockAcquired)
                        {
                            using (var command = context.Database.GetDbConnection().CreateCommand())
                            {
                                command.CommandText = "SELECT RELEASE_LOCK('tssnetcore_ef_migrations')";
                                command.ExecuteScalar();
                            }
                        }
                    }
                    finally
                    {
                        context.Database.CloseConnection();
                    }
                }
            }

        }
    }
}

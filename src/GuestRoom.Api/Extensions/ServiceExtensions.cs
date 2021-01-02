using System;
using GuestRoom.Domain;
using GuestRoom.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GuestRoom.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddEfCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => { options.UseSqlite(configuration.GetConnectionString("SqliteConnection")); },
                                                ServiceLifetime.Transient);

            services.AddIdentity<AppUser, AppRole>(options => { options.SignIn.RequireConfirmedAccount = true; })
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<AppDbContext>();
        }

        internal static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<AppDbContext>>();

                try
                {
                    var context = services.GetService<AppDbContext>();

                    context.Database.Migrate();
                    logger.LogInformation($"Migrated database for context {nameof(AppDbContext)}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Error while migrating database for context {nameof(AppDbContext)}");
                }
            }

            return host;
        }
    }
}
using System;
using System.Diagnostics.CodeAnalysis;
using GuestRoom.Api.Contracts.Security;
using GuestRoom.Api.Models.Configuration;
using GuestRoom.Api.Services.Security;
using GuestRoom.Domain;
using GuestRoom.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

namespace GuestRoom.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    internal static class ServiceExtensions
    {
        internal static void AddEfCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => { options.UseSqlite(configuration.GetConnectionString("SqliteConnection")); },
                                                ServiceLifetime.Transient);

            services.AddIdentity<AppUser, AppRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;
                })
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<ISignInManager, SignInManager>();
            services.AddTransient<ITokenService, TokenService>();
        }

        internal static void AddGuestRoomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Token>(configuration.GetSection(nameof(Token)));
            services.AddScoped<IAuthService, AuthService>();
            services.AddMailKit(config => config.UseMailKit(configuration.GetSection(nameof(MailKitOptions)).Get<MailKitOptions>()));
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
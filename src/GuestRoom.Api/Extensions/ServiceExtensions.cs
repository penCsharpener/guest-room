using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using GuestRoom.Api.Contracts.Security;
using GuestRoom.Api.Models.Configuration;
using GuestRoom.Api.Services.Security;
using GuestRoom.Domain;
using GuestRoom.Domain.Models;
using GuestRoom.Domain.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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

            services.AddJsonWebTokenConfiguration(configuration);

            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<ISignInManager, SignInManager>();
            services.AddTransient<ITokenService, TokenService>();
        }

        internal static void AddGuestRoomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Token>(configuration.GetSection(nameof(Token)));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IFileProvider, FileProvider>();
            services.AddScoped<IJsonConverter, JsonConverter>();
            services.AddScoped<IContentStore, ContentStore>();
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

        internal static void AddJsonWebTokenConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtAppSettingOptions = configuration.GetSection(nameof(Token));
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAppSettingOptions["Key"]));

            services.ConfigureJwtIssuerOptions(jwtAppSettingOptions, signingKey);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.ClaimsIssuer = jwtAppSettingOptions[nameof(Token.Issuer)];
                    options.TokenValidationParameters = GetTokenValidationParameters(jwtAppSettingOptions, signingKey);
                    options.SaveToken = true;
                });
        }

        private static void ConfigureJwtIssuerOptions(this IServiceCollection services, IConfigurationSection jwtAppSettingOptions, SymmetricSecurityKey signingKey)
        {
            services.Configure<Token>(options =>
            {
                options.Audience = jwtAppSettingOptions[nameof(Token.Audience)];
                options.Issuer = jwtAppSettingOptions[nameof(Token.Issuer)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });
        }

        private static TokenValidationParameters GetTokenValidationParameters(IConfigurationSection jwtAppSettingOptions, SymmetricSecurityKey signingKey)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = IsRelease(),
                ValidIssuer = jwtAppSettingOptions[nameof(Token.Issuer)],
                ValidateAudience = IsRelease(),
                ValidAudience = jwtAppSettingOptions[nameof(Token.Audience)],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }

        internal static bool IsRelease()
        {
#if DEBUG
            return false;
#else
            return true;
#endif
        }
    }
}
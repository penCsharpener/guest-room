using GuestRoom.Api.Contracts.Security;
using GuestRoom.Api.Models.Configuration;
using GuestRoom.Api.Services.Security;
using GuestRoom.Domain;
using GuestRoom.Domain.Models;
using GuestRoom.Domain.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NETCore.MailKit.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GuestRoom.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    internal static class ServiceExtensions
    {
        internal static void AddEfCore(this IServiceCollection services, AppSettings appsettings, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options => { options.UseSqlite(connectionString); }, ServiceLifetime.Transient);

            services.AddIdentity<AppUser, AppRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;
#if DEBUG
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 1;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredUniqueChars = 1;
#endif
                })
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddJsonWebTokenConfiguration(appsettings.Token);

            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<ISignInManager, SignInManager>();
            services.AddTransient<ITokenService, TokenService>();
        }

        internal static void AddGuestRoomServices(this IServiceCollection services, AppSettings appsettings)
        {
            services.AddSingleton(appsettings);
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IFileProvider, FileProvider>();
            services.AddScoped<IJsonConverter, JsonConverter>();
            services.AddScoped<IContentStore, ContentStore>();
            services.AddMailKit(config => config.UseMailKit(appsettings.MailKitOptions));
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

        internal static void AddJsonWebTokenConfiguration(this IServiceCollection services, Token tokenSettings)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.Key));

            services.ConfigureJwtIssuerOptions(tokenSettings, signingKey);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.ClaimsIssuer = tokenSettings.Issuer;
                    options.TokenValidationParameters = GetTokenValidationParameters(tokenSettings, signingKey);
                    options.SaveToken = true;
                });
        }

        private static void ConfigureJwtIssuerOptions(this IServiceCollection services, Token tokenSettings, SymmetricSecurityKey signingKey)
        {
            services.Configure<Token>(options =>
            {
                options.Audience = tokenSettings.Audience;
                options.Issuer = tokenSettings.Issuer;
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });
        }

        private static TokenValidationParameters GetTokenValidationParameters(Token tokenSettings, SymmetricSecurityKey signingKey)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = IsRelease(),
                ValidIssuer = tokenSettings.Issuer,
                ValidateAudience = IsRelease(),
                ValidAudience = tokenSettings.Audience,
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
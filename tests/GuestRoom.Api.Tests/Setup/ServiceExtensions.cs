using System;
using GuestRoom.Api.Contracts.Security;
using GuestRoom.Api.Controllers;
using GuestRoom.Api.Models.Configuration;
using GuestRoom.Api.Services.Security;
using GuestRoom.Domain;
using GuestRoom.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using NSubstitute;

namespace GuestRoom.Api.Tests.Setup
{
    public static class ServiceExtensions
    {
        public static IServiceProvider SetupDependencies()
        {
            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddIdentity<AppUser, AppRole>(options => { options.SignIn.RequireConfirmedAccount = true; })
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            var defaultHttpContext = new DefaultHttpContext();
            defaultHttpContext.Request.Scheme = "https";
            defaultHttpContext.Request.Host = HostString.FromUriComponent("https://localhost:5001/api");
            var controllerContext = new ControllerContext { HttpContext = defaultHttpContext };

            services.AddScoped(
                x => new AccountController(x.GetService<IAuthService>(), x.GetService<ITokenService>(), Substitute.For<ILogger<AccountController>>()) { ControllerContext = controllerContext });

            services.AddLogging();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<ISignInManager, SignInManager>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddMediatR(typeof(TokenService).Assembly);

            var tokenOptions = new Token { Key = "super secure key", Issuer = "https://localhost:5001" };
            services.AddScoped(_ => Options.Create(tokenOptions));
            services.AddScoped<IAuthService, AuthService>();

            services.AddMailKit(config => config.UseMailKit(new MailKitOptions
            {
                Server = "127.0.0.1",
                Port = 25,
                SenderName = "Guest room integration tests",
                SenderEmail = "guestroom-integration-tests@email.com",
                Account = "",
                Password = "",
                Security = false
            }));

            return services.BuildServiceProvider();
        }
    }
}
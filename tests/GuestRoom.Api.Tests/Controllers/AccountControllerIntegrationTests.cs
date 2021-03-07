using System;
using System.Threading.Tasks;
using FluentAssertions;
using GuestRoom.Api.Contracts.Security;
using GuestRoom.Api.Controllers;
using GuestRoom.Api.Models.Account;
using GuestRoom.Api.Tests.Setup;
using GuestRoom.Domain;
using GuestRoom.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GuestRoom.Api.Tests.Controllers
{
    public class AccountControllerIntegrationTests
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AccountController _testObject;

        public AccountControllerIntegrationTests()
        {
            _serviceProvider = ServiceExtensions.SetupDependencies();
            _testObject = _serviceProvider.GetService<AccountController>();
        }

        [Fact(Skip = Constants.INTEGRATION_TESTS)]
        public async Task Registration_Sends_Email_And_Creates_User_In_Database_And_Confirm_Account()
        {
            var authService = _serviceProvider.GetRequiredService<IAuthService>();

            RegistrationConfirmationEventArgs confirmationEventArgs = null;

            void Handler(object _, RegistrationConfirmationEventArgs e)
            {
                confirmationEventArgs = e;
            }

            authService.OnConfirmationLinkCreated += Handler;

            var result = await _testObject.Register(new RegisterDto { DisplayName = "John Doe", Email = "j.doe@email.com", Password = "Pwd123!!" });
            var context = _serviceProvider.GetRequiredService<AppDbContext>();
            var users = await context.Users.ToListAsync();

            users.Count.Should().Be(1);
            users[0].DisplayName.Should().Be("John Doe");
            users[0].EmailConfirmed.Should().Be(false);

            result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be(201);

            var verifyResult = await _testObject.VerifyEmail(confirmationEventArgs.UserId, confirmationEventArgs.Code);

            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == "j.doe@email.com");
            user.EmailConfirmed.Should().BeTrue();
            verifyResult.Should().BeOfType<OkResult>();

            authService.OnConfirmationLinkCreated -= Handler;
        }

        [Fact(Skip = Constants.INTEGRATION_TESTS)]
        public async Task Forgot_Password_Sends_Email_With_Link()
        {
            var authService = _serviceProvider.GetRequiredService<IAuthService>();

            ForgotPasswordEventArgs forgotPasswordEventArgs = null;

            void Handler(object _, ForgotPasswordEventArgs e)
            {
                forgotPasswordEventArgs = e;
            }

            authService.OnResetPasswordLinkCreated += Handler;

            var context = _serviceProvider.GetRequiredService<AppDbContext>();

            var user = new AppUser
            {
                Email = "j.doe@email.com",
                UserName = "j.doe@email.com",
                NormalizedEmail = "j.doe@email.com".ToUpper(),
                NormalizedUserName = "j.doe@email.com".ToUpper(),
                EmailConfirmed = true,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            user.PasswordHash = new PasswordHasher<AppUser>().HashPassword(user, "oldPwd");
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var result = await _testObject.ForgotPassword(new ForgotPasswordDto { EmailAddress = "j.doe@email.com", ClientUri = "https://localhost:5001/api/account/resetpassword/" });

            result.Should().BeOfType<OkResult>();
            forgotPasswordEventArgs.Email.Should().Be("j.doe@email.com");
            forgotPasswordEventArgs.Token.Should().EndWith("==");

            authService.OnResetPasswordLinkCreated -= Handler;
        }
    }
}
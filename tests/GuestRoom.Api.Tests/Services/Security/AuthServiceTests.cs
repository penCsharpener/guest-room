using FluentAssertions;
using GuestRoom.Api.Contracts.Security;
using GuestRoom.Api.Models.Configuration;
using GuestRoom.Api.Services.Security;
using GuestRoom.Domain;
using GuestRoom.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GuestRoom.Api.Tests
{
    public class AuthServiceTests
    {
        private readonly IEmailService _emailService = Substitute.For<IEmailService>();
        private readonly IUserManager _userManager = Substitute.For<IUserManager>();
        private readonly ISignInManager _signInManager = Substitute.For<ISignInManager>();
        private readonly AppDbContext _handlerContext;
        private readonly ILogger<AuthService> _logger = Substitute.For<ILogger<AuthService>>();
        private readonly AuthService _testObject;

        public AuthServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using (var context = new AppDbContext(options))
            {
                context.Users.Add(new AppUser { Email = "jane.doe@email.com", DisplayName = "Jane Doe" });
                context.SaveChanges();
            }

            var appsettings = new AppSettings
            {
                ClientUrl = "https://localhost:5000"
            };

            _handlerContext = new AppDbContext(options);
            _testObject = new AuthService(appsettings, _userManager, _signInManager, _emailService, _logger);
        }

        [Fact]
        public async Task Registration_Is_Successful()
        {
            _userManager.CreateAsync(Arg.Any<AppUser>(), Arg.Any<string>()).Returns(IdentityResult.Success);

            var result = await _testObject.RegisterAsync(new AppUser { Email = "j.doe@email.com", DisplayName = "John Doe" }, "pwd123PGD!");

            result.Should().BeTrue();
        }

        [Fact]
        public async Task Login_Is_Successful()
        {
            var user = new AppUser { Email = "j.doe@email.com", DisplayName = "John Doe" };
            _userManager.FindByEmailAsync(Arg.Any<string>()).Returns(user);
            _signInManager.CheckPasswordSignInAsync(Arg.Any<AppUser>(), Arg.Any<string>(), Arg.Any<bool>()).Returns(SignInResult.Success);

            var result = await _testObject.LoginAsync("j.doe@email.com", "pwd123PGD!");

            result.Should().NotBeNull();
            result.DisplayName.Should().Be("John Doe");
        }

        [Fact]
        public async Task Login_Fails_User_Not_Found()
        {
            _userManager.FindByEmailAsync(Arg.Any<string>()).Returns(default(AppUser));

            var result = await _testObject.LoginAsync("j.doe@email.com", "pwd123PGD!");

            result.Should().BeNull();
        }

        [Fact]
        public async Task Login_Fails_Password_Check_Unsuccessful()
        {
            var user = new AppUser { Email = "j.doe@email.com", DisplayName = "John Doe" };
            _userManager.FindByEmailAsync(Arg.Any<string>()).Returns(user);
            _signInManager.CheckPasswordSignInAsync(Arg.Any<AppUser>(), Arg.Any<string>(), Arg.Any<bool>()).Returns(SignInResult.Failed);

            var result = await _testObject.LoginAsync("j.doe@email.com", "pwd123PGD!");

            result.Should().BeNull();
        }

        [Fact]
        public async Task User_Is_Registered_Returns_True()
        {
            var user = new AppUser { Email = "j.doe@email.com", DisplayName = "John Doe" };
            _userManager.FindByEmailAsync(Arg.Is("j.doe@email.com")).Returns(user);

            var result = await _testObject.UserIsRegisteredAsync("j.doe@email.com");

            result.Should().BeTrue();
        }

        [Fact]
        public async Task User_Is_Registered_Returns_False()
        {
            var user = new AppUser { Email = "jane.doe@email.com", DisplayName = "Jane Doe" };
            _userManager.FindByEmailAsync(Arg.Is("jane.doe@email.com")).Returns(user);

            var result = await _testObject.UserIsRegisteredAsync("j.doe@email.com");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task User_Registration_Fails_To_Create_User()
        {
            _userManager.CreateAsync(Arg.Any<AppUser>(), Arg.Any<string>()).Returns(IdentityResult.Failed(new IdentityError()));

            var result = await _testObject.RegisterAsync(new AppUser { Email = "j.doe@email.com", DisplayName = "John Doe" }, "pwd123");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task ConfirmEmailAsync_Confirms_User()
        {
            _userManager.FindByEmailAsync(Arg.Any<string>()).Returns(new AppUser());
            _userManager.ConfirmEmailAsync(Arg.Any<AppUser>(), Arg.Any<string>()).Returns(IdentityResult.Success);

            var result = await _testObject.ConfirmEmailAsync("j.doe@email.com", "code");

            result.Should().BeTrue();
        }

        [Fact]
        public async Task ConfirmEmailAsync_Cannot_Find_User()
        {
            _userManager.FindByIdAsync(Arg.Any<int>()).Returns(default(AppUser));

            var result = await _testObject.ConfirmEmailAsync("j.doe@email.com", "code");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task ResetPasswordAsync_Sends_Email()
        {
            var user = new AppUser { Email = "jane.doe@email.com", DisplayName = "Jane Doe" };
            _userManager.FindByEmailAsync(Arg.Any<string>()).Returns(user);
            _userManager.GeneratePasswordResetTokenAsync(Arg.Any<AppUser>()).Returns("token");
            ForgotPasswordEventArgs eventArgs = default;

            void Handler(object sender, ForgotPasswordEventArgs e)
            {
                eventArgs = e;
            }

            _testObject.OnResetPasswordLinkCreated += Handler;

            var result = await _testObject.ForgotPasswordAsync("jane.doe@email.com");

            result.Should().BeTrue();
            eventArgs.Email.Should().Be("jane.doe@email.com");
            eventArgs.Token.Should().Be("token");
            await _emailService.Received(1).SendAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());

            _testObject.OnResetPasswordLinkCreated -= Handler;
        }

        [Fact]
        public async Task ResetPasswordAsync_Doesnt_Find_User()
        {
            _userManager.FindByEmailAsync(Arg.Any<string>()).Returns(default(AppUser));

            var result = await _testObject.ForgotPasswordAsync("jane.doe@email.com");

            result.Should().BeFalse();
        }
    }
}
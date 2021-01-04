using System;
using System.Threading.Tasks;
using FluentAssertions;
using GuestRoom.Api.Contracts.Security;
using GuestRoom.Api.Services.Security;
using GuestRoom.Domain;
using GuestRoom.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using NSubstitute;
using Xunit;

namespace GuestRoom.Api.Tests
{
    public class AuthServiceTests
    {
        private readonly IEmailService _emailService = Substitute.For<IEmailService>();
        private readonly AppDbContext _handlerContext;
        private readonly ILogger<AuthService> _logger = Substitute.For<ILogger<AuthService>>();
        private AuthService _testObject;

        public AuthServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using (var context = new AppDbContext(options))
            {
                context.Users.Add(new AppUser { Email = "jane.doe@email.com", DisplayName = "Jane Doe" });
                context.SaveChanges();
            }

            _handlerContext = new AppDbContext(options);
        }

        [Fact]
        public async Task Registration_Is_Successful()
        {
            var userManager = Substitute.For<IUserManager>();
            userManager.CreateAsync(Arg.Any<AppUser>(), Arg.Any<string>()).Returns(IdentityResult.Success);
            _testObject = new AuthService(userManager, Substitute.For<ISignInManager>(), _emailService, _logger);

            var meta = new RegistrationMetaData { Password = "pwd123PGD!", RequestScheme = "https://", RequestHostUrl = "localhost:5000" };
            var result = await _testObject.RegisterAsync(new AppUser { Email = "j.doe@email.com", DisplayName = "John Doe" }, meta);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task Login_Is_Successful()
        {
            var userManager = Substitute.For<IUserManager>();
            var user = new AppUser { Email = "j.doe@email.com", DisplayName = "John Doe" };
            userManager.FindByEmailAsync(Arg.Any<string>()).Returns(user);
            var signInManager = Substitute.For<ISignInManager>();
            signInManager.CheckPasswordSignInAsync(Arg.Any<AppUser>(), Arg.Any<string>(), Arg.Any<bool>()).Returns(SignInResult.Success);
            _testObject = new AuthService(userManager, signInManager, _emailService, _logger);

            var result = await _testObject.LoginAsync("j.doe@email.com", "pwd123PGD!");

            result.Should().NotBeNull();
            result.DisplayName.Should().Be("John Doe");
        }

        [Fact]
        public async Task Login_Fails_User_Not_Found()
        {
            var userManager = Substitute.For<IUserManager>();
            userManager.FindByEmailAsync(Arg.Any<string>()).Returns(default(AppUser));
            var signInManager = Substitute.For<ISignInManager>();
            _testObject = new AuthService(userManager, signInManager, _emailService, _logger);

            var result = await _testObject.LoginAsync("j.doe@email.com", "pwd123PGD!");

            result.Should().BeNull();
        }

        [Fact]
        public async Task Login_Fails_Password_Check_Unsuccessful()
        {
            var userManager = Substitute.For<IUserManager>();
            var user = new AppUser { Email = "j.doe@email.com", DisplayName = "John Doe" };
            userManager.FindByEmailAsync(Arg.Any<string>()).Returns(user);
            var signInManager = Substitute.For<ISignInManager>();
            signInManager.CheckPasswordSignInAsync(Arg.Any<AppUser>(), Arg.Any<string>(), Arg.Any<bool>()).Returns(SignInResult.Failed);
            _testObject = new AuthService(userManager, signInManager, _emailService, _logger);

            var result = await _testObject.LoginAsync("j.doe@email.com", "pwd123PGD!");

            result.Should().BeNull();
        }

        [Fact]
        public async Task User_Is_Registered_Returns_True()
        {
            var userManager = Substitute.For<IUserManager>();
            var user = new AppUser { Email = "j.doe@email.com", DisplayName = "John Doe" };
            userManager.FindByEmailAsync(Arg.Is("j.doe@email.com")).Returns(user);
            var signInManager = Substitute.For<ISignInManager>();
            _testObject = new AuthService(userManager, signInManager, _emailService, _logger);

            var result = await _testObject.UserIsRegisteredAsync("j.doe@email.com");

            result.Should().BeTrue();
        }

        [Fact]
        public async Task User_Is_Registered_Returns_False()
        {
            var userManager = Substitute.For<IUserManager>();
            var user = new AppUser { Email = "jane.doe@email.com", DisplayName = "Jane Doe" };
            userManager.FindByEmailAsync(Arg.Is("jane.doe@email.com")).Returns(user);
            var signInManager = Substitute.For<ISignInManager>();
            _testObject = new AuthService(userManager, signInManager, _emailService, _logger);

            var result = await _testObject.UserIsRegisteredAsync("j.doe@email.com");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task User_Registration_Fails_To_Create_User()
        {
            var userManager = Substitute.For<IUserManager>();
            userManager.CreateAsync(Arg.Any<AppUser>(), Arg.Any<string>()).Returns(IdentityResult.Failed(new IdentityError()));
            _testObject = new AuthService(userManager, Substitute.For<ISignInManager>(), _emailService, _logger);

            var result = await _testObject.RegisterAsync(new AppUser { Email = "j.doe@email.com", DisplayName = "John Doe" }, new RegistrationMetaData());

            result.Should().BeFalse();
        }

        [Fact]
        public async Task ConfirmEmailAsync_Confirms_User()
        {
            var userManager = Substitute.For<IUserManager>();
            userManager.FindByIdAsync(Arg.Any<int>()).Returns(new AppUser());
            userManager.ConfirmEmailAsync(Arg.Any<AppUser>(), Arg.Any<string>()).Returns(IdentityResult.Success);
            _testObject = new AuthService(userManager, Substitute.For<ISignInManager>(), _emailService, _logger);

            var result = await _testObject.ConfirmEmailAsync(2, "code");

            result.Should().BeTrue();
        }

        [Fact]
        public async Task ConfirmEmailAsync_Cannot_Find_User()
        {
            var userManager = Substitute.For<IUserManager>();
            userManager.FindByIdAsync(Arg.Any<int>()).Returns(default(AppUser));
            _testObject = new AuthService(userManager, Substitute.For<ISignInManager>(), _emailService, _logger);

            var result = await _testObject.ConfirmEmailAsync(2, "code");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task ResetPasswordAsync_Sends_Email()
        {
            var userManager = Substitute.For<IUserManager>();
            var user = new AppUser { Email = "jane.doe@email.com", DisplayName = "Jane Doe" };
            userManager.FindByEmailAsync(Arg.Any<string>()).Returns(user);
            userManager.GeneratePasswordResetTokenAsync(Arg.Any<AppUser>()).Returns("token");
            ForgotPasswordEventArgs eventArgs = default;
            _testObject = new AuthService(userManager, Substitute.For<ISignInManager>(), _emailService, _logger);

            void Handler(object sender, ForgotPasswordEventArgs e)
            {
                eventArgs = e;
            }

            _testObject.OnResetPasswordLinkCreated += Handler;

            var result = await _testObject.ResetPasswordAsync("jane.doe@email.com", "https://localhost:5001/api/resetpassword/");

            result.Should().BeTrue();
            eventArgs.Email.Should().Be("jane.doe@email.com");
            eventArgs.Token.Should().Be("token");
            await _emailService.Received(1).SendAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());

            _testObject.OnResetPasswordLinkCreated -= Handler;
        }

        [Fact]
        public async Task ResetPasswordAsync_Doesnt_Find_User()
        {
            var userManager = Substitute.For<IUserManager>();
            userManager.FindByEmailAsync(Arg.Any<string>()).Returns(default(AppUser));
            _testObject = new AuthService(userManager, Substitute.For<ISignInManager>(), _emailService, _logger);

            var result = await _testObject.ResetPasswordAsync("jane.doe@email.com", "https://localhost:5001/api/resetpassword/");

            result.Should().BeFalse();
        }
    }
}
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
using NSubstitute;
using Xunit;

namespace GuestRoom.Api.Tests
{
    public class AuthServiceTests
    {
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
            _testObject = new AuthService(userManager, Substitute.For<ISignInManager>(), _logger);

            var result = await _testObject.RegisterAsync(new AppUser { Email = "j.doe@email.com", DisplayName = "John Doe" }, "pwd123PGD!");

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
            _testObject = new AuthService(userManager, signInManager, _logger);

            var result = await _testObject.LoginAsync("j.doe@email.com", "pwd123PGD!");

            result.Should().NotBeNull();
            result.DisplayName.Should().Be("John Doe");
        }

        [Fact]
        public async Task User_Is_Registered_Returns_True()
        {
            var userManager = Substitute.For<IUserManager>();
            var user = new AppUser { Email = "j.doe@email.com", DisplayName = "John Doe" };
            userManager.FindByEmailAsync(Arg.Any<string>()).Returns(user);
            var signInManager = Substitute.For<ISignInManager>();
            _testObject = new AuthService(userManager, signInManager, _logger);

            var result = await _testObject.UserIsRegisteredAsync("j.doe@email.com");

            result.Should().BeTrue();
        }

        [Fact]
        public async Task User_Is_Registered_Returns_False()
        {
            var userManager = Substitute.For<IUserManager>();
            var user = new AppUser { Email = "jane.doe@email.com", DisplayName = "Jane Doe" };
            userManager.FindByEmailAsync(Arg.Any<string>()).Returns(user);
            var signInManager = Substitute.For<ISignInManager>();
            _testObject = new AuthService(userManager, signInManager, _logger);

            var result = await _testObject.UserIsRegisteredAsync("j.doe@email.com");

            result.Should().BeFalse();
        }
    }
}
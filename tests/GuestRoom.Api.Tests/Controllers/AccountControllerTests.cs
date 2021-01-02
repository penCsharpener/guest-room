using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using GuestRoom.Api.Contracts.Security;
using GuestRoom.Api.Controllers;
using GuestRoom.Api.Models;
using GuestRoom.Api.Models.Account;
using GuestRoom.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace GuestRoom.Api.Tests.Controllers
{
    public class AccountControllerTests
    {
        private readonly IAuthService _authService = Substitute.For<IAuthService>();
        private readonly ILogger<AccountController> _logger = Substitute.For<ILogger<AccountController>>();
        private readonly ITokenService _tokenService = Substitute.For<ITokenService>();
        private AccountController _testObject;

        public AccountControllerTests()
        {
            _tokenService.CreateToken(Arg.Any<AppUser>()).Returns("eyabcdef1234567890");
        }

        [Fact]
        public async Task User_Is_Registered()
        {
            _authService.UserIsRegisteredAsync(Arg.Any<string>()).Returns(false);
            _authService.RegisterAsync(Arg.Any<AppUser>(), Arg.Any<string>()).Returns(true);
            _testObject = new AccountController(_authService, _tokenService, _logger);

            var result = await _testObject.Register(new RegisterDto { DisplayName = "John Doe", Email = "j.doe@email.com", Password = "pwd123" });

            result.Value.DisplayName.Should().Be("John Doe");
            result.Result.Should().BeNull();
        }

        [Fact]
        public async Task User_Exists_And_Not_Registered()
        {
            _authService.UserIsRegisteredAsync(Arg.Any<string>()).Returns(true);
            _testObject = new AccountController(_authService, _tokenService, _logger);

            var result = await _testObject.Register(new RegisterDto { DisplayName = "John Doe", Email = "j.doe@email.com", Password = "pwd123" });

            var objResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Which;
            objResult.StatusCode.Should().Be(400);
            var apiResult = objResult.Value.Should().BeOfType<ApiValidationErrorResponse>().Which;
            apiResult.Errors.Count().Should().Be(1);
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task User_Cannot_Be_Created()
        {
            _authService.UserIsRegisteredAsync(Arg.Any<string>()).Returns(false);
            _authService.RegisterAsync(Arg.Any<AppUser>(), Arg.Any<string>()).Returns(false);
            _testObject = new AccountController(_authService, _tokenService, _logger);

            var result = await _testObject.Register(new RegisterDto { DisplayName = "John Doe", Email = "j.doe@email.com", Password = "pwd123" });

            var objResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Which;
            objResult.StatusCode.Should().Be(400);
            var apiResult = objResult.Value.Should().BeOfType<ApiResponse>().Which;
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task User_Cannot_Login()
        {
            _authService.LoginAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(default(AppUser));
            _testObject = new AccountController(_authService, _tokenService, _logger);

            var result = await _testObject.Login(new LoginDto { Email = "j.doe@email.com", Password = "pwd123" });

            var objResult = result.Result.Should().BeOfType<UnauthorizedObjectResult>().Which;
            objResult.StatusCode.Should().Be(401);
            var apiResult = objResult.Value.Should().BeOfType<ApiResponse>().Which;
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task User_Can_Login()
        {
            _authService.LoginAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(new AppUser { Email = "j.doe@email.com", DisplayName = "John Doe" });
            _testObject = new AccountController(_authService, _tokenService, _logger);

            var result = await _testObject.Login(new LoginDto { Email = "j.doe@email.com", Password = "pwd123" });

            result.Value.DisplayName.Should().Be("John Doe");
            result.Result.Should().BeNull();
        }
    }
}
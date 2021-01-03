using System.Net;
using System.Threading.Tasks;
using GuestRoom.Api.Contracts.Security;
using GuestRoom.Api.Extensions;
using GuestRoom.Api.Models;
using GuestRoom.Api.Models.Account;
using GuestRoom.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GuestRoom.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AccountController> _logger;
        private readonly ITokenService _tokenService;

        public AccountController(IAuthService authService, ITokenService tokenService, ILogger<AccountController> logger)
        {
            _authService = authService;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _authService.LoginAsync(loginDto.Email, loginDto.Password);

            if (user == null)
            {
                _logger.LogInformation($"Failed login attempt by user {loginDto.Email.ToEmailForLogging()}");

                return Unauthorized(new ApiResponse(HttpStatusCode.Unauthorized));
            }

            return new UserDto { Email = user.Email, Token = _tokenService.CreateToken(user), DisplayName = user.DisplayName };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var userExists = await _authService.UserIsRegisteredAsync(registerDto.Email);

            if (userExists)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address is in use." } });
            }

            var user = new AppUser { DisplayName = registerDto.DisplayName, Email = registerDto.Email, UserName = registerDto.Email };

            var meta = new RegistrationMetaData { Password = registerDto.Password, RequestScheme = Request?.Scheme, RequestHostUrl = Request?.Host.ToString() };
            var result = await _authService.RegisterAsync(user, meta);

            if (!result)
            {
                return BadRequest(new ApiResponse(HttpStatusCode.BadRequest));
            }

            _logger.LogInformation($"New user '{registerDto.Email.ToEmailForLogging()}' was registered.");

            return new UserDto { DisplayName = user.DisplayName, Token = _tokenService.CreateToken(user), Email = user.Email };
        }

        [HttpGet("verifyemail")]
        public async Task<ActionResult> VerifyEmail(int userId, string code)
        {
            var result = await _authService.ConfirmEmailAsync(userId, code);

            if (!result)
            {
                return new BadRequestObjectResult(new ApiResponse(HttpStatusCode.BadRequest));
            }

            return Ok();
        }
    }
}
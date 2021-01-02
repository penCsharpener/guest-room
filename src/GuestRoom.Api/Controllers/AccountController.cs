﻿using System.Net;
using System.Threading.Tasks;
using GuestRoom.Api.Contracts.Security;
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

            var result = await _authService.RegisterAsync(user, registerDto.Password);

            if (!result)
            {
                return BadRequest(new ApiResponse(HttpStatusCode.BadRequest));
            }

            return new UserDto { DisplayName = user.DisplayName, Token = _tokenService.CreateToken(user), Email = user.Email };
        }
    }
}
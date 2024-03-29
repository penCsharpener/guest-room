﻿using GuestRoom.Api.Contracts.Security;
using GuestRoom.Api.Extensions;
using GuestRoom.Api.Models;
using GuestRoom.Api.Models.Account;
using GuestRoom.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers;

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

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetUser()
    {
        var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

        var user = await _authService.FindByEmailAsync(email);

        if (user == null)
        {
            return null;
        }

        return new UserDto()
        {
            Email = user.Email,
            Token = _tokenService.CreateToken(user),
            DisplayName = user.DisplayName
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto, [FromQuery] string ReturnUrl = null)
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
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        if (registerDto.Password != registerDto.PasswordConfirm)
        {
            return BadRequest(new ApiValidationErrorResponse { Errors = new[] { "Password and password confirmation don't match." } });
        }

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

        _logger.LogInformation($"New user '{registerDto.Email.ToEmailForLogging()}' was registered.");

        return StatusCode(201);
    }

    [HttpPost("email/verify")]
    public async Task<ActionResult> VerifyEmail(VerifyEmailDto request)
    {
        var result = await _authService.ConfirmEmailAsync(request.Email, request.Code);

        if (!result)
        {
            return new BadRequestObjectResult(new ApiResponse(HttpStatusCode.BadRequest));
        }

        return Ok();
    }

    [HttpPost("password/forgot")]
    public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordDto parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        await _authService.ForgotPasswordAsync(parameters.EmailAddress);

        return Ok();
    }

    [HttpPost("password/reset")]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _authService.ResetPasswordAsync(parameters.Email, parameters.Token, parameters.Password);

        if (!result)
        {
            return BadRequest("User not found or error during password reset.");
        }

        return Ok();
    }

    [HttpPost("email/change")]
    [Authorize]
    public async Task<ActionResult> ChangeEmail(ChangePasswordDto parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var userExists = await _authService.UserIsRegisteredAsync(parameters.NewEmail);

        if (userExists)
        {
            return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address is in use." } });
        }

        var result = await _authService.ChangeEmailAsync(parameters.UserId, parameters.Password, parameters.NewEmail);

        if (!result)
        {
            return BadRequest("User not found or couldn't send token for new email.");
        }

        return Ok();
    }
}

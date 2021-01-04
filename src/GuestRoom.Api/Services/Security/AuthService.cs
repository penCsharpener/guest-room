using System;
using System.Net;
using System.Threading.Tasks;
using GuestRoom.Api.Contracts.Security;
using GuestRoom.Domain.Models;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;

namespace GuestRoom.Api.Services.Security
{
    public class AuthService : IAuthService
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<AuthService> _logger;
        private readonly ISignInManager _signInManager;
        private readonly IUserManager _userManager;

        public AuthService(IUserManager userManager, ISignInManager signInManager, IEmailService emailService, ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _logger = logger;
        }

        public event EventHandler<RegistrationConfirmationEventArgs> OnConfirmationLinkCreated;
        public event EventHandler<ForgotPasswordEventArgs> OnResetPasswordLinkCreated;

        public async Task<bool> RegisterAsync(AppUser user, RegistrationMetaData registrationMeta)
        {
            var result = await _userManager.CreateAsync(user, registrationMeta.Password);

            if (!result.Succeeded)
            {
                return false;
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            OnConfirmationLinkCreated?.Invoke(this, new RegistrationConfirmationEventArgs(user.Id, code));

            code = WebUtility.UrlEncode(code);

            var link = $"{registrationMeta.RequestScheme}://{registrationMeta.RequestHostUrl}/{registrationMeta.ControllerName}/{registrationMeta.MethodName}?userId={user.Id}&code={code}";

            await _emailService.SendAsync(user.Email, "Verify Email", $"<a href=\"{link}\">Verify Email</a>");

            return true;
        }

        public async Task<bool> ConfirmEmailAsync(int userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            return result.Succeeded;
        }

        public async Task<AppUser> LoginAsync(string email, string userPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userPassword, false);

            if (!result.Succeeded)
            {
                return null;
            }

            return user;
        }

        public async Task<bool> UserIsRegisteredAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user != null;
        }

        public async Task<bool> ResetPasswordAsync(string emailAddress, string clientUri)
        {
            var user = await _userManager.FindByEmailAsync(emailAddress);

            if (user == null)
            {
                return false;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            OnResetPasswordLinkCreated?.Invoke(this, new ForgotPasswordEventArgs(emailAddress, token));

            token = WebUtility.UrlEncode(token);

            await _emailService.SendAsync(user.Email, "Reset Password", $"<a href=\"{clientUri.Trim('/')}/?token={token}&email={user.Email}\">Reset Password</a>");

            return true;
        }
    }
}
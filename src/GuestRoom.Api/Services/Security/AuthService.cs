using System.Threading.Tasks;
using GuestRoom.Api.Contracts.Security;
using GuestRoom.Domain.Models;
using Microsoft.Extensions.Logging;

namespace GuestRoom.Api.Services.Security
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _authService;
        private readonly ISignInManager _signInManager;
        private readonly IUserManager _userManager;

        public AuthService(IUserManager userManager, ISignInManager signInManager, ILogger<AuthService> authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
        }

        public async Task<bool> RegisterAsync(AppUser user, string userPassword)
        {
            var result = await _userManager.CreateAsync(user, userPassword);

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
    }
}
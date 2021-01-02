using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GuestRoom.Api.Contracts.Security;
using GuestRoom.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GuestRoom.Api.Services.Security
{
    public class UserManager : IUserManager
    {
        private readonly UserManager<AppUser> _userManager;

        public UserManager(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<IdentityResult> CreateAsync(AppUser user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public Task<AppUser> FindByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public Task<AppUser> FindByUserByClaimsPrincipleWithAddressAsync(ClaimsPrincipal userClaimsPrincipal)
        {
            var email = userClaimsPrincipal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            return _userManager.Users.SingleOrDefaultAsync(x => x.Email.Equals(email));
        }

        public Task<AppUser> FindByEmailFromClaimsPrinciple(ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            return _userManager.Users.SingleOrDefaultAsync(x => x.Email.Equals(email));
        }

        public Task<IdentityResult> UpdateAsync(AppUser user)
        {
            return _userManager.UpdateAsync(user);
        }
    }
}
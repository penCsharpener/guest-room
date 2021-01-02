using System.Security.Claims;
using System.Threading.Tasks;
using GuestRoom.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace GuestRoom.Api.Contracts.Security
{
    public interface IUserManager
    {
        Task<IdentityResult> CreateAsync(AppUser user, string password);
        Task<AppUser> FindByEmailAsync(string email);
        Task<AppUser> FindByUserByClaimsPrincipleWithAddressAsync(ClaimsPrincipal userClaimsPrincipal);
        Task<IdentityResult> UpdateAsync(AppUser user);
    }
}
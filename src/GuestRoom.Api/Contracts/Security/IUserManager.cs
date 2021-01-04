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
        Task<AppUser> FindByIdAsync(int userId);
        Task<AppUser> FindByEmailFromClaimsPrinciple(ClaimsPrincipal user);
        Task<AppUser> FindByUserByClaimsPrincipleWithAddressAsync(ClaimsPrincipal userClaimsPrincipal);
        Task<string> GenerateChangeEmailTokenAsync(AppUser user, string newEmail);
        Task<string> GenerateEmailConfirmationTokenAsync(AppUser user);
        Task<string> GeneratePasswordResetTokenAsync(AppUser user);
        Task<IdentityResult> UpdateAsync(AppUser user);
        Task<IdentityResult> ConfirmEmailAsync(AppUser user, string code);
        Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword);
    }
}
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GuestRoom.Api.Contracts.Security;
using GuestRoom.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace GuestRoom.Api.Services.Security
{
    [ExcludeFromCodeCoverage]
    public class SignInManager : ISignInManager
    {
        private readonly SignInManager<AppUser> _signInManager;

        public SignInManager(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public Task<SignInResult> CheckPasswordSignInAsync(AppUser user, string password, bool lockoutOnFailure)
        {
            return _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
        }
    }
}
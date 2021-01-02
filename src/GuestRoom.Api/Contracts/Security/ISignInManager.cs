using System.Threading.Tasks;
using GuestRoom.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace GuestRoom.Api.Contracts.Security
{
    public interface ISignInManager
    {
        Task<SignInResult> CheckPasswordSignInAsync(AppUser user, string password, bool lockoutOnFailure);
    }
}
using System.Threading.Tasks;
using GuestRoom.Domain.Models;

namespace GuestRoom.Api.Contracts.Security
{
    public interface IAuthService
    {
        Task<AppUser> LoginAsync(string email, string userPassword);
        Task<bool> RegisterAsync(AppUser user, string userPassword);
        Task<bool> UserIsRegisteredAsync(string email);
    }
}
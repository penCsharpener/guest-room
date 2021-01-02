using GuestRoom.Domain.Models;

namespace GuestRoom.Api.Contracts.Security
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
using System;
using System.Threading.Tasks;
using GuestRoom.Domain.Models;

namespace GuestRoom.Api.Contracts.Security
{
    public interface IAuthService
    {
        Task<bool> ConfirmEmailAsync(int userId, string code);
        Task<AppUser> LoginAsync(string email, string userPassword);
        Task<bool> RegisterAsync(AppUser user, RegistrationMetaData registrationMeta);
        Task<bool> UserIsRegisteredAsync(string email);

        event EventHandler<RegistrationConfirmationEventArgs> OnConfirmationLinkCreated;
    }

    public record RegistrationMetaData
    {
        public string Password { get; init; }
        public string MethodName { get; init; } = "verifyemail";
        public string ControllerName { get; init; } = "account";
        public string RequestScheme { get; init; }
        public string RequestHostUrl { get; init; }
    }

    public record RegistrationConfirmationEventArgs(int UserId, string Code) { }
}
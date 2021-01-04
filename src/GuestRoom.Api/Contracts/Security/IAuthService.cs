using System;
using System.Threading.Tasks;
using GuestRoom.Domain.Models;

namespace GuestRoom.Api.Contracts.Security
{
    public interface IAuthService
    {
        Task<bool> ConfirmEmailAsync(int userId, string code);
        Task<AppUser> LoginAsync(string email, string userPassword);
        Task<bool> RegisterAsync(AppUser user, string password, string verifyEndpointUri);
        Task<bool> UserIsRegisteredAsync(string email);
        Task<bool> ForgotPasswordAsync(string emailAddress, string clientUri);
        Task<bool> ResetPasswordAsync(string email, string token, string password);
        Task<bool> ChangeEmailAsync(int userId, string password, string newEmail, string clientUri);

        event EventHandler<RegistrationConfirmationEventArgs> OnConfirmationLinkCreated;
        event EventHandler<ForgotPasswordEventArgs> OnResetPasswordLinkCreated;
    }

    public record RegistrationConfirmationEventArgs(int UserId, string Code) { }

    public record ForgotPasswordEventArgs(string Email, string Token) { }
}
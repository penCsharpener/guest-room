using GuestRoom.Domain.Models;
using System;
using System.Threading.Tasks;

namespace GuestRoom.Api.Contracts.Security
{
    public interface IAuthService
    {
        Task<bool> ConfirmEmailAsync(string userEmail, string code);
        Task<AppUser> LoginAsync(string email, string userPassword);
        Task<bool> RegisterAsync(AppUser user, string password);
        Task<bool> UserIsRegisteredAsync(string email);
        Task<bool> ForgotPasswordAsync(string emailAddress);
        Task<bool> ResetPasswordAsync(string email, string token, string password);
        Task<bool> ChangeEmailAsync(int userId, string password, string newEmail);
        Task<AppUser> FindByEmailAsync(string email);

        event EventHandler<RegistrationConfirmationEventArgs> OnConfirmationLinkCreated;
        event EventHandler<ForgotPasswordEventArgs> OnResetPasswordLinkCreated;
    }

    public record RegistrationConfirmationEventArgs(string UserEmail, string Code) { }

    public record ForgotPasswordEventArgs(string Email, string Token) { }
}
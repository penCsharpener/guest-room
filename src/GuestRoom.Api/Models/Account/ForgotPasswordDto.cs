using System.ComponentModel.DataAnnotations;

namespace GuestRoom.Api.Models.Account
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string ClientUri { get; set; }
    }
}
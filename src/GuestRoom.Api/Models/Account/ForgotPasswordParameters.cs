using System.ComponentModel.DataAnnotations;

namespace GuestRoom.Api.Models.Account
{
    public class ForgotPasswordParameters
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string ClientUri { get; set; }
    }
}
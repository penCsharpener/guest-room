using System.ComponentModel.DataAnnotations;

namespace GuestRoom.Api.Models.Account
{
    public class ChangePasswordDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }
    }
}
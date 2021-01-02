using Microsoft.AspNetCore.Identity;

namespace GuestRoom.Domain.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string DisplayName { get; set; }
    }
}
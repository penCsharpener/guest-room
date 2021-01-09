using Microsoft.IdentityModel.Tokens;

namespace GuestRoom.Api.Models.Configuration
{
    public class Token
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
    }
}
using NETCore.MailKit.Infrastructure.Internal;

namespace GuestRoom.Api.Models.Configuration
{
    public class AppSettings
    {
        public Token Token { get; set; }
        public string ApiUrl { get; set; }
        public string ClientUrl { get; set; }
        public MailKitOptions MailKitOptions { get; set; }
        public ApplicationPaths ApplicationPaths { get; set; }
    }
}

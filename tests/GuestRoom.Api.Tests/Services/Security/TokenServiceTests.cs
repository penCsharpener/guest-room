using FluentAssertions;
using GuestRoom.Api.Models.Configuration;
using GuestRoom.Api.Services.Security;
using GuestRoom.Domain.Models;
using Microsoft.Extensions.Options;
using Xunit;

namespace GuestRoom.Api.Tests.Services.Security
{
    public class TokenServiceTests
    {
        private readonly TokenService _testObject;

        public TokenServiceTests()
        {
            _testObject = new TokenService(Options.Create(new Token { Key = "secureKeysecureKeysecureKeysecureKey", Issuer = "http://localhost:5001" }));
        }

        [Fact]
        public void Service_Creates_Token()
        {
            var token = _testObject.CreateToken(new AppUser { Email = "j.doe@email.com", DisplayName = "John Doe" });

            token.Should().NotBeNullOrEmpty();
            token.Should().StartWith("ey");
        }
    }
}
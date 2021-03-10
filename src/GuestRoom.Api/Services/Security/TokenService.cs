using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GuestRoom.Api.Contracts.Security;
using GuestRoom.Api.Models.Configuration;
using GuestRoom.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GuestRoom.Api.Services.Security
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly Token _token;

        public TokenService(AppSettings appsettings)
        {
            _token = appsettings.Token;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token.Key));
        }

        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.Email, user.Email), new Claim(JwtRegisteredClaimNames.GivenName, user.DisplayName) };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor { Subject = new ClaimsIdentity(claims), Expires = DateTime.Now.AddDays(7), SigningCredentials = creds, Issuer = _token.Issuer };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
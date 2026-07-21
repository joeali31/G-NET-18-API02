using E_Commerce01.Application.Helpers;
using E_Commerce01.Application.Services.Contracts;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace E_Commerce01.Infrastructure.Identity.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSetting _settings;
        public JwtService(IOptions<JwtSetting> options)
        {
            _settings = options.Value;
        }

        public string CreateTokenAsync(string userId, string email, string userName, IReadOnlyList<string> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, userName)
            };


            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role , role));
            }


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));

            var jwtToken = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                expires: DateTime.UtcNow.AddDays(_settings.DurationInDays),
                claims: claims,
                signingCredentials: new SigningCredentials(securityKey , SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}

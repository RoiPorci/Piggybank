using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Piggybank.Business.Interfaces;
using Piggybank.Models;
using Piggybank.Shared.Constants;
using Piggybank.Shared.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Piggybank.Business.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenDto GenerateJwtToken(AppUser user, IEnumerable<string> roles)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            string jwtSecret = _configuration[ConfigConstants.JwtSecret]
                ?? throw new InvalidOperationException(string.Format(ErrorMessages.JwtSecretNotFound, ConfigConstants.JwtSecret));

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            string jwtExpiresInMinutes = _configuration[ConfigConstants.JwtExpiresInMinutes]
                ?? throw new InvalidOperationException(string.Format(ErrorMessages.JwtExpiresInMinutesNotFound, ConfigConstants.JwtExpiresInMinutes));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration[ConfigConstants.JwtIssuer],
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(jwtExpiresInMinutes)),
                signingCredentials: credentials);

            return new TokenDto { Token = new JwtSecurityTokenHandler().WriteToken(token) };
        }
    }
}

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
    /// <summary>
    /// Implements the <see cref="ITokenService"/> interface to generate JWT tokens.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private string _jwtSecret;
        private string _jwtExpiresInMinutes;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration used to retrieve JWT settings.</param>
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            GetJwtConfiguration(_configuration);
        }

        /// <inheritdoc />
        public TokenDto GenerateJwtToken(AppUser user, IEnumerable<string> roles)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));       

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);  

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration[ConfigConstants.JwtIssuer],
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(_jwtExpiresInMinutes)),
                signingCredentials: credentials);

            return new TokenDto { Token = new JwtSecurityTokenHandler().WriteToken(token) };
        }

        /// <summary>
        /// Retrieves JWT configuration values from the configuration.
        /// Throws an exception if the required configuration is missing.
        /// </summary>
        /// <param name="configuration">The configuration object used to retrieve JWT settings.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when JWT secret or expiration settings are missing in the configuration.
        /// </exception>
        private void GetJwtConfiguration(IConfiguration configuration)
        {
            IConfigurationSection jwtSettings = _configuration.GetSection(ConfigConstants.JwtSection);

            _jwtSecret = jwtSettings[ConfigConstants.JwtSecret]
                ?? throw new InvalidOperationException(string.Format(ErrorMessages.JwtSecretNotFound, ConfigConstants.JwtSecret));

            _jwtExpiresInMinutes = jwtSettings[ConfigConstants.JwtExpiresInMinutes]
                ?? throw new InvalidOperationException(string.Format(ErrorMessages.JwtExpiresInMinutesNotFound, ConfigConstants.JwtExpiresInMinutes));
        }
    }
}

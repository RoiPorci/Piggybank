using Piggybank.Models;
using Piggybank.Shared.Dtos;

namespace Piggybank.Business.Interfaces
{
    /// <summary>
    /// Provides functionality for generating JWT tokens for users.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates a JWT token for the specified user with the given roles.
        /// </summary>
        /// <param name="user">The user for whom the token is being generated.</param>
        /// <param name="roles">A list of roles associated with the user.</param>
        /// <returns>A <see cref="TokenDto"/> containing the generated JWT token.</returns>
        TokenDto GenerateJwtToken(AppUser user, IEnumerable<string> roles);
    }
}

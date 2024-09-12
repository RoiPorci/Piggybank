using Piggybank.Models;
using Piggybank.Shared.Dtos;

namespace Piggybank.Business.Interfaces
{
    public interface ITokenService
    {
        TokenDto GenerateJwtToken(AppUser user, IEnumerable<string> roles);
    }
}

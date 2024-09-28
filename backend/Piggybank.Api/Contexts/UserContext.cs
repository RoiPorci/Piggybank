using System.Security.Claims;

namespace Piggybank.Api.Contexts
{
    /// <summary>
    /// Implements <see cref="IUserContext"/> to retrieve user information from the current HTTP context.
    /// </summary>
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserContext"/> class with the provided HTTP context accessor.
        /// </summary>
        /// <param name="httpContextAccessor">The accessor used to retrieve the current HTTP context.</param>
        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc />
        public string? GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}

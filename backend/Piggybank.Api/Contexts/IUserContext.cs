namespace Piggybank.Api.Contexts
{
    /// <summary>
    /// Provides methods for accessing the current user's context information.
    /// </summary>
    public interface IUserContext
    {
        /// <summary>
        /// Retrieves the current user's unique identifier.
        /// </summary>
        /// <returns>The user ID if available, otherwise <see langword="null"/>.</returns>
        string? GetUserId();
    }
}

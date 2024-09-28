namespace Piggybank.Shared.Dtos
{
    /// <summary>
    /// Data Transfer Object that contains a JWT token.
    /// </summary>
    public class TokenDto
    {
        /// <summary>
        /// The JWT token string.
        /// </summary>
        public string? Token { get; set; }
    }
}

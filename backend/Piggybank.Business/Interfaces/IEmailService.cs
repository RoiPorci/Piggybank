namespace Piggybank.Business.Interfaces
{
    /// <summary>
    /// Provides email sending functionality.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email asynchronously to the specified recipient.
        /// </summary>
        /// <param name="toEmail">The email address of the recipient.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The body content of the email.</param>
        /// <returns>A task representing the asynchronous email sending operation.</returns>
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}

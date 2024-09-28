using Microsoft.Extensions.Configuration;
using Piggybank.Business.Interfaces;
using System.Net.Mail;
using System.Net;

namespace Piggybank.Business.Services
{
    /// <summary>
    /// Implements the <see cref="IEmailService"/> interface to send emails using SMTP.
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private string _host;
        private int _port;
        private string _username;
        private string _password;
        private bool _enableSsl;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// Loads SMTP configuration from the provided configuration object.
        /// </summary>
        /// <param name="configuration">The configuration object to retrieve SMTP settings.</param>
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            GetSmtpConfiguration(configuration);
        }

        /// <inheritdoc />
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            SmtpClient client = new SmtpClient(_host)
            {
                Port = _port,
                Credentials = new NetworkCredential(_username, _password),
                EnableSsl = _enableSsl,
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(_username),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            await client.SendMailAsync(mailMessage);
        }

        /// <summary>
        /// Retrieves the SMTP configuration from the configuration object.
        /// </summary>
        /// <param name="configuration">The configuration object to retrieve SMTP settings.</param>
        private void GetSmtpConfiguration(IConfiguration configuration)
        {
            IConfigurationSection smtpSettings = _configuration.GetSection("Smtp");
            _host = smtpSettings["Host"];
            _port = int.Parse(smtpSettings["Port"]);
            _username = smtpSettings["UserName"];
            _password = smtpSettings["Password"];
            _enableSsl = bool.Parse(smtpSettings["EnableSsl"]);
        }
    }
}

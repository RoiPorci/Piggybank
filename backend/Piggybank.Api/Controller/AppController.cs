using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Piggybank.Api.Contexts;
using System.Net;

namespace Piggybank.Api.Controller
{
    /// <summary>
    /// Base controller class that provides common functionality for handling errors and accessing the current user's ID.
    /// </summary>
    public class AppController : ControllerBase
    {
        protected readonly ILogger<AppController> _logger;
        private readonly IUserContext _userContext;
        protected string _userId = string.Empty;

        /// <summary>
        /// Gets the current user's ID from the <see cref="IUserContext"/>. 
        /// If the user ID is not available, throws an <see cref="ArgumentNullException"/>.
        /// </summary>
        protected string UserId
        {
            get
            {
                if (string.IsNullOrEmpty(_userId))
                {
                    string? userIdNullable = _userContext.GetUserId();

                    if (userIdNullable == null)
                        throw new ArgumentNullException(nameof(UserId));

                    UserId = userIdNullable;
                }
                return _userId;
            }
            set { _userId = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging information.</param>
        /// <param name="userContext">The user context to retrieve the current user's ID.</param>
        public AppController(ILogger<AppController> logger, IUserContext userContext)
        {
            _logger = logger;
            _userContext = userContext;
        }

        /// <summary>
        /// Handles errors by logging the provided message and returning an HTTP status code with the error message.
        /// </summary>
        /// <param name="logMessage">The message to log.</param>
        /// <param name="responseMessage">The message to return in the HTTP response.</param>
        /// <param name="httpStatusCode">The HTTP status code to return (default is <see cref="HttpStatusCode.BadRequest"/>).</param>
        /// <returns>An <see cref="ObjectResult"/> containing the error response.</returns>
        protected ObjectResult HandleError(string logMessage, string responseMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            _logger.LogInformation(logMessage);

            return StatusCode((int)httpStatusCode, responseMessage);
        }

        /// <summary>
        /// Handles errors from an <see cref="IdentityResult"/> by logging the error descriptions and returning an HTTP status code with the error message.
        /// </summary>
        /// <param name="result">The <see cref="IdentityResult"/> containing error details.</param>
        /// <param name="responseMessage">The message to return in the HTTP response.</param>
        /// <param name="httpStatusCode">The HTTP status code to return (default is <see cref="HttpStatusCode.BadRequest"/>).</param>
        /// <returns>An <see cref="ObjectResult"/> containing the error response.</returns>
        protected ObjectResult HandleError(IdentityResult result, string responseMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            return HandleError(string.Join(", ", result.Errors.Select(error => error.Description)), responseMessage, httpStatusCode);
        }
    }
}

using Piggybank.Shared.Constants;
using System.Net;

namespace Piggybank.Api.Middlewares
{
    /// <summary>
    /// Middleware for handling errors in the application.
    /// Logs unhandled exceptions and returns a generic internal server error message.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware"/> class.
    /// </remarks>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">The logger instance for logging errors.</param>
    public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger = logger;

        /// <summary>
        /// Invokes the middleware to handle the request.
        /// If an unhandled exception occurs, logs the error and returns a 500 Internal Server Error response.
        /// </summary>
        /// <param name="context">The HTTP context for the current request.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessages.UnhandledException);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(HttpMessages.InternalServerError);
            }
        }
    }
}

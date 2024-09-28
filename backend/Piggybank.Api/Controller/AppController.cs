using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Piggybank.Api.Contexts;
using System.Net;

namespace Piggybank.Api.Controller
{
    public class AppController : ControllerBase
    {
        protected readonly ILogger<AppController> _logger;
        private readonly IUserContext _userContext;
        protected string _userId = string.Empty;

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

        public AppController(ILogger<AppController> logger, IUserContext userContext)
        {
            _logger = logger;
            _userContext = userContext;
        }

        protected ObjectResult HandleError(string logMessage, string responseMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            _logger.LogInformation(logMessage);

            return StatusCode((int)httpStatusCode, responseMessage);
        }

        protected ObjectResult HandleError(IdentityResult result, string responseMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            return HandleError(string.Join(", ", result.Errors.Select(error => error.Description)), responseMessage, httpStatusCode);
        }
    }
}

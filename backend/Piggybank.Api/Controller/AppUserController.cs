using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Patterns;
using Piggybank.Business.Interfaces;
using Piggybank.Models;
using Piggybank.Shared.Constants;

namespace Piggybank.Api.Controller
{
    [Route(RoutePatterns.ApiRoute)]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserService _userService;
        private readonly ILogger<AppUserController> _logger;

        public AppUserController(IAppUserService userService, ILogger<AppUserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> Get()
        {
            IEnumerable<AppUser> users = await _userService.GetAllAsync();

            return Ok(users);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Piggybank.Business.Interfaces;
using Piggybank.Shared.Constants;
using Piggybank.Shared.Dtos;

namespace Piggybank.Api.Controller
{
    [Route(RoutePatterns.AdminApiRoute)]
    [ApiController]
    [Authorize(Policy = ConfigConstants.AdminPolicy)]
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
        public async Task<ActionResult<IEnumerable<AppUserDto>>> Get()
        {
            IEnumerable<AppUserDto> users = await _userService.GetAllWithRolesAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUserDto>> GetById(string id)
        {
            AppUserDto? user = await _userService.GetByIdWithRolesAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}

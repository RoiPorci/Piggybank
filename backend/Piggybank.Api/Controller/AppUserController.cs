using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Piggybank.Api.Contexts;
using Piggybank.Business.Interfaces;
using Piggybank.Models;
using Piggybank.Shared.Constants;
using Piggybank.Shared.Dtos;

namespace Piggybank.Api.Controller
{
    [Route(RoutePatterns.AdminApiRoute)]
    [ApiController]
    [Authorize(Policy = ConfigConstants.AdminPolicy)]
    public class AppUserController : AppController
    {
        private readonly IAppUserService _userService;

        public AppUserController(IAppUserService userService, ILogger<AppUserController> logger, IUserContext userContext) 
            : base(logger, userContext)
        {
            _userService = userService;
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

        [HttpPost, ValidateModel]
        public async Task<ActionResult> Create([FromBody] CreateAppUserDto createUserDto)
        {
            IdentityResult result = await _userService.AddAsync(
                new AppUser{ 
                    UserName = createUserDto.UserName,
                    Email = createUserDto.Email,
                },
                createUserDto.Password,
                createUserDto.Roles
            );

            if (!result.Succeeded)
                return HandleError(result, HttpMessages.UserCreationFailure);

            return Ok(HttpMessages.UserCreationSuccess);
        }

        [HttpPut("{id}"), ValidateModel]
        public async Task<ActionResult> Update(string id, [FromBody] UpdateAppUserWithRolesDto updateUserDto)
        {
            AppUser? user = await _userService.GetByIdAsync(id);

            if (user == null)
                return HandleError(ErrorMessages.UserNotFound, HttpMessages.UserNotFound);

            user.UserName = updateUserDto.UserName;
            user.Email = updateUserDto.Email;

            IdentityResult result = await _userService.UpdateAsync(user, updateUserDto.Roles);

            if (!result.Succeeded)
                return HandleError(result, HttpMessages.UserUpdateFailure);

            return Ok(HttpMessages.UserUpdateSuccess);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            IdentityResult result = await _userService.DeleteAsync(id);

            if (!result.Succeeded)
                return HandleError(ErrorMessages.DeleteFailure, HttpMessages.UserDeleteFailure);

            return Ok(HttpMessages.UserDeleteSuccess);
        }
    }
}

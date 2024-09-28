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
    /// <summary>
    /// Provides endpoints for managing users in the system, including user creation, retrieval, updating, and deletion.
    /// Only accessible to administrators.
    /// </summary>
    [Route(RoutePatterns.AdminApiRoute)]
    [ApiController]
    [Authorize(Policy = ConfigConstants.AdminPolicy)]
    public class AppUserController : AppController
    {
        private readonly IAppUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserController"/> class.
        /// </summary>
        /// <param name="userService">The user service for managing users.</param>
        /// <param name="logger">The logger instance for logging information.</param>
        /// <param name="userContext">The user context for retrieving the current user's information.</param>
        public AppUserController(IAppUserService userService, ILogger<AppUserController> logger, IUserContext userContext)
            : base(logger, userContext)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves a list of all users along with their roles.
        /// </summary>
        /// <returns>A list of users with their associated roles.</returns>
        /// <response code="200">List of users retrieved successfully.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AppUserDto>), 200)]
        public async Task<ActionResult<IEnumerable<AppUserDto>>> Get()
        {
            IEnumerable<AppUserDto> users = await _userService.GetAllWithRolesAsync();

            return Ok(users);
        }

        /// <summary>
        /// Retrieves detailed information for a specific user by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The user's detailed information, including roles, or a 404 if the user is not found.</returns>
        /// <response code="200">User retrieved successfully.</response>
        /// <response code="404">User not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AppUserDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<AppUserDto>> GetById(string id)
        {
            AppUserDto? user = await _userService.GetByIdWithRolesAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Creates a new user in the system with specified roles.
        /// </summary>
        /// <param name="createUserDto">The user creation details, including username, email, password, and roles.</param>
        /// <returns>An OK response if the user is created successfully, or an error if the creation fails.</returns>
        /// <response code="200">User created successfully.</response>
        /// <response code="400">User creation failed.</response>
        [HttpPost, ValidateModel]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Create([FromBody] CreateAppUserDto createUserDto)
        {
            IdentityResult result = await _userService.AddAsync(
                new AppUser
                {
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

        /// <summary>
        /// Updates the details of an existing user, including their roles.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <param name="updateUserDto">The updated user details, including username, email, and roles.</param>
        /// <returns>An OK response if the user is updated successfully, or an error if the update fails.</returns>
        /// <response code="200">User updated successfully.</response>
        /// <response code="400">User update failed.</response>
        [HttpPut("{id}"), ValidateModel]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
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

        /// <summary>
        /// Deletes a user from the system by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>An OK response if the user is deleted successfully, or an error if the deletion fails.</returns>
        /// <response code="200">User deleted successfully.</response>
        /// <response code="400">User deletion failed.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Delete(string id)
        {
            IdentityResult result = await _userService.DeleteAsync(id);

            if (!result.Succeeded)
                return HandleError(ErrorMessages.DeleteFailure, HttpMessages.UserDeleteFailure);

            return Ok(HttpMessages.UserDeleteSuccess);
        }
    }
}

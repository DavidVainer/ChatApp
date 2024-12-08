using ChatApp.Application.Models.Dto;
using ChatApp.Application.Services.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    /// <summary>
    /// Handles user operation requests.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userManager.GetAllUsers();
            return Ok(users);
        }

        /// <summary>
        /// Retrieves a specific user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The fetched user.</returns>
        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var user = _userManager.GetUserById(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(user);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="dto">The user creation details.</param>
        /// <returns>The new created user.</returns>
        [HttpPost]
        public IActionResult AddUser([FromBody] CreateUserDto dto)
        {
            var user = _userManager.CreateUser(dto);
            return Ok(user);
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            _userManager.DeleteUser(id);

            return Ok(new { message = "User deleted successfully." });
        }
    }
}

using ChatApp.Application.Models;
using ChatApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    /// <summary>
    /// Handles user authentication requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginManager _loginManager;

        /// <summary>
        /// Initiates a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="loginManager">Login manager service.</param>
        public AuthController(ILoginManager loginManager)
        {
            _loginManager = loginManager ?? throw new ArgumentNullException(nameof(loginManager));
        }

        /// <summary>
        /// Logs in a user with the provided credentials.
        /// </summary>
        /// <param name="dto">Request data object.</param>
        /// <returns>The authenticated user details.</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserDto dto)
        {
            try
            {
                var user = _loginManager.Login(dto);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Log outs a user.
        /// </summary>
        /// <param name="userId">User unique identfier.</param>
        [HttpPost("logout")]
        public IActionResult Logout(Guid userId)
        {
            try
            {
                _loginManager.Logout(userId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

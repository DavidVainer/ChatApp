using ChatApp.Application.Models.Dto;
using ChatApp.Application.Services.Auth;
using ChatApp.Application.Services.Managers;
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
        private readonly ITokenService _tokenService;

        public AuthController(ILoginManager loginManager, ITokenService tokenService)
        {
            _loginManager = loginManager ?? throw new ArgumentNullException(nameof(loginManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
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

                var token = _tokenService.GenerateToken(user);

                return Ok(new
                {
                    UserId = user.Id,
                    Token = token
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}

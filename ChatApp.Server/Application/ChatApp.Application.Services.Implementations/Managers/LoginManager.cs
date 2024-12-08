using ChatApp.Application.Models;
using ChatApp.Domain.Models;

namespace ChatApp.Application.Services.Implementations
{
    /// <summary>
    /// Encapsulates login related operations.
    /// </summary>
    public class LoginManager : ILoginManager
    {
        private readonly IEntityRepository<User> _userRepository;
        private readonly IPasswordService _passwordService;

        public LoginManager(IEntityRepository<User> userRepository, IPasswordService passwordService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
        }

        /// <summary>
        /// Authenticates a user with the provided credentials and logs them in.
        /// </summary>
        /// <param name="dto">Dto containing the user's login credentials.</param>
        /// <returns>The logged in user.</returns>
        public IUser Login(LoginUserDto dto)
        {
            var userFilter = new User { Email = dto.Email };
            var user = _userRepository.GetByFilter(userFilter).FirstOrDefault();

            if (user == null || !_passwordService.VerifyPassword(dto.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            return user;
        }
    }
}

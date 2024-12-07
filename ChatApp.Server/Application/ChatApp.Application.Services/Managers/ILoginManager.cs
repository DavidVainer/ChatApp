using ChatApp.Application.Models;
using ChatApp.Domain.Models;

namespace ChatApp.Application.Services
{
    /// <summary>
    /// Encapsulates login related operations.
    /// </summary>
    public interface ILoginManager
    {
        /// <summary>
        /// Authenticates a user with the provided credentials and logs them in.
        /// </summary>
        /// <param name="dto">Dto containing the user's login credentials.</param>
        /// <returns>The logged in user.</returns>
        IUser Login(LoginUserDto dto);
    }
}

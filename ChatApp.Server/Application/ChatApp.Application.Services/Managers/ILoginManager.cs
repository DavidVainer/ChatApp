using ChatApp.Application.Models.Dto;
using ChatApp.Domain.Models.Entities;

namespace ChatApp.Application.Services.Managers
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

using ChatApp.Application.Models;
using ChatApp.Domain.Models;

namespace ChatApp.Application.Services
{
    /// <summary>
    /// Encapsulates user related operations.
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Creates a new user with the specified details.
        /// </summary>
        /// <param name="dto">Dto containing the user's details for creation.</param>
        /// <returns>The new created user.</returns>
        IUser CreateUser(CreateUserDto dto);

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to retrieve.</param>
        /// <returns>The found user.</returns>
        IUser? GetUserById(Guid userId);
    }
}

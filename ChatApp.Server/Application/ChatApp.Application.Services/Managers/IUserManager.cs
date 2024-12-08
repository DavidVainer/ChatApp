using ChatApp.Application.Models.Dto;
using ChatApp.Domain.Models.Entities;

namespace ChatApp.Application.Services.Managers
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
        /// Retrieves all users.
        /// </summary>
        /// <returns>Collection of all users.</returns>
        IEnumerable<IUser> GetAllUsers();

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to retrieve.</param>
        /// <returns>The found user.</returns>
        IUser? GetUserById(Guid userId);

        /// <summary>
        /// Deletes a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to delete.</param>
        void DeleteUser(Guid userId);
    }
}

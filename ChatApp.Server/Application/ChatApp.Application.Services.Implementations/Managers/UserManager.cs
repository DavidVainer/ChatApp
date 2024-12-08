using ChatApp.Application.Models.Dto;
using ChatApp.Application.Services.Auth;
using ChatApp.Application.Services.Managers;
using ChatApp.Application.Services.Repositories;
using ChatApp.Domain.Models.Entities;

namespace ChatApp.Application.Services.Implementations.Managers
{
    /// <summary>
    /// Encapsulates user related operations.
    /// </summary>
    public class UserManager : IUserManager
    {
        private readonly IEntityRepository<User> _userRepository;
        private readonly IEntityIdGenerator _entityIdGenerator;
        private readonly IPasswordService _passwordService;

        public UserManager(IEntityRepository<User> userRepository, IEntityIdGenerator entityIdGenerator, IPasswordService passwordService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _entityIdGenerator = entityIdGenerator ?? throw new ArgumentNullException(nameof(entityIdGenerator));
            _passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
        }

        /// <summary>
        /// Creates a new user with the specified details.
        /// </summary>
        /// <param name="dto">Dto containing the user's details for creation.</param>
        /// <returns>The new created user.</returns>
        public IUser CreateUser(CreateUserDto dto)
        {
            var hashedPassword = _passwordService.HashPassword(dto.Password);
            var user = new User
            {
                Id = _entityIdGenerator.Generate(),
                Email = dto.Email,
                Password = hashedPassword,
                DisplayName = dto.DisplayName,
                CreatedAt = DateTime.Now,
                Deleted = false,
            };

            _userRepository.Insert(user);

            return user;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>Collection of all users.</returns>
        public IEnumerable<IUser> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            return users;
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to retrieve.</param>
        /// <returns>The found user.</returns>
        public IUser? GetUserById(Guid userId)
        {
            var user = _userRepository.GetById(userId);

            return user;
        }


        /// <summary>
        /// Deletes a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to delete.</param>
        public void DeleteUser(Guid userId)
        {
            var user = new User { Id = userId };

            _userRepository.Delete(user);
        }
    }
}

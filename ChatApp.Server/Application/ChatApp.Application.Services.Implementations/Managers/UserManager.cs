﻿using ChatApp.Application.Models;
using ChatApp.Domain.Models;

namespace ChatApp.Application.Services.Implementations
{
    /// <summary>
    /// Encapsulates user related operations.
    /// </summary>
    public class UserManager : IUserManager
    {
        private readonly IEntityRepository<User> _userRepository;
        private readonly IEntityIdGenerator _entityIdGenerator;
        private readonly IPasswordService _passwordService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="userRepository">User repository service.</param>
        /// <param name="entityIdGenerator">Entity id generator service.</param>
        /// <param name="passwordService">Password service.</param>
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
                IsOnline = false,
                CreatedAt = DateTime.UtcNow
            };

            _userRepository.Insert(user);

            return user;
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to retrieve.</param>
        /// <returns>The found user.</returns>
        public IUser? GetUserById(Guid userId)
        {
            var userFilter = new User { Id = userId };
            var user = _userRepository.GetByFilter(userFilter).FirstOrDefault();

            return user;
        }
    }
}
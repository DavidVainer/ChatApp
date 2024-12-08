using ChatApp.Application.Services.Auth;

namespace ChatApp.Infrastructure.Implementations.Auth
{
    /// <summary>
    /// Provides methods for hashing and verifying passwords using Bcrypt.
    /// </summary>
    public class BcryptPasswordService : IPasswordService
    {
        /// <summary>
        /// Hashes a plain password using Bcrypt.
        /// </summary>
        /// <param name="plainPassword">Plain text password.</param>
        /// <returns>Hashed password.</returns>
        public string HashPassword(string plainPassword)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);
            return hashedPassword;
        }

        /// <summary>
        /// Verifies a plain password against a hashed password using Bcrypt.
        /// </summary>
        /// <param name="plainPassword">Plain text password.</param>
        /// <param name="hashedPassword">Hashed password.</param>
        /// <returns>Whether the submitted passwored is verified.</returns>
        public bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            var isVerified = BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
            return isVerified;
        }
    }
}

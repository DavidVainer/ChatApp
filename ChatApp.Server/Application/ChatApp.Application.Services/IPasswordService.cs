namespace ChatApp.Application.Services
{
    /// <summary>
    /// Provides methods for hashing and verifying passwords.
    /// </summary>
    public interface IPasswordService
    {
        /// <summary>
        /// Hashes a plain password.
        /// </summary>
        /// <param name="plainPassword">Plain text password.</param>
        /// <returns>Hashed password.</returns>
        string HashPassword(string plainPassword);

        /// <summary>
        /// Verifies a plain password against a hashed password.
        /// </summary>
        /// <param name="plainPassword">Plain text password.</param>
        /// <param name="hashedPassword">Hashed password.</param>
        /// <returns>Whether the submitted passwored is verified.</returns>
        bool VerifyPassword(string plainPassword, string hashedPassword);
    }
}

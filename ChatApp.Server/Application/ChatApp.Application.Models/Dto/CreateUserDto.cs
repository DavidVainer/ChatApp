namespace ChatApp.Application.Models
{
    /// <summary>
    /// Data transfer object for creating new a user.
    /// </summary>
    public class CreateUserDto
    {
        /// <summary>
        /// The user's email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The user's plaintext password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The display name of the user.
        /// </summary>
        public string DisplayName { get; set; }
    }
}

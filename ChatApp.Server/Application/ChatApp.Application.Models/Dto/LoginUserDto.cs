namespace ChatApp.Application.Models
{
    /// <summary>
    /// Data transfer object for user login.
    /// </summary>
    public class LoginUserDto
    {
        /// <summary>
        /// The user's submitted email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The user's submitted plain password.
        /// </summary>
        public string Password { get; set; }
    }
}

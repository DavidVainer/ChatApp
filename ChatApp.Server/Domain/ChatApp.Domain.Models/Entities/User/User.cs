namespace ChatApp.Domain.Models
{
    /// <summary>
    /// Represents a user in the chat.
    /// </summary>
    public class User : IUser
    {
        /// <summary>
        /// The user's unique identifier.
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// The email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The hashed password of the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The user's name displayed in the chat.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The date when the user was created.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Indicates whether the user has been deleted.
        /// </summary>
        public bool? Deleted { get; set; }
    }
}

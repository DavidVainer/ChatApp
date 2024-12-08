namespace ChatApp.Domain.Models
{
    /// <summary>
    /// Represents a user in the chat.
    /// </summary>
    public interface IUser : IUserProfile
    {
        /// <summary>
        /// The email address of the user.
        /// </summary>
        string Email { get; }

        /// <summary>
        /// The hashed password of the user.
        /// </summary>
        string Password { get; }

        /// <summary>
        /// The date when the user was created.
        /// </summary>
        DateTime? CreatedAt { get; }

        /// <summary>
        /// Indicates whether the user has been deleted.
        /// </summary>
        bool? Deleted { get; }
    }
}

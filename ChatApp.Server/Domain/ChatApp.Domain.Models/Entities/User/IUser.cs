namespace ChatApp.Domain.Models
{
    /// <summary>
    /// Represents a user in the chat.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// The user's unique identifier.
        /// </summary>
        Guid? Id { get; }

        /// <summary>
        /// The email address of the user.
        /// </summary>
        string Email { get; }

        /// <summary>
        /// The hashed password of the user.
        /// </summary>
        string Password { get; }

        /// <summary>
        /// The user's name displayed in the chat.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// The date when the user was created.
        /// </summary>
        DateTime? CreatedAt { get; }
    }
}

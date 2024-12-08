namespace ChatApp.Domain.Models
{
    /// <summary>
    /// Defines user properties relevant to the chat system.
    /// </summary>
    public interface IUserProfile
    {
        /// <summary>
        /// The user's unique identifier.
        /// </summary>
        Guid? Id { get; }

        /// <summary>
        /// The user's name displayed in the chat.
        /// </summary>
        string DisplayName { get; }
    }
}

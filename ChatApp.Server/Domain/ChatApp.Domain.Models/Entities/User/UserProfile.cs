
namespace ChatApp.Domain.Models
{
    /// <summary>
    /// Defines user properties relevant to the chat system.
    /// </summary>
    public class UserProfile : IUserProfile
    {
        /// <summary>
        /// The user's unique identifier.
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// The user's name displayed in the chat.
        /// </summary>
        public string DisplayName { get; set; }
    }
}

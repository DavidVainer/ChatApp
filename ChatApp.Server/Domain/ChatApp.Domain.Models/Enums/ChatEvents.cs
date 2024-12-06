namespace ChatApp.Domain.Models
{
    /// <summary>
    /// Represents the chat events that can be triggered.
    /// </summary>
    public enum ChatEvents
    {
        /// <summary>
        /// When a user joins a chat room.
        /// </summary>
        UserJoined,

        /// <summary>
        /// When a user leaves a chat room.
        /// </summary>
        UserLeft,

        /// <summary>
        /// When a message is received in a chat room.
        /// </summary>
        ReceiveMessage,

        /// <summary>
        /// When a message is seen by a user.
        /// </summary>
        MessageSeen,
    }
}

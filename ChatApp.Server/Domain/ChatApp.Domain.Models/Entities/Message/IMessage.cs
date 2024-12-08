namespace ChatApp.Domain.Models
{
    /// <summary>
    /// Represents a message sent within a chat room.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// The message's unique identifier.
        /// </summary>
        Guid? Id { get; }

        /// <summary>
        /// The identifier of the chat room where the message was sent.
        /// </summary>
        Guid? RoomId { get; }

        /// <summary>
        /// The identifier of the user who sent the message.
        /// </summary>
        Guid? SenderId { get; }

        /// <summary>
        /// The content of the message.
        /// </summary>
        string Content { get; }

        /// <summary>
        /// The time the message was sent.
        /// </summary>
        DateTime? SentAt { get; }

        /// <summary>
        /// List of users identifiers who have seen this message.
        /// </summary>
        IList<Guid> SeenBy { get; }
    }
}

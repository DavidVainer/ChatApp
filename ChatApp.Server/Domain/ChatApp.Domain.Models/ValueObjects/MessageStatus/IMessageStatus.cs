namespace ChatApp.Domain.Models
{
    /// <summary>
    /// Represents indication whether a user have seen a message or not.
    /// </summary>
    public interface IMessageStatus
    {
        /// <summary>
        /// The identifier of the message.
        /// </summary>
        Guid? MessageId { get; }

        /// <summary>
        /// The identifier of the associated room.
        /// </summary>
        Guid? RoomId { get; }

        /// <summary>
        /// The identifier of the associated user.
        /// </summary>
        Guid? UserId { get; }

        /// <summary>
        /// The time when the user seen the message.
        /// </summary>
        DateTime? SeenAt { get; }
    }
}

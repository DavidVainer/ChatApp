namespace ChatApp.Domain.Models.ValueObjects
{
    /// <summary>
    /// Represents a participant in a chat room, 
    /// </summary>
    public interface IRoomParticipant
    {
        /// <summary>
        /// The identifier of the room the user is participating in.
        /// </summary>
        Guid? RoomId { get; }

        /// <summary>
        /// The identifier of the participating user.
        /// </summary>
        Guid? UserId { get; }

        /// <summary>
        /// The date when the user joined the room.
        /// </summary>
        DateTime? JoinedAt { get; }
    }
}

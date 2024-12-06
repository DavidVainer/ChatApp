namespace ChatApp.Domain.Models
{
    /// <summary>
    /// Represents a participant in a chat room, 
    /// </summary>
    public class RoomParticipant : IRoomParticipant
    {
        /// <summary>
        /// The identifier of the room the user is participating in.
        /// </summary>
        public Guid? RoomId { get; set; }

        /// <summary>
        /// The identifier of the participating user.
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// The date when the user joined the room.
        /// </summary>
        public DateTime? JoinedAt { get; set; }
    }
}

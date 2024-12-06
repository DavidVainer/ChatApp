namespace ChatApp.Application.Models
{
    /// <summary>
    /// Data Transfer Object for adding a new participant to a room.
    /// </summary>
    public class AddParticipantDto
    {
        /// <summary>
        /// The unique identifier of the room.
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        /// The unique identifier of the user.
        /// </summary>
        public Guid UserId { get; set; }
    }
}

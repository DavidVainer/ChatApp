namespace ChatApp.Domain.Models
{
    /// <summary>
    /// Aggregate object containing full room participant details.
    /// </summary>
    public class RoomParticipantDetails : IRoomParticipantDetails
    {
        /// <summary>
        /// The identifier of the participating user.
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Participating user's display name.
        /// </summary>
        public string UserDisplayName { get; set; }

        /// <summary>
        /// The date when the user joined the room.
        /// </summary>
        public DateTime? JoinedAt { get; set; }
    }
}

namespace ChatApp.Domain.Models
{
    /// <summary>
    /// Aggregate object containing full room details.
    /// </summary>
    public class RoomDetails : IRoomDetails
    {
        /// <summary>
        /// Room's unique identifier.
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        /// Room's name.
        /// </summary>
        public string RoomName { get; set; }

        /// <summary>
        /// Collection of messages sent in the room.
        /// </summary>
        public IEnumerable<IMessage> Messages { get; set; }

        /// <summary>
        /// Collection of participants in the room.
        /// </summary>
        public IEnumerable<IRoomParticipantDetails> Participants { get; set; }
    }
}

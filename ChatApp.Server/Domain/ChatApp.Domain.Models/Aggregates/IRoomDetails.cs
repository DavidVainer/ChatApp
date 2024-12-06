namespace ChatApp.Domain.Models
{
    /// <summary>
    /// Aggregate object containing full room details.
    /// </summary>
    public interface IRoomDetails
    {
        /// <summary>
        /// Room's unique identifier.
        /// </summary>
        Guid RoomId { get; set; }

        /// <summary>
        /// Room's name.
        /// </summary>
        string RoomName { get; set; }

        /// <summary>
        /// Collection of messages sent in the room.
        /// </summary>
        IEnumerable<IMessage> Messages { get; set; }

        /// <summary>
        /// Collection of participants in the room.
        /// </summary>
        IEnumerable<IRoomParticipantDetails> Participants { get; set; }
    }
}

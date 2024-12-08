using ChatApp.Domain.Models.Entities;

namespace ChatApp.Domain.Models.Aggregates
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
        /// Collection of users who have written messages in the room.
        /// </summary>
        IEnumerable<IUserProfile> MessageAuthors { get; set; }

        /// <summary>
        /// Collection of participants in the room.
        /// </summary>
        IEnumerable<IRoomParticipantDetails> Participants { get; set; }
    }
}

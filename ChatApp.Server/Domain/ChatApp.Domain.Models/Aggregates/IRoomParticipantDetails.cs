namespace ChatApp.Domain.Models
{
    /// <summary>
    /// Aggregate object containing full room participant details.
    /// </summary>
    public interface IRoomParticipantDetails
    {
        /// <summary>
        /// The identifier of the participating user.
        /// </summary>
        Guid? UserId { get; }

        /// <summary>
        /// Participating user's display name.
        /// </summary>
        string UserDisplayName { get; }

        /// <summary>
        /// The date when the user joined the room.
        /// </summary>
        DateTime? JoinedAt { get; }
    }
}

namespace ChatApp.Domain.Models
{
    /// <summary>
    /// Represents a chat room.
    /// </summary>
    public interface IRoom
    {
        /// <summary>
        /// The room's unique identifier.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// The room's given name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The time when the room was created.
        /// </summary>
        DateTime? CreatedAt { get; }

        /// <summary>
        /// Indicates whether the room has been deleted.
        /// </summary>
        bool? Deleted { get; }
    }
}

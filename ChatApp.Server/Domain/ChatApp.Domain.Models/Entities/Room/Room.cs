namespace ChatApp.Domain.Models.Entities
{
    /// <summary>
    /// Represents a chat room.
    /// </summary>
    public class Room : IRoom
    {
        /// <summary>
        /// The room's unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The room's given name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The time when the room was created.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Indicates whether the room has been deleted.
        /// </summary>
        public bool? Deleted { get; set; }
    }
}

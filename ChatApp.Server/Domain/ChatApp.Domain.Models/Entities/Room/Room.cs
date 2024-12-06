namespace ChatApp.Domain.Models
{
    /// <summary>
    /// Represents a chat room.
    /// </summary>
    public class Room: IRoom
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
        /// The type of the room.
        /// </summary>
        public RoomType RoomType { get; set; }

        /// <summary>
        /// The time when the room was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}

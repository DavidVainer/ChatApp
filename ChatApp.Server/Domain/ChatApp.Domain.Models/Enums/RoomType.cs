namespace ChatApp.Domain.Models
{
    /// <summary>
    /// Represents the type of a chat room.
    /// </summary>
    public enum RoomType
    {
        /// <summary>
        /// A private chat between two participants.
        /// </summary>
        Private = 0,

        /// <summary>
        /// A group chat involving multiple participants.
        /// </summary>
        Group = 1,
    }
}
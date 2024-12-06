namespace ChatApp.Application.Models
{
    /// <summary>
    /// Data transfer object for sending a new message.
    /// </summary>
    public class SendMessageDto
    {
        /// <summary>
        /// The unique identifier of the room.
        /// </summary>
        public Guid RoomId { get; }

        /// <summary>
        /// The unique identifier of the user that sent a message.
        /// </summary>
        public Guid SenderId { get; }

        /// <summary>
        /// The content of the message.
        /// </summary>
        public string Content { get; }
    }
}

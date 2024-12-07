﻿namespace ChatApp.Domain.Models.Entities
{
    /// <summary>
    /// Represents a message sent within a chat room.
    /// </summary>
    public class Message : IMessage
    {
        /// <summary>
        /// The message's unique identifier.
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// The identifier of the chat room where the message was sent.
        /// </summary>
        public Guid? RoomId { get; set; }

        /// <summary>
        /// The identifier of the user who sent the message.
        /// </summary>
        public Guid? SenderId { get; set; }

        /// <summary>
        /// The content of the message.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The time the message was sent.
        /// </summary>
        public DateTime? SentAt { get; set; }

        /// <summary>
        /// List of users identifiers who have seen this message.
        /// </summary>
        public IList<Guid> SeenBy { get; set; }

        /// <summary>
        /// Indicates whether the message has been deleted.
        /// </summary>
        public bool? Deleted { get; set; }
    }
}

namespace ChatApp.Domain.Models
{
    /// <summary>
    /// Represents indication whether a user have seen a message or not.
    /// </summary>
    public class MessageStatus : IMessageStatus
    {
        /// <summary>
        /// The identifier of the message.
        /// </summary>
        public Guid? MessageId { get; set; }

        /// <summary>
        /// The identifier of the associated user.
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// The time when the user seen the message.
        /// </summary>
        public DateTime? SeenAt { get; set; }
    }
}

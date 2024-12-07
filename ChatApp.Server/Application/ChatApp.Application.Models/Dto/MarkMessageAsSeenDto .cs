namespace ChatApp.Application.Models
{
    /// <summary>
    /// Data transfer object for marking a message status as seen.
    /// </summary>
    public class MarkMessageAsSeenDto
    {
        /// <summary>
        /// The unique identifier of the mssage.
        /// </summary>
        public Guid MessageId { get; set; }

        /// <summary>
        /// The unique identifier of the user who seen the message.
        /// </summary>
        public Guid UserId { get; set; }
    }
}

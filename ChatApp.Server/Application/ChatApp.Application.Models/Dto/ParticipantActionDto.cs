namespace ChatApp.Application.Models.Dto
{
    /// <summary>
    /// Data Transfer Object for room participant actions.
    /// </summary>
    public class ParticipantActionDto
    {
        /// <summary>
        /// The unique identifier of the room.
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        /// The unique identifier of the user.
        /// </summary>
        public Guid UserId { get; set; }
    }
}

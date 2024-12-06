using ChatApp.Domain.Models;

namespace ChatApp.Application.Models
{
    /// <summary>
    /// Data transfer object for creating a new room.
    /// </summary>
    public class CreateRoomDto
    {
        /// <summary>
        /// The given room name.
        /// </summary>
        public string Name { get; set; }
    }
}

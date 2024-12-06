using ChatApp.Application.Models;
using ChatApp.Domain.Models;

namespace ChatApp.Application.Services
{
    /// <summary>
    /// Encapsulates room related operations.
    /// </summary>
    public interface IRoomManager
    {
        /// <summary>
        /// Creates a new room with the specified details.
        /// </summary>
        /// <param name="dto">Dto containing the room creation details.</param>
        /// <returns>The new created room.</returns>
        IRoom CreateRoom(CreateRoomDto dto);

        /// <summary>
        /// Adds a participant to the specified room.
        /// </summary>
        /// <param name="dto">Dto containing participant details.</param>
        void AddParticipant(ParticipantActionDto dto);

        /// <summary>
        /// Removes a participant from the specified room.
        /// </summary>
        /// <param name="dto">Dto containing participant details and the room ID.</param>
        void RemoveParticipant(ParticipantActionDto dto);

        /// <summary>
        /// Deletes the specified room.
        /// </summary>
        /// <param name="roomId">The unique identifier of the room to be deleted.</param>
        void DeleteRoom(Guid roomId);

        /// <summary>
        /// Retrieves all rooms that a specific user participates in.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A collection of rooms associated with the user.</returns>
        IEnumerable<IRoom> GetUserRooms(Guid userId);
    }
}

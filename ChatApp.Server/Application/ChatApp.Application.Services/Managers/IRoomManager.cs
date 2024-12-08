using ChatApp.Application.Models.Dto;
using ChatApp.Domain.Models.Aggregates;
using ChatApp.Domain.Models.Entities;

namespace ChatApp.Application.Services.Managers
{
    /// <summary>
    /// Encapsulates room related operations.
    /// </summary>
    public interface IRoomManager
    {
        /// <summary>
        /// Retrieves all rooms.
        /// </summary>
        /// <returns>Collection of all existing rooms.</returns>
        IEnumerable<IRoom> GetAllRooms();

        /// <summary>
        /// Retrieves all active rooms.
        /// </summary>
        /// <returns>Collection of existing active rooms.</returns>
        IEnumerable<IRoom> GetActiveRooms();

        /// <summary>
        /// Retrieves the details of the specified room.
        /// </summary>
        /// <param name="roomId">Room unique identifier.</param>
        /// <returns>Aggregated object contains room details.</returns>
        IRoomDetails GetRoomDetails(Guid roomId);

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
    }
}

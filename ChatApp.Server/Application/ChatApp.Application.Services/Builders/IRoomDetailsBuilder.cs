using ChatApp.Domain.Models;

namespace ChatApp.Application.Services
{
    /// <summary>
    /// Provides functionality to build room details aggregated object.
    /// </summary>
    public interface IRoomDetailsBuilder
    {
        /// <summary>
        /// Initializes the builder.
        /// </summary>
        /// <param name="roomId">Unique identifier of the room.</param>
        void Initialize(Guid roomId);

        /// <summary>
        /// Sets the room properties by gathering data about the room.
        /// </summary>
        void SetRoomProperties();

        /// <summary>
        /// Sets the messages by gathering data about messages in the room.
        /// </summary>
        void SetMessages();

        /// <summary>
        /// Sets the participants by gathering data about users in the room.
        /// </summary>
        void SetParticipants();

        /// <summary>
        /// Constructs the room details aggregated object collection.
        /// </summary>
        /// <returns>Room details object.</returns>
        IRoomDetails Build();
    }
}

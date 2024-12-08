using ChatApp.Domain.Models.Aggregates;
using ChatApp.Domain.Models.ValueObjects;

namespace ChatApp.Application.Services.Builders
{
    /// <summary>
    /// Provides functionality to build room participant details aggregated object.
    /// </summary>
    public interface IRoomParticipantDetailsBuilder
    {
        /// <summary>
        /// Initializes the builder.
        /// </summary>
        /// <param name="roomId">Unique identifier of the room.</param>
        void Initialize(Guid roomId);

        /// <summary>
        /// Sets the participant details by gathering data about users in the room.
        /// </summary>
        /// <param name="filter">Room participant filter.</param>
        void SetParticipantDetails(RoomParticipant filter);

        /// <summary>
        /// Constructs the room participant details aggregated object collection.
        /// </summary>
        /// <returns>A collection of participant details.</returns>
        IEnumerable<IRoomParticipantDetails> Build();
    }
}

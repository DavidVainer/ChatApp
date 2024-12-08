using ChatApp.Application.Services.Builders;
using ChatApp.Domain.Models.Aggregates;
using ChatApp.Domain.Models.ValueObjects;

namespace ChatApp.Application.Services.Implementations.Builders
{
    /// <summary>
    /// Provides functionality to build room participant details aggregated object.
    /// </summary>
    public class RoomParticipantDetailsBuilder : IRoomParticipantDetailsBuilder
    {
        private readonly IUnitOfWork _unitOfWork;

        private IList<IRoomParticipantDetails> _participantDetails;

        public RoomParticipantDetailsBuilder(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Initializes the builder.
        /// </summary>
        /// <param name="roomId">Unique identifier of the room.</param>
        public void Initialize(Guid roomId)
        {
            _participantDetails = new List<IRoomParticipantDetails>();
        }

        /// <summary>
        /// Sets the participant details by gathering data about users in the room.
        /// </summary>
        /// <param name="filter">Room participant filter.</param>
        public void SetParticipantDetails(RoomParticipant filter)
        {
            var participants = _unitOfWork.Participants.GetByFilter(filter);

            foreach (var participant in participants)
            {
                var user = _unitOfWork.Users.GetById((Guid)participant.UserId);

                var details = new RoomParticipantDetails
                {
                    UserId = user.Id,
                    UserDisplayName = user.DisplayName,
                    JoinedAt = participant.JoinedAt,
                };

                _participantDetails.Add(details);
            }
        }

        /// <summary>
        /// Constructs the room participant details aggregated object collection.
        /// </summary>
        /// <returns>A collection of participant details.</returns>
        public IEnumerable<IRoomParticipantDetails> Build()
        {
            return _participantDetails;
        }
    }
}

using ChatApp.Domain.Models;

namespace ChatApp.Application.Services.Implementations
{
    /// <summary>
    /// Provides functionality to build room participant details aggregated object.
    /// </summary>
    public class RoomParticipantDetailsBuilder : IRoomParticipantDetailsBuilder
    {
        private readonly IValueObjectRepository<RoomParticipant> _participantRepository;
        private readonly IEntityRepository<User> _userRepository;

        private IList<IRoomParticipantDetails> _participantDetails;
        private Guid _roomId;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomParticipantDetailsBuilder"/> class.
        /// </summary>
        /// <param name="participantRepository">Participant repository service.</param>
        /// <param name="userRepository">User repository service.</param>
        public RoomParticipantDetailsBuilder(IValueObjectRepository<RoomParticipant> participantRepository, IEntityRepository<User> userRepository)
        {
            _participantRepository = participantRepository ?? throw new ArgumentNullException(nameof(participantRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        }

        /// <summary>
        /// Initializes the builder.
        /// </summary>
        /// <param name="roomId">Unique identifier of the room.</param>
        public void Initialize(Guid roomId)
        {
            _participantDetails = new List<IRoomParticipantDetails>();
            _roomId = roomId;
        }

        /// <summary>
        /// Sets the participant details by gathering data about users in the room.
        /// </summary>
        public void SetParticipantDetails()
        {
            var participantsFilter = new RoomParticipant { RoomId = _roomId };
            var participants = _participantRepository.GetByFilter(participantsFilter);

            foreach (var participant in participants)
            {
                var userFilter = new User { Id = participant.UserId };
                var user = _userRepository.GetByFilter(userFilter).FirstOrDefault();

                if (user == null)
                {
                    throw new InvalidOperationException("User not found.");
                }

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

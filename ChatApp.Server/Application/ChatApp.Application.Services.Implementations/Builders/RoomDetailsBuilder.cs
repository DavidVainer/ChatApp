using ChatApp.Domain.Models;

namespace ChatApp.Application.Services.Implementations
{
    /// <summary>
    /// Provides functionality to build room details aggregated object.
    /// </summary>
    public class RoomDetailsBuilder : IRoomDetailsBuilder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoomParticipantDetailsBuilder _roomParticipantsBuilder;

        private IRoomDetails? _roomDetails;

        public RoomDetailsBuilder(IUnitOfWork unitOfWork, IRoomParticipantDetailsBuilder roomParticipantsBuilder)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _roomParticipantsBuilder = roomParticipantsBuilder ?? throw new ArgumentNullException(nameof(roomParticipantsBuilder));
        }

        /// <summary>
        /// Initializes the builder.
        /// </summary>
        /// <param name="roomId">Unique identifier of the room.</param>
        public void Initialize(Guid roomId)
        {
            _roomDetails = new RoomDetails {
                RoomId = roomId,
                Messages = new List<IMessage>(),
                MessageAuthors = new List<IUserProfile>(),
                Participants = new List<IRoomParticipantDetails>(),
            };
        }

        /// <summary>
        /// Sets the room properties by gathering data about the room.
        /// </summary>
        public void SetRoomProperties()
        {
            var room = _unitOfWork.Rooms.GetByFilter(new Room { Id = _roomDetails.RoomId }).FirstOrDefault();

            _roomDetails.RoomName = room.Name;
        }

        /// <summary>
        /// Sets the messages by gathering data about messages in the room.
        /// </summary>
        public void SetMessages()
        {
            var messages = _unitOfWork.Messages.GetByFilter(new Message { RoomId = _roomDetails.RoomId });
            var messageStatuses = _unitOfWork.MessageStatuses.GetByFilter(new MessageStatus { RoomId = _roomDetails.RoomId });

            foreach (var message in messages)
            {
                message.SeenBy = new List<Guid>();

                var seeners = messageStatuses
                    .Where(s => s.MessageId == message.Id)
                    .Select(s => (Guid)s.UserId)
                    .Distinct()
                    .ToList();

                message.SeenBy = seeners;
            }

            _roomDetails.Messages = messages;
        }

        /// <summary>
        /// Sets the message authors by gathering data about users who sent messages in the room.
        /// </summary>
        public void SetMessageAuthors()
        {
            var messageSenders = _roomDetails.Messages.Select(m => m.SenderId).Distinct().ToList();
            var messageAuthors = new List<IUserProfile>();

            foreach (var senderId in messageSenders)
            {
                var user = _unitOfWork.Users.GetByFilter(new User { Id = senderId }).FirstOrDefault();

                messageAuthors.Add(new UserProfile
                {
                    Id = user.Id,
                    DisplayName = user.DisplayName,
                });
            }

            _roomDetails.MessageAuthors = messageAuthors;
        }

        /// <summary>
        /// Sets the participants by gathering data about users in the room.
        /// </summary>
        public void SetParticipants()
        {
            _roomParticipantsBuilder.Initialize(_roomDetails.RoomId);
            _roomParticipantsBuilder.SetParticipantDetails(new RoomParticipant { RoomId = _roomDetails.RoomId });

            var participants = _roomParticipantsBuilder.Build();

            _roomDetails.Participants = participants;
        }

        /// <summary>
        /// Constructs the room details aggregated object collection.
        /// </summary>
        /// <returns>Room details object.</returns>
        public IRoomDetails Build()
        {
            return _roomDetails;
        }
    }
}

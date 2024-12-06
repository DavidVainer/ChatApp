using ChatApp.Domain.Models;

namespace ChatApp.Application.Services.Implementations
{
    /// <summary>
    /// Provides functionality to build room details aggregated object.
    /// </summary>
    public class RoomDetailsBuilder : IRoomDetailsBuilder
    {
        private readonly IEntityRepository<Room> _roomRepository;
        private readonly IEntityRepository<Message> _messageRepository;
        private readonly IRoomParticipantDetailsBuilder _roomParticipantsBuilder;

        private IRoomDetails? _roomDetails;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomDetailsBuilder"/> class.
        /// </summary>
        /// <param name="roomRepository">Room repository service.</param>
        /// <param name="messageRepository">Message repository service.</param>
        /// <param name="roomParticipantsBuilder">Room participants builder service.</param>
        public RoomDetailsBuilder(IEntityRepository<Room> roomRepository, IEntityRepository<Message> messageRepository, IRoomParticipantDetailsBuilder roomParticipantsBuilder)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            _roomParticipantsBuilder = roomParticipantsBuilder ?? throw new ArgumentNullException(nameof(roomParticipantsBuilder));
        }

        /// <summary>
        /// Initializes the builder.
        /// </summary>
        /// <param name="roomId">Unique identifier of the room.</param>
        public void Initialize(Guid roomId)
        {
            _roomDetails = new RoomDetails { RoomId = roomId };
        }

        /// <summary>
        /// Sets the room properties by gathering data about the room.
        /// </summary>
        public void SetRoomProperties()
        {
            var roomFilter = new Room { Id = _roomDetails.RoomId };
            var room = _roomRepository.GetByFilter(roomFilter).FirstOrDefault();

            if (room == null)
            {
                throw new InvalidOperationException("Room not found.");
            }

            _roomDetails.RoomName = room.Name;
        }

        /// <summary>
        /// Sets the messages by gathering data about messages in the room.
        /// </summary>
        public void SetMessages()
        {
            var messagesFilter = new Message { RoomId = _roomDetails.RoomId };
            var messages = _messageRepository.GetByFilter(messagesFilter);

            _roomDetails.Messages = messages;
        }

        /// <summary>
        /// Sets the participants by gathering data about users in the room.
        /// </summary>
        public void SetParticipants()
        {
            _roomParticipantsBuilder.Initialize(_roomDetails.RoomId);
            _roomParticipantsBuilder.SetParticipantDetails();

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

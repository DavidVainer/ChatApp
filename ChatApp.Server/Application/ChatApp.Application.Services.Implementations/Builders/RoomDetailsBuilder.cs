using ChatApp.Domain.Models;

namespace ChatApp.Application.Services.Implementations
{
    /// <summary>
    /// Provides functionality to build room details aggregated object.
    /// </summary>
    public class RoomDetailsBuilder : IRoomDetailsBuilder
    {
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<Message> _messageRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<MessageStatus> _messageStatusRepository;
        private readonly IRoomParticipantDetailsBuilder _roomParticipantsBuilder;

        private IRoomDetails? _roomDetails;

        public RoomDetailsBuilder(
            IRepository<Room> roomRepository,
            IRepository<Message> messageRepository,
            IRepository<User> userRepository,
            IRepository<MessageStatus> messageStatusRepository,
            IRoomParticipantDetailsBuilder roomParticipantsBuilder)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _messageStatusRepository = messageStatusRepository ?? throw new ArgumentNullException(nameof(messageStatusRepository));
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


            foreach (var message in messages)
            {
                var seenBy = _messageStatusRepository.GetByFilter(new MessageStatus { MessageId = (Guid)message.Id }).Select(x => (Guid)x.UserId);

                message.SeenBy = seenBy;
            }

            _roomDetails.Messages = messages;
        }

        /// <summary>
        /// Sets the message authors by gathering data about users who sent messages in the room.
        /// </summary>
        public void SetMessageAuthors()
        {
            var messageAuthorsIds = _roomDetails.Messages.Select(m => m.SenderId).Distinct().ToList();
            var messageAuthors = new List<User>();

            foreach (var authorId in messageAuthorsIds)
            {
                var userFilter = new User { Id = authorId };
                var user = _userRepository.GetByFilter(userFilter).FirstOrDefault();

                if (user != null)
                {
                    messageAuthors.Add(user);
                }
            }

            _roomDetails.MessageAuthors = messageAuthors;
        }

        /// <summary>
        /// Sets the participants by gathering data about users in the room.
        /// </summary>
        public void SetParticipants()
        {
            _roomParticipantsBuilder.Initialize(_roomDetails.RoomId);
            _roomParticipantsBuilder.SetParticipantDetails(new RoomParticipant {  RoomId = _roomDetails.RoomId });

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

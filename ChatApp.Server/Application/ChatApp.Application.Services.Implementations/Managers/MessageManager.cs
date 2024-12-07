using ChatApp.Application.Models;
using ChatApp.Domain.Models;

namespace ChatApp.Application.Services.Implementations
{
    /// <summary>
    /// Encapsulates message related operations.
    /// </summary>
    public class MessageManager : IMessageManager
    {
        private readonly IRepository<Message> _messageRepository;
        private readonly IRepository<MessageStatus> _messageStatusRepository;
        private readonly IEntityIdGenerator _entityIdGenerator;

        public MessageManager(IRepository<Message> messageRepository, IRepository<MessageStatus> messageStatusRepository, IEntityIdGenerator entityIdGenerator)
        {
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            _messageStatusRepository = messageStatusRepository ?? throw new ArgumentNullException(nameof(messageStatusRepository));
            _entityIdGenerator = entityIdGenerator ?? throw new ArgumentNullException(nameof(entityIdGenerator));
        }

        /// <summary>
        /// Sends a new message in a specified room.
        /// </summary>
        /// <param name="dto">Dto containing the message details.</param>
        /// <returns>The sent message.</returns>
        public IMessage SendMessage(SendMessageDto dto)
        {
            var message = new Message
            {
                Id = _entityIdGenerator.Generate(),
                RoomId = dto.RoomId,
                SenderId = dto.SenderId,
                Content = dto.Content,
                SentAt = DateTime.Now,
            };

            _messageRepository.Insert(message);

            return message;
        }

        /// <summary>
        /// Marks a specific message as seen by a user.
        /// </summary>
        /// <param name="dto">Dto containing the message and user details.</param>
        public void MarkMessageAsSeen(MarkMessageAsSeenDto dto)
        {
            var messageStatus = new MessageStatus
            {
                MessageId = dto.MessageId,
                UserId = dto.UserId,
                SeenAt = DateTime.Now,
            };

            _messageStatusRepository.Insert(messageStatus);
        }
    }
}

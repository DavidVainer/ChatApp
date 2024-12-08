using ChatApp.Application.Models.Dto;
using ChatApp.Application.Services.Managers;
using ChatApp.Application.Services.Repositories;
using ChatApp.Domain.Models.Entities;
using ChatApp.Domain.Models.ValueObjects;

namespace ChatApp.Application.Services.Implementations.Managers
{
    /// <summary>
    /// Encapsulates message related operations.
    /// </summary>
    public class MessageManager : IMessageManager
    {
        private readonly IEntityRepository<Message> _messageRepository;
        private readonly IValueObjectRepository<MessageStatus> _messageStatusRepository;
        private readonly IEntityIdGenerator _entityIdGenerator;

        public MessageManager(IEntityRepository<Message> messageRepository, IValueObjectRepository<MessageStatus> messageStatusRepository, IEntityIdGenerator entityIdGenerator)
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
                Deleted = false,
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
                RoomId = dto.RoomId,
                UserId = dto.UserId,
                SeenAt = DateTime.Now,
            };

            _messageStatusRepository.Insert(messageStatus);
        }
    }
}

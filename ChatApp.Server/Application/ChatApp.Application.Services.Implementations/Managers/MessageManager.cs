﻿using ChatApp.Application.Models;
using ChatApp.Domain.Models;

namespace ChatApp.Application.Services.Implementations
{
    /// <summary>
    /// Encapsulates message related operations.
    /// </summary>
    public class MessageManager : IMessageManager
    {
        private readonly IEntityRepository<Message> _messageRepository;
        private readonly IValueObjectRepository<MessageStatus> _messageStatusRepository;
        private readonly IEntityIdGenerator _entityIdGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageManager"/> class.
        /// </summary>
        /// <param name="messageRepository">Message repository.</param>
        /// <param name="messageStatusRepository">Message status repository.</param>
        /// <param name="entityIdGenerator">Entity id generator service.</param>
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
                SentAt = DateTime.UtcNow
            };

            _messageRepository.Insert(message);

            return message;
        }

        /// <summary>
        /// Retrieves all messages from a specified room.
        /// </summary>
        /// <param name="roomId">The unique identifier of the room.</param>
        /// <returns>A collection of messages from the specified room.</returns>
        public IEnumerable<IMessage> GetRoomMessages(Guid roomId)
        {
            var messagesFilter = new Message { RoomId = roomId };
            var messages = _messageRepository.GetByFilter(messagesFilter);

            return messages;
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
                SeenAt = DateTime.UtcNow
            };

            _messageStatusRepository.Insert(messageStatus);
        }
    }
}
using ChatApp.Application.Models;
using ChatApp.Application.Services;
using ChatApp.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Infrastructure.Implementations
{
    /// <summary>
    /// Hub for manging real-time chat communication.
    /// </summary>
    public class ChatHub : Hub
    {
        private readonly IRoomManager _roomManager;
        private readonly IMessageManager _messageManager;
        private readonly IChatNotificationService _chatNotificationService;
        private readonly IRoomParticipantDetailsBuilder _roomParticipantDetailsBuilder;

        public ChatHub(IRoomManager roomManager, IMessageManager messageManager, IChatNotificationService chatNotificationService, IRoomParticipantDetailsBuilder roomParticipantDetailsBuilder)
        {
            _roomManager = roomManager ?? throw new ArgumentNullException(nameof(roomManager));
            _messageManager = messageManager ?? throw new ArgumentNullException(nameof(messageManager));
            _chatNotificationService = chatNotificationService ?? throw new ArgumentNullException(nameof(chatNotificationService));
            _roomParticipantDetailsBuilder = roomParticipantDetailsBuilder ?? throw new ArgumentNullException(nameof(roomParticipantDetailsBuilder));
        }

        /// <summary>
        /// Adds a user to a chat room, both in the database and the SignalR group.
        /// </summary>
        /// <param name="roomId">The unique identifier of the room.</param>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task JoinRoom(string roomId, string userId)
        {
            var participantActionDto = new ParticipantActionDto
            {
                RoomId = Guid.Parse(roomId),
                UserId = Guid.Parse(userId),
            };

            _roomManager.AddParticipant(participantActionDto);

            _roomParticipantDetailsBuilder.Initialize(Guid.Parse(roomId));
            _roomParticipantDetailsBuilder.SetParticipantDetails(new RoomParticipant { UserId = Guid.Parse(userId) });

            var participantDetails = _roomParticipantDetailsBuilder.Build().FirstOrDefault();

            await _chatNotificationService.AddToGroupAsync(roomId, Context.ConnectionId);
            await _chatNotificationService.NotifyUserJoinedAsync(roomId, participantDetails);
        }

        /// <summary>
        /// Removes a user from a chat room, both in the database and the SignalR group.
        /// </summary>
        /// <param name="roomId">The unique identifier of the room.</param>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task LeaveRoom(string roomId, string userId)
        {
            var participantActionDto = new ParticipantActionDto
            {
                RoomId = Guid.Parse(roomId),
                UserId = Guid.Parse(userId),
            };

            _roomManager.RemoveParticipant(participantActionDto);

            await _chatNotificationService.RemoveFromGroupAsync(roomId, Context.ConnectionId);
            await _chatNotificationService.NotifyUserLeftAsync(roomId, userId);
        }

        /// <summary>
        /// Sends a message to a chat room, saving it in the database and notifying all room participants.
        /// </summary>
        /// <param name="roomId">The unique identifier of the room.</param>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="message">The sent message.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SendMessage(string roomId, string userId, string message)
        {
            var sendMessageDto = new SendMessageDto
            {
                RoomId = Guid.Parse(roomId),
                SenderId = Guid.Parse(userId),
                Content = message
            };

            var messageModel = _messageManager.SendMessage(sendMessageDto);

            await _chatNotificationService.NotifyMessageRecievedAsync(roomId, messageModel);
        }

        /// <summary>
        /// Marks a message as seen, updating the database and notifying the participants.
        /// </summary>
        /// <param name="roomId">The unique identifier of the room.</param>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="messageId">The unique identifier of the message.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task MarkMessageAsSeen(string roomId, string userId, string messageId)
        {
            var markMessageAsSeenDto = new MarkMessageAsSeenDto
            {
                MessageId = Guid.Parse(messageId),
                RoomId = Guid.Parse(roomId),
                UserId = Guid.Parse(userId)
            };

            _messageManager.MarkMessageAsSeen(markMessageAsSeenDto);

            await _chatNotificationService.NotifyMessageSeenAsync(roomId, messageId, userId);
        }
    }

}
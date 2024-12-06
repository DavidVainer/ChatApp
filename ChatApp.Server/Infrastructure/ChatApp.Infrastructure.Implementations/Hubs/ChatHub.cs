using ChatApp.Application.Models;
using ChatApp.Application.Services;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatHub"/> class.
        /// </summary>
        /// <param name="roomManager">Room manager service.</param>
        /// <param name="messageManager">Message manager service.</param>
        /// <param name="chatNotificationService">Chat notification service.</param>
        public ChatHub(IRoomManager roomManager, IMessageManager messageManager, IChatNotificationService chatNotificationService)
        {
            _roomManager = roomManager ?? throw new ArgumentNullException(nameof(roomManager));
            _messageManager = messageManager ?? throw new ArgumentNullException(nameof(messageManager));
            _chatNotificationService = chatNotificationService ?? throw new ArgumentNullException(nameof(chatNotificationService));
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

            await _chatNotificationService.AddToGroupAsync(Context.ConnectionId, roomId);
            await _chatNotificationService.NotifyUserJoinedAsync(roomId, Context.ConnectionId);
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

            _roomManager.AddParticipant(participantActionDto);

            await _chatNotificationService.RemoveFromGroupAsync(Context.ConnectionId, roomId);
            await _chatNotificationService.NotifyUserLeftAsync(roomId, Context.ConnectionId);
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
    }

}
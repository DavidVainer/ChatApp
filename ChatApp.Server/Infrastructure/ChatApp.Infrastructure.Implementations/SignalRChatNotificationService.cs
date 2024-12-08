using ChatApp.Application.Services;
using ChatApp.Domain.Models.Aggregates;
using ChatApp.Domain.Models.Entities;
using ChatApp.Domain.Models.Enums;
using ChatApp.Infrastructure.Implementations.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Infrastructure.Implementations
{
    /// <summary>
    /// Represents a service for sending real-time chat notifications using SignalR.
    /// </summary>
    public class SignalRChatNotificationService : IChatNotificationService
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public SignalRChatNotificationService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        /// <summary>
        /// Adds a connection to group.
        /// </summary>
        /// <param name="roomId">Room unique identifier.</param>
        /// <param name="userId">User unique identifier.</param>
        /// <returns>A task representing asynchronus operation.</returns>
        public async Task AddToGroupAsync(string roomId, string userId)
        {
            await _hubContext.Groups.AddToGroupAsync(userId, roomId);
        }

        /// <summary>
        /// Removes a connection from group.
        /// </summary>
        /// <param name="roomId">Room unique identifier.</param>
        /// <param name="userId">User unique identifier.</param>
        /// <returns>A task representing asynchronus operation.</returns>
        public async Task RemoveFromGroupAsync(string roomId, string userId)
        {
            await _hubContext.Groups.RemoveFromGroupAsync(userId, roomId);
        }

        /// <summary>
        /// Notifies that a user has joined the chat.
        /// </summary>
        /// <param name="roomId">Room unique identifier.</param>
        /// <param name="participantDetails">Participant details object.</param>
        /// <returns>A task representing asynchronus operation.</returns>
        public async Task NotifyUserJoinedAsync(string roomId, IRoomParticipantDetails participantDetails)
        {
            await _hubContext.Clients.Group(roomId).SendAsync(ChatEvents.UserJoined.ToString(), participantDetails);
        }

        /// <summary>
        /// Notifies that a user has left the chat.
        /// </summary>
        /// <param name="roomId">Room unique identifier.</param>
        /// <param name="userId">User unique identifier.</param>
        /// <returns>A task representing asynchronus operation.</returns>
        public async Task NotifyUserLeftAsync(string roomId, string userId)
        {
            await _hubContext.Clients.Group(roomId).SendAsync(ChatEvents.UserLeft.ToString(), userId);
        }

        /// <summary>
        /// Notifies about a received message in the chat.
        /// </summary>
        /// <param name="roomId">Room unique identifier.</param>
        /// <param name="message">Sent message model.</param>
        /// <returns>A task representing asynchronus operation.</returns>
        public async Task NotifyMessageRecievedAsync(string roomId, IMessage message)
        {
            await _hubContext.Clients.Group(roomId).SendAsync("ReceiveMessage", message);
        }

        /// <summary>
        /// Marks a message as seen by a user.
        /// </summary>
        /// <param name="roomId">Room unique identifier.</param>
        /// <param name="messageId">Message unique identifier.</param>
        /// <param name="userId">User unique identifier.</param>
        /// <returns>A task representing asynchronus operation.</returns>
        public async Task NotifyMessageSeenAsync(string roomId, string messageId, string userId)
        {
            await _hubContext.Clients.Group(roomId).SendAsync(ChatEvents.MessageSeen.ToString(), messageId, userId);
        }
    }
}

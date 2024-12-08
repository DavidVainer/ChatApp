using ChatApp.Domain.Models.Aggregates;
using ChatApp.Domain.Models.Entities;

namespace ChatApp.Application.Services
{
    /// <summary>
    /// Represents a service for sending real-time chat notifications.
    /// </summary>
    public interface IChatNotificationService
    {
        /// <summary>
        /// Adds a connection to group.
        /// </summary>
        /// <param name="connectionId">User unique identifier.</param>
        /// <param name="roomId">Room unique identifier.</param>
        /// <returns>A task representing asynchronus operation.</returns>
        Task AddToGroupAsync(string roomId, string userId);

        /// <summary>
        /// Removes a connection from group.
        /// </summary>
        /// <param name="connectionId">User unique identifier.</param>
        /// <param name="roomId">Room unique identifier.</param>
        /// <returns>A task representing asynchronus operation.</returns>
        Task RemoveFromGroupAsync(string roomId, string userId);

        /// <summary>
        /// Notifies that a user has joined the chat.
        /// </summary>
        /// <param name="roomId">Room unique identifier.</param>
        /// <param name="participantDetails">Participant details object.</param>
        /// <returns>A task representing asynchronus operation.</returns>
        Task NotifyUserJoinedAsync(string roomId, IRoomParticipantDetails participantDetails);

        /// <summary>
        /// Notifies that a user has left the chat.
        /// </summary>
        /// <param name="roomId">Room unique identifier.</param>
        /// <param name="connectionId">User unique identifier.</param>
        /// <returns>A task representing asynchronus operation.</returns>
        Task NotifyUserLeftAsync(string roomId, string userId);

        /// <summary>
        /// Notifies about a received message in the chat.
        /// </summary>
        /// <param name="roomId">Room unique identifier.</param>
        /// <param name="message">Sent message model.</param>
        /// <returns>A task representing asynchronus operation.</returns>
        Task NotifyMessageRecievedAsync(string roomId, IMessage message);

        /// <summary>
        /// Marks a message as seen by a user.
        /// </summary>
        /// <param name="roomId">Room unique identifier.</param>
        /// <param name="messageId">Message unique identifier.</param>
        /// <param name="userId">User unique identifier.</param>
        /// <returns>A task representing asynchronus operation.</returns>
        Task NotifyMessageSeenAsync(string roomId, string messageId, string userId);
    }
}

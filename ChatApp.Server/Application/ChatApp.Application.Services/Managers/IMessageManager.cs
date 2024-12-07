﻿using ChatApp.Application.Models.Dto;
using ChatApp.Domain.Models.Entities;

namespace ChatApp.Application.Services.Managers
{
    /// <summary>
    /// Encapsulates message related operations.
    /// </summary>
    public interface IMessageManager
    {
        /// <summary>
        /// Sends a new message in a specified room.
        /// </summary>
        /// <param name="dto">Dto containing the message details.</param>
        /// <returns>The sent message.</returns>
        IMessage SendMessage(SendMessageDto dto);

        /// <summary>
        /// Marks a specific message as seen by a user.
        /// </summary>
        /// <param name="dto">Dto containing the message and user details.</param>
        void MarkMessageAsSeen(MarkMessageAsSeenDto dto);
    }
}
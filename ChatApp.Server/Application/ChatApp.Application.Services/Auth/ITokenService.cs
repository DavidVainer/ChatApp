﻿using ChatApp.Domain.Models;

namespace ChatApp.Application.Services
{
    /// <summary>
    /// Defines the contract for a service that generates authentication tokens.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates a token for the specified user.
        /// </summary>
        /// <param name="user">The user to generate authentication token to.</param>
        /// <returns>A token as a string.</returns>
        string GenerateToken(IUser user);
    }
}
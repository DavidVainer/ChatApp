using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Models;
using ChatApp.Infrastructure.Services;
using Dapper;
using System.Data;

namespace ChatApp.Infrastructure.Implementations
{
    /// <summary>
    /// Base repository implementation for managing message status operations using Dapper.
    /// </summary>
    public class MessageStatusRepository : BaseRepository<MessageStatus>
    {
        public MessageStatusRepository(IDbConnection dbConnection, IRepositorySettings settings, IFilterQueryBuilder filterQueryBuilder)
            : base(dbConnection, settings, filterQueryBuilder)
        {
        }

        /// <summary>
        /// Builds insert parameters for the message status item.
        /// </summary>
        /// <param name="item">The message status item.</param>
        /// <returns>The dynamic parameters.</returns>
        protected override DynamicParameters GetInsertParameters(MessageStatus item)
        {
            var parameters = new DynamicParameters();

            parameters.Add("MessageId", item.MessageId);
            parameters.Add("RoomId", item.RoomId);
            parameters.Add("UserId", item.UserId);
            parameters.Add("SeenAt", item.SeenAt);

            return parameters;
        }

        /// <summary>
        /// Builds delete parameters for the message status item.
        /// </summary>
        /// <param name="item">The message status item.</param>
        /// <returns>The dynamic parameters.</returns>
        protected override DynamicParameters GetDeleteParameters(MessageStatus item)
        {
            var parameters = new DynamicParameters();

            parameters.Add("MessageId", item.MessageId);
            parameters.Add("RoomId", item.RoomId);
            parameters.Add("UserId", item.UserId);

            return parameters;
        }
    }
}

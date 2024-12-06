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
    public class MessageStatusRepository : BaseValueObjectRepository<MessageStatus>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageStatusRepository"/> class.
        /// </summary>
        /// <param name="dbConnection">Database connection.</param>
        /// <param name="settings">Repository settings.</param>
        /// <param name="filterQueryBuilder">Get by filter query builder service.</param>
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
            parameters.Add("UserId", item.UserId);

            return parameters;
        }
    }
}

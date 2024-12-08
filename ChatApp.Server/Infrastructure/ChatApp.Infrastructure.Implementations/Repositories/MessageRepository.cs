using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Models;
using ChatApp.Infrastructure.Services;
using Dapper;
using System.Data;

namespace ChatApp.Infrastructure.Implementations
{
    /// <summary>
    /// Base repository implementation for managing message operations using Dapper.
    /// </summary>
    public class MessageRepository : BaseEntityRepository<Message>
    {
        public MessageRepository(IDbConnection dbConnection, IRepositorySettings settings, IFilterQueryBuilder filterQueryBuilder)
            : base(dbConnection, settings, filterQueryBuilder)
        {
        }

        /// <summary>
        /// Builds insert parameters for the message entity.
        /// </summary>
        /// <param name="entity">The message entity.</param>
        /// <returns>The dynamic parameters.</returns>
        protected override DynamicParameters GetInsertParameters(Message entity)
        {
            var parameters = new DynamicParameters();

            parameters.Add("Id", entity.Id);
            parameters.Add("RoomId", entity.RoomId);
            parameters.Add("SenderId", entity.SenderId);
            parameters.Add("Content", entity.Content);
            parameters.Add("SentAt", entity.SentAt);
            parameters.Add("Deleted", entity.Deleted);

            return parameters;
        }

        /// <summary>
        /// Builds delete parameters for the message entity.
        /// </summary>
        /// <param name="entity">The message entity.</param>
        /// <returns>The dynamic parameters.</returns>
        protected override DynamicParameters GetDeleteParameters(Message entity)
        {
            var parameters = new DynamicParameters();

            parameters.Add("Id", entity.Id);

            return parameters;
        }
    }
}

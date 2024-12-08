using ChatApp.Domain.Models.ValueObjects;
using ChatApp.Infrastructure.Models.RepositorySettings;
using ChatApp.Infrastructure.Services;
using Dapper;
using System.Data;

namespace ChatApp.Infrastructure.Implementations.Repositories
{
    /// <summary>
    /// Base repository implementation for managing room participant operations using Dapper.
    /// </summary>
    public class RoomParticipantRepository : BaseValueObjectRepository<RoomParticipant>
    {
        public RoomParticipantRepository(IDbConnection dbConnection, IRepositorySettings settings, IFilterQueryBuilder filterQueryBuilder)
            : base(dbConnection, settings, filterQueryBuilder)
        {
        }

        /// <summary>
        /// Builds insert parameters for the room participant item.
        /// </summary>
        /// <param name="item">The room participant item.</param>
        /// <returns>The dynamic parameters.</returns>
        protected override DynamicParameters GetInsertParameters(RoomParticipant item)
        {
            var parameters = new DynamicParameters();

            parameters.Add("RoomId", item.RoomId);
            parameters.Add("UserId", item.UserId);
            parameters.Add("JoinedAt", item.JoinedAt);

            return parameters;
        }

        /// <summary>
        /// Builds delete parameters for the room participant item.
        /// </summary>
        /// <param name="item">The room participant item.</param>
        /// <returns>The dynamic parameters.</returns>
        protected override DynamicParameters GetDeleteParameters(RoomParticipant item)
        {
            var parameters = new DynamicParameters();

            parameters.Add("RoomId", item.RoomId);
            parameters.Add("UserId", item.UserId);

            return parameters;
        }
    }
}

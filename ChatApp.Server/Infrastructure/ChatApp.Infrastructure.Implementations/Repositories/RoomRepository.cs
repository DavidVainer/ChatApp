﻿using ChatApp.Domain.Models.Entities;
using ChatApp.Infrastructure.Models.RepositorySettings;
using ChatApp.Infrastructure.Services;
using Dapper;
using System.Data;

namespace ChatApp.Infrastructure.Implementations.Repositories
{
    /// <summary>
    /// Base repository implementation for managing room operations using Dapper.
    /// </summary>
    public class RoomRepository : BaseEntityRepository<Room>
    {
        public RoomRepository(IDbConnection dbConnection, IRepositorySettings settings, IFilterQueryBuilder filterQueryBuilder)
            : base(dbConnection, settings, filterQueryBuilder)
        {
        }

        /// <summary>
        /// Builds insert parameters for the room entity.
        /// </summary>
        /// <param name="entity">The room entity.</param>
        /// <returns>The dynamic parameters.</returns>
        protected override DynamicParameters GetInsertParameters(Room entity)
        {
            var parameters = new DynamicParameters();

            parameters.Add("Id", entity.Id);
            parameters.Add("Name", entity.Name);
            parameters.Add("CreatedAt", entity.CreatedAt);
            parameters.Add("Deleted", entity.Deleted);

            return parameters;
        }

        /// <summary>
        /// Builds delete parameters for the room entity.
        /// </summary>
        /// <param name="entity">The room entity.</param>
        /// <returns>The dynamic parameters.</returns>
        protected override DynamicParameters GetDeleteParameters(Room entity)
        {
            var parameters = new DynamicParameters();

            parameters.Add("Id", entity.Id);

            return parameters;
        }
    }
}

﻿using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Models;
using ChatApp.Infrastructure.Services;
using Dapper;
using System.Data;

namespace ChatApp.Infrastructure.Implementations
{
    /// <summary>
    /// Base repository implementation for managing room operations using Dapper.
    /// </summary>
    public class RoomRepository : BaseEntityRepository<Room>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomRepository"/> class.
        /// </summary>
        /// <param name="dbConnection">Database connection.</param>
        /// <param name="settings">Repository settings.</param>
        /// <param name="filterQueryBuilder">Get by filter query builder service.</param>
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
            parameters.Add("RoomType", entity.RoomType);
            parameters.Add("CreatedAt", entity.CreatedAt);

            return parameters;
        }

        /// <summary>
        /// Builds update parameters for the room entity.
        /// </summary>
        /// <param name="entity">The room entity.</param>
        /// <returns>The dynamic parameters.</returns>
        protected override DynamicParameters GetUpdateParameters(Room entity)
        {
            var parameters = new DynamicParameters();

            parameters.Add("Id", entity.Id);
            parameters.Add("Name", entity.Name);

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
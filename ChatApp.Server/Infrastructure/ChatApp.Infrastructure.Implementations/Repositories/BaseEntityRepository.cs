using ChatApp.Application.Services;
using ChatApp.Infrastructure.Models;
using ChatApp.Infrastructure.Services;
using Dapper;
using System.Data;

namespace ChatApp.Infrastructure.Implementations
{
    /// <summary>
    /// Base repository implementation for managing ENTITY operations using Dapper.
    /// </summary>
    /// <typeparam name="T">The ENTITY type.</typeparam>
    public abstract class BaseEntityRepository<T> : BaseValueObjectRepository<T>, IEntityRepository<T>
        where T : class
    {
        public BaseEntityRepository(IDbConnection dbConnection, IRepositorySettings settings, IFilterQueryBuilder filterQueryBuilder)
            : base(dbConnection, settings, filterQueryBuilder)
        {
        }

        /// <summary>
        /// Updates a record within a data source.
        /// </summary>
        /// <param name="item">The record with the up-to-date properties.</param>
        /// <returns>The updated record.</returns>
        public T Update(T item)
        {
            var parameters = GetUpdateParameters(item);

            _dbConnection.Execute(_settings.UpdateQuery, parameters);

            return item;
        }

        /// <summary>
        /// Builds update parameters for the specified record.
        /// </summary>
        /// <param name="entity">The accosiated record.</param>
        /// <returns>The dynamic parameters.</returns>
        protected abstract DynamicParameters GetUpdateParameters(T item);
    }
}

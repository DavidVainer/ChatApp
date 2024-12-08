using ChatApp.Application.Services;
using ChatApp.Infrastructure.Models;
using ChatApp.Infrastructure.Services;
using Dapper;
using System.Data;

namespace ChatApp.Infrastructure.Implementations
{
    /// <summary>
    /// Base repository implementation for managing CRUD operations using Dapper for entities.
    /// </summary>
    /// <typeparam name="T">The value object type.</typeparam>
    public abstract class BaseEntityRepository<T> : BaseValueObjectRepository<T>, IEntityRepository<T>
        where T : class
    {
        public BaseEntityRepository(IDbConnection dbConnection, IRepositorySettings settings, IFilterQueryBuilder filterQueryBuilder)
            : base(dbConnection, settings, filterQueryBuilder)
        {
        }

        /// <summary>
        /// Gets a record by its unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <returns>Matching record.</returns>
        public T GetById(Guid id)
        {
            var record = _dbConnection.QueryFirstOrDefault<T>(_settings.GetByIdQuery, new { Id = id });
            return record;
        }
    }
}

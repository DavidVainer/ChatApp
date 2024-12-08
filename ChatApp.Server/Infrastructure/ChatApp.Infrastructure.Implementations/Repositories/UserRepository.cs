using ChatApp.Domain.Models.Entities;
using ChatApp.Infrastructure.Models.RepositorySettings;
using ChatApp.Infrastructure.Services;
using Dapper;
using System.Data;

namespace ChatApp.Infrastructure.Implementations.Repositories
{
    /// <summary>
    /// Base repository implementation for managing user operations using Dapper.
    /// </summary>
    public class UserRepository : BaseEntityRepository<User>
    {
        public UserRepository(IDbConnection dbConnection, IRepositorySettings settings, IFilterQueryBuilder filterQueryBuilder)
            : base(dbConnection, settings, filterQueryBuilder)
        {
        }

        /// <summary>
        /// Builds insert parameters for the user entity.
        /// </summary>
        /// <param name="entity">The user entity.</param>
        /// <returns>The dynamic parameters.</returns>
        protected override DynamicParameters GetInsertParameters(User entity)
        {
            var parameters = new DynamicParameters();

            parameters.Add("Id", entity.Id);
            parameters.Add("Email", entity.Email);
            parameters.Add("Password", entity.Password);
            parameters.Add("DisplayName", entity.DisplayName);
            parameters.Add("CreatedAt", entity.CreatedAt);
            parameters.Add("Deleted", entity.Deleted);

            return parameters;
        }

        /// <summary>
        /// Builds delete parameters for the user entity.
        /// </summary>
        /// <param name="entity">The user entity.</param>
        /// <returns>The dynamic parameters.</returns>
        protected override DynamicParameters GetDeleteParameters(User entity)
        {
            var parameters = new DynamicParameters();

            parameters.Add("Id", entity.Id);

            return parameters;
        }
    }
}

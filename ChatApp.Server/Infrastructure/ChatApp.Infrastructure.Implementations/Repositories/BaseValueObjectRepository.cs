﻿using ChatApp.Application.Services;
using ChatApp.Infrastructure.Models;
using ChatApp.Infrastructure.Services;
using Dapper;
using System.Data;

namespace ChatApp.Infrastructure.Implementations
{
    /// <summary>
    /// Base repository implementation for managing value object operations using Dapper.
    /// </summary>
    /// <typeparam name="T">The value object type.</typeparam>
    public abstract class BaseValueObjectRepository<T> : IValueObjectRepository<T>
        where T : class
    {
        protected readonly IDbConnection _dbConnection;
        protected readonly IRepositorySettings _settings;
        protected readonly IFilterQueryBuilder _filterQueryBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseValueObjectRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbConnection">Database connection.</param>
        /// <param name="settings">Repository settings.</param>
        /// <param name="filterQueryBuilder">Get by filter query builder service.</param>
        public BaseValueObjectRepository(IDbConnection dbConnection, IRepositorySettings settings, IFilterQueryBuilder filterQueryBuilder)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _filterQueryBuilder = filterQueryBuilder ?? throw new ArgumentNullException(nameof(filterQueryBuilder));
        }

        /// <summary>
        /// Gets all records from a data source.
        /// </summary>
        /// <returns>Collection of all existing records.</returns>
        public IEnumerable<T> GetAll()
        {
            var records = _dbConnection.Query<T>(_settings.GetAllQuery);
            return records;
        }

        /// <summary>
        /// Gets records matching the non-null properties of the filter object.
        /// </summary>
        /// <param name="item">An object with properties to filter by.</param>
        /// <returns>Collection of matching records.</returns>
        public IEnumerable<T> GetByFilter(T item)
        {
            var query = _filterQueryBuilder.Build(item, _settings.TableName);
            var parameters = BuildDynamicParameters(item);
            var entities = _dbConnection.Query<T>(query, parameters);

            return entities;
        }

        /// <summary>
        /// Inserts a record into a data source.
        /// </summary>
        /// <param name="item">The record to be added.</param>
        /// <returns>The inserted record.</returns>
        public T Insert(T item)
        {
            var parameters = GetInsertParameters(item);

            _dbConnection.Execute(_settings.InsertQuery, parameters);

            return item;
        }

        /// <summary>
        /// Deletes a record by its unique identifier.
        /// </summary>
        /// <param name="item">The record to be deleted.</param>
        /// <returns>The deleted record.</returns>
        public T Delete(T item)
        {
            var parameters = GetDeleteParameters(item);

            _dbConnection?.Execute(_settings.DeleteQuery, parameters);

            return item;
        }

        /// <summary>
        /// Builds insert parameters for the specified record.
        /// </summary>
        /// <param name="T">The accosiated item.</param>
        /// <returns>The dynamic parameters.</returns>
        protected abstract DynamicParameters GetInsertParameters(T item);

        /// <summary>
        /// Builds delete parameters for the specified record.
        /// </summary>
        /// <param name="item">The accosiated item.</param>
        /// <returns>The dynamic parameters.</returns>
        protected abstract DynamicParameters GetDeleteParameters(T item);

        /// <summary>
        /// Builds parameters for filtering queries based on the items's non-null properties.
        /// </summary>
        /// <param name="item">The accosiated entity.</param>
        /// <returns>The dynamic parameters.</returns>
        private DynamicParameters BuildDynamicParameters(T item)
        {
            var parameters = new DynamicParameters();

            foreach (var prop in item.GetType().GetProperties())
            {
                var value = prop.GetValue(item);
                if (value != null)
                {
                    parameters.Add($"@{prop.Name}", value);
                }
            }

            return parameters;
        }
    }
}
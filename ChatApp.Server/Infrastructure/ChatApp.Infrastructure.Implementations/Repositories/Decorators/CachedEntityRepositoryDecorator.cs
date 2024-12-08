using ChatApp.Application.Services;
using ChatApp.Common.Services;
using ChatApp.Infrastructure.Services;

namespace ChatApp.Infrastructure.Implementations
{
    /// <summary>
    /// Decorator for caching value object repository methods.
    /// </summary>
    /// <typeparam name="T">Value object type.</typeparam>
    public class CachedEntityRepositoryDecorator<T> : CachedValueObjectRepositoryDecorator<T>, IEntityRepository<T>
        where T : class
    {
        public CachedEntityRepositoryDecorator(
            IEntityRepository<T> entityRepository,
            ICache cache, 
            IGetByFilterCacheKeyGenerator
            getByFilterCacheKeyGenerator,
            string cacheKeyPrefix)
            : base(entityRepository, cache, getByFilterCacheKeyGenerator, cacheKeyPrefix)
        {
        }
        /// <summary>
        /// Gets a record by its unique identifier from a data source or cache.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <returns>Matching record.</returns>
        public T GetById(Guid id)
        {
            var cacheKey = $"{_cacheKeyPrefix}_GetById_{id}";
            var cachedItem = _cache.Get<T>(cacheKey);

            if (cachedItem != null)
            {
                return cachedItem;
            }

            var items = ((IEntityRepository<T>)_repository).GetById(id);

            _cache.Set(cacheKey, items, TimeSpan.FromMinutes(5));

            return items;
        }
    }
}

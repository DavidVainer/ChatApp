using ChatApp.Application.Services;
using ChatApp.Common.Services;
using ChatApp.Infrastructure.Services;

namespace ChatApp.Infrastructure.Implementations
{
    /// <summary>
    /// Decorator for caching value object repository methods.
    /// </summary>
    /// <typeparam name="T">Value object type.</typeparam>
    public class CachedValueObjectRepositoryDecorator<T> : IValueObjectRepository<T>
        where T : class
    {
        protected readonly IValueObjectRepository<T> _repository;
        protected readonly ICache _cache;
        protected readonly string _cacheKeyPrefix;

        private readonly IGetByFilterCacheKeyGenerator _getByFilterCacheKeyGenerator;

        public CachedValueObjectRepositoryDecorator(
            IValueObjectRepository<T> repository,
            ICache cache, IGetByFilterCacheKeyGenerator
            getByFilterCacheKeyGenerator,
            string cacheKeyPrefix)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _getByFilterCacheKeyGenerator = getByFilterCacheKeyGenerator ?? throw new ArgumentNullException(nameof(getByFilterCacheKeyGenerator));
            _cacheKeyPrefix = cacheKeyPrefix ?? throw new ArgumentNullException(nameof(cacheKeyPrefix));
        }

        /// <summary>
        /// Gets all records from a data source or cache.
        /// </summary>
        /// <returns>Collection of all existing records.</returns>
        public IEnumerable<T> GetAll()
        {
            var key = $"{_cacheKeyPrefix}_GetAll";

            var cachedItems = _cache.Get<IEnumerable<T>>(key);

            if (cachedItems != null && cachedItems.Any())
            {
                return cachedItems;
            }

            var items = _repository.GetAll();

            _cache.Set(key, items, TimeSpan.FromMinutes(5));

            return items;
        }

        /// <summary>
        /// Gets records matching the non-null properties of the filter object from a data source or cache.
        /// </summary>
        /// <param name="item">An object with properties to filter by.</param>
        /// <returns>Collection of matching records.</returns>
        public IEnumerable<T> GetByFilter(T item)
        {
            var cacheKey = _getByFilterCacheKeyGenerator.Generate(item, _cacheKeyPrefix);
            var cachedItems = _cache.Get<IEnumerable<T>>(cacheKey);

            if (cachedItems != null && cachedItems.Any())
            {
                return cachedItems;
            }

            var items = _repository.GetByFilter(item);

            _cache.Set(cacheKey, items, TimeSpan.FromMinutes(5));

            return items;
        }

        /// <summary>
        /// Inserts a record into a data source and clears the cache.
        /// </summary>
        /// <param name="item">The record to be added.</param>
        /// <returns>The inserted record.</returns>
        public T Insert(T item)
        {
            var insertedItem = _repository.Insert(item);

            ClearCacheByPrefix();

            return insertedItem;
        }

        /// <summary>
        /// Deletes a record by its unique identifier from a data source and clears the cache.
        /// </summary>
        /// <param name="item">The record to be deleted.</param>
        /// <returns>The deleted record.</returns>
        public T Delete(T item)
        {
            var deletedItem = _repository.Delete(item);

            ClearCacheByPrefix();

            return deletedItem;
        }

        private void ClearCacheByPrefix()
        {
            var cacheKeyPrefix = $"{_cacheKeyPrefix}_GetByFilter_";

            _cache.Remove($"{_cacheKeyPrefix}_GetAll");

            foreach (var key in _cache.GetKeys())
            {
                if (key.StartsWith(cacheKeyPrefix))
                {
                    _cache.Remove(key);
                }
            }
        }
    }
}

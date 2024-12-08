using ChatApp.Common.Services;
using Microsoft.Extensions.Caching.Memory;

namespace ChatApp.Infrastructure.Implementations
{
    /// <summary>
    /// Memory caching operations for managing data in a cache.
    /// </summary>
    public class InMemoryCache : ICache
    {
        private readonly MemoryCache _memoryCache;

        public InMemoryCache(MemoryCache memoryCache)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        /// <summary>
        /// Retrieves a cached item by its key.
        /// </summary>
        /// <typeparam name="T">The type of the cached value.</typeparam>
        /// <param name="key">The unique key associated with the cached item.</param>
        public T Get<T>(string key)
        {
            _memoryCache.TryGetValue(key, out T value);
            return value;
        }

        /// <summary>
        /// Retrieves all the keys in the cache.
        /// </summary>
        /// <returns>Collection of keys.</returns>
        public IEnumerable<string> GetKeys()
        {
            return _memoryCache.Keys.Select(key => key.ToString());
        }

        /// <summary>
        /// Caches an item with the specified key and expiration time.
        /// </summary>
        /// <typeparam name="T">The type of the value to cache.</typeparam>
        /// <param name="key">The unique key associated with the item.</param>
        /// <param name="value">The value to store in the cache.</param>
        public void Set<T>(string key, T value, TimeSpan expiry)
        {
            _memoryCache.Set(key, value, expiry);
        }

        /// <summary>
        /// Removes a cached item by its key.
        /// </summary>
        /// <param name="key">The unique key of the cached item to remove.</param>
        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        /// <summary>
        /// Checks whether a cached item with the specified key exists.
        /// </summary>
        /// <param name="key">The unique key associated with the cached item.</param>
        /// <returns>True if the cached item already exists; otherwise, false.</returns>
        public bool Exists(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }
    }
}

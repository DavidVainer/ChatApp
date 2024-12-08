namespace ChatApp.Common.Services
{
    /// <summary>
    /// Defines caching operations for managing data in a cache.
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Retrieves a cached item by its key.
        /// </summary>
        /// <typeparam name="T">The type of the cached value.</typeparam>
        /// <param name="key">The unique key associated with the cached item.</param>
        T Get<T>(string key);

        /// <summary>
        /// Retrieves all the keys in the cache.
        /// </summary>
        /// <returns>Collection of keys.</returns>
        IEnumerable<string> GetKeys();

        /// <summary>
        /// Caches an item with the specified key and expiration time.
        /// </summary>
        /// <typeparam name="T">The type of the value to cache.</typeparam>
        /// <param name="key">The unique key associated with the item.</param>
        /// <param name="value">The value to store in the cache.</param>
        void Set<T>(string key, T value, TimeSpan expiry);

        /// <summary>
        /// Removes a cached item by its key.
        /// </summary>
        /// <param name="key">The unique key of the cached item to remove.</param>
        void Remove(string key);

        /// <summary>
        /// Checks whether a cached item with the specified key exists.
        /// </summary>
        /// <param name="key">The unique key associated with the cached item.</param>
        /// <returns>True if the cached item already exists; otherwise, false.</returns>
        bool Exists(string key);
    }
}

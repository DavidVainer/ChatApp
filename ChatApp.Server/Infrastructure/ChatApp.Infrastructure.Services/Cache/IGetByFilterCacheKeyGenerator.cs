namespace ChatApp.Infrastructure.Services.Cache
{
    /// <summary>
    /// Generates a unique cache key for filtered records. 
    /// </summary>
    /// <param name="item">Item to filter by.</param>
    /// <returns>Generated cache key.</returns>
    public interface IGetByFilterCacheKeyGenerator
    {
        /// <summary>
        /// Generates a unique cache key for filtered records. 
        /// </summary>
        /// <param name="item">Item to filter by.</param>
        /// <param name="cacheKeyPrefix">Prefix to be added to the generated cache key.</param>
        /// <returns>Generated cache key.</returns>
        string Generate<T>(T item, string cacheKeyPrefix);
    }
}

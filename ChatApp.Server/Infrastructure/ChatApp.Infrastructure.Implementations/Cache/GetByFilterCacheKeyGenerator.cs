using ChatApp.Infrastructure.Services;
using System.Text.Json;

namespace ChatApp.Infrastructure.Implementations
{
    /// <summary>
    /// Generates a unique cache key for filtered records. 
    /// </summary>
    /// <param name="item">Item to filter by.</param>
    /// <returns>Generated cache key.</returns>
    public class GetByFilterCacheKeyGenerator : IGetByFilterCacheKeyGenerator
    {
        /// <summary>
        /// Generates a unique cache key for filtered records. 
        /// </summary>
        /// <param name="item">Item to filter by.</param>
        /// <param name="cacheKeyPrefix">Prefix to be added to the generated cache key.</param>
        /// <returns>Generated cache key.</returns>
        public string Generate<T>(T item, string cacheKeyPrefix)
        {
            string serializedItem = JsonSerializer.Serialize(item);
            var cacheKey = $"{cacheKeyPrefix}_GetByFilter_{serializedItem}";

            return cacheKey;
        }
    }
}
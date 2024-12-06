namespace ChatApp.Common.Services
{
    /// <summary>
    /// Provides functionality to resolve services registered using a name or a key.
    /// </summary>
    public interface IStrategyResolver
    {
        /// <summary>
        /// Resolves a service of the specified type that is registered with a given name.
        /// </summary>
        /// <typeparam name="T">The type of the service to resolve.</typeparam>
        /// <param name="name">The name with which the service is registered.</param>
        /// <returns>The resolved service instance.</returns>
        T ResolveNamed<T>(string name);

        /// <summary>
        /// Resolves a service of the specified type that is registered with a given key.
        /// </summary>
        /// <typeparam name="T">The type of the service to resolve.</typeparam>
        /// <param name="key">The key with which the service is registered.</param>
        /// <returns>The resolved service instance.</returns>
        T ResolveKeyed<T>(object key);
    }
}

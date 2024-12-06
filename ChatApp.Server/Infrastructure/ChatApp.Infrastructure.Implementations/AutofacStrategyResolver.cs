using Autofac;
using ChatApp.Common.Services;

namespace ChatApp.Infrastructure.Implementations
{
    /// <summary>
    /// Provides functionality to resolve services registered in Autofac using a name or a key.
    /// </summary>
    public class AutofacStrategyResolver : IStrategyResolver
    {
        private readonly IComponentContext _container;

        public AutofacStrategyResolver(ILifetimeScope ioCContainer)
        {
            _container = ioCContainer ?? throw new ArgumentNullException(nameof(ioCContainer));
        }

        /// <summary>
        /// Resolves a service of the specified type that is registered with a given name.
        /// </summary>
        /// <typeparam name="T">The type of the service to resolve.</typeparam>
        /// <param name="name">The name with which the service is registered.</param>
        /// <returns>The resolved service instance.</returns>
        public T ResolveNamed<T>(string name)
        {
            var resolvedNamed = _container.ResolveNamed<T>(name);
            return resolvedNamed;
        }

        /// <summary>
        /// Resolves a service of the specified type that is registered with a given key.
        /// </summary>
        /// <typeparam name="T">The type of the service to resolve.</typeparam>
        /// <param name="key">The key with which the service is registered.</param>
        /// <returns>The resolved service instance.</returns>
        public T ResolveKeyed<T>(object key)
        {
            var resolvedKeyed = _container.ResolveKeyed<T>(key);
            return resolvedKeyed;
        }
    }
}

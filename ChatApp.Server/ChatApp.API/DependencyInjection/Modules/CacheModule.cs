using Autofac;
using ChatApp.Common.Services;
using ChatApp.Infrastructure.Implementations.Cache;
using ChatApp.Infrastructure.Services.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace ChatApp.API.DependencyInjection.Modules
{
    /// <summary>
    /// Registers the cache-related services in the IoC container.
    /// </summary>
    public class CacheModule : Module
    {
        private readonly IConfiguration _configuration;

        public CacheModule(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Loads the cache-related services into the IoC container.
        /// </summary>
        /// <param name="builder">Container builder services.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MemoryCache>()
                .SingleInstance();

            builder
                .RegisterType<InMemoryCache>()
                .As<ICache>()
                .SingleInstance();

            builder
                .RegisterType<GetByFilterCacheKeyGenerator>()
                .As<IGetByFilterCacheKeyGenerator>()
                .InstancePerLifetimeScope();
        }
    }
}

using Autofac;
using Autofac.Extensions.DependencyInjection;
using ChatApp.API.DependencyInjection.Modules;
using ChatApp.Application.Services;
using ChatApp.Application.Services.Auth;
using ChatApp.Infrastructure.Implementations;
using ChatApp.Infrastructure.Implementations.Auth;

namespace ChatApp.API.DependencyInjection
{
    /// <summary>
    /// Configures the IoC container for the application using Autofac.
    /// </summary>
    public static class IoCConfig
    {
        /// <summary>
        /// Configures the Autofac IoC container as the DI provider.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        public static void ConfigureIoCContainer(this WebApplicationBuilder builder)
        {
            builder.Host
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(container =>
                {
                    RegisterModules(container, builder.Configuration);
                });
        }

        /// <summary>
        /// Registers the Autofac modules in the container.
        /// </summary>
        /// <param name="builder">The IoC container.</param>
        /// <param name="configuration">Configuration service.</param>
        private static void RegisterModules(ContainerBuilder builder, IConfiguration configuration)
        {
            builder
                .RegisterType<JwtTokenService>()
                .As<ITokenService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder
                .RegisterModule(new RepositoriesModule(configuration))
                .RegisterModule(new ManagersModule(configuration))
                .RegisterModule(new NotificationsModule(configuration))
                .RegisterModule(new CacheModule(configuration));
        }
    }
}

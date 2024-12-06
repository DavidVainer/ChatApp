using Autofac;
using Autofac.Extensions.DependencyInjection;
using ChatApp.Common.Services;
using ChatApp.Infrastructure.Implementations;

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
        /// <param name="container">The IoC container.</param>
        /// <param name="configuration">Configuration service.</param>
        private static void RegisterModules(ContainerBuilder container, IConfiguration configuration)
        {
            container
                .RegisterType<AutofacStrategyResolver>()
                .As<IStrategyResolver>()
                .InstancePerLifetimeScope();
        }
    }
}

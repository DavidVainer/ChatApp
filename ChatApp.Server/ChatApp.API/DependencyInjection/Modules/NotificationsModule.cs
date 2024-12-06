using Autofac;
using ChatApp.Application.Services;
using ChatApp.Infrastructure.Implementations;

namespace ChatApp.API.DependencyInjection.Modules
{
    /// <summary>
    /// Registers the notifications-related services in the IoC container (WebSockets).
    /// </summary>
    public class NotificationsModule : Module
    {
        private readonly IConfiguration _configuration;

        public NotificationsModule(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Loads the notifications-related services into the IoC container.
        /// </summary>
        /// <param name="builder">Container builder services.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<SignalRChatNotificationService>()
                .As<IChatNotificationService>()
                .InstancePerLifetimeScope();
        }
    }
}

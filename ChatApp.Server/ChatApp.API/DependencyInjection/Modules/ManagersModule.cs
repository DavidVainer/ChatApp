using Autofac;
using ChatApp.Application.Services;
using ChatApp.Application.Services.Implementations;
using ChatApp.Infrastructure.Implementations;

namespace ChatApp.API.DependencyInjection.Modules
{
    /// <summary>
    /// Registers the managers-related services in the IoC container.
    /// </summary>
    public class ManagersModule : Module
    {
        private readonly IConfiguration _configuration;

        public ManagersModule(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Loads the repositories-related services into the IoC container.
        /// </summary>
        /// <param name="builder">Container builder services.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<UserManager>()
                .As<IUserManager>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<LoginManager>()
                .As<ILoginManager>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<RoomManager>()
                .As<IRoomManager>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<MessageManager>()
                .As<IMessageManager>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<EntityIdGenerator>()
                .As<IEntityIdGenerator>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<BcryptPasswordService>()
                .As<IPasswordService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<RoomDetailsBuilder>()
                .As<IRoomDetailsBuilder>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<RoomParticipantDetailsBuilder>()
                .As<IRoomParticipantDetailsBuilder>()
                .InstancePerLifetimeScope();
        }
    }
}

using Autofac;
using Autofac.Core;
using ChatApp.Application.Services.Repositories;
using ChatApp.Common.Services;
using ChatApp.Domain.Models.Entities;
using ChatApp.Domain.Models.ValueObjects;
using ChatApp.Infrastructure.Implementations;
using ChatApp.Infrastructure.Implementations.Repositories;
using ChatApp.Infrastructure.Implementations.Repositories.Decorators;
using ChatApp.Infrastructure.Models.RepositorySettings;
using ChatApp.Infrastructure.Services;
using ChatApp.Infrastructure.Services.Cache;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ChatApp.API.DependencyInjection.Modules
{
    /// <summary>
    /// Registers the repositories-related services in the IoC container.
    /// </summary>
    public class RepositoriesModule : Module
    {
        private const string CONNECTION_STRING_CONFIG_KEY = "sqlConnection";
        private const string USER_REPOSITORY_SETTINGS_NAME = "UserRepositorySettings";
        private const string ROOM_REPOSITORY_SETTINGS_NAME = "RoomRepositorySettings";
        private const string MESSAGE_REPOSITORY_SETTINGS_NAME = "MessageRepositorySettings";
        private const string ROOM_PARTICIPANT_REPOSITORY_SETTINGS_NAME = "RoomParticipantRepositorySettings";
        private const string MESSAGE_STATUS_REPOSITORY_SETTINGS_NAME = "MessageStatusSettings";

        private readonly IConfiguration _configuration;

        public RepositoriesModule(IConfiguration configuration)
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
                .Register(ctx =>
                {
                    var connectionString = _configuration.GetConnectionString(CONNECTION_STRING_CONFIG_KEY);
                    var connection = new SqlConnection(connectionString);
                    return connection;
                })
                .As<IDbConnection>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<FilterSqlQueryBuilder>()
                .As<IFilterQueryBuilder>()
                .InstancePerLifetimeScope();

            builder
                .RegisterInstance(new RepositorySettings
                {
                    TableName = "Users",
                    GetAllQuery = "SELECT * FROM Users",
                    GetByIdQuery = "SELECT * FROM Users WHERE Id = @Id",
                    InsertQuery = @"
                        INSERT INTO Users (Id, Email, Password, DisplayName, CreatedAt, Deleted)
                        VALUES (@Id, @Email, @Password, @DisplayName, @CreatedAt, @Deleted)",
                    DeleteQuery = "UPDATE Users SET Deleted = 1 WHERE Id = @Id"
                })
                .Named<IRepositorySettings>(USER_REPOSITORY_SETTINGS_NAME);

            builder
                .RegisterType<UserRepository>()
                .As<IEntityRepository<User>>()
                .WithParameter(ResolvedParameter.ForNamed<IRepositorySettings>(USER_REPOSITORY_SETTINGS_NAME))
                .InstancePerLifetimeScope();

            builder
                .RegisterInstance(new RepositorySettings
                {
                    TableName = "Rooms",
                    GetAllQuery = "SELECT * FROM Rooms",
                    GetByIdQuery = "SELECT * FROM Rooms WHERE Id = @Id",
                    InsertQuery = @"
                        INSERT INTO Rooms (Id, Name, CreatedAt, Deleted)
                        VALUES (@Id, @Name, @CreatedAt, @Deleted)",
                    DeleteQuery = "UPDATE Rooms SET Deleted = 1 WHERE Id = @Id"
                })
                .Named<IRepositorySettings>(ROOM_REPOSITORY_SETTINGS_NAME);

            builder
                .RegisterType<RoomRepository>()
                .As<IEntityRepository<Room>>()
                .WithParameter(ResolvedParameter.ForNamed<IRepositorySettings>(ROOM_REPOSITORY_SETTINGS_NAME))
                .InstancePerLifetimeScope();

            builder
                .RegisterInstance(new RepositorySettings
                {
                    TableName = "Messages",
                    GetAllQuery = "SELECT * FROM Messages",
                    GetByIdQuery = "SELECT * FROM Messages WHERE Id = @Id",
                    InsertQuery = @"
                        INSERT INTO Messages (Id, RoomId, SenderId, Content, SentAt, Deleted)
                        VALUES (@Id, @RoomId, @SenderId, @Content, @SentAt, @Deleted)",
                    DeleteQuery = "UPDATE Messages SET Deleted = 1 WHERE Id = @Id"
                })
                .Named<IRepositorySettings>(MESSAGE_REPOSITORY_SETTINGS_NAME);

            builder
                .RegisterType<MessageRepository>()
                .As<IEntityRepository<Message>>()
                .WithParameter(ResolvedParameter.ForNamed<IRepositorySettings>(MESSAGE_REPOSITORY_SETTINGS_NAME))
                .InstancePerLifetimeScope();

            builder
                .RegisterDecorator<IEntityRepository<Message>>((c, parameters, inner) =>
                new CachedEntityRepositoryDecorator<Message>(inner, c.Resolve<ICache>(), c.Resolve<IGetByFilterCacheKeyGenerator>(), "Message"));

            builder
                .RegisterInstance(new RepositorySettings
                {
                    TableName = "RoomParticipants",
                    GetAllQuery = "SELECT * FROM RoomParticipants",
                    InsertQuery = @"
                        IF NOT EXISTS (
                            SELECT 1
                            FROM RoomParticipants 
                            WHERE RoomId = @RoomId AND UserId = @UserId
                        )
                        BEGIN
                            INSERT INTO RoomParticipants (RoomId, UserId, JoinedAt)
                            VALUES (@RoomId, @UserId, @JoinedAt)
                        END",
                    DeleteQuery = "DELETE FROM RoomParticipants WHERE RoomId = @RoomId AND UserId = @UserId"
                })
                .Named<IRepositorySettings>(ROOM_PARTICIPANT_REPOSITORY_SETTINGS_NAME);

            builder
                .RegisterType<RoomParticipantRepository>()
                .As<IValueObjectRepository<RoomParticipant>>()
                .WithParameter(ResolvedParameter.ForNamed<IRepositorySettings>(ROOM_PARTICIPANT_REPOSITORY_SETTINGS_NAME))
                .InstancePerLifetimeScope();

            builder
                .RegisterDecorator<IValueObjectRepository<RoomParticipant>>((c, parameters, inner) =>
                new CachedValueObjectRepositoryDecorator<RoomParticipant>(inner, c.Resolve<ICache>(), c.Resolve<IGetByFilterCacheKeyGenerator>(), "RoomParticipant"));

            builder
                .RegisterInstance(new RepositorySettings
                {
                    TableName = "MessageStatuses",
                    GetAllQuery = "SELECT * FROM MessageStatuses",
                    InsertQuery = @"
                        INSERT INTO MessageStatuses (MessageId, RoomId, UserId, SeenAt)
                        VALUES (@MessageId, @RoomId, @UserId, @SeenAt)",
                    DeleteQuery = "DELETE FROM MessageStatuses WHERE MessageId = @MessageId AND UserId = @UserId"
                })
                .Named<IRepositorySettings>(MESSAGE_STATUS_REPOSITORY_SETTINGS_NAME);

            builder
                .RegisterType<MessageStatusRepository>()
                .As<IValueObjectRepository<MessageStatus>>()
                .WithParameter(ResolvedParameter.ForNamed<IRepositorySettings>(MESSAGE_STATUS_REPOSITORY_SETTINGS_NAME))
                .InstancePerLifetimeScope();

            builder
                .RegisterDecorator<IValueObjectRepository<MessageStatus>>((c, parameters, inner) =>
                new CachedValueObjectRepositoryDecorator<MessageStatus>(inner, c.Resolve<ICache>(), c.Resolve<IGetByFilterCacheKeyGenerator>(), "MessageStatus"));
        }
    }
}

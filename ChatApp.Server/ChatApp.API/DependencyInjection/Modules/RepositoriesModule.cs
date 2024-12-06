using Autofac;
using Autofac.Core;
using ChatApp.Application.Services;
using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Implementations;
using ChatApp.Infrastructure.Models;
using ChatApp.Infrastructure.Services;
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
                    InsertQuery = @"
                        INSERT INTO Users (Id, Email, Password, DisplayName, CreatedAt, IsOnline)
                        VALUES (@Id, @Email, @Password, @DisplayNAme, @CreatedAt, @IsOnline)",
                    UpdateQuery = @"
                        UPDATE Users
                        SET IsOnline = @IsOnline
                        WHERE Id = @Id",
                    DeleteQuery = "DELETE FROM Users WHERE Id = @Id"
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
                    InsertQuery = @"
                        INSERT INTO Rooms (Id, Name, CreatedAt)
                        VALUES (@Id, @Name, @CreatedAt)",
                    UpdateQuery = @"
                        UPDATE Rooms
                        SET Name = @Name
                        WHERE Id = @Id",
                    DeleteQuery = "DELETE FROM Rooms WHERE Id = @Id"
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
                    InsertQuery = @"
                        INSERT INTO Messages (Id, RoomId, SenderId, Content, SentAt)
                        VALUES (@Id, @RoomId, @SenderId, @Content, @SentAt)",
                    UpdateQuery = @"
                        UPDATE Messages
                        SET Content = @Content
                        WHERE Id = @Id",
                    DeleteQuery = "DELETE FROM Messages WHERE Id = @Id"
                })
                .Named<IRepositorySettings>(MESSAGE_REPOSITORY_SETTINGS_NAME);

            builder
                .RegisterType<MessageRepository>()
                .As<IEntityRepository<Message>>()
                .WithParameter(ResolvedParameter.ForNamed<IRepositorySettings>(MESSAGE_REPOSITORY_SETTINGS_NAME))
                .InstancePerLifetimeScope();

            builder
                .RegisterInstance(new RepositorySettings
                {
                    TableName = "RoomParticipants",
                    GetAllQuery = "SELECT * FROM RoomParticipants",
                    InsertQuery = @"
                        INSERT INTO RoomParticipants (RoomId, UserId, JoinedAt)
                        VALUES (@RoomId, @UserId, @JoinedAt)",
                    DeleteQuery = "DELETE FROM RoomParticipants WHERE RoomId = @RoomId AND UserId = @UserId"
                })
                .Named<IRepositorySettings>(ROOM_PARTICIPANT_REPOSITORY_SETTINGS_NAME);

            builder
                .RegisterType<RoomParticipantRepository>()
                .As<IValueObjectRepository<RoomParticipant>>()
                .WithParameter(ResolvedParameter.ForNamed<IRepositorySettings>(ROOM_PARTICIPANT_REPOSITORY_SETTINGS_NAME))
                .InstancePerLifetimeScope();

            builder
                .RegisterInstance(new RepositorySettings
                {
                    TableName = "MessageStatuses",
                    GetAllQuery = "SELECT * FROM MessageStatuses",
                    InsertQuery = @"
                        INSERT INTO RoomParticipants (MessageId, UserId, SeenAt)
                        VALUES (@MessageId, @UserId, @SeenAt)",
                    DeleteQuery = "DELETE FROM MessageStatuses WHERE MessageId = @MessageId AND UserId = @UserId"
                })
                .Named<IRepositorySettings>(MESSAGE_STATUS_REPOSITORY_SETTINGS_NAME);

            builder
                .RegisterType<MessageStatusRepository>()
                .As<IValueObjectRepository<MessageStatus>>()
                .WithParameter(ResolvedParameter.ForNamed<IRepositorySettings>(MESSAGE_STATUS_REPOSITORY_SETTINGS_NAME))
                .InstancePerLifetimeScope();
        }
    }
}

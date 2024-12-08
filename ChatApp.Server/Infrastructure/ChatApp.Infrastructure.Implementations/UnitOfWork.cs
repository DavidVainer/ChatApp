using ChatApp.Application.Services;
using ChatApp.Domain.Models;

namespace ChatApp.Infrastructure.Implementations
{
    /// <summary>
    /// Unit of work for managing data repositories in a single transaction.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            IRepository<Room> roomsRepository,
            IRepository<User> usersRepository,
            IRepository<RoomParticipant> participantsRepository,
            IRepository<Message> messagesRepository,
            IRepository<MessageStatus> messageStatusesRepository)
        {
            Rooms = roomsRepository ?? throw new ArgumentNullException(nameof(roomsRepository));
            Users = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            Participants = participantsRepository ?? throw new ArgumentNullException(nameof(participantsRepository));
            Messages = messagesRepository ?? throw new ArgumentNullException(nameof(messagesRepository));
            MessageStatuses = messageStatusesRepository ?? throw new ArgumentNullException(nameof(messageStatusesRepository));
        }

        /// <summary>
        /// Provides access to the repository for managing chat rooms.
        /// </summary>
        public IRepository<Room> Rooms { get; set; }

        /// <summary>
        /// Provides access to the repository for managing user data.
        /// </summary>
        public IRepository<User> Users { get; set; }

        /// <summary>
        /// Provides access to the repository for managing participants in chat rooms.
        /// </summary>
        public IRepository<RoomParticipant> Participants { get; set; }

        /// <summary>
        /// Provides access to the repository for managing chat messages.
        /// </summary>
        public IRepository<Message> Messages { get; set; }

        /// <summary>
        /// Provides access to the repository for managing message statuses;
        /// </summary>
        public IRepository<MessageStatus> MessageStatuses { get; set; }
    }
}

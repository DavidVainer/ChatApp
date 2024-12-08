using ChatApp.Domain.Models;

namespace ChatApp.Application.Services
{
    /// <summary>
    /// Defines a contract for managing data repositories in a single unit of work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Provides access to the repository for managing chat rooms.
        /// </summary>
        IRepository<Room> Rooms { get; }

        /// <summary>
        /// Provides access to the repository for managing user data.
        /// </summary>
        IRepository<User> Users { get; }

        /// <summary>
        /// Provides access to the repository for managing participants in chat rooms.
        /// </summary>
        IRepository<RoomParticipant> Participants { get; }

        /// <summary>
        /// Provides access to the repository for managing chat messages.
        /// </summary>
        IRepository<Message> Messages { get; }

        /// <summary>
        /// Provides access to the repository for managing message statuses;
        /// </summary>
        IRepository<MessageStatus> MessageStatuses { get; }
    }
}

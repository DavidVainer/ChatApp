using ChatApp.Application.Services.Repositories;
using ChatApp.Domain.Models.Entities;
using ChatApp.Domain.Models.ValueObjects;

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
        IEntityRepository<Room> Rooms { get; }

        /// <summary>
        /// Provides access to the repository for managing user data.
        /// </summary>
        IEntityRepository<User> Users { get; }

        /// <summary>
        /// Provides access to the repository for managing chat messages.
        /// </summary>
        IEntityRepository<Message> Messages { get; }

        /// <summary>
        /// Provides access to the repository for managing participants in chat rooms.
        /// </summary>
        IValueObjectRepository<RoomParticipant> Participants { get; }

        /// <summary>
        /// Provides access to the repository for managing message statuses;
        /// </summary>
        IValueObjectRepository<MessageStatus> MessageStatuses { get; }
    }
}

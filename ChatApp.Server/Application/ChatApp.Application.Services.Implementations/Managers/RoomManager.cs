using ChatApp.Application.Models;
using ChatApp.Domain.Models;

namespace ChatApp.Application.Services.Implementations
{
    /// <summary>
    /// Encapsulates room related operations.
    /// </summary>
    public class RoomManager : IRoomManager
    {
        private readonly IEntityRepository<Room> _roomRepository;
        private readonly IValueObjectRepository<RoomParticipant> _participantRepository;
        private readonly IEntityIdGenerator _entityIdGenerator;

        /// <summary>
        /// Initializes a new instnace of <see cref="RoomManager"/> class.
        /// </summary>
        /// <param name="roomRepository">Room repository service.</param>
        /// <param name="participantRepository">Participant repository service.</param>
        /// <param name="entityIdGenerator">Entity id generator service.</param>
        public RoomManager(IEntityRepository<Room> roomRepository, IValueObjectRepository<RoomParticipant> participantRepository, IEntityIdGenerator entityIdGenerator)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
            _participantRepository = participantRepository ?? throw new ArgumentNullException(nameof(participantRepository));
            _entityIdGenerator = entityIdGenerator ?? throw new ArgumentNullException(nameof(entityIdGenerator));

        }

        /// <summary>
        /// Creates a new room with the specified details.
        /// </summary>
        /// <param name="dto">Dto containing the room creation details.</param>
        /// <returns>The new created room.</returns>
        public IRoom CreateRoom(CreateRoomDto dto)
        {
            var room = new Room
            {
                Id = _entityIdGenerator.Generate(),
                Name = dto.Name,
                RoomType = dto.RoomType,
                CreatedAt = DateTime.UtcNow
            };

            _roomRepository.Insert(room);

            return room;
        }

        /// <summary>
        /// Adds a participant to the specified room.
        /// </summary>
        /// <param name="dto">Dto containing participant details.</param>
        public void AddParticipant(ParticipantActionDto dto)
        {
            var participant = new RoomParticipant
            {
                RoomId = dto.RoomId,
                UserId = dto.UserId,
                JoinedAt = DateTime.UtcNow
            };

            _participantRepository.Insert(participant);
        }

        /// <summary>
        /// Removes a participant from the specified room.
        /// </summary>
        /// <param name="dto">Dto containing participant details and the room ID.</param>
        public void RemoveParticipant(ParticipantActionDto dto)
        {
            var participantFilter = new RoomParticipant
            {
                RoomId = dto.RoomId,
                UserId = dto.UserId
            };
            var participant = _participantRepository.GetByFilter(participantFilter).FirstOrDefault();

            _participantRepository.Delete(participant);
        }

        /// <summary>
        /// Deletes the specified room.
        /// </summary>
        /// <param name="roomId">The unique identifier of the room to be deleted.</param>
        public void DeleteRoom(Guid roomId)
        {
            var roomFilter = new Room { Id = roomId };
            var room = _roomRepository.GetByFilter(roomFilter).FirstOrDefault();

            _roomRepository.Delete(room);
        }

        /// <summary>
        /// Retrieves all rooms that a specific user participates in.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A collection of rooms associated with the user.</returns>
        public IEnumerable<IRoom> GetUserRooms(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}

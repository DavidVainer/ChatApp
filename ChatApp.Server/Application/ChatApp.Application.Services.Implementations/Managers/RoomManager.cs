using ChatApp.Application.Models.Dto;
using ChatApp.Application.Services.Builders;
using ChatApp.Application.Services.Managers;
using ChatApp.Application.Services.Repositories;
using ChatApp.Domain.Models.Aggregates;
using ChatApp.Domain.Models.Entities;
using ChatApp.Domain.Models.ValueObjects;

namespace ChatApp.Application.Services.Implementations.Managers
{
    /// <summary>
    /// Encapsulates room related operations.
    /// </summary>
    public class RoomManager : IRoomManager
    {
        private readonly IEntityRepository<Room> _roomRepository;
        private readonly IValueObjectRepository<RoomParticipant> _participantRepository;
        private readonly IEntityIdGenerator _entityIdGenerator;
        private readonly IRoomDetailsBuilder _roomDetailsBuilder;

        public RoomManager(
            IEntityRepository<Room> roomRepository,
            IValueObjectRepository<RoomParticipant> participantRepository,
            IEntityIdGenerator entityIdGenerator,
            IRoomDetailsBuilder roomDetailsBuilder)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
            _participantRepository = participantRepository ?? throw new ArgumentNullException(nameof(participantRepository));
            _entityIdGenerator = entityIdGenerator ?? throw new ArgumentNullException(nameof(entityIdGenerator));
            _roomDetailsBuilder = roomDetailsBuilder ?? throw new ArgumentNullException(nameof(roomDetailsBuilder));
        }

        /// <summary>
        /// Retrieves all rooms.
        /// </summary>
        /// <returns>Collection of existing rooms.</returns>
        public IEnumerable<IRoom> GetAllRooms()
        {
            var rooms = _roomRepository.GetAll();
            return rooms;
        }

        /// <summary>
        /// Retrieves all active rooms.
        /// </summary>
        /// <returns>Collection of existing active rooms.</returns>
        public IEnumerable<IRoom> GetActiveRooms()
        {
            var rooms = _roomRepository.GetAll().Where(room => !(room.Deleted ?? false));
            return rooms;
        }

        /// <summary>
        /// Retrieves the details of the specified room.
        /// </summary>
        /// <param name="roomId">Room unique identifier.</param>
        /// <returns>Aggregated object contains room details.</returns>
        public IRoomDetails GetRoomDetails(Guid roomId)
        {
            _roomDetailsBuilder.Initialize(roomId);
            _roomDetailsBuilder.SetRoomProperties();
            _roomDetailsBuilder.SetMessages();
            _roomDetailsBuilder.SetMessageAuthors(); // TODO: Remove passwords
            _roomDetailsBuilder.SetParticipants();

            var roomDetails = _roomDetailsBuilder.Build();

            return roomDetails;
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
                CreatedAt = DateTime.Now,
                Deleted = false,
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
                JoinedAt = DateTime.Now,
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
            var room = _roomRepository.GetById(roomId);

            _roomRepository.Delete(room);
        }
    }
}

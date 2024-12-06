using ChatApp.Application.Models;
using ChatApp.Application.Services;
using ChatApp.Application.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    /// <summary>
    /// Handles room operation requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomManager _roomManager;

        /// <summary>
        /// Initiates a new instance of the <see cref="RoomsController"/> class.
        /// </summary>
        /// <param name="roomManager">Room manager service.</param>
        public RoomsController(IRoomManager roomManager)
        {
            _roomManager = roomManager ?? throw new ArgumentNullException(nameof(roomManager));
        }

        /// <summary>
        /// Retrieves all rooms.
        /// </summary>
        /// <returns>A collection of all rooms.</returns>
        [HttpGet]
        public IActionResult GetAllRooms()
        {
            var rooms = _roomManager.GetAllRooms();
            return Ok(rooms);
        }

        /// <summary>
        /// Retrieves room details.
        /// </summary>
        /// <returns>A collection of all rooms.</returns>
        [HttpGet("{id:guid}")]
        public IActionResult GetRoomDetails(Guid id)
        {
            var room = _roomManager.GetRoomDetails(id);
            return Ok(room);
        }

        /// <summary>
        /// Creates a new room.
        /// </summary>
        /// <param name="dto">The room creation details.</param>
        /// <returns>The new created room.</returns>
        [HttpPost]
        public IActionResult CreateRoom([FromBody] CreateRoomDto dto)
        {
            var room = _roomManager.CreateRoom(dto);
            return Ok(room);
        }

        /// <summary>
        /// Deletes a room by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the room.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteRoom(Guid id)
        {
            _roomManager.DeleteRoom(id);
            return Ok(new { message = "Room deleted successfully." });
        }
    }
}

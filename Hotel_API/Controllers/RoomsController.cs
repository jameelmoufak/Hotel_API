using AutoMapper;
using Hotel_API.Models.ViewModels;
using Hotel_API.Models;
using Hotel_API.Services.InterfacesRepository;
using Hotel_API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Hotel_API.Controllers
{
    [Route("api/hotels/{hotelId}/Rooms")]
    [ApiController]
    [Authorize]
    public class RoomsController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IRoomRepository repository;
        private readonly IMapper mapper;

        public RoomsController(IConfiguration configuration,
            IRoomRepository repository,
            IMapper mapper)
        {
            this.configuration = configuration;
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpGet("download")]
        [AllowAnonymous]
        public ActionResult GetRoomsFile(int hotelId)
        {
            var emp = repository.GetAllRoomsFile(hotelId);
            if (emp == null) return NotFound();
            return File(repository.GetAllRoomsFile(hotelId), "text/plain", "Rooms_list");
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Room>>> GetHotelRooms(int hotelId, int pageNumber = 1, int pageSize = 5, string? name = null)
        {
            var (rooms, paginationData) = await repository.GetHotelRoomsAsync(hotelId, pageNumber, pageSize, name);
            if (rooms == null)
                return NotFound();
            Response.Headers.Append("Pagination-Data", JsonConvert.SerializeObject(paginationData));
            return Ok(rooms);
        }
        [HttpGet("{roomId}")]
        [AllowAnonymous]
        public async Task<ActionResult<Room>> GetRoom([FromRoute] int hotelId, [FromRoute] int roomId, [FromQuery] bool summary = false)
        {
            var room = await repository.GetRoomByIdAsync(hotelId, roomId);
            if (room == null)
                return NotFound();
            return summary ? Ok(mapper.Map<RoomSummary>(room)) : Ok(room);
        }
        [HttpPost("{RoomTypeId}")]
        public ActionResult<Room> CreateRoom([FromRoute] int hotelId, int RoomTypeId, [FromBody] RoomForCreate room)
        {
            var newRoom = repository.AddRoom(hotelId, RoomTypeId, room);
            if (newRoom == null)
                return NotFound();
            return Ok(newRoom);
        }
        [HttpPut("{roomId}/{RoomTypeId}")]
        public ActionResult<Employee> UpdateEmployee(int hotelId, int roomId, int RoomTypeId, RoomForUpdate room)
        {
            var updatedRoom = repository.UpdateRoom(hotelId, roomId, RoomTypeId, room);
            if (updatedRoom == null) return NotFound();
            return Ok(updatedRoom);
        }
        [HttpPatch("{RoomId}")]
        public async Task<ActionResult> PartiallyUpdateRoom(int hotelId, int RoomId, JsonPatchDocument<RoomForUpdate> patchDocument)
        {
            var existingRoom = await repository.GetRoomByIdAsync(hotelId, RoomId);
            if (existingRoom == null) return NotFound();
            var RoomToPatch = new RoomForUpdate();
            RoomToPatch.Number = existingRoom.Number;
            Enum.TryParse(existingRoom.Status, out StatusEnum StatusNumber);
            RoomToPatch.StatusNumber = (int)StatusNumber;
            RoomToPatch.FloorNumber = existingRoom.FloorNumber;
            
            patchDocument.ApplyTo(RoomToPatch, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            repository.UpdateRoom(hotelId, RoomId, existingRoom.RoomTypeId, RoomToPatch);
            return NoContent();
        }
        [HttpDelete("{roomId}")]
        public ActionResult DeleteHotel(int hotelId, int roomId)
        {
            var delete = repository.DeleteRoom(hotelId, roomId);
            if (delete == false) return NotFound();
            return NoContent();
        }
    }
}

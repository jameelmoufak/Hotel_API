using Hotel_API.DbContexts;
using Hotel_API.Models;
using Hotel_API.Pagination;
using Hotel_API.Services.InterfacesRepository;
using Hotel_API.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Hotel_API.Services
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelContext context;

        public RoomRepository(HotelContext context)
        {
            this.context = context;
        }
        public byte[] GetAllRoomsFile(int hotelId)
        {
            var rooms = context.Rooms.Include(r=>r.Booking).Where(r=>r.HotelId == hotelId).ToList();
            if (rooms == null) return null;
            var path = "Rooms_list.txt";
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };
            string roomstxt = JsonConvert.SerializeObject(rooms, settings);
            File.WriteAllText(path, roomstxt);
            return File.ReadAllBytes(path);
        }
        public async Task<(List<Room>, PaginationMetaData)> GetHotelRoomsAsync(int hotelId, int pageNumber, int pageSize, string? statusFilter)
        {
            var totalItemCount = await context.Rooms.Where(r => r.HotelId == hotelId).CountAsync();
            var paginationData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);
            var query = context.Rooms.Where(e => e.HotelId == hotelId);
            if (!string.IsNullOrEmpty(statusFilter))
                query = query.Where(r => r.Status.ToLower() == statusFilter.ToLower());
            var rooms = await query.OrderBy(h => h.Number)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
            //    .Include(r => r.Booking)
                .ToListAsync();
            if (rooms == null) return (null, null);
            return (rooms, paginationData);
        }

        public async Task<Room?> GetRoomByIdAsync(int hotelId, int RoomId)
        {
            var room = await context.Rooms
               // .Include(e => e.Booking)
                .Where(e => e.HotelId == hotelId)
                .FirstOrDefaultAsync(e => e.Id == RoomId);
            if (room == null) return null;
            return room;
        }
        public Room AddRoom(int hotelId, int RoomTypeId, RoomForCreate room)
        {
            if (context.Hotels.FirstOrDefault(h => h.Id == hotelId) == null) return null;
            if (context.RoomTypes.FirstOrDefault(r => r.Id == RoomTypeId) == null) return null;
            var newRoom = new Room()
            {
                RoomTypeId = RoomTypeId,
                HotelId = hotelId,
                Number = room.Number,
                Status = Convert.ToString((StatusEnum)room.StatusNumber),
                FloorNumber = room.FloorNumber
            };
            context.Rooms.Add(newRoom);
            context.SaveChanges();
            return newRoom;
        }
        public Room UpdateRoom(int hotelId, int RoomId, int RoomTypeId, RoomForUpdate room)
        {
            var existingRoom = context.Rooms.Where(r => r.HotelId == hotelId && r.RoomTypeId == RoomTypeId).FirstOrDefault(r => r.Id == RoomId);
            if (existingRoom == null) return null;
            existingRoom.Number = room.Number;
            existingRoom.FloorNumber = room.FloorNumber;
            existingRoom.Status = Convert.ToString((StatusEnum)room.StatusNumber);
            context.Rooms.Update(existingRoom);
            context.SaveChanges();
            return existingRoom;
        }

        public bool DeleteRoom(int hotelId, int RoomId)
        {
            var room = context.Rooms.Where(r => r.HotelId == hotelId).FirstOrDefault(r => r.Id == RoomId);
            if (room == null) return false;
            context.Rooms.Remove(room);
            context.SaveChanges();
            return true;
        }



    }
}

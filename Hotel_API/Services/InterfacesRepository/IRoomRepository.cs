using Hotel_API.Models;
using Hotel_API.Pagination;
using Hotel_API.ViewModels;

namespace Hotel_API.Services.InterfacesRepository
{
    public interface IRoomRepository
    {
        /// <summary>
        /// Convert your List of Rooms to Json string then upload it to text file that you can download it
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns>A byte array of your data</returns>
        byte[] GetAllRoomsFile(int hotelId);
        /// <summary>
        /// To get all your Rooms by Hotel Id with PaginationMetaData and filtering by name
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
        /// <returns>A specific page of Rooms with Page data</returns>
        Task<(List<Room>, PaginationMetaData)> GetHotelRoomsAsync(int hotelId, int pageNumber, int pageSize, string? keyword);
        /// <summary>
        /// To get a specific Room by its id
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="RoomId"></param>
        /// <returns>Room By Id</returns>
        Task<Room?> GetRoomByIdAsync(int hotelId, int RoomId);
        /// <summary>
        /// To Create new Room and add it to your database
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="RoomTypeId"></param>
        /// <param name="room"></param>
        /// <returns>New Added Room</returns>
        Room AddRoom(int hotelId, int RoomTypeId, RoomForCreate room);

        /// <summary>
        /// To Update Existing Room in your database
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="RoomTypeId"></param>
        /// <param name="room"></param>
        /// <returns>Updated Room</returns>
        Room UpdateRoom(int hotelId, int RoomId, int RoomTypeId, RoomForUpdate room);
        /// <summary>
        /// To Remove Existing Room from your database
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="RoomId"></param>
        /// <returns>true if Done, false if Not Found</returns>
        bool DeleteRoom(int hotelId, int RoomId);

    }
}

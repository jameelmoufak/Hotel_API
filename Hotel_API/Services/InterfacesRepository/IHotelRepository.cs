using Hotel_API.Models;
using Hotel_API.Models.ViewModels;
using Hotel_API.Pagination;
using Hotel_API.ViewModels;

namespace Hotel_API.Services.InterfacesRepository
{
    public interface IHotelRepository
    {
        /// <summary>
        /// Convert your List of Hotels to Json string then you can upload it to text file
        /// </summary>
        /// <returns>A byte array of your data</returns>
        byte[] GetAllHotelsFile();
        /// <summary>
        /// To get all your Hotels with PaginationMetaData and filtering by name
        /// </summary>
        /// <param name="pageNumber">Number of page</param>
        /// <param name="pageSize">Size of Page</param>
        /// <param name="keyword">A string keyword to do filtering</param>
        /// <returns>A specific page of hotels with Page data</returns>
        Task<(List<Hotel>, PaginationMetaData)> GetHotelsAsync(int pageNumber, int pageSize, string? keyword);
        /// <summary>
        /// To get a specific hotel by its id
        /// </summary>
        /// <param name="id">id of Hotel</param>
        /// <returns>hotel by id</returns>
        Task<Hotel?> GetHotelByIdAsync(int id);
        /// <summary>
        /// To Create new hotel and add it to your database
        /// </summary>
        /// <param name="hotel">HotelForCreate variable that contain require Hotel variables</param>
        /// <returns>Added Hotel</returns>
        Hotel AddHotel(HotelForCreate hotel);
        /// <summary>
        /// To Update your existing hotel
        /// </summary>
        /// <param name="id">id of hotel</param>
        /// <param name="hotel"></param>
        /// <returns>updated hotel</returns>
        Hotel UpdateHotel(int id, HotelForUpdate hotel);
        /// <summary>
        /// To delete existing hotel
        /// </summary>
        /// <param name="id">id of hotel</param>
        /// <returns>false if not found, true if removing is done</returns>
        Task<bool> DeleteHotel(int id);
    }
}

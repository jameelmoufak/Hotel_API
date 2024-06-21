using Hotel_API.DbContexts;
using Hotel_API.Models;
using Hotel_API.Pagination;
using Hotel_API.Services.InterfacesRepository;
using Hotel_API.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace Hotel_API.Services
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelContext context;
        public HotelRepository(HotelContext context)
        {
            this.context = context;
        }
        //دالة تقوم بعرض جميع الفنادق وتحميلها الى ملف نصي قابل للتحميل
        public byte[] GetAllHotelsFile()
        {
            var hotels = context.Hotels.Include(h => h.Employees).Include(h => h.Room).ThenInclude(r => r.Booking).ToList();
            var path = "hotel_list.txt";
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };
            string hoteltxt = JsonConvert.SerializeObject(hotels, settings);
            System.IO.File.WriteAllText(path, hoteltxt);
            return File.ReadAllBytes(path);
        }
        public async Task<(List<Hotel>, PaginationMetaData)> GetHotelsAsync(int pageNumber, int pageSize, string? keyword)
        {
            var totalItemCount = await context.Hotels.CountAsync();
            var paginationData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);
            var query = context.Hotels as IQueryable<Hotel>;
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(h => h.Name.ToLower().Contains(keyword.ToLower()));
            var hotels = await query.OrderBy(h => h.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
               // .Include(h => h.Employees)
                //.Include(h => h.Room)
                .ToListAsync();
            return (hotels, paginationData);
        }

        public async Task<Hotel?> GetHotelByIdAsync(int id)
        {
            var hotel = await context
                .Hotels
            //    .Include(h => h.Employees)
            //    .Include(h => h.Room)
                .FirstOrDefaultAsync(h => h.Id == id);
            return hotel;
        }



        public Hotel AddHotel(HotelForCreate hotel)
        {
            var newHotel = new Hotel() { Address = hotel.Address, Email = hotel.Email, Name = hotel.Name, Phone = hotel.Phone };
            context.Hotels.Add(newHotel);
            context.SaveChanges();
            return newHotel;
        }

        public Hotel UpdateHotel(int id, HotelForUpdate hotel)
        {
            var existingHotel = context.Hotels.FirstOrDefault(h => h.Id == id);
            if (existingHotel == null) return null;
            existingHotel.Address = hotel.Address;
            existingHotel.Phone = hotel.Phone;
            existingHotel.Email = hotel.Email;
            existingHotel.Name = hotel.Name;
            context.Hotels.Update(existingHotel);
            context.SaveChanges();
            return existingHotel;
        }

        public async Task<bool> DeleteHotel(int id)
        {
            var hotel = await context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
            if (hotel == null) return false;
            context.Hotels.Remove(hotel);
            await context.SaveChangesAsync();
            return true;

        }
    }
}

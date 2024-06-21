using AutoMapper;
using Hotel_API.Models.ViewModels;

namespace Hotel_API.Models.Profiles
{
    public class BookingProfile:Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingSummary>();
        }
    }
}

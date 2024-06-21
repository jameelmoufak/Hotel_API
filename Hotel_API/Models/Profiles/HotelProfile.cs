using AutoMapper;
using Hotel_API.Models.ViewModels;

namespace Hotel_API.Models.Profiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<Hotel, HotelSummary>();
        }
    }
}

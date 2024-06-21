using AutoMapper;
using Hotel_API.Models.ViewModels;

namespace Hotel_API.Models.Profiles
{
    public class RoomTypeProfile:Profile
    {
        public RoomTypeProfile()
        {
            CreateMap<RoomType, RoomTypeSummary>();
        }
    }
}

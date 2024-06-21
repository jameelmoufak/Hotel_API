using AutoMapper;
using Hotel_API.Models.ViewModels;

namespace Hotel_API.Models.Profiles
{
    public class RoomProfile:Profile
    {
        public RoomProfile()
        {
            CreateMap<Guest, RoomSummary>();
        }
    }
}

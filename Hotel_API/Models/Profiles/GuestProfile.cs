using AutoMapper;
using Hotel_API.Models.ViewModels;

namespace Hotel_API.Models.Profiles
{
    public class GuestProfile:Profile
    {
        public GuestProfile()
        {
            CreateMap<Guest, GuestSummary>()
                .ForMember(d => d.FullName, s => s.MapFrom(s => s.FirstName + " " + s.LastName));
        }
    }
}
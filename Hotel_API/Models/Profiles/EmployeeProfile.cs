using AutoMapper;
using Hotel_API.Models.ViewModels;

namespace Hotel_API.Models.Profiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        { //قمنا بدمج الاسم والكنية في متحول واحد
            CreateMap<Employee, EmployeeSummary>()
                .ForMember(d=>d.FullName,s=>s.MapFrom(s=>s.FirstName+" "+s.LastName));
        }
    }
}

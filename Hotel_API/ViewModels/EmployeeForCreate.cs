using System.ComponentModel.DataAnnotations;

namespace Hotel_API.ViewModels
{
    public class EmployeeForCreate
    {
        [Required(ErrorMessage = "Enter First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Enter Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Enter Date of Birthday")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Started Date")]
        public DateTime StartedDate { get; set; }
    }
}

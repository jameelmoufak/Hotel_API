using System.ComponentModel.DataAnnotations;

namespace Hotel_API.ViewModels
{
    public class HotelForCreate
    {
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Phone Number")]
        [MaxLength(16)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; }
    }
}

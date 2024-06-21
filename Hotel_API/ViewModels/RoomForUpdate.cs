using System.ComponentModel.DataAnnotations;

namespace Hotel_API.ViewModels
{
    public class RoomForUpdate
    {
        [Required(ErrorMessage = "Enter Number")]
        public int Number { get; set; }
        [Required(ErrorMessage = "Enter Floor Number")]
        public int FloorNumber { get; set; }
        [Required(ErrorMessage = "Enter Status Number (1, 2, 3, 4)")]
        [Range(1, 4)]
        public int StatusNumber { get; set; }
    }
}

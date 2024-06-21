namespace Hotel_API.Models.ViewModels
{
    public class GuestSummary
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Booking Booking { get; set; }
        
    }
}

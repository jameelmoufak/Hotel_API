namespace Hotel_API.Models.ViewModels
{
    public class BookingSummary
    {
        public int Id { get; set; }
        public DateTime CheckInAt { get; set; }
        public DateTime CheckOutAt { get; set; }
        public double Price { get; set; }
        public int RoomId { get; set; }
        public int EmployeeId { get; set; }
        public int GuestId { get; set; }
    }
}

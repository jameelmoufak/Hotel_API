namespace Hotel_API.ViewModels
{
    public class BookingForUpdate
    {
        public DateTime CheckInAt { get; set; }
        public DateTime CheckOutAt { get; set; }
        public double Price { get; set; }
        public int RoomId { get; set; }
        public int EmployeeId { get; set; }
        public int GuestId { get; set; }
    }
}

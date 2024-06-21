using System.Runtime.Serialization;

namespace Hotel_API.Models
{
   // [DataContract(IsReference = true)]
    public class Booking
    {

        public int Id { get; set; }
        public DateTime CheckInAt { get; set; }
        public DateTime CheckOutAt { get; set; }
        public double Price { get; set; }
        public Booking()
        {
            Payment = new List<Payment>();
        }
        public Room Room { get; set; }
        public int RoomId { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public List<Payment> Payment { get; }
        public int GuestId { get; set; }
    }
}

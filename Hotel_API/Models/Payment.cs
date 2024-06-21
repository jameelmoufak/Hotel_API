using System.Runtime.Serialization;

namespace Hotel_API.Models
{
    // [DataContract(IsReference = true)]
    public class Payment
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public Booking Booking { get; set; }
        public int BookingId { get; set; }
        public Guest Guest { get; set; }
        public int GuestId { get; set; }
    }
}

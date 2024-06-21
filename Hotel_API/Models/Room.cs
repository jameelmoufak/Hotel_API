using System.Runtime.Serialization;

namespace Hotel_API.Models
{
    // [DataContract(IsReference = true)]
    public enum StatusEnum
        {
            Occupied=1,
            Dirty=2,
            Ready=3,
            Out_Of_Order=4
    }
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int FloorNumber { get; set; }
        public string Status { get; set; }
        public Room()
        {
            Booking = new List<Booking>();
        }
        public RoomType RoomType { get; set; }
        public int RoomTypeId { get; set; }
        public List<Booking> Booking { get; }
        public Hotel Hotel { get; set; }
        public int HotelId { get; set; }
    }
}

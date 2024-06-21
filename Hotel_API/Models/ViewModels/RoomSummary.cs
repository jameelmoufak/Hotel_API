namespace Hotel_API.Models.ViewModels
{
    public class RoomSummary
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int FloorNumber { get; set; }
        public string Status { get; set; }
        public int RoomTypeId { get; set; }
        public int HotelId { get; set; }
    }
}

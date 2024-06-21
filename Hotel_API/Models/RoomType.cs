using System.Runtime.Serialization;

namespace Hotel_API.Models
{
    // [DataContract(IsReference = true)]
    public class RoomType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public int NumOfBeds { get; set; }
        public RoomType()
        {
            Room = new List<Room>();
        }
        public List<Room> Room { get; }
        
    }
}

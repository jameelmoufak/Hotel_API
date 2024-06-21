using System.Runtime.Serialization;

namespace Hotel_API.Models
{
    // [DataContract(IsReference = true)]
    public class Hotel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Hotel()
        {
            Employees = new List<Employee>();
            Room = new List<Room>();
        }
        public List<Employee> Employees { get; }
        public List<Room> Room { get; set; }

    }
}

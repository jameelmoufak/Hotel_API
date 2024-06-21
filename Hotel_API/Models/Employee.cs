using System.Runtime.Serialization;

namespace Hotel_API.Models
{
    // [DataContract(IsReference = true)]
    public class Employee
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public DateTime StartedDate { get; set; }
        public Employee()
        {
            Booking = new List<Booking>();
        }
        public Hotel Hotel { get; set; }
        public int HotelId { get; set; }
        public List<Booking> Booking { get; }
    }
}

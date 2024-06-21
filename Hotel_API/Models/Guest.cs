using System.Runtime.Serialization;

namespace Hotel_API.Models
{
    // [DataContract(IsReference = true)]
    public class Guest
    {
        public Guest()
        {
            Payment = new List<Payment>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Booking Booking { get; set; }
        public List<Payment> Payment { get; }
    }
}

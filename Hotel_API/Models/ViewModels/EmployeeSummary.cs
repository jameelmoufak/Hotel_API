namespace Hotel_API.Models.ViewModels
{
    public class EmployeeSummary
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public DateTime StartedDate { get; set; }
        public int HotelId { get; set; }
        
    }
}

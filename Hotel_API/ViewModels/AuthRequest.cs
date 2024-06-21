namespace Hotel_API.ViewModels
{
    public class AuthRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class HotelUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

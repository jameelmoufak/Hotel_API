using Hotel_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel_API.DbContexts
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options):
            base(options)
        { }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // اضافة بذرة البيانات
            modelBuilder.Entity<Hotel>()
                .HasData(new Hotel { Id = 1, Name = "Alfa Hotel", Email = "AlfaHotel@gmail.com", Phone = "5134776", Address = "Cairo/ Street 14" });
            modelBuilder.Entity<Employee>()
                .HasData(new Employee { Id = 1, FirstName = "Samer", LastName = "AboSamra", DOB = Convert.ToDateTime("4 / 5 / 2006"), Email = "samer.abosamra@gmail.com", StartedDate = DateTime.Now, Title = "Cairo/ street 12", HotelId = 1 });
            modelBuilder.Entity<RoomType>()
                .HasData(new RoomType { Id = 1, TypeName = "Excellent", NumOfBeds = 2 });
            modelBuilder.Entity<Room>()
                .HasData(new Room { Id = 1, Number = 1, FloorNumber = 1, Status = Convert.ToString((StatusEnum)1), RoomTypeId = 1, HotelId = 1 });
            modelBuilder.Entity<Guest>()
                .HasData(new Guest { Id = 1, FirstName = "Ahmad", LastName = "AboHamid", DOB = new DateTime(1990, 1, 1), Email = "ahmad.abohamid@gmail.com", Phone = "5569211" });
            modelBuilder.Entity<Booking>()
                .HasData(new Booking { Id = 1, CheckInAt = Convert.ToDateTime("5 / 11 / 2024"), CheckOutAt = DateTime.Now, Price = 450, EmployeeId = 1, GuestId = 1, RoomId = 1 });
            modelBuilder.Entity<Payment>()
                .HasData(new Payment { Id = 1, TotalAmount = 450, CreatedDate = DateTime.Now, BookingId = 1, GuestId = 1 });



            //هنا قمنا بتحديد ما الذي يجب فعله عند حذف احد عناصر من جدول مرتبط بجداول أخرى
            //من أجل التخلص من الخطأ المتعلق بالجداول التي تشكل حلقات مع بعضها
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Booking)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Payment>()
                .HasOne(g => g.Booking)
                .WithMany(r => r.Payment)
                .HasForeignKey(b => b.BookingId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Hotel)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.HotelId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Room>()
                .HasOne(e => e.Hotel)
                .WithMany(e => e.Room)
                .HasForeignKey(e => e.HotelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

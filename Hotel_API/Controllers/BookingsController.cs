using AutoMapper;
using Azure;
using Hotel_API.DbContexts;
using Hotel_API.Models;
using Hotel_API.Models.ViewModels;
using Hotel_API.Pagination;
using Hotel_API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Microsoft.AspNetCore.Authorization;

namespace Hotel_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly HotelContext context;
        private readonly IMapper mapper;

        public BookingsController(IConfiguration configuration,
            HotelContext context,
            IMapper mapper)
        {
            this.configuration = configuration;
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet("download")]
        [AllowAnonymous]
        public ActionResult GetBookingsFile()
        {
            var bookings = context.Bookings.Include(b => b.Payment).ToList();
            var path = "Booking_list.txt";
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };
            string bookingtxt = JsonConvert.SerializeObject(bookings, settings);
            System.IO.File.WriteAllText(path, bookingtxt);
            return File(System.IO.File.ReadAllBytes(path), "text/plain", "Booking_list");
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<Booking>> GetBookings(int pageNumber = 1, int pageSize = 5)
        {
            var totalItemCount = context.Bookings.Count();
            var paginationData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);
            var bookings = context.Bookings
                .OrderBy(h => h.Price)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();
            Response.Headers.Append("Pagination-Data", JsonConvert.SerializeObject(paginationData));
            return Ok(bookings);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<Booking> GetBooking(int id, bool summary = false)
        {
            var bookings = context.Bookings.FirstOrDefault(b => b.Id == id);
            if (bookings == null) return NotFound();
            return summary ? Ok(mapper.Map<BookingSummary>(bookings)) : Ok(bookings);
        }
        [HttpPost]
        public ActionResult<Booking> CreateBooking(int RoomId, int EmployeeId, int GuestId, BookingForCreate booking)
        {
            if (context.Rooms.FirstOrDefault(r => r.Id == RoomId) == null ||
                context.Employees.FirstOrDefault(e => e.Id == EmployeeId) == null ||
                context.Guests.FirstOrDefault(g => g.Id == GuestId) == null)
                return NotFound();
            var newBooking = new Booking()
            {
                GuestId = GuestId,
                RoomId = RoomId,
                EmployeeId = EmployeeId,
                Price = booking.Price,
                CheckInAt = booking.CheckInAt,
                CheckOutAt = booking.CheckOutAt,
            };
            context.Bookings.Add(newBooking);
            context.SaveChanges();
            return Ok(newBooking);
        }
        [HttpPut("{bookingId}")]
        public ActionResult<Booking> UpdateBooking(int bookingId,BookingForUpdate booking)
        {
            var existingBooking = context.Bookings.FirstOrDefault(b=>b.Id == bookingId);
            if (existingBooking == null) return NotFound();
            existingBooking.CheckOutAt = booking.CheckOutAt;
            existingBooking.CheckInAt = booking.CheckInAt;
            existingBooking.RoomId = booking.RoomId;
            existingBooking.EmployeeId = booking.EmployeeId;
            existingBooking.Price = booking.Price;
            existingBooking.GuestId = booking.GuestId;
            context.Bookings.Update(existingBooking);
            context.SaveChanges();
            return Ok(existingBooking);
        }
        [HttpPatch("{bookingId}")]
        public ActionResult PartiallyUpdateBooking(int bookingId, JsonPatchDocument<BookingForUpdate> patchDocument)
        {
            Booking existingBooking = context.Bookings.FirstOrDefault(a => a.Id == bookingId);
            if (existingBooking == null) return NotFound();
            var BookingToPatch = new BookingForUpdate
            {
                CheckInAt = existingBooking.CheckInAt,
                CheckOutAt = existingBooking.CheckOutAt,
                EmployeeId = existingBooking.EmployeeId,
                Price = existingBooking.Price,
                GuestId = existingBooking.GuestId,
                RoomId = existingBooking.RoomId
            };
            patchDocument.ApplyTo(BookingToPatch, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            context.Bookings.Update(existingBooking);
            context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{bookingId}")]
        public ActionResult DeleteBooking(int bookingId)
        {
            var booking = context.Bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null) return NotFound();
            context.Bookings.Remove(booking);
            context.SaveChanges();
            return NoContent();
        }
    }
}

using AutoMapper;
using Hotel_API.DbContexts;
using Hotel_API.Models.ViewModels;
using Hotel_API.Models;
using Hotel_API.Pagination;
using Hotel_API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Microsoft.AspNetCore.Authorization;

namespace Hotel_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GuestsController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly HotelContext context;
        private readonly IMapper mapper;

        public GuestsController(IConfiguration configuration,
            HotelContext context,
            IMapper mapper)
        {
            this.configuration = configuration;
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet("download")]
        [AllowAnonymous]
        public ActionResult GetGuestsFile()
        {
            var guests = context.Guests.ToList();
            var path = "Guests_list.txt";
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };
            string Gueststxt = JsonConvert.SerializeObject(guests, settings);
            System.IO.File.WriteAllText(path, Gueststxt);
            return File(System.IO.File.ReadAllBytes(path), "text/plain", "Guests_list");
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<Guest>> GetGuests(int pageNumber = 1, int pageSize = 5, string? name = null)
        {
            var totalItemCount = context.Guests.Count();
            var paginationData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);
            var query = context.Guests as IQueryable<Guest>;
            if (!string.IsNullOrEmpty(name))
                query = query.Where(h => h.FirstName.ToLower().Contains(name.ToLower()));
            var guests = query.OrderBy(h => h.FirstName)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();
            Response.Headers.Append("Pagination-Data", JsonConvert.SerializeObject(paginationData));
            return Ok(guests);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<Guest> GetGuest(int id, bool summary = false)
        {
            var guests = context.Guests.FirstOrDefault(b => b.Id == id);
            if (guests == null) return NotFound();
            return summary ? Ok(mapper.Map<GuestSummary>(guests)) : Ok(guests);
        }
        [HttpPost]
        public ActionResult<Guest> CreateGuest(GuestForCreate guest)
        {
            var newGuest = new Guest()
            {
                DOB = guest.DOB,
                Email = guest.Email,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                Phone = guest.Phone,
            };
            context.Guests.Add(newGuest);
            context.SaveChanges();
            return Ok(newGuest);
        }
        [HttpPut("{guestId}")]
        public ActionResult<Guest> UpdateGuest(int guestId, GuestForUpdate guest)
        {
            var existingGuest = context.Guests.FirstOrDefault(b => b.Id == guestId);
            if (existingGuest == null) return NotFound();
            existingGuest.FirstName = guest.FirstName;
            existingGuest.LastName = guest.LastName;
            existingGuest.Phone = guest.Phone;
            existingGuest.Email = guest.Email;
            existingGuest.DOB = guest.DOB;
            context.Guests.Update(existingGuest);
            context.SaveChanges();
            return Ok(existingGuest);
        }
        [HttpPatch("{GuestId}")]
        public ActionResult PartiallyUpdateBooking(int GuestId, JsonPatchDocument<GuestForUpdate> patchDocument)
        {
            Guest existingGuest = context.Guests.FirstOrDefault(a => a.Id == GuestId);
            if (existingGuest == null) return NotFound();
            var GuestToPatch = new GuestForUpdate
            {
                DOB = existingGuest.DOB,
                Email = existingGuest.Email,
                FirstName = existingGuest.FirstName,
                LastName = existingGuest.LastName,
                Phone = existingGuest.Phone
            };
            patchDocument.ApplyTo(GuestToPatch, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            context.Guests.Update(existingGuest);
            context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{GuestId}")]
        public ActionResult DeleteGuest(int GuestId)
        {
            var guest = context.Guests.FirstOrDefault(b => b.Id == GuestId);
            if (guest == null) return NotFound();
            context.Guests.Remove(guest);
            context.SaveChanges();
            return NoContent();
        }
    }
}

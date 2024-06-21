using AutoMapper;
using Hotel_API.Models;
using Hotel_API.Models.ViewModels;
using Hotel_API.Services.InterfacesRepository;
using Hotel_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace Hotel_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HotelsController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IHotelRepository repository;
        private readonly IMapper mapper;

        public HotelsController(IConfiguration configuration,
            IHotelRepository repository,
            IMapper mapper)
        {
            this.configuration = configuration;
            this.repository = repository;
            this.mapper = mapper;
        }
        

        [HttpGet("download")]
        [AllowAnonymous]
        public  ActionResult GetHotelsFile()
        {
            return File(repository.GetAllHotelsFile(), "text/plain", "hotel_list");
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Hotel>>> GetHotels(int pageNumber = 1, int pageSize = 5, string? name = null)
        {
            var (hotels, paginationData) = await repository.GetHotelsAsync(pageNumber, pageSize, name);
            if (hotels == null)
                return NotFound();
            Response.Headers.Append("Pagination-Data", JsonConvert.SerializeObject(paginationData));
            return Ok(hotels);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public  async Task<ActionResult<Hotel>> GetHotelById(int id, bool summary = false)
        {
            var hotel = await repository.GetHotelByIdAsync(id);
            if (hotel == null)
                return NotFound();
            return summary ? Ok(mapper.Map<HotelSummary>(hotel)) : Ok(hotel);
        }
        [HttpPost]
        public ActionResult<Hotel> CreateHotel([FromBody] HotelForCreate hotel)
        {
            var newHotel = repository.AddHotel(hotel);
            return Ok(newHotel);
        }

        [HttpPut("{id}")]
        public ActionResult<Hotel> UpdateHotel([FromRoute] int id, [FromBody] HotelForUpdate hotel)
        {
            var updatedHotel = repository.UpdateHotel(id, hotel);
            if (updatedHotel == null) return NotFound();
            return Ok(updatedHotel);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartiallyUpdateHotel(int id, JsonPatchDocument<HotelForUpdate> patchDocument)
        {
            Hotel existingHotel = await repository.GetHotelByIdAsync(id);
            if (existingHotel == null) return NotFound();
            var HotelToPatch = new HotelForUpdate { Address = existingHotel.Address, Email = existingHotel.Email, Name = existingHotel.Name, Phone = existingHotel.Phone };
            patchDocument.ApplyTo(HotelToPatch, ModelState);
            if(!ModelState.IsValid) return BadRequest(ModelState);
            repository.UpdateHotel(id,HotelToPatch);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Removehotel(int id)
        {
            var removed = await repository.DeleteHotel(id);
            if (removed == false) return NotFound();
            return NoContent();
        }
    }
}

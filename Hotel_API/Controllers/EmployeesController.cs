using AutoMapper;
using Hotel_API.Models;
using Hotel_API.Models.ViewModels;
using Hotel_API.Pagination;
using Hotel_API.Services.InterfacesRepository;
using Hotel_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Hotel_API.Controllers
{
    [Route("api/hotels/{hotelId}/Employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IEmployeeRepository repository;
        private readonly IMapper mapper;

        public EmployeesController(IConfiguration configuration,
            IEmployeeRepository repository,
            IMapper mapper)
        {
            this.configuration = configuration;
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpGet("download")]
        public ActionResult GetEmployeesFile(int hotelId)
        {
            var emp=repository.GetAllEmployeesFile(hotelId);
            if (emp == null) return NotFound();
            return File(repository.GetAllEmployeesFile(hotelId), "text/plain", "Employee_list");
        }
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetHotelEmployees(int hotelId, int pageNumber = 1, int pageSize = 5, string? name = null)
        {
            var (employees, paginationData) = await repository.GetHotelEmployeesAsync(hotelId, pageNumber, pageSize, name);
            if (employees == null)
                return NotFound();
            Response.Headers.Append("Pagination-Data", JsonConvert.SerializeObject(paginationData));
            return Ok(employees);
        }
        [HttpGet("{employeeId}")]
        public async Task<ActionResult<Employee>> GetEmployee([FromRoute] int hotelId, [FromRoute] int employeeId, [FromQuery] bool summary = false)
        {
            var employee = await repository.GetEmployeeByIdAsync(hotelId, employeeId);
            if (employee == null)
                return NotFound();
            return summary ? Ok(mapper.Map<EmployeeSummary>(employee)) : Ok(employee);
        }
        [HttpPost]
        [Authorize]
        public ActionResult<Employee> CreateEmployee([FromRoute] int hotelId, [FromBody] EmployeeForCreate employee)
        {
            var newEmpolyee = repository.AddEmployee(hotelId,employee);
            if (newEmpolyee == null)
                return NotFound();
            return Ok(newEmpolyee);
        }
        [HttpPut("{employeeId}")]
        [Authorize]
        public ActionResult<Employee> UpdateEmployee(int hotelId, int employeeId, EmployeeForUpdate emp)
        {
            var updatedEmployee = repository.UpdateEmployee(hotelId, employeeId, emp);
            if (updatedEmployee == null) return NotFound();
            return Ok(updatedEmployee);
        }
        [HttpPatch("{employeeId}")]
        [Authorize]
        public async Task<ActionResult> PartiallyUpdateEmployee(int hotelId, int employeeId, JsonPatchDocument<EmployeeForUpdate> patchDocument)
        {
            var existingEmployee = await repository.GetEmployeeByIdAsync(hotelId, employeeId);
            if (existingEmployee == null) return NotFound();
            var EmployeeToPatch = new EmployeeForUpdate
            {
                FirstName = existingEmployee.FirstName,
                LastName = existingEmployee.LastName,
                DOB = existingEmployee.DOB,
                Email = existingEmployee.Email,
                StartedDate = existingEmployee.StartedDate,
                Title = existingEmployee.Title
            };
            patchDocument.ApplyTo(EmployeeToPatch, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            repository.UpdateEmployee(hotelId, employeeId, EmployeeToPatch);
            return NoContent();
        }
        [HttpDelete("{employeeId}")]
        [Authorize]
        public ActionResult  DeleteHotel(int hotelId, int employeeId)
        {
            var delete = repository.DeleteEmployee(hotelId, employeeId);
            if (delete == false) return NotFound();
            return NoContent();
        }

    }
}

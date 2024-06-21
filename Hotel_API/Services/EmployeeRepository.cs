using Hotel_API.DbContexts;
using Hotel_API.Models;
using Hotel_API.Pagination;
using Hotel_API.Services.InterfacesRepository;
using Hotel_API.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Hotel_API.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HotelContext context;

        public EmployeeRepository(HotelContext context)
        {
            this.context = context;
        }
        public byte[] GetAllEmployeesFile(int hotelId)
        {
            var employee = context.Employees.Include(e => e.Booking).ThenInclude(b => b.Payment).Where(e => e.HotelId == hotelId).ToList();
            if (employee == null) return null;
            var path = "Employees_list.txt";
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };
            string employeetxt = JsonConvert.SerializeObject(employee, settings);
            File.WriteAllText(path, employeetxt);
            return File.ReadAllBytes(path);
        }
        public async Task<(List<Employee>, PaginationMetaData)> GetHotelEmployeesAsync(int hotelId, int pageNumber, int pageSize, string? keyword)
        {
            var totalItemCount = await context.Employees.Where(e => e.HotelId == hotelId).CountAsync();
            var paginationData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);
            var query = context.Employees.Where(e => e.HotelId == hotelId);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(h => h.FirstName.ToLower().Contains(keyword.ToLower()) || h.LastName.ToLower().Contains(keyword.ToLower()));
            var employees = await query.OrderBy(h => h.FirstName)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
              //  .Include(h => h.Booking)
                .ToListAsync();
            if (employees == null) return (null,null);
            return (employees, paginationData);
        }
        public async Task<Employee?> GetEmployeeByIdAsync(int hotelId, int employeeId)
        {
            var employee = await context.Employees
               // .Include(e => e.Booking)
                .Where(e => e.HotelId == hotelId)
                .FirstOrDefaultAsync(e => e.Id == employeeId);
            if (employee == null) return null;
            return employee;

        }
        public Employee AddEmployee(int hotelId, EmployeeForCreate emp)
        {
            if (context.Hotels.FirstOrDefault(h => h.Id == hotelId) == null) return null;
            var newEmployee = new Employee()
            {
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Email = emp.Email,
                DOB = emp.DOB,
                StartedDate = emp.StartedDate,
                Title = emp.Title,
                HotelId = hotelId
            };
            context.Employees.Add(newEmployee);
            context.SaveChanges();
            return newEmployee;
        }
        public Employee UpdateEmployee(int hotelId, int employeeId, EmployeeForUpdate emp)
        {
            var existingEmployee = context.Employees.Where(e => e.HotelId == hotelId).FirstOrDefault(e => e.Id == employeeId);
            if (existingEmployee == null) return null;
            existingEmployee.FirstName = emp.FirstName;
            existingEmployee.LastName = emp.LastName;
            existingEmployee.Email = emp.Email;
            existingEmployee.DOB = emp.DOB;
            existingEmployee.StartedDate = emp.StartedDate;
            existingEmployee.Title = emp.Title;
            context.Employees.Update(existingEmployee);
            context.SaveChanges();
            return existingEmployee;
        }
        public bool DeleteEmployee(int hotelId, int employeeId)
        {
            var employee = context.Employees.Where(e => e.HotelId == hotelId).FirstOrDefault(e => e.Id == employeeId);
            if (employee == null) return false;
            context.Employees.Remove(employee);
            context.SaveChanges();
            return true;
        }
    }
}

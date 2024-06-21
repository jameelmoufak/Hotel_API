using Hotel_API.Models;
using Hotel_API.Pagination;
using Hotel_API.ViewModels;

namespace Hotel_API.Services.InterfacesRepository
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Convert your List of Employees to Json string then upload it to text file that you can download it
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns>A byte array of your data</returns>
        byte[] GetAllEmployeesFile(int hotelId);
        /// <summary>
        /// To get all your Employees by Hotel Id with PaginationMetaData and filtering by name
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
        /// <returns>A specific page of Employees with Page data</returns>
        Task<(List<Employee>, PaginationMetaData)> GetHotelEmployeesAsync(int hotelId, int pageNumber, int pageSize, string? keyword);
        /// <summary>
        /// To get a specific Employee by its id
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="employeeId"></param>
        /// <returns>Employee By Id</returns>
        Task<Employee?> GetEmployeeByIdAsync(int hotelId, int employeeId);
        /// <summary>
        /// To Create new Employee and add it to your database
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="emp"></param>
        /// <returns>New Added Employee</returns>
        Employee AddEmployee(int hotelId, EmployeeForCreate emp);
        /// <summary>
        /// To Update Existing Employee in your database
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="employeeId"></param>
        /// <param name="emp"></param>
        /// <returns>Updated Employee</returns>
        Employee UpdateEmployee(int hotelId, int employeeId, EmployeeForUpdate emp);
        /// <summary>
        /// To Remove Existing Employee from your database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if Done, false if Not Found</returns>
        bool DeleteEmployee(int hotelId, int employeeId);
    }
}

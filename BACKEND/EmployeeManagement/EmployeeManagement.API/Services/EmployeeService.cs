using EmployeeManagement.API.DTOs.Requests;
using EmployeeManagement.API.DTOs.Responses;
using EmployeeManagement.API.Models;
using EmployeeManagement.API.Repositories.IRepositories;
using EmployeeManagement.API.Services.IServices;
using System.Linq;

namespace EmployeeManagement.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }





        public async Task<EmployeeResponseDto> AddEmployeeAsync(AddEmployeeRequestDto request)
        {
            Employee employee = MapToEmployee(request);

            Employee addedEmployee = await _employeeRepository.AddEmployeeAsync(employee);

            return MapToResponseDto(addedEmployee);
        }



        public async Task<List<EmployeeResponseDto>> GetAllEmployeesAsync()
        {
            List<Employee> employees = await _employeeRepository.GetAllEmployeesAsync();

            return employees.Select(MapToResponseDto).ToList();
        }





        private static Employee MapToEmployee(AddEmployeeRequestDto request)
        {
            return new Employee
            {
                EmployeeName = request.EmployeeName,
                DateOfBirth = request.DateOfBirth,
                JoiningDate = request.JoiningDate,
                MobileNumber = request.MobileNumber,
                Address = request.Address,
                Division = request.Division,
                District = request.District,
                Religion = request.Religion,
                Designation = request.Designation,
                Salary = request.Salary
            };
        }

        private static EmployeeResponseDto MapToResponseDto(Employee employee)
        {
            return new EmployeeResponseDto
            {
                EmployeeCode = employee.EmployeeCode,
                EmployeeName = employee.EmployeeName,
                DateOfBirth = employee.DateOfBirth,
                JoiningDate = employee.JoiningDate,
                MobileNumber = employee.MobileNumber,
                Address = employee.Address,
                Division = employee.Division,
                District = employee.District,
                Religion = employee.Religion,
                Designation = employee.Designation,
                Salary = employee.Salary,
                CreatedAt = employee.CreatedAt,
                CreatedBy = employee.CreatedBy
            };
        }
    }
}

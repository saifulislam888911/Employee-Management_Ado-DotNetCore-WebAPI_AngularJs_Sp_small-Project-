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



        public async Task<EmployeeResponseDto> AddEmployeeAsync(AddEmployeeRequestDto addRequest)
        {
            Employee employee = MapToEmployee(addRequest);

            Employee addedEmployee = await _employeeRepository.AddEmployeeAsync(employee);

            return MapToResponseDto(addedEmployee);
        }

        public async Task<List<EmployeeResponseDto>> GetAllEmployeesAsync()
        {
            List<Employee> employees = await _employeeRepository.GetAllEmployeesAsync();

            return employees.Select(MapToResponseDto).ToList();
        }

        public async Task<EmployeeResponseDto?> GetEmployeeByCodeAsync(int employeeCode)
        {
            Employee? employee = await _employeeRepository.GetEmployeeByCodeAsync(employeeCode);

            if (employee == null)
            {
                return null;
            }
            
            return MapToResponseDto(employee);
        }

        public async Task<EmployeeResponseDto?> UpdateEmployeeAsync(int employeeCode, UpdateEmployeeRequestDto updateRequest)
        {
            Employee? existingEmployee = await _employeeRepository.GetEmployeeByCodeAsync(employeeCode);

            if (existingEmployee == null)
            {
                return null;
            }

            Employee mergedEmployee = MergeEmployeeForUpdate(existingEmployee, updateRequest);

            Employee? updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(employeeCode, mergedEmployee);

            if (updatedEmployee == null) 
            {
                return null;
            }

            return MapToResponseDto(updatedEmployee);
        }   

        public async Task<bool> DeleteEmployeeAsync(int employeeCode)
        {
            return await _employeeRepository.DeleteEmployeeAsync(employeeCode);
        }



        public async Task<List<EmployeeResponseDto>> FilterEmployeesAsync(FilterEmployeeRequestDto filterRequest)
        {
            List<Employee> employees = await _employeeRepository.FilterEmployeesAsync(
                filterRequest.Name,
                filterRequest.Designation,
                filterRequest.FromDate,
                filterRequest.ToDate,
                filterRequest.Salary,
                filterRequest.MinSalary,
                filterRequest.MaxSalary
            );
            
            return employees.Select(MapToResponseDto).ToList();
        }



        private static Employee MapToEmployee(AddEmployeeRequestDto addRequest)
        {
            return new Employee
            {
                EmployeeName = addRequest.EmployeeName,
                DateOfBirth = addRequest.DateOfBirth,
                JoiningDate = addRequest.JoiningDate,
                MobileNumber = addRequest.MobileNumber,
                Address = addRequest.Address,
                Division = addRequest.Division,
                District = addRequest.District,
                Religion = addRequest.Religion,
                Designation = addRequest.Designation,
                Salary = addRequest.Salary
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



        private static Employee MergeEmployeeForUpdate(Employee existingEmployee, UpdateEmployeeRequestDto updateRequest)
        {
            return new Employee
            {
                EmployeeCode = existingEmployee.EmployeeCode,

                EmployeeName = updateRequest.EmployeeName ?? existingEmployee.EmployeeName,
                DateOfBirth = updateRequest.DateOfBirth ?? existingEmployee.DateOfBirth,
                JoiningDate = updateRequest.JoiningDate ?? existingEmployee.JoiningDate,
                MobileNumber = updateRequest.MobileNumber ?? existingEmployee.MobileNumber,
                Address = updateRequest.Address ?? existingEmployee.Address,
                Division = updateRequest.Division ?? existingEmployee.Division,
                District = updateRequest.District ?? existingEmployee.District,
                Religion = updateRequest.Religion ?? existingEmployee.Religion,
                Designation = updateRequest.Designation ?? existingEmployee.Designation,
                Salary = updateRequest.Salary ?? existingEmployee.Salary,

                CreatedAt = existingEmployee.CreatedAt,
                CreatedBy = existingEmployee.CreatedBy
            };
        }       
    }
}

using EmployeeManagement.API.DTOs.Requests;
using EmployeeManagement.API.DTOs.Responses;

namespace EmployeeManagement.API.Services.IServices
{
    public interface IEmployeeService
    {    
        Task<EmployeeResponseDto> AddEmployeeAsync(AddEmployeeRequestDto addRequest);
        Task<List<EmployeeResponseDto>> GetAllEmployeesAsync();
        Task<EmployeeResponseDto?> GetEmployeeByCodeAsync(int employeeCode);
        Task<EmployeeResponseDto?> UpdateEmployeeAsync(int employeeCode, UpdateEmployeeRequestDto updateRequest);
        Task<bool> DeleteEmployeeAsync(int employeeCode);
        
        Task<List<EmployeeResponseDto>> FilterEmployeesAsync(FilterEmployeeRequestDto filterRequest);
    }
}

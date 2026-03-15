using EmployeeManagement.API.DTOs.Requests;
using EmployeeManagement.API.DTOs.Responses;

namespace EmployeeManagement.API.Services.IServices
{
    public interface IEmployeeService
    {    
        Task<EmployeeResponseDto> AddEmployeeAsync(AddEmployeeRequestDto request);
        Task<List<EmployeeResponseDto>> GetAllEmployeesAsync();
    }
}

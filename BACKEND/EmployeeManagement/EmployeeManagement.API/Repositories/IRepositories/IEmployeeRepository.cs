using EmployeeManagement.API.Models;

namespace EmployeeManagement.API.Repositories.IRepositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<List<Employee>> GetAllEmployeesAsync();
    }
}

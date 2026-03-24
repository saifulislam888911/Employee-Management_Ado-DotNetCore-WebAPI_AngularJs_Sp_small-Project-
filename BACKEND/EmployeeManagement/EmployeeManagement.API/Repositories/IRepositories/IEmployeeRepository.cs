using EmployeeManagement.API.Models;

namespace EmployeeManagement.API.Repositories.IRepositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByCodeAsync(int employeeCode);
        Task<Employee?> UpdateEmployeeAsync(int employeeCode, Employee employee);
        Task<bool> DeleteEmployeeAsync(int employeeCode);

        Task<List<Employee>> FilterEmployeesAsync(
            string? name,
            string? designation,
            DateTime? fromDate,
            DateTime? toDate,
            decimal? salary,
            decimal? minSalary,
            decimal? maxSalary    
        );
    }
}

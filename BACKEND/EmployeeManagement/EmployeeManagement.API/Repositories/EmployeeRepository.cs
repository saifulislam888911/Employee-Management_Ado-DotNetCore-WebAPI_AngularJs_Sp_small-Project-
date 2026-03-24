using EmployeeManagement.API.Data;
using EmployeeManagement.API.Models;
using EmployeeManagement.API.Repositories.IRepositories;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagement.API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public EmployeeRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }



        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            using SqlConnection connection = _dbConnectionFactory.CreateConnection();
            using SqlCommand command = new SqlCommand("dbo.sp_AddEmployee", connection);

            command.CommandType = CommandType.StoredProcedure;

            AddEmployeeParameters(command, employee);

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return MapEmployee(reader);
            }

            throw new InvalidOperationException("Unsuccessful : Failed to Add Employee.");
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            List<Employee> employees = new List<Employee>();

            using SqlConnection connection = _dbConnectionFactory.CreateConnection();
            using SqlCommand command = new SqlCommand("dbo.sp_GetAllEmployees", connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                employees.Add(MapEmployee(reader));
            }

            return employees;
        }

        public async Task<Employee?> GetEmployeeByCodeAsync(int employeeCode)
        {
            using SqlConnection connection = _dbConnectionFactory.CreateConnection();
            using SqlCommand command = new SqlCommand("dbo.sp_GetEmployeeByCode", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = employeeCode;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync()) 
            {
                return MapEmployee(reader);
            }

            return null;
        }

        public async Task<Employee?> UpdateEmployeeAsync(int employeeCode, Employee employee)
        {
            using SqlConnection connection = _dbConnectionFactory.CreateConnection();
            using SqlCommand command = new SqlCommand("dbo.sp_UpdateEmployee", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = employeeCode;

            AddEmployeeParameters(command, employee);

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return MapEmployee(reader);
            }

            return null;
        }

        public async Task<bool> DeleteEmployeeAsync(int employeeCode)
        {
            Employee? existingEmployee = await GetEmployeeByCodeAsync(employeeCode);

            if (existingEmployee == null)
            {
                return false;
            }

            using SqlConnection connection = _dbConnectionFactory.CreateConnection();
            using SqlCommand command = new SqlCommand("dbo.sp_DeleteEmployee", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = employeeCode;

            await connection.OpenAsync();

            await command.ExecuteReaderAsync();

            return true;
        }



        public async Task<List<Employee>> FilterEmployeesAsync(
            string? name,
            string? designation,
            DateTime? fromDate,
            DateTime? toDate,
            decimal? salary,
            decimal? minSalary,
            decimal? maxSalary
        )
        {
            List<Employee> employees = new List<Employee>();

            using SqlConnection connection = _dbConnectionFactory.CreateConnection();
            using SqlCommand command = new SqlCommand("dbo.sp_FilterEmployees", connection);
            
            command.CommandType = CommandType.StoredProcedure;

            AddFilterEmployeesParameters(command, name, designation, fromDate, toDate, salary, minSalary, maxSalary);

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                employees.Add(MapEmployee(reader));
            }

            return employees;
        }
        


        private static void AddEmployeeParameters(SqlCommand command, Employee employee)
        {
            command.Parameters.Add("@EmployeeName", SqlDbType.NVarChar, 150).Value = employee.EmployeeName;
            command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = employee.DateOfBirth;
            command.Parameters.Add("@JoiningDate", SqlDbType.Date).Value = employee.JoiningDate;
            command.Parameters.Add("@MobileNumber", SqlDbType.NVarChar, 20).Value = employee.MobileNumber;
            command.Parameters.Add("@Address", SqlDbType.NVarChar, 500).Value = employee.Address;
            command.Parameters.Add("@Division", SqlDbType.NVarChar, 100).Value = employee.Division;
            command.Parameters.Add("@District", SqlDbType.NVarChar, 100).Value = employee.District;
            command.Parameters.Add("@Religion", SqlDbType.NVarChar, 50).Value = employee.Religion;
            command.Parameters.Add("@Designation", SqlDbType.NVarChar, 100).Value = employee.Designation;

            var salaryParam = command.Parameters.Add("@Salary", SqlDbType.Decimal);
            salaryParam.Precision = 18;
            salaryParam.Scale = 2;
            salaryParam.Value = employee.Salary;
        }

        private static void AddFilterEmployeesParameters(
            SqlCommand command,
            string? name,
            string? designation,
            DateTime? fromDate,
            DateTime? toDate,
            decimal? salary,
            decimal? minSalary,
            decimal? maxSalary)
        {
            command.Parameters.Add("@Name", SqlDbType.NVarChar, 150).Value = (object?)name ?? DBNull.Value;
            command.Parameters.Add("@Designation", SqlDbType.NVarChar, 100).Value = (object?)designation ?? DBNull.Value;
            command.Parameters.Add("@FromDate", SqlDbType.Date).Value = (object?)fromDate ?? DBNull.Value;
            command.Parameters.Add("@ToDate", SqlDbType.Date).Value = (object?)toDate ?? DBNull.Value;

            var salaryParam = command.Parameters.Add("@Salary", SqlDbType.Decimal);
            salaryParam.Precision = 18;
            salaryParam.Scale = 2;
            salaryParam.Value = (object?)salary ?? DBNull.Value;

            var minSalaryParam = command.Parameters.Add("@MinSalary", SqlDbType.Decimal);
            minSalaryParam.Precision = 18;
            minSalaryParam.Scale = 2;
            minSalaryParam.Value = (object?)minSalary ?? DBNull.Value;

            var maxSalaryParam = command.Parameters.Add("@MaxSalary", SqlDbType.Decimal);
            maxSalaryParam.Precision = 18;
            maxSalaryParam.Scale = 2;
            maxSalaryParam.Value = (object?)maxSalary ?? DBNull.Value;
        }



        private static Employee MapEmployee(SqlDataReader reader)
        {
            return new Employee {
                EmployeeCode = Convert.ToInt32(reader["EmployeeCode"]),
                EmployeeName = reader["EmployeeName"]?.ToString() ?? string.Empty,
                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                MobileNumber = reader["MobileNumber"]?.ToString() ?? string.Empty,
                Address = reader["Address"]?.ToString() ?? string.Empty,
                Division = reader["Division"]?.ToString() ?? string.Empty,
                District = reader["District"]?.ToString() ?? string.Empty,
                Religion = reader["Religion"]?.ToString() ?? string.Empty,
                Designation = reader["Designation"]?.ToString() ?? string.Empty,
                Salary = Convert.ToDecimal(reader["Salary"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                CreatedBy = reader["CreatedBy"]?.ToString() ?? string.Empty
            };
        }
    }
}

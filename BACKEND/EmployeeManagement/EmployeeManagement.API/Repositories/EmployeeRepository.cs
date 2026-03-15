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

            while(await reader.ReadAsync())
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

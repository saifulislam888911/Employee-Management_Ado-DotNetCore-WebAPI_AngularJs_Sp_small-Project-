using EmployeeManagement.API.DTOs.Requests;
using EmployeeManagement.API.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }



        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeRequestDto addRequest)
        {
            try
            {
                var result = await _employeeService.AddEmployeeAsync(addRequest);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                    {
                        message = "Error: Failed to Add Employee.",
                        error = ex.Message
                    }
                );
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var result = await _employeeService.GetAllEmployeesAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                    {
                        message = "Error: Failed to Get All Employees.",
                        error = ex.Message
                    }
                );
            }
        }

        [HttpGet("{employeeCode:int}")]
        public async Task<IActionResult> GetEmployeeByCode(int employeeCode)
        {
            try
            {
                var result = await _employeeService.GetEmployeeByCodeAsync(employeeCode);

                if (result == null)
                {
                    return NotFound(new
                        {
                            message = $"Error: EmployeeCode-{employeeCode} Not Found."
                        }
                    );
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                    {
                        message = "Error: Failed to Get Employee.",
                        error = ex.Message
                    }
                );
            }
        }

        [HttpPut("{employeeCode:int}")]
        public async Task<IActionResult> UpdateEmployee(int employeeCode, [FromBody] UpdateEmployeeRequestDto updateRequest) 
        {
            try
            {
                var result = await _employeeService.UpdateEmployeeAsync(employeeCode, updateRequest);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                    {
                        message = "Error: Failed to Update Employee.",
                        error = ex.Message
                    }
                );
            }
        }

        [HttpDelete("{employeeCode:int}")]
        public async Task<IActionResult> DeleteEmployee(int employeeCode)
        {
            try
            {
                var isDeleted = await _employeeService.DeleteEmployeeAsync(employeeCode);

                if (!isDeleted)
                {
                    return NotFound(new
                        {
                            message = $"Error: EmployeeCode-{employeeCode} Not Found."
                        }
                   );
                }

                return Ok(new
                    {
                        message = $"Successful: EmployeeCode-{employeeCode} Deleted."
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                    {
                        message = "Error: Failed to Delete Employee.",
                        error = ex.Message
                    }
                );
            }
        }


        /*
        [HttpGet("filter")]
        public async Task<IActionResult> FilterEmployees([FromBody] FilterEmployeeRequestDto filterRequest)
        {
            var result = await _employeeService.FilterEmployeesAsync(filterRequest);

            return Ok(result);
        }*/

        [HttpGet("filter")]
        public async Task<IActionResult> FilterEmployees(
            [FromQuery] string? name,
            [FromQuery] string? designation,
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate,
            [FromQuery] decimal? salary,
            [FromQuery] decimal? minSalary,
            [FromQuery] decimal? maxSalary
        )
        {
            try
            {
                FilterEmployeeRequestDto filterRequest = new FilterEmployeeRequestDto
                {
                    Name = name,
                    Designation = designation,
                    FromDate = fromDate,
                    ToDate = toDate,
                    Salary = salary,
                    MinSalary = minSalary,
                    MaxSalary = maxSalary
                };

                var result = await _employeeService.FilterEmployeesAsync(filterRequest);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                    {
                        message = "Error: Failed to Filter Employees.",
                        error = ex.Message
                    }
                );
            }
        }
    }
}

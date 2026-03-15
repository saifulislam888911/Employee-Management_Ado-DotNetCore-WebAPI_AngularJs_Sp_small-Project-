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
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeRequestDto request)
        {
            if (request == null)
            {
                return BadRequest("Unsuccessful : Request body is required.");
            }

            var result = await _employeeService.AddEmployeeAsync(request);

            return Ok(result);
        }



        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _employeeService.GetAllEmployeesAsync();

            return Ok(result);
        }

    }
}

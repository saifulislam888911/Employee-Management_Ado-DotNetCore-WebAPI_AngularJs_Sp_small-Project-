namespace EmployeeManagement.API.DTOs.Requests
{
    public class UpdateEmployeeRequestDto
    {
        public string? EmployeeName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string? MobileNumber { get; set; }
        public string? Address { get; set; }
        public string? Division { get; set; }
        public string? District { get; set; }
        public string? Religion { get; set; }
        public string? Designation { get; set; }
        public decimal? Salary { get; set; }
    }
}

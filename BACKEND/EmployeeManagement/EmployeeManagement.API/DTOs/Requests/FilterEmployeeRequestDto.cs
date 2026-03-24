namespace EmployeeManagement.API.DTOs.Requests
{
    public class FilterEmployeeRequestDto
    {
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal? Salary { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
    }
}

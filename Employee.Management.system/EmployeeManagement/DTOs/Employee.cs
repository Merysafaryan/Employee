using Employee.Management.system.Responses;
using System.ComponentModel.DataAnnotations;

namespace Employee.Management.system.EmployeeManagement.DTOs
{
    public class Employee
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImgUrl { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public Position Position { get; set; }
    }
}

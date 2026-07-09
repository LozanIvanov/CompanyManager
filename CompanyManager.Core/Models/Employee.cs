using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Core.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; } = string.Empty;
        [Range(1, 100000, ErrorMessage = "Salary must be greater than 0")]
        public decimal Salary { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select a department")]
        public int DepartmentId { get; set; }

        public Department? Department { get; set; }
    }
}

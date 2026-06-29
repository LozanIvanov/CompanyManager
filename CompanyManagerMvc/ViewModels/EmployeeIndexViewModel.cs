using CompanyManager.Core.Models;

namespace CompanyManagerMvc.ViewModels
{
    public class EmployeeIndexViewModel
    {
        public List<Employee> Employees { get; set; } = new();

        public List<Department> Departments { get; set; } = new();

        public string? SearchText { get; set; }

        public int? SelectedDepartmentId { get; set; }
    }
}

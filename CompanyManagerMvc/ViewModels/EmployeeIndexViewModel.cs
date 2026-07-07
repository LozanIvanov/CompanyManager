using CompanyManager.Core.Models;

namespace CompanyManagerMvc.ViewModels
{
    public class EmployeeIndexViewModel
    {
        public List<Employee> Employees { get; set; } = new();

        public List<Department> Departments { get; set; } = new();
        public string NameSort { get; set; } = string.Empty;

        public string SalarySort { get; set; } = string.Empty;

        public string DepartmentSort { get; set; } = string.Empty;
        public string? SortOrder { get; set; }

        public string? SearchText { get; set; }

        public int? SelectedDepartmentId { get; set; }
    }
}

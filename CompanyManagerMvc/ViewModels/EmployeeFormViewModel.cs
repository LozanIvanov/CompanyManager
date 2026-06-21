using CompanyManager.Core.Models;

namespace CompanyManagerMvc.ViewModels;

public class EmployeeFormViewModel
{
    public Employee Employee { get; set; }
        = new Employee();

    public List<Department> Departments { get; set; }
        = new List<Department>();
}
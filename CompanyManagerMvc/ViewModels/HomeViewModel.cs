using CompanyManager.Core.Models;

namespace CompanyManagerMvc.ViewModels
{
    public class HomeViewModel
    {
        public List<DashboardCardViewModel> Cards { get; set; }
            = new();
        public decimal AverageSalary { get; set; }
        public decimal HighestSalary { get; set; }
        public decimal LowestSalary { get; set; }

        public decimal TotalSalary { get; set; }
        public List<DepartmentEmployeeCount> DepartmentStatistics { get; set; }
    = new();
    }
}

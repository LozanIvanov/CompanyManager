using CompanyManager.Application.Services;
using CompanyManagerMvc.Models;
using CompanyManagerMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CompanyManagerMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeService employeeService;

        private readonly DepartmentService departmentService;

        public HomeController(EmployeeService employeeService,DepartmentService departmentService)
        {
            this.employeeService = employeeService;
            this.departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var employeeCount = await employeeService.GetEmployeeCountAsync();
            var departmentCount = await departmentService.GetDepartmentCountAsync();
            var model = new HomeViewModel();
            model.Cards.Add(new DashboardCardViewModel
            {
                Title = "Employees",
                Count = employeeCount,
                Controller = "Employee",
                ButtonText = "Manage Employees",
                Icon = "bi bi-people",
                ButtonClass = "btn-primary"
            });
            model.Cards.Add(new DashboardCardViewModel
            {
                Title = "Department",
                Count = departmentCount,
                Controller = "Department",
                ButtonText = "Manage Department",
                Icon = "bi bi-building",
                ButtonClass = "btn-success"
            });
            return View(model);
  
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

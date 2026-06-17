using CompanyManager.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagerMvc.Controllers;

public class EmployeeController : Controller
{
    private readonly EmployeeService service;

    public EmployeeController(EmployeeService service)
    {
        this.service = service;
    }

    public async Task<IActionResult> Index()
    {
        var employees = await service.GetAllEmployeesAsync();

        return View(employees);
    }
}
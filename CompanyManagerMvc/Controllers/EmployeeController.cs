using CompanyManager.Application.Services;
using CompanyManager.Core.Models;
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
    [HttpGet]
    public IActionResult Create()
    {

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Employee employee)
    {
        await service.AddEmployeeAsync(employee);

        return RedirectToAction("Index");
    }
}
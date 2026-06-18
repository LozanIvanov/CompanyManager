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
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var employee = await service.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
            return View(employee);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Employee employee)
    {
        await service.UpdateEmployeeAsync(employee);
        return RedirectToAction("Index");
    }
}
using CompanyManager.Application.Services;
using CompanyManager.Core.Models;
using CompanyManagerMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagerMvc.Controllers;

public class EmployeeController : Controller
{
    private readonly EmployeeService service;
    private readonly DepartmentService departmentService;

    public EmployeeController(EmployeeService service, DepartmentService departmentService)
    {
        this.service = service;
        this.departmentService = departmentService;
    }

    public async Task<IActionResult> Index()
    {
        var employees = await service.GetAllEmployeesAsync();

        return View(employees);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {

        var viewModel = new EmployeeFormViewModel
        {
            Departments = await departmentService.GetAllDepartmentAsync()
        };

        return View(viewModel);
    }
    [HttpPost]
    public async Task<IActionResult> Create(EmployeeFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.Departments =
                await departmentService.GetAllDepartmentAsync();

            return View(viewModel);
        }
        await service.AddEmployeeAsync(viewModel.Employee);

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
    
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteEmployeeAsync(id);
        return RedirectToAction("Index");
    }

}
using CompanyManager.Application.Services;
using CompanyManager.Core.Models;
using CompanyManagerMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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

    public async Task<IActionResult> Index(
     string? searchText,
     int? departmentId,
     string? sortOrder,
     int page = 1,
     int pageSize = 4)
    {

        var model = new EmployeeIndexViewModel
        {
            Departments = await departmentService.GetAllDepartmentAsync(),
            SearchText = searchText,
            SelectedDepartmentId = departmentId,
            SortOrder = sortOrder
        };

        model.NameSort = sortOrder == "name" ? "name_desc" : "name";
        model.SalarySort = sortOrder == "salary" ? "salary_desc" : "salary";
        model.DepartmentSort = sortOrder == "department" ? "department_desc" : "department";

        model.Employees = await service.GetEmployeesAsync(
            searchText,
            departmentId,
            sortOrder,
            page,
            pageSize);

        model.CurrentPage = page;
        model.PageSize = pageSize;

        model.TotalItems = await service.GetEmployeesCountAsync(
            searchText,
            departmentId);

        model.TotalPages = (int)Math.Ceiling(
            (double)model.TotalItems / pageSize);

        return View(model);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Create()
    {

        var viewModel = new EmployeeFormViewModel
        {
            Departments = await departmentService.GetAllDepartmentAsync()
        };

        return View(viewModel);
    }
    [Authorize(Roles = "Admin")]
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
        TempData["Success"] = "Employee created successfully.";
        return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin")]
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
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Edit(Employee employee)
    {
        await service.UpdateEmployeeAsync(employee);
        TempData["Success"] = "Employee updated successfully.";
        return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await service.GetEmployeeByIdAsync(id);

        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await service.DeleteEmployeeAsync(id);
        TempData["Success"] = "Employee deleted successfully.";
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var employee = await service.GetEmployeeByIdAsync(id);

        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

}
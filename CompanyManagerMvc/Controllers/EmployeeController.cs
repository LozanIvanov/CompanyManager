using CompanyManager.Application.Services;
using CompanyManager.Core.Models;
using CompanyManagerMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;

namespace CompanyManagerMvc.Controllers;

public class EmployeeController : Controller
{
    private readonly EmployeeService service;
    private readonly DepartmentService departmentService;
    private readonly ILogger<EmployeeController> logger;

    public EmployeeController(
        EmployeeService service,
        DepartmentService departmentService,

        ILogger<EmployeeController> logger)
    {
        this.service = service;
        this.departmentService = departmentService;
        this.logger = logger;
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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmployeeFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.Departments =
                await departmentService.GetAllDepartmentAsync();
            logger.LogWarning(
              "Invalid employee create attempt by user {User}",
               User.Identity?.Name);

            return View(viewModel);
        }
        await service.AddEmployeeAsync(viewModel.Employee);
        logger.LogInformation(
           "Employee {EmployeeName} was created by {User}",
           viewModel.Employee.Name,
           User.Identity?.Name);
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
            logger.LogWarning(
              "Employee with id {EmployeeId} was not found by user {User}",
              id,
              User.Identity?.Name);
            return NotFound();
        }
        return View(employee);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Employee employee)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning(
               "Invalid employee edit attempt for employee {EmployeeId} by {User}",
               employee.Id,
               User.Identity?.Name);

            return View(employee);
        }

        await service.UpdateEmployeeAsync(employee);
        logger.LogInformation(
            "Employee {EmployeeId} was updated by {User}",
             employee.Id,
             User.Identity?.Name);
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
        var employee = await service.GetEmployeeByIdAsync(id);

        if (employee == null)
        {
            logger.LogWarning(
                "Delete failed. Employee with id {EmployeeId} was not found. User: {User}",
                id,
                User.Identity?.Name);

            return NotFound();
        }
        await service.DeleteEmployeeAsync(id);

        logger.LogInformation(
            "Employee {EmployeeId} - {EmployeeName} was deleted by {User}",
            employee.Id,
            employee.Name,
            User.Identity?.Name);
        TempData["Success"] = "Employee deleted successfully.";
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var employee = await service.GetEmployeeByIdAsync(id);

        if (employee == null)
        {
            logger.LogWarning(
        "Employee with id {EmployeeId} was not found by user {User}",
        id,
        User.Identity?.Name);
            return NotFound();
        }

        return View(employee);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> ExportToExcel()
    {
        var employees = await service.GetEmployeesAsync(
            searchText: null,
            departmentId: null,
            sortOrder: "name",
            page: 1,
            pageSize: int.MaxValue);

        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("Employees");

        worksheet.Cell(1, 1).Value = "Id";
        worksheet.Cell(1, 2).Value = "Name";
        worksheet.Cell(1, 3).Value = "Salary";
        worksheet.Cell(1, 4).Value = "Department";

        int row = 2;

        foreach (var employee in employees)
        {
            worksheet.Cell(row, 1).Value = employee.Id;
            worksheet.Cell(row, 2).Value = employee.Name;
            worksheet.Cell(row, 3).Value = employee.Salary;
            worksheet.Cell(row, 4).Value =
                employee.Department?.Name ?? "No Department";

            row++;
        }

        worksheet.Range(1, 1, 1, 4)
            .Style.Font.Bold = true;

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();

        workbook.SaveAs(stream);
        logger.LogInformation(
           "Employees were exported to Excel by {User}",
           User.Identity?.Name);
        var fileName =
            $"Employees_{DateTime.Now:yyyy-MM-dd}.xlsx";
        return File(
            stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileName);
    }

}
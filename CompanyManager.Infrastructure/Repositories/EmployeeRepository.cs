using CompanyManager.Core.Interfaces;
using CompanyManager.Core.Models;
using CompanyManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext context;

    public EmployeeRepository(AppDbContext context)
    {
        this.context = context;
    }
    public async Task<List<Employee>> GetEmployeesAsync(
    string? searchText,
    int? departmentId,
    string? sortOrder)
    {
        var query = context.Employees
            .Include(e => e.Department)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            query = query.Where(e => e.Name.Contains(searchText));
        }

        if (departmentId != null)
        {
            query = query.Where(e => e.DepartmentId == departmentId.Value);
        }

        query = sortOrder switch
        {
            "name" => query.OrderBy(e => e.Name),

            "name_desc" => query.OrderByDescending(e => e.Name),

            "salary" => query.OrderBy(e => e.Salary),

            "salary_desc" => query.OrderByDescending(e => e.Salary),

            "department" => query.OrderBy(e => e.Department!.Name),

            "department_desc" => query.OrderByDescending(e => e.Department!.Name),

            _ => query.OrderBy(e => e.Id)
        };

        return await query.ToListAsync();
    }
  
    public async Task<int> GetCountAsync()
    {
        return await context.Employees.CountAsync();
    }
    public async Task<List<Employee>> GetAllAsync()
    {
        return await context.Employees
            .Include(e => e.Department)
            .ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task AddAsync(Employee employee)
    {
        context.Employees.Add(employee);

        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Employee employee)
    {
        context.Employees.Update(employee);

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Employee employee)
    {
        context.Employees.Remove(employee);

        await context.SaveChangesAsync();
    }
}

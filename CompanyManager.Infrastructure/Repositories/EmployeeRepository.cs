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

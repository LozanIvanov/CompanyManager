using CompanyManager.Core.Interfaces;
using CompanyManager.Core.Models;

namespace CompanyManager.Application.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        this.repository = repository;
    }
    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        return await repository.GetAllAsync();
    }
    public async Task AddEmployeeAsync(Employee employee)
    {
        await repository.AddAsync(employee);
    }
    public async Task UpdateEmployeeAsync(Employee employee)
    {
       await repository.UpdateAsync(employee);
    }
    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        return await repository.GetByIdAsync(id);
    }
}

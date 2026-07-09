using CompanyManager.Core.Interfaces;
using CompanyManager.Core.Models;

namespace CompanyManager.Application.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository repository;

    public Task<List<Employee>> GetEmployeesAsync(
    string? searchText,
    int? departmentId,
    string? sortOrder, int page,
    int pageSize)
    {
        return repository.GetEmployeesAsync(
            searchText,
            departmentId,
            sortOrder, page,
    pageSize);
    }
    public async Task<decimal> GetAverageSalaryAsync()
    {
        return await repository.GetAverageSalaryAsync();
    }
    public async Task<decimal> GetHighestSalaryAsync()
    {
        return await repository.GetHighestSalaryAsync();
    }
    public async Task<decimal> GetLowestSalaryAsync()
    {
        return await repository.GetLowestSalaryAsync();
    }
    public async Task<decimal>GetTotalSalaryAsync()
    {
        return await repository.GetTotalSalaryAsync();
    }
    public EmployeeService(IEmployeeRepository repository)
    {
        this.repository = repository;
    }
    public async Task<int> GetEmployeeCountAsync()
    {
        return await repository.GetCountAsync();
    }
    public Task<int> GetEmployeesCountAsync(
    string? searchText,
    int? departmentId)
    {
        return repository.GetEmployeesCountAsync(
            searchText,
            departmentId);
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
    public async Task DeleteEmployeeAsync(int id)
    {
        var employee = await repository.GetByIdAsync(id);

        if (employee == null)
        {
            return;
        }

        await repository.DeleteAsync(employee);
    }
}

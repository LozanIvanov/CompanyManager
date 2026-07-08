using CompanyManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManager.Core.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeesAsync(
    string? searchText,
    int? departmentId,
    string? sortOrder,
    int page,
    int pageSize);
        Task<int> GetEmployeesCountAsync(
    string? searchText,
    int? departmentId);

        Task<int> GetCountAsync();
        Task<List<Employee>> GetAllAsync();

        Task<Employee?> GetByIdAsync(int id);

        Task AddAsync(Employee employee);

        Task UpdateAsync(Employee employee);

        Task DeleteAsync(Employee employee);

    }
}

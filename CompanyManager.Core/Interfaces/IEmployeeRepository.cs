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
        Task<List<Employee>> SearchByNameAndDepartmentAsync(string searchText, int departmentId);
        Task<List<Employee>> SearchByNameAsync(string searchText);
        Task<List<Employee>> FilterByDepartmentAsync(int departmentId);
        Task<int> GetCountAsync();
        Task<List<Employee>> GetAllAsync();

        Task<Employee?> GetByIdAsync(int id);

        Task AddAsync(Employee employee);

        Task UpdateAsync(Employee employee);

        Task DeleteAsync(Employee employee);
    }
}

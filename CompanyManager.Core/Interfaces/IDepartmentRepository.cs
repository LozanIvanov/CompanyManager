using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManager.Core.Models;

namespace CompanyManager.Core.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync();

        Task<Department?> GetByIdAsync(int id);

        Task AddAsync(Department department);

        Task UpdateAsync(Department department);

        Task DeleteAsync(Department department);
    }
}

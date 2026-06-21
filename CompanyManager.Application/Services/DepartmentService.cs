using CompanyManager.Core.Interfaces;
using CompanyManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManager.Application.Services
{
    public class DepartmentService
    {
        private readonly IDepartmentRepository repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            this.repository = repository;
        }
        public async Task<List<Department>>GetAllDepartmentAsync()
        {
            return await repository.GetAllAsync();
        }
        public async Task<Department?>GetDepartmentById(int id)
        {
            return await repository.GetByIdAsync(id);
        }
        public async Task AddDepartmentAsync(Department department)
        {
            await repository.AddAsync(department);
        }
        public async Task UpdateDepartmentAsync(Department department)
        {
            await repository.UpdateAsync(department);
        }
        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await repository.GetByIdAsync(id);
            if (department == null)
            {
                return;
            }
            await repository.DeleteAsync(department);
        }
    }
}

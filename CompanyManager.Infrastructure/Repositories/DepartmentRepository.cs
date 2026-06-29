using CompanyManager.Core.Interfaces;
using CompanyManager.Core.Models;
using CompanyManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManager.Infrastructure.Repositories
{
    public  class DepartmentRepository:IDepartmentRepository
    {
        private readonly AppDbContext context;

        public DepartmentRepository(AppDbContext context)
        {
            this.context = context;
        }
       public async Task<int> GetCountAsync()
        {
            return await context.Departments.CountAsync();
        }
        public Task<List<Department>> SearchByNameAsync(string searchText)
        {
         return context.Departments.Where(x=>x.Name.Contains(searchText)).ToListAsync();    
        }
        public async Task AddAsync(Department department)
        {
            context.Departments.Add(department);
             await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Department department)
        {
            context.Departments.Remove(department);
            await context.SaveChangesAsync();
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await context.Departments.ToListAsync(); 
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
          return await context.Departments.FirstOrDefaultAsync(x=>x.Id== id);    
            
        }

        public async Task UpdateAsync(Department department)
        {
            context.Departments.Update(department);
            await context.SaveChangesAsync();
        }
       
    }
}

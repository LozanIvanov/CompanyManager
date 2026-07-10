using Microsoft.EntityFrameworkCore;
using CompanyManager.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManager.Infrastructure.Data
{
    public class AppDbContext: IdentityDbContext<IdentityUser>
    {
        public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Department>()
                .Property(d => d.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);

            base.OnModelCreating(modelBuilder);
        }


    }
}

using EmploymentManagementSystem.Core.Entities;
using Hemdan.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentManagementSystem.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Adding Indexes for Filtering & Searching
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.UserName)
                .HasDatabaseName("IX_Employee_Name");

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.JobTitle)
                .HasDatabaseName("IX_Employee_JobTitle");

            modelBuilder.Entity<Employee>()
               .HasIndex(e => e.Salary)
               .HasDatabaseName("IX_Employee_Salary");

        }
    }
}

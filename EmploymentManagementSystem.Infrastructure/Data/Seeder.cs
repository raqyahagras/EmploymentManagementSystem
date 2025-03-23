using EmploymentManagementSystem.Core.Entities;
using Hemdan.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentManagementSystem.Infrastructure.Data
{
    public class Seeder
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IServiceProvider _serviceProvider;

        public Seeder(UserManager<Employee> userManager, IServiceProvider serviceProvider)
        {
            _userManager = userManager;
            _serviceProvider = serviceProvider;
        }

        public Seeder(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public static async Task SeedData(RoleManager<IdentityRole> roleManager, UserManager<Employee> userManager, ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            await SeedRoles(roleManager);
            await SeedPermissions(serviceProvider);
            await SeedRolePermissions(context, roleManager);
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "Admin" },
                new IdentityRole { Name = "User" }
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }

        private static async Task SeedPermissions(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (!context.Permissions.Any())
            {
                var permissions = new List<Permission>
                {
                    new Permission { Name = "Employee.Create", Updated = DateTime.Now },
                    new Permission { Name = "Employee.Edit", Updated = DateTime.Now },
                    new Permission { Name = "Employee.Delete", Updated = DateTime.Now },
                    new Permission { Name = "Employee.View", Updated = DateTime.Now },
                };

                await context.Permissions.AddRangeAsync(permissions);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedRolePermissions(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");
            if (adminRole == null) return;

            var permissions = await context.Permissions.ToListAsync();
            var existingRolePermissions = await context.RolePermissions
                .Where(rp => rp.RoleId == adminRole.Id)
                .ToListAsync();

            foreach (var permission in permissions)
            {
                if (!existingRolePermissions.Any(rp => rp.PermissionId == permission.Id))
                {
                    context.RolePermissions.Add(new RolePermission
                    {
                        RoleId = adminRole.Id,
                        PermissionId = permission.Id
                    });
                }
            }

            await context.SaveChangesAsync();
        }

        public async Task SeedUsersAndRolesAsync()
        {
            string adminEmail = "Admin@gmail.com";
            string adminPassword = "User??123"; // Ensure a strong password

            // Check if the user already exists
            var existingUser = await _userManager.FindByEmailAsync(adminEmail);
            if (existingUser == null)
            {
                var newUser = new Employee
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "AdminTest",
                    Email = adminEmail,
                    Salary = 25000,
                    JobTitle = "HR",
                    IsActive = true,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(newUser, adminPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, "Admin");
                }
            }
        }
    }
}


